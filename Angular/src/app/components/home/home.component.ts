import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Product } from '../../classes/product';
@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
    Settings : any;
    products = new Array<Product>();
    constructor( private http: Http) {
        const settings = require( '../../classes/settings' );
        this.Settings = settings.Settings; 
        this.http.get(this.Settings.ORIGIN_URL + '/api/Products/').subscribe(result => {
            this.products = result.json() as Product[];
            console.log(this.products[0].productID);
        });
    }
    public getProducts(): Product[] {
        return this.products;
    }
}
