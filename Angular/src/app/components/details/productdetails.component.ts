import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Http } from '@angular/http';
import { ShoppingCartService } from '../../_services/cart.service';
import { Product } from '../../classes/product';
@Component({
    selector: 'productDetails',
    template: require('./productdetails.component.html'),
    providers: [
        ShoppingCartService]
})
export class ProductDetailsComponent {
    id: number;
    Settings : any;
    quantity = 1;
    imageLink: string;
    private sub: any;
    model = new Product();
    constructor(private route: ActivatedRoute, private http: Http, private cartService: ShoppingCartService) {
        const settings = require( '../../classes/settings' );
        this.Settings = settings.Settings; 
    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.id = +params['id'];
            console.log(this.id);         
            this.imageLink = this.Settings.ORIGIN_URL + '/api/Products/GetProductImage/' + this.id;
            this.http.get(this.Settings.ORIGIN_URL + '/api/Products/' + this.id).subscribe(result => {
                this.model = result.json() as Product;
            });

        });
}
    public addToCart(product: Product) {
        product.imageLink = this.imageLink;
        product.productID = this.id;
        for (var i = 0; i < this.quantity; i++) {
            this.cartService.addToCart(product);
        }
        
    }
   
}
