import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-register',
  standalone:true,
   imports:[
    CommonModule,
    HttpClientModule,
    FormsModule,
    RouterModule
  ],
  templateUrl: './register.component.html'
})
export class RegisterComponent {
  username = '';
  password = '';
  message = '';
  error = '';

  constructor(private authService: AuthService, private router: Router) {}

  register() {
    if (!this.username || !this.password) {
      this.error = 'Username and password are required';
      return;
    }
debugger
    this.authService.register(this.username, this.password).subscribe({
  next: (res: string) => {
    this.message = "Registered successfully"; // will just be "Registered successfully"
    setTimeout(() => this.router.navigate(['/']), 1500);
  },
  error: (err) => {
    this.error = err.error || 'Registration failed';
  }
});

  }
}
