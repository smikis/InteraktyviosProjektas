import { Component, OnInit, Inject } from '@angular/core';
import { ShoppingCartService } from '../../_services/cart.service';
import { Product } from '../../classes/product';
import { Observable } from 'rxjs';
import { of } from 'rxjs/observable/of';
import { Router } from '@angular/router';
import { Http } from '@angular/http';
@Component({
    selector: 'shoppingCart',
    templateUrl: './shoppingcart.component.html'
})
export class ShoppingCartComponent implements OnInit {

    public shoppingCartItems$: Observable<Product[]>;
    public shoppingCartItems: Product[] = [];
    private totalAmount: number;

    constructor(private http: Http, @Inject('ORIGIN_URL') private originUrl: string,private cartService: ShoppingCartService, private router: Router) {
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
        this.http.post(this.originUrl + '/api/Sales/PostSales', this.shoppingCartItems)
            .subscribe(
            response => {
                console.log(response);
            }
            ,
            error => {
                console.log(error);
            }
            )
    }
        

    ngOnInit() {
    }

}