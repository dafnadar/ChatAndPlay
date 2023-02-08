import { Component, Input, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { startWith } from 'rxjs';
import { Message } from 'src/app/models/message.model';
import { User } from 'src/app/models/user.model';
import { MessageService } from 'src/app/services/message.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-chat-room',
  templateUrl: './chat-room.component.html',
  styleUrls: ['./chat-room.component.css'],
})
export class ChatRoomComponent implements OnInit {
  @Input() user1: User = new User();
  @Input() user2: User = new User(); 
  room: string = '';
  txtMsg: string = '';
  invite: boolean = true;

  message: Message = new Message();
  messageList: Message[] = [];
  connection!: HubConnection;

  constructor(private msgService: MessageService, private userService: UserService) {     
  }

  ngOnInit(): void {  
    this.connection = new HubConnectionBuilder()
      .withUrl('http://localhost:5209/chatHub')
      .withAutomaticReconnect()
      .build();        
      this.startConnection();      
    }

  startConnection() {          
    this.connection.start().then((res) => {
      var users: User[] = [this.user1, this.user2];
      this.userService.CheckIfRoomExist(users).subscribe((res) => {
        if (res === '') {
          this.createRoom(users);
        } else {
          this.room = res;
          this.joinRoom();
        }
      });
    })
  }

  endConnection() {
    this.connection.stop();
  }

  createRoom(users: User[]) {
    this.userService.setRoomForUsers(users).subscribe((res) => {
      this.room = res;
      this.joinRoom();
    });
  }

  joinRoom() {
    console.log(this.room);
    this.connection.invoke('JoinRoom', this.room);
    this.loadMessages();
    this.receiveMessage();
  }

  loadMessages() {
    this.msgService.loadMessages(this.room).subscribe((messages) => {
      this.messageList = messages;
    })
  }

  receiveMessage() {
    this.connection.on('ReceiveMessage', (sender: string, text: string) => {
      var msg = this.createMessage(sender, text, this.room);
      this.messageList.push(msg);
    });
  }
  
  SendMessage(text: string) {
    this.user1.roomId.push(this.room);
    this.user2.roomId.push(this.room);
    this.connection.invoke('SendMessageToGroup', this.room, this.user1.username, text);
    var msg = this.createMessage(this.user1.username, text, this.room);
    this.addMessageToDB(msg);
    this.txtMsg="";            
  }

  createMessage(sender: string, text: string, room: string): Message {
    var msg = new Message();
    msg.sender = sender;
    msg.textMsg = text;
    msg.room = room;
    return msg;
  }

  addMessageToDB(msg: Message) {
    this.msgService.addMessageToDB(msg).subscribe(() => {
      
    })
  }

  inviteToGame() {
    this.invite = true;
  }
}
