import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Http } from '@angular/http';

@Component({
    selector: 'productDetails',
    template: require('./productdetails.component.html')
})
export class ProductDetailsComponent {
    id: number;
    quantity: number;
    imageLink: string;
    private sub: any;
    model = new Product();
    constructor(private route: ActivatedRoute, @Inject('ORIGIN_URL') private originUrl: string, private http: Http) {

    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.id = +params['id'];
            console.log(this.id);         
            this.imageLink = this.originUrl + '/api/Products/GetProductImage/' + this.id;
            this.http.get(this.originUrl + '/api/Products/' + this.id).subscribe(result => {
                this.model = result.json() as Product;
            });

        });
}

   
}

class Product {
    name: string;
    description: string;
    price: number;
    createdDate: Date;
    quantity: number;
}