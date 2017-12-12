import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Product } from '../../classes/product';
import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';
@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {

    Settings : any;
    products = new Array<Product>();
    constructor( private http: Http, private spinnerService: Ng4LoadingSpinnerService) {
        const settings = require( '../../classes/settings' );
        this.Settings = settings.Settings;
        spinnerService.show(); 
        this.http.get(this.Settings.ORIGIN_URL + '/api/Products/').subscribe(result => {
            this.products = result.json() as Product[];
            console.log(this.products[0].productID);
            spinnerService.hide();
        });
    }
    public getProducts(): Product[] {
        return this.products;
    }
}
