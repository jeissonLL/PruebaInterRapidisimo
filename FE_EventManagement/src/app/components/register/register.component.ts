import { Component } from '@angular/core';
import { RegisterService } from '../../services/register.service';
import { register } from '../../interfaces/register';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, RouterModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  user: register = {
    name: '',
    email: '',
    password: '',
  };

  constructor(
    private registerService: RegisterService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  createUser() {
    if (!this.validateFields()) {
      return;
    }

    this.registerService.createUser(this.user).subscribe({
      next: () => { 
        this.toastr.success('Usuario creado con éxito');
        this.router.navigate(['/login']);
      },
      error: () => { 
        this.toastr.error('El usuario no ha sido creado');
      },
    });
  }

  private validateFields(): boolean {
    if (!this.user.name.trim() || !this.user.email.trim() || !this.user.password.trim()) {
      this.toastr.warning('Todos los campos son obligatorios');
      return false;
    }

    if (!this.validateEmail(this.user.email)) {
      this.toastr.warning('El formato del correo no es válido');
      return false;
    }

    if (this.user.password.length < 6) {
      this.toastr.warning('La contraseña debe tener al menos 6 caracteres');
      return false;
    }

    return true;
  }

  private validateEmail(email: string): boolean {
    const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    return emailPattern.test(email);
  }
}
