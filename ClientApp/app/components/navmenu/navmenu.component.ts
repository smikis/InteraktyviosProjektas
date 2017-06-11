import { Component } from '@angular/core';
import { AuthService } from "../../_services/auth.service";
import { Router } from '@angular/router';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css'],
    providers: [AuthService]
})
export class NavMenuComponent {
    constructor(private router: Router, private authService: AuthService) {
        
    }

    IsLoggedIn(): boolean {
        return this.authService.checkLogin();
    }  


    onLogoutClick() {
        console.log("Logout");
        console.log(sessionStorage.getItem("token"));
    }

}
