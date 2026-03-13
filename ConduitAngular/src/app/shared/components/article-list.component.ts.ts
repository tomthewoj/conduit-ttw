import { Component, Input, Output, EventEmitter } from '@angular/core';
import { SimpleArticle } from '../../models/simpleArticle';
import { PaginationComponent } from './pagination.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ArticleService } from '../../services/article.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-articleList',
  templateUrl: './article-list.component.html',
  imports: [FormsModule, CommonModule, RouterModule, PaginationComponent]
})
export class ArticleListComponent {
  constructor(private articleService: ArticleService, private route: ActivatedRoute, private authService: AuthService, private router: Router) { }
  currentPage: number = 1;
  totalPages: number = 0;

  @Input() articles: SimpleArticle[] = [];
  @Input() articleCount = 0;
  @Input() limit = 10;
  @Input() offset = 0;

  @Output() limitChange = new EventEmitter<number>();
  @Output() pageChange = new EventEmitter<number>();

  ngOnInit(): void {
    const savedLimit = localStorage.getItem('articleLimit');
    if (savedLimit) this.limit = Number(savedLimit);
    this.limitChange.emit(this.limit);
  }
  ngOnChanges(): void
  {
    this.totalPages = (Math.ceil(this.articleCount / this.limit) || 1);
  }
  resetPages(): void {
    this.offset = 0;
    this.currentPage = 1;
    this.pageChange.emit(this.offset);
  }
  loadPage(page: number) {
    this.currentPage = page;
    this.offset = (this.currentPage - 1) * this.limit;
    this.pageChange.emit(this.offset);
  }
  goToArticle(slug: string) {
    this.router.navigate(['/articles', slug]);
  }
  onLimitChange(): void {
    localStorage.setItem('articleLimit', this.limit.toString());
    this.limitChange.emit(this.limit);
    this.resetPages();
  }
}
