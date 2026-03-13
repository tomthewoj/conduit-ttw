import { Routes } from '@angular/router';
import { LoginComponent } from './components/login.component';
import { ArticleComponent } from './components/article.component';
import { RegistrationComponent } from './components/registration.component';
import { ArticleEditorComponent } from './components/articleEditor.component';
import { ArticlesComponent } from './components/articles.component';
import { ProfileComponent } from './components/profile.component';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegistrationComponent },
  { path: 'articles/:slug', component: ArticleComponent },
  { path: 'tags/:tag', component: ArticlesComponent },
  { path: 'articles', component: ArticlesComponent },
  { path: 'feed', component: ArticlesComponent },
  { path: 'favorites/:username', component: ArticlesComponent },
  { path: 'articleEditor', component: ArticleEditorComponent },
  { path: 'articleEditor/:slug', component: ArticleEditorComponent },
  { path: 'profile/:username', component: ProfileComponent },
  { path: 'profile', component: ProfileComponent }
];
