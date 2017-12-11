import { Component, Inject, ViewChild } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { URLSearchParams } from "@angular/http"
import { Router } from '@angular/router';
import { NguiPopupComponent, NguiMessagePopupComponent } from "@ngui/popup/dist";
@Component({
    selector: 'newProduct',
    template: require('./newproduct.component.html')
})
export class NewProductComponent {
    Settings : any;
    @ViewChild(NguiPopupComponent) popup: NguiPopupComponent;
    model = new Product();
    Errors = [

    ];
    file: File;
    constructor(private http: Http, private router: Router) { 
        const settings = require( '../../classes/settings' );
        this.Settings = settings.Settings;    
    }
   
   

    onSubmit() {
        console.log(this.model);
        let headers = new Headers();
        headers.append("Authorization", "Bearer " + sessionStorage.getItem("token"));
        this.http.post(this.Settings.ORIGIN_URL + '/api/Products/', this.model, { headers: headers } ).subscribe(result => {
            this.popup.open(NguiMessagePopupComponent, {
                title: 'Success',
                message: 'Successfully created new product',
                buttons: {
                    OK: () => {
                        this.router.navigate(['/']);
                    }
                }              
            });
        });      
    } 
    onChange(event: EventTarget) {
        let eventObj: MSInputMethodContext = <MSInputMethodContext>event;
        let target: HTMLInputElement = <HTMLInputElement>eventObj.target;
        let files: FileList = target.files;
        this.file = files[0];
        console.log(this.file);
    }


}

class Product {
    Name: string;
    Description: string;
    Price: number;
    Quantity: number;
}
class Category {
    CategoryID: string;
    CategoryName: string;
}
