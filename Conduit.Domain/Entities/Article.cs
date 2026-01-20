using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Domain.Entities
{
    public class Article
    {
        public Guid Id { get; init; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }

        public Guid AuthorId { get; init; }

        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; set; }
        private Article(Guid? id,string? slug, string title, string description, string body, Guid authorId) //make slug be created
        {
            if (id == null)
            {
                Id = new Guid();
            }
            else Id = (Guid)id;
            
            Title = title;

            if (slug == null)
            {
                string baseSlug = title.Replace(' ', '-').ToLower();
                Slug = $"{baseSlug}-{Guid.NewGuid().ToString()[..8]}";
            }
            else Slug = slug;


            Description = description;
            Body = body;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            AuthorId = authorId;
        }
        public static Article CreateArticle (Guid? id, string? slug, string title, string description, string body,Guid authorId)
        {
            return new Article(id, slug, title, description, body, authorId);
        }
        public void UpdateArticle(string? title, string? description, string? body)
        {
            if(title != null) this.Title = title;
            if (description != null) this.Description = description;
            if (body != null) this.Body = body;
            UpdatedAt = DateTime.Now;
        }
    }
}
