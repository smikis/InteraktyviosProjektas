import { Component, Input, Inject, OnInit} from '@angular/core';

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
    constructor( @Inject('ORIGIN_URL') private originUrl: string) {
       
    }
    ngOnInit() {
        this.imageLink = this.originUrl + '/api/Products/GetProductImage/' + this.id;
        this.descriptionLink = this.originUrl + '/productDetails/' + this.id;
    }


}