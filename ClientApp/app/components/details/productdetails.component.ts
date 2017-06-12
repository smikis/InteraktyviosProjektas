import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'productDetails',
    template: require('./productdetails.component.html')
})
export class ProductDetailsComponent {
    id: number;
    private sub: any;
    model = new Product();
    constructor(private route: ActivatedRoute) {
        this.model.name = "Samsung test name";
        this.model.description = "  Stay connected either on the phone or the Web with the Galaxy S4 I337 from Samsung. With 16 GB of memory and a 4G connection, this phone stores precious photos and video and lets you upload them to a cloud or social network at blinding-fast speed. With a 17-hour operating life from one charge, this phone allows you keep in touch even on the go.\n With its built-in photo editor, the Galaxy S4 allows you to edit photos with the touch of a finger, eliminating extraneous background items.Usable with most carriers, this smartphone is the perfect companion for work or entertainment.";
        this.model.price = 99.99;
    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.id = +params['id']; // (+) converts string 'id' to a number
            console.log(this.id);
            // In a real app: dispatch action to load the details here.
        });
}

   
}

class Product {
    name: string;
    description: string;
    price: number;
    createdDate: Date;
}