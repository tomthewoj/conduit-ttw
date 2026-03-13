import { Profile } from "./profile";

export interface Comment {
  id: string;
  createdAt: Date;
  updatedAt: Date;
  body: string;
  author: Profile;

}
