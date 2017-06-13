import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Product } from '../../classes/product';
@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
    products = new Array<Product>();
    constructor( @Inject('ORIGIN_URL') private originUrl: string, private http: Http) {
        this.http.get(this.originUrl + '/api/Products/').subscribe(result => {
            this.products = result.json() as Product[];
            console.log(this.products[0].productID);
        });
    }
    public getProducts(): Product[] {
        return this.products;
    }

   

}
