import { Component, OnInit, signal } from '@angular/core';
import { UserService } from './services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.css'
})
export class App implements OnInit {
  protected readonly token = signal('token');
  
  constructor(private userService: UserService) {
    
  }
  ngOnInit(): void {
    this.userService.getToken().subscribe((response) => {
      this.token.set(response.token);
    });
  }
}
