import { Component, Inject } from '@angular/core';
import { Product } from '../../classes/product';
@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {

    constructor( @Inject('ORIGIN_URL') private originUrl: string) { }
    public getProducts(): Product[] {
        return this.products();
    }

    private products(): Product[] {
        return <Product[]>[
            <Product>{
                id: 1, name: 'Blue item', price: 123.09, description: "desc", quantity: 10, imageLink: this.originUrl + "/api/Products/GetProductImage/" +  1 },
            <Product>{ id: 2, name: 'Green and gray', price: 99.09, description: "descasdasdasd asdvsd asdadsf", quantity: 10, imageLink: this.originUrl + "/api/Products/GetProductImage/" + 1}
        ];
    }

}
