import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ArticleService } from '../services/article.service';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { ProfileService } from '../services/profile.service';
import { Profile } from '../models/profile';
import { SimpleArticle } from '../models/simpleArticle';
import { ArticleListComponent } from '../shared/components/article-list.component.ts';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [FormsModule, CommonModule, ArticleListComponent],
  templateUrl: './profile.component.html'
})
export class ProfileComponent {
  profile: Profile | null = null;
  articleCount: number = 0;
  currentPage: number = 1;
  totalPages: number = 0;
  limit: number = 10;
  offset: number = 0;
  articles: SimpleArticle[] = [];
  username: string = "";
  constructor(private profileService: ProfileService, private route: ActivatedRoute, private authService: AuthService, private router: Router, private articlesService: ArticleService) { }
  ngOnInit(): void {
    const paraUsername = this.route.snapshot.paramMap.get('username');
    if (paraUsername != null) this.username = paraUsername;
    else
    {
      var currentUsername = this.authService.getUsername();
      if (currentUsername != null) this.username = currentUsername
    }
      
    if (this.username) this.profileService.getProfile(this.username).subscribe(data => {
      this.profile = data
    });
    const savedLimit = localStorage.getItem('articleLimit');
    if (savedLimit) this.limit = Number(savedLimit);
    this.reloadPage();
  }
  reloadPage() {
    this.articlesService.listUserArticles(this.limit, this.offset, this.username).subscribe(data => {
      this.articles = data.articles;
      this.articleCount = data.articlesCount;
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
  toggleFollow() {
    if (this.profile != null) {
      if (this.profile.following == true) {
        this.profileService.unfollowUser(this.username).subscribe();
        this.profile.following = false;
      }

      else {
        this.profileService.followUser(this.username).subscribe();
        this.profile.following = true;
      }
    }
    this.reloadPage();
  }

}
