import { Component, OnInit } from '@angular/core';
import { ShoppingCartService } from '../../_services/cart.service';
import { Product } from '../../classes/product';
import { Observable } from 'rxjs';
import { of } from 'rxjs/observable/of';

@Component({
    selector: 'shoppingCart',
    templateUrl: './shoppingcart.component.html'
})
export class ShoppingCartComponent implements OnInit {

    public shoppingCartItems$: Observable<Product[]>;
    public shoppingCartItems: Product[] = [];

    constructor(private cartService: ShoppingCartService) {
        this.shoppingCartItems$ = this
            .cartService
            .getItems();
        console.log(this.shoppingCartItems$);
        this.shoppingCartItems$.subscribe(_ => this.shoppingCartItems = _);
    }

    ngOnInit() {
    }

}