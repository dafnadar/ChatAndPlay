import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { User } from "../models/user.model";

@Injectable()
export class UserService {

    API_URL = "http://localhost:5209/Users";

    constructor(private httpClient: HttpClient) {        
    }

    register(user: User): Observable<any> // Observable: declare what i'm getting back from server (type User)
    {
        return this.httpClient.post<any>(`${this.API_URL}/register`, user, {observe:'body',responseType:'text' as 'json'});
    }

    login(user: User): Observable<any> 
    {
        return this.httpClient.post<any>(`${this.API_URL}/login`, user, {observe:'body',responseType:'text' as 'json'});
    }

    getUserByUsername(user: User): Observable<User>
    {
        return this.httpClient.post<User>(`${this.API_URL}/getUserByUsername`, user);
    }

    getUserById(id: number): Observable<User>
    {
        return this.httpClient.get<User>(`${this.API_URL}/`+id);
    }

    // getUsersExceptSelf(id: number): Observable<User[]>
    // {
    //     return this.httpClient.get<User[]>(`${this.API_URL}/getUsers/`+id);
    // }

    Logout(user: User)
    {
        return this.httpClient.put(`${this.API_URL}/logout/`, user);
    }

    setActiveUser(user: User): Observable<User> 
    {
        return this.httpClient.put<User>(`${this.API_URL}/setActiveUser/`, user);
    }

    getUsersBySearch(search: any[]): Observable<User[]>
    {
        return this.httpClient.post<User[]>(`${this.API_URL}/getUsersBySearch/`, search)
    }

    CheckIfRoomExist(users: User[]): Observable<any>
    {
        return this.httpClient.post<any>(`${this.API_URL}/checkRoom`, users, {observe:'body',responseType:'text' as 'json'});
    }

    setRoomForUsers(users: User[]): Observable<any>
    {
        return this.httpClient.put(`${this.API_URL}/setRoom`, users, {observe:'body',responseType:'text' as 'json'});
    }
}