import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { URLSearchParams } from "@angular/http"
import { Router } from '@angular/router';
import { AuthService } from "../../_services/auth.service";
@Component({
    selector: 'login',
    template: require('./login.component.html'),
    providers: [AuthService]
})
export class LoginComponent {
    model = new Login();
    Errors = [

    ];

    constructor(private http: Http, @Inject('ORIGIN_URL') private originUrl: string, private router: Router, private autheService : AuthService) {

    }


    onSubmit() {
        let data = new URLSearchParams();     
        data.append('email', this.model.email);
        data.append('password', this.model.password);
        this.http.post(this.originUrl + '/api/Account/Login', data)
            .subscribe(
            response => {
                var result = response.json() as LoginResult;
                console.log(result);
                this.autheService.setToken(result.accessToken);
                //this.router.navigate(['/']);
            }
            ,
            error => {
                this.Errors = JSON.parse(error._body);
            }
            )
    }

}

interface LoginResult {
    requestAt: string;
    expiresIn: string;
    tokenType: string;
    accessToken: string;
}

class Login {
    email: string;
    password: string;
}