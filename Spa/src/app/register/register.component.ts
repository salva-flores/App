import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Input() valuesFromHome: any; // Used to communicate with other components
  @Output() cancelRegister = new EventEmitter(); // Used for comm with components
  model: any = {};

  constructor(private authService: AuthService,  private alertify: AlertifyService) { }

  // tslint:disable-next-line: typedef
  ngOnInit() { }

  // tslint:disable-next-line: typedef
  register() {
    this.authService.register(this.model).subscribe(() => {
      console.log(this.model, ' Registration successful: ');
      this.alertify.success('Registration successful!');
    }, error => {
      console.log(error);
      this.alertify.error(error);
    });
  }

  // tslint:disable-next-line: typedef
  cancel() {
    this.cancelRegister.emit(false);
    console.log('Cencel');
    // registerToggle();
  }

}
