import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { URLSearchParams } from "@angular/http"
import { Router } from '@angular/router';
import { AuthService } from "../../_services/auth.service";
@Component({
    selector: 'login',
    template: require('./login.component.html'),
    styleUrls: ['./login.component.css'],
    providers: [AuthService]
})
export class LoginComponent {
    Settings : any;
    model = new Login();
    Errors = [

    ];

    constructor(private http: Http, private router: Router, private autheService : AuthService) {
        const settings = require( '../../classes/settings' );
        this.Settings = settings.Settings;
    }

    onSubmit() {
        this.http.post(this.Settings.ORIGIN_URL + '/api/login', this.model)
            .subscribe(
            response => {
                var result = JSON.parse(response.json()) as LoginResult;
                this.autheService.setToken(result.accessToken);
                this.autheService.setAdmin(result.isAdmin);
                this.router.navigate(['/']);
            }
            ,
            error => {
                this.Errors = JSON.parse(error._body);
            }
            )
    }

}

interface ILoginResult {
    requestAt: string;
    expiresIn: string;
    tokenType: string;
    accessToken: string;
    isAdmin: boolean;
}

class LoginResult implements ILoginResult {
    requestAt: string;
    expiresIn: string;
    tokenType: string;
    accessToken: string;
    isAdmin: boolean;
}

class Login {
    email: string;
    password: string;
}