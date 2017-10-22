import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { ShoppingCartService } from '../../_services/cart.service';
import { Product } from '../../classes/product';
import { Observable } from 'rxjs';
import { of } from 'rxjs/observable/of';
import { Router } from '@angular/router';
import { Http, Headers } from '@angular/http';
import { NguiPopupComponent, NguiMessagePopupComponent } from "@ngui/popup/dist";
@Component({
    selector: 'shoppingCart',
    templateUrl: './shoppingcart.component.html'
})
export class ShoppingCartComponent implements OnInit {
    @ViewChild(NguiPopupComponent) popup: NguiPopupComponent;

    public shoppingCartItems$: Observable<Product[]>;
    public shoppingCartItems: Product[] = [];
    private totalAmount: number;

    constructor(private http: Http, @Inject('ORIGIN_URL') private originUrl: string, private cartService: ShoppingCartService, private router: Router) {
        this.shoppingCartItems$ = this
            .cartService
            .getItems();

        this.shoppingCartItems$.subscribe(_ => this.shoppingCartItems = _);
    }


    onlyUnique(value, index, self) {
        return self.indexOf(value) === index;
    }

    getUniqueItems() {
        let unique = [new Set(this.shoppingCartItems)];
        return unique[0];
    }

    getItemCount(item) {
        return this.cartService.getItemCount(item);
    }

    getTotalAmount() {
        this.totalAmount = this.cartService.getTotalAmount();
        return this.totalAmount;
    }

    routeToHome() {
        console.log("home");
        this.router.navigate(['/']);
    }

    checkout() {
        console.log("checkout");
        console.log(this.shoppingCartItems);
        let headers = new Headers();
        headers.append("Authorization", "Bearer " + sessionStorage.getItem("token"));
        this.http.post(this.originUrl + '/api/Sales/PostSales', this.shoppingCartItems, { headers: headers })
            .subscribe(
            response => {
                console.log(response);
                this.popup.open(NguiMessagePopupComponent, {
                    title: 'Success',
                    message: 'Thank you for you purchase',
                    buttons: {
                        OK: () => {
                            this.cartService.clear();
                            this.router.navigate(['/']);
                        }
                    }
                });
            },
            error => {
                console.log(error);
            }
            )
    }


    ngOnInit() {
    }

}