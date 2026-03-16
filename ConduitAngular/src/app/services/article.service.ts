import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ArticleFeed } from '../models/articleFeed';
import { ArticleEdit } from '../models/articleEdit';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {
  private articlesUrl = `${environment.apiBaseUrl}/articles`;
  private articleUrl = `${environment.apiBaseUrl}/article`
  private feedUrl = `${environment.apiBaseUrl}/articles/feed`;
  private tagsUrl = `${environment.apiBaseUrl}/tags`;

  constructor(private http: HttpClient) { }

  getArticle(slug: string): Observable<any> {
    return this.http.get(`${this.articlesUrl}/${slug}`);
  }
  listArticles(limit: number, offset: number, filter: string = ""): Observable<ArticleFeed> {
    var searchParams = `?limit=${limit}&offset=${offset}`;
    if (filter != "") searchParams += `&${filter}`;
    return this.http.get<ArticleFeed>(this.articlesUrl + searchParams);

  }
  feedArticles(limit: number, offset: number): Observable<any> {
    return this.http.get<ArticleFeed>(this.feedUrl + "?limit=" + limit + "&offset=" + offset);
  }
  listUserArticles(limit: number, offset: number, author: string): Observable<any> {
    return this.http.get<ArticleFeed>(this.articlesUrl + "?limit=" + limit + "&offset=" + offset + "&author=" + author);
  }
  createArticle(article: ArticleEdit): Observable<any> {
    return this.http.post(this.articlesUrl, article);
  }
  updateArticle(article: ArticleEdit): Observable<any> {
    return this.http.put(`${this.articlesUrl}/${article.slug}`, article);
  }
  deleteArticle(slug: string): Observable<any> {
    return this.http.delete(`${this.articlesUrl}/${slug}`);
  }
  favoriteArticle(slug: string): Observable<any> {
    return this.http.post(`${this.articlesUrl}/${slug}/favorite`,null);
  }
  unfavoriteArticle(slug: string): Observable<any> {
    return this.http.delete(`${this.articlesUrl}/${slug}/favorite`);
  }
  getComments(slug: string): Observable<any> {
    return this.http.get(`${this.articleUrl}/${slug}/comments`);
  }
  postComment(slug: string, body: string): Observable<any> {
    return this.http.post(`${this.articleUrl}/${slug}/comments?commentBody=${body}`, null);
  }
  removeComment(slug: string, commentId: string): Observable<any> {
    return this.http.delete(`${this.articleUrl}/${slug}/comments/${commentId}`);
  }
  getAllTags(): Observable<any> {
    return this.http.get(`${this.tagsUrl}`);
  }
}
