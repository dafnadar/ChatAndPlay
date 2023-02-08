import { HttpClient, HttpHeaders } from "@angular/common/http";

import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Message } from "../models/message.model";
import { User } from "../models/user.model";

@Injectable()
export class MessageService {

    API_URL = "http://localhost:5209/Messages";

    constructor(private httpClient: HttpClient) {        
    }

    addMessageToDB(message: Message)
    {
        return this.httpClient.post(`${this.API_URL}/addMessage/`, message);        
    }

    loadMessages(roomId: string): Observable<Message[]>
    {
        return this.httpClient.post<Message[]>(`${this.API_URL}/loadMessages/`, [roomId]);
    }

    

    
}