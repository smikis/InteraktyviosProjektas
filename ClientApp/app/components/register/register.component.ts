import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { URLSearchParams } from "@angular/http"
import { Router} from '@angular/router';
@Component({
    selector: 'register',
    template: require('./register.component.html'),
    styleUrls: ['./register.component.css']
})
export class RegisterComponent {
    model = new Register();
    Errors = [
       
    ];

    constructor(private http: Http, @Inject('ORIGIN_URL') private originUrl: string, private router: Router) {
       
    }

    checkPasswordForSymbols() {
        if (this.model.password == undefined || this.model.password.search(/[<>@!#$%^&*()_+[\]{}\?:;\|'\"\\,.\/~`\-=]/) == -1) {
            return true;
        }
        return false;
    }

    checkPasswordForNumbers() {
        if (this.model.password == undefined || this.model.password.search(/\d/) == -1) {
            return true;
        }
        return false;
    }

    onSubmit() {
        let data = new URLSearchParams();
        data.append('name', this.model.name);
        data.append('lastName', this.model.lastName);
        data.append('email', this.model.email);
        data.append('password', this.model.password);
        data.append('confirmPassword', this.model.confirmPassword);
        this.http.post(this.originUrl + '/api/Account/Register', data)
            .subscribe(
            response => {
                this.router.navigate(['/login']);
            }
            ,
            error => {
                this.Errors = JSON.parse(error._body);
            }
            )}
    
}

class Register {
    name: string;
    lastName: string;
    email: string;
    password: string;
    confirmPassword: string;
}