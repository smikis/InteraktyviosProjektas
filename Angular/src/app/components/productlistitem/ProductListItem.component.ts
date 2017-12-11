import { Component, Input, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'product-list-item',
    template: require('./ProductListItem.component.html'),
    styleUrls: ['./ProductListItem.component.css']
})
export class ProductListItemComponent {
    Settings : any;
    @Input() public id: number;
    @Input() public name: string;
    @Input() public price: number;
    @Input() public description: string;
    public imageLink: string;
    public descriptionLink: string;
    constructor(private router: Router) {
        const settings = require( '../../classes/settings' );
        this.Settings = settings.Settings;  
    }
    ngOnInit() {
        this.imageLink = this.Settings.ORIGIN_URL + '/api/Products/GetProductImage/' + this.id;
        this.descriptionLink = this.Settings.ORIGIN_URL + '/productDetails/' + this.id;
    }

    onDetailsClick() {
        this.router.navigate(['/productDetails/' + this.id]);
    }


}