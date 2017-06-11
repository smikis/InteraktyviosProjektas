import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'productDetails',
    template: require('./productdetails.component.html')
})
export class ProductDetailsComponent {
    id: number;
    private sub: any;

constructor(private route: ActivatedRoute) {}

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.id = +params['id']; // (+) converts string 'id' to a number
            console.log(this.id);
            // In a real app: dispatch action to load the details here.
        });
    }

}