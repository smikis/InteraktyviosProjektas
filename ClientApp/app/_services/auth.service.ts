import { Injectable } from "@angular/core";
import { Headers, Http } from "@angular/http";

@Injectable()
export class AuthService {
    private tokenKey = "token";
    private adminKey = "isAdmin";
    private token: string;
    constructor(
        private http: Http
    ) { 

    }


    logout() {
        sessionStorage.removeItem(this.tokenKey);
        sessionStorage.removeItem(this.adminKey);
    }

    setToken(token) {
        sessionStorage.setItem(this.tokenKey, token);
    }

    setAdmin(admin) {
        sessionStorage.setItem(this.adminKey, admin);
    }

    checkLogin(): boolean {
        var token = sessionStorage.getItem(this.tokenKey);
        return token != null;
    }

    checkAdmin(): boolean {
        var admin = sessionStorage.getItem(this.adminKey);
        if (admin != null) {
            return admin == 'true';
        }
        return false;
    }


    public getLocalToken(): string {
        if (!this.token) {
            this.token = sessionStorage.getItem(this.tokenKey);
        }
        return this.token;
    }

}