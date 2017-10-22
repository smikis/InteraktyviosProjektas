import { Component, Input, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'product-list-item',
    template: require('./ProductListItem.component.html'),
    styleUrls: ['./ProductListItem.component.css']
})
export class ProductListItemComponent {
    @Input() public id: number;
    @Input() public name: string;
    @Input() public price: number;
    @Input() public description: string;
    public imageLink: string;
    public descriptionLink: string;
    constructor( @Inject('ORIGIN_URL') private originUrl: string, private router: Router) {
       
    }
    ngOnInit() {
        this.imageLink = this.originUrl + '/api/Products/GetProductImage/' + this.id;
        this.descriptionLink = this.originUrl + '/productDetails/' + this.id;
    }

    onDetailsClick() {
        this.router.navigate(['/productDetails/' + this.id]);
    }


}