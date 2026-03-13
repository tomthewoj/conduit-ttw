import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { ArticleService } from '../services/article.service';
import { CommonModule } from '@angular/common';
import { Article } from '../models/article';
import { ActivatedRoute } from '@angular/router';
import { Comment } from '../models/comment';

@Component({
  selector: 'app-article',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterModule],
  templateUrl: './article.component.html'
})
export class ArticleComponent {

  article: Article | null = null;
  isAuthor: boolean = false;
  comments: Comment[] = [];
  commentBody = "";
  isLoggedIn = false;
  currentUser = "";
  constructor(private articleService: ArticleService, private route: ActivatedRoute, private auth: AuthService, private router: Router) {  }

  ngOnInit(): void {
    this.auth.isLoggedIn$.subscribe(state => this.isLoggedIn = state);
    this.currentUser = this.auth.getUsername() ?? '';
    const slug = this.route.snapshot.paramMap.get('slug');
    if (slug) this.articleService.getArticle(slug).subscribe(data =>
    {
      this.article = data
      if (this.auth.getUsername() == this.article?.author.username) this.isAuthor = true;
      this.loadComments();
    });
  }
  editArticle(): void {
    this.router.navigate(['/articleEditor', this.article?.slug]);
  }
  deleteArticle(): void {
    if (this.article?.slug) {
      this.articleService.deleteArticle(this.article.slug).subscribe({
        next: () => {
          console.log('Article deleted');
        },
        error: err => console.error('Delete failed', err)
      });
    }
  }
  toggleFavorite() {
    if (this.article?.favorited != null) {
      if (this.article.favorited == true) {
        this.articleService.unfavoriteArticle(this.article.slug).subscribe();
        this.article.favorited = false;
      }

      else {
        this.articleService.favoriteArticle(this.article.slug).subscribe();
        this.article.favorited = true;
      }
    }
  }
  loadComments() {
    if (this.article)
      this.articleService.getComments(this.article.slug).subscribe(data => {
        this.comments = data.comments;
      });
  }
  submitComment() {
    if (this.article)
      this.articleService.postComment(this.article.slug, this.commentBody).subscribe(data => { this.loadComments(); });

  }
  deleteComment(commentId: string) {
    if (this.article)
      this.articleService.removeComment(this.article.slug, commentId).subscribe(data => { this.loadComments(); });

  }
}
