import { Injectable } from '@angular/core';
import { Product } from '../classes/product';
import { BehaviorSubject, Observable, Subject, Subscriber } from 'rxjs';
import { of } from 'rxjs/observable/of';
@Injectable()
export class ShoppingCartService {

    private productsInCartSubject: BehaviorSubject<Product[]> = new BehaviorSubject([]);
    private productsInCart: Product[] = [];

    constructor() {
        this.productsInCartSubject.subscribe(_ => this.productsInCart = _);
    }

    public addToCart(item: Product) {
        console.log("Adding to cart service");
        this.productsInCartSubject.next([...this.productsInCart, item]);
        console.log(this.productsInCartSubject);
    }

    public getItems(): Observable<Product[]> {
        return this.productsInCartSubject;
    }


}