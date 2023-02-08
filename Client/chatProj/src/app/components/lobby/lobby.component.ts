import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { User } from 'src/app/models/user.model';
import { UserService } from 'src/app/services/user.service';
import { ChatRoomComponent } from '../chat-room/chat-room.component';

@Component({
  selector: 'app-lobby',
  templateUrl: './lobby.component.html',
  styleUrls: ['./lobby.component.css']
})
export class LobbyComponent implements OnInit {

  user: User = new User();
  users: User[] = [];
  user2: User = new User();
  search: any[] = [];
  searchKey: string = "";
  @ViewChild("chatRoom") chatRoom!: ChatRoomComponent;
  userSubject: BehaviorSubject<string> = new BehaviorSubject<string>("start");  
 
  constructor(private userService: UserService, private route: ActivatedRoute, private router: Router) {
   }

  ngOnInit(): void {    
    this.route.params.subscribe(params => {
      this.user.userId = params['id'];  
      this.userService.getUserById(this.user.userId).subscribe(user => {
        this.user = user;
        this.setActiveUser(); 
        this.setUsersArray();
      });      
    });      
  }

  setActiveUser() {
    this.userService.setActiveUser(this.user).subscribe(user => {
      this.user = user;
    });
  }

  setUsersArray() {
    setInterval(() => {
      this.search = [];
      this.search.push(this.searchKey);
      this.search.push(this.user.userId);
      this.userService.getUsersBySearch(this.search).subscribe(users => {
        this.users = users;
      });
    }, 3000);        
  }

  Logout() {
    this.userService.Logout(this.user).subscribe(() => {
      this.router.navigateByUrl('/login');      
    });
  }

  // HostListener: listen to changes on browser
  // popstate - changes in routing in URL changes
  @HostListener('window:popstate', ['$event']) 
  onPopState() { 
    this.Logout();
  }

  @HostListener('window:beforeunload', ['$event'])
   onWindowClose() {
    this.Logout();
  }

  goToChatRoom(user2: User) {       
    if (this.userSubject.value !== "start" && user2.username !== this.userSubject.value) {
      //this.chatRoom.exitRoom();
      this.chatRoom.endConnection();
      this.chatRoom.ngOnInit();
    }

    this.user2 = user2;
    this.userSubject.next(user2.username);

  }

}
