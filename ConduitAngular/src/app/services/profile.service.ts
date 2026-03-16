import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class ProfileService
{
  private profileUrl = `${environment.apiBaseUrl}/profile`;
  constructor(private http: HttpClient) { }
  getProfile(username: string): Observable<any> {
    return this.http.get(`${this.profileUrl}/${username}`);
  }
  followUser(username: string): Observable<any> {
    console.log(`${this.profileUrl}/${username}/follow`);
    return this.http.post(`${this.profileUrl}/${username}/follow`, null);
  }
  unfollowUser(username: string) {
    return this.http.delete(`${this.profileUrl}/${username}/follow`);
  }
}
