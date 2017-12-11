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
    Settings : any;
    model = new Register();
    Errors = [
       
    ];

    constructor(private http: Http, private router: Router) {
        const settings = require( '../../classes/settings' );
        this.Settings = settings.Settings; 
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
        this.http.post(this.Settings.ORIGIN_URL + '/api/Account', this.model)
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