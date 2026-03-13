import { Profile } from "./profile";

export interface Article {
  slug: string
  title: string;
  description: string;
  body: string;
  favorited: boolean,
  tagList: string[]
  author: Profile
}
