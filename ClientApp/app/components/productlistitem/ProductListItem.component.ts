import { Component, Input } from '@angular/core';

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
    @Input() public imageLink: string;
    constructor() {
       
    }


}