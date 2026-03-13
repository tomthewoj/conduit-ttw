import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SimpleArticle } from '../models/simpleArticle';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ArticleService } from '../services/article.service';
import { ArticleListComponent } from '../shared/components/article-list.component.ts';
import { Tag } from '../models/tag';
@Component({
  selector: 'app-articles',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterModule, ArticleListComponent],
  templateUrl: './articles.component.html'
})
export class ArticlesComponent {
  mode: string = 'article'; // article or feed
  articleCount: number = 0;
  currentPage: number = 1;
  totalPages: number = 0;
  limit: number = 10;
  offset: number = 0;
  articles: SimpleArticle[] = []; // could define type instead of any i guess

  title: string = "";

  filterValue: string = "";
  selectedFilter: string = "";
  filterCombined: string = "";

  tagList: Tag[] = [];
  constructor(private articlesService: ArticleService, private router: Router, private route: ActivatedRoute) {  }

  ngOnInit(): void {
    this.loadSavedArticleLimit();
    this.reloadPage();
    this.displayTags();
  }

  loadSavedArticleLimit() {
    const savedLimit = localStorage.getItem('articleLimit');
    if (savedLimit) this.limit = Number(savedLimit);
  }
  reloadPage()
  {
    this.route.params.subscribe(params => {

      const tag = params['tag'];
      const username = params['username'];
      const feed = this.route.snapshot.routeConfig?.path === 'feed';

      if (feed) {
        this.mode = "feed";
        this.loadFeed();
      }
      else if (tag) {
        this.mode = "tag";
        this.loadTagArticles(tag);
      }
      else if (username) {
        this.mode = "favorites";
        this.loadFavorites(username);
      }
      else {
        this.mode = "article";
        this.loadArticles();
      }
    });
  }
  limitChange(newLimit: number) {
    this.limit = newLimit;
    this.reloadPage();
  }
  pageChange(newOffset: number) {
    this.offset = newOffset;
    this.reloadPage();
  }
  loadTagArticles(tag: string) {
    this.filterCombined = "&tag=" + tag;
    this.loadArticles();
    this.title = "Articles with Tag: "+ tag;
  }
  loadFavorites(username: string) {
    this.filterCombined = "&favorited=" + username;
    this.loadArticles();
    this.title = username + "'s Favorites";
  }
  applyFilter() {
    if (this.filterValue != "") {
      this.filterCombined = this.selectedFilter + "=" + this.filterValue;
      this.loadArticles();
      this.title = "Filtered Articles";
    }
    else
      this.filterCombined="";
      this.loadArticles();

  }
  loadArticles() {
    this.articlesService.listArticles(this.limit, this.offset, this.filterCombined).subscribe(data => {
      this.articles = data.articles;
      this.articleCount = data.articlesCount;
    });
    this.title = "Articles";
  }
  loadFeed() {
    this.articlesService.feedArticles(this.limit, this.offset).subscribe(data => {
      this.articles = data.articles;
      this.articleCount = data.articlesCount;
    });
    this.title = "Your Feed";
  }
  displayTags() {
    this.articlesService.getAllTags().subscribe(data => {
      this.tagList = data;
    });;
  }
}

// this doesn't work: this.totalPages = (this.totalArticles / this.limit) + 1;
/*
  previousPage() : void {
    if (this.currentPage > 1) {
      this.currentPage -= 1;
      this.offset = (this.currentPage - 1) * this.limit;
      this.loadInitiator();
    }
  }
  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage += 1;
      this.offset = (this.currentPage - 1) * this.limit;
      this.loadInitiator();
    }
  }
*/
