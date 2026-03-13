import { SimpleArticle } from "./simpleArticle";

export interface ArticleFeed {
  articles: SimpleArticle[];
  articlesCount: number;
}
