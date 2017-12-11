import { Component } from '@angular/core';
import { AuthService } from "../../_services/auth.service";
import { Router } from '@angular/router';
import { ShoppingCartService } from '../../_services/cart.service';
import { Product } from '../../classes/product';
import { Observable } from 'rxjs';
import { of } from 'rxjs/observable/of';
@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css'],
    providers: [AuthService, 
        ShoppingCartService]
})
export class NavMenuComponent {
    public shoppingCartItems$: Observable<Product[]>;
    constructor(private router: Router, private authService: AuthService, private cartService: ShoppingCartService) {
        this.shoppingCartItems$ = this
            .cartService
            .getItems();

        this.shoppingCartItems$.subscribe(_ => _);
    }



    isLoggedIn(): boolean {
        return this.authService.checkLogin();
    }  

    isAdmin(): boolean {
        return this.authService.checkAdmin();
    }  

    onLogoutClick() {
        this.authService.logout();
        this.router.navigate(['/login']);
    }

}
