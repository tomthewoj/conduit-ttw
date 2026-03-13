using Conduit.Application.DTOs.Responses;
using Conduit.Application.DTOs.Responses.Multiple;
using Conduit.Application.Interfaces;
using Conduit.Domain.Entities;
using Conduit.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Queries.Comments
{
    public class GetCommentHandler : IRequestHandler<GetCommentsQuery, ListCommentItemsResponse>
    {

        ICommentRepository _repo;
        IArticleRepository _articleRepo;
        IUserRepository _userRepo;
        IFollowingRepository _followRepo;
        public GetCommentHandler(ICommentRepository repo, IArticleRepository articleRepo, IUserRepository userRepo, IFollowingRepository followRepo)
        {
            _repo = repo;
            _articleRepo = articleRepo;
            _userRepo = userRepo;
            _followRepo = followRepo;
        }

        public async Task<ListCommentItemsResponse> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            var commentList = new List<CommentResponse>();
            var article = await _articleRepo.GetArticleBySlug(request.slug);
            var comments = await _repo.GetAllComments(article.Id, request.limit, request.offset);

            var authorIds = comments.Select(c => c.AuthorId).Distinct().ToList();
            var authors = await _userRepo.GetUsersByIds(authorIds);



            foreach (var comment in comments)
            {
                bool isFollowing = request.currentUserId != null ? await _followRepo.IsUserFollowing((Guid)request.currentUserId, comment.AuthorId) : false; // can be optimized
                var commentAuthor = authors[comment.AuthorId];
                commentList.Add(new CommentResponse(
                    comment.Id,
                    comment.CreatedAt,
                    comment.UpdatedAt,
                    comment.Body,
                    new ProfileResponse(commentAuthor.UserName, commentAuthor.Profile.Bio, commentAuthor.Profile.ImageLink, isFollowing)));
            }
            return new ListCommentItemsResponse(commentList);
        }
    }
}
