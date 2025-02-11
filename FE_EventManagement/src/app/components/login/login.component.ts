import { Component, signal } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { RouterModule } from '@angular/router';
import { login } from '../../interfaces/login';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  auth: login = {
    email: '',
    password: '',
};
  errorMessage = signal('');

  constructor(
    private authService: AuthService,
    private toarts: ToastrService,
    private router: Router
  ) {}

  login() {
    if (!this.validateFields()) {
      return;
    }
    
    this.errorMessage.set('');
    
    this.authService.login({ 
      email: this.auth.email, 
      password: this.auth.password 
    }).subscribe({
        next: () => {this.toarts.success('login exitoso'),
        this.router.navigate(['/dashboard'])
      },
        error: () => {this.toarts.error('Credenciales invalidas')
      },
    });
  }

  private validateFields(): boolean {
    if (!this.auth.email.trim() || !this.auth.password.trim()) {
      this.toarts.warning('Todos los campos son obligatorios');
      return false;
    }

    return true;
  }
}
