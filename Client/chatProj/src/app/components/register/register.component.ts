import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user.model';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  user: User = new User();
  message: string = '';

  constructor(private userService: UserService, private router: Router) { 
  }

  ngOnInit(): void {
  }

  registerHandler() { // subscribe (listen to server): act function and wait for response from server. ASYNC!!
    this.userService.register(this.user).subscribe(err => {
      if (err === "") {
        this.router.navigateByUrl('/login')
      } 
      else {
        this.message = err;
      }
    //     // message to client:
    //     if (this.user.username === "") {
    //       this.message = "Please enter username"
    //     } 
    //     else {
    //     this.message = "Username already exists. please choose another username"
    //   }  
    // }
    })
    } 
}
