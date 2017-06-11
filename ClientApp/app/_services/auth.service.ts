import { Injectable } from "@angular/core";
import { Headers, Http } from "@angular/http";

@Injectable()
export class AuthService {
    private tokenKey = "token";
    private token: string;
    public isLoggedIn = false;
    public isAdmin = false;
    constructor(
        private http: Http
    ) { 

    }


    logout() {
        sessionStorage.removeItem(this.tokenKey);
    }

    setToken(token) {
        this.isLoggedIn = true;
        sessionStorage.setItem("token", token);
    }

    checkLogin(): boolean {
        var token = sessionStorage.getItem(this.tokenKey);
        return token != null;
    }

    public getLocalToken(): string {
        if (!this.token) {
            this.token = sessionStorage.getItem(this.tokenKey);
        }
        return this.token;
    }

    private initAuthHeaders(): Headers {
        let token = this.getLocalToken();
        if (token == null) throw "No token";

        var headers = new Headers();
        headers.append("Authorization", "Bearer " + token);

        return headers;
    }
}