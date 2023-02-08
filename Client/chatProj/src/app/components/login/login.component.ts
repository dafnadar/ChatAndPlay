import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user.model';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  user: User = new User();
  message: string = '';

  constructor(private userService: UserService, private router: Router) { }

  loginHandler() {
    this.userService.login(this.user).subscribe(err => {
      this.message = err;
      if (this.message === "") {
        this.userService.getUserByUsername(this.user).subscribe(result => {
          this.user = result;
          this.router.navigateByUrl(`lobby/${this.user.userId}`);
        })
      }
      else {
        this.message = err;
      }
    })

  }

  ngOnInit(): void {
  }

}
