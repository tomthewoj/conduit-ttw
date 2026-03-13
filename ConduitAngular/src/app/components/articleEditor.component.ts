import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { SimpleArticle } from '../models/simpleArticle';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ArticleService } from '../services/article.service';
import { ArticleEdit } from '../models/articleEdit';
import { ActivatedRoute } from '@angular/router'
import { Article } from '../models/article';
@Component({
  selector: 'app-articleEditor',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterModule],
  templateUrl: './articleEditor.component.html'
})

export class ArticleEditorComponent {
  constructor(private auth: AuthService, private art: ArticleService, private route: ActivatedRoute) { }

  ngOnInit(): void {

    this.isLoggedIn = this.auth.isLoggedIn();
    const slug = this.route.snapshot.paramMap.get('slug');

    if (slug) {
      this.mode = 'edit';
      this.loadArticle(slug);
    } else {
      this.mode = 'create';
    }
  }
  mode = '';
  title = '';
  description = '';
  body = '';
  slug = '';
  tags = [] as string[];
  
  isLoggedIn = false;

  tagInput = '';

  loadArticle(slug: string) {
    this.art.getArticle(slug).subscribe(data => {
      this.title = data.title;
      this.description = data.description;
      this.body = data.body;
      this.tags = data.tagList;
      this.slug = data.slug;
    });
  }
  removeTag(tag: string) {
    this.tags = this.tags.filter(t => t !== tag);
  }
  addTag() {
    const value = this.tagInput.trim();
    if (value && !this.tags.includes(value)) {
      this.tags.push(value);
    }
    this.tagInput = '';
  }
  submitEdit() {
    if (this.mode == "edit") {
      const article: ArticleEdit = { title: this.title, description: this.description, body: this.body, tags: this.tags, slug: this.slug }
      this.art.updateArticle(article).subscribe({
        next: (response) => {
          console.log(response);
        },
        error: () => {
          console.log("error");
        }
      });
    }
    if (this.mode == "create") {
      const article: ArticleEdit = { title: this.title, description: this.description, body: this.body, tags: this.tags, slug: this.slug }
      this.art.createArticle(article).subscribe({
        next: (response) => {
          console.log(response);
        },
        error: () => {
          console.log("error");
        }
      });
    }

  }
}
