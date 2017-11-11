﻿import { Component, Inject, ViewChild } from '@angular/core';
import { Http } from '@angular/http';
import { URLSearchParams } from "@angular/http"
import { Router } from '@angular/router';
import { Ng2UploaderService } from 'ng2-uploader/src/services/ng2-uploader';
import { Ng2UploaderOptions } from 'ng2-uploader/src/classes/ng2-uploader-options.class';
import { NguiPopupComponent, NguiMessagePopupComponent } from "@ngui/popup/dist";
@Component({
    selector: 'newProduct',
    template: require('./newproduct.component.html')
})
export class NewProductComponent {
    @ViewChild(NguiPopupComponent) popup: NguiPopupComponent;
    model = new Product();
    Errors = [

    ];
    uploader: Ng2UploaderService;
    options: Ng2UploaderOptions;
    file: File;
    constructor(private http: Http, @Inject('ORIGIN_URL') private originUrl: string, private router: Router) {
        this.uploader = new Ng2UploaderService();      
    }
   
   

    onSubmit() {
        var testOptions = new INg2UploaderOptions();
        testOptions.url = this.originUrl + "/api/Products";
        testOptions.data = {
            name: this.model.name,
            description: this.model.description,
            price: this.model.price,
            quantity: this.model.quantity
        };
        testOptions.authToken = sessionStorage.getItem("token");
        testOptions.authTokenPrefix = "Bearer";
        var options = new Ng2UploaderOptions(testOptions);
        this.uploader.setOptions(options);
        this.uploader.uploadFile(this.file);
        
        this.popup.open(NguiMessagePopupComponent, {
            title: 'Success',
            message: 'Successfully created new product',
            buttons: {
                OK: () => {
                    this.router.navigate(['/']);
                }
            }              
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
    name: string;
    description: string;
    price: number;
    quantity: number;
}
class Category {
    CategoryID: string;
    CategoryName: string;
}


class INg2UploaderOptions {
    url: string;
    cors?: boolean;
    withCredentials?: boolean;
    multiple?: boolean;
    maxUploads?: number;
    data?: any;
    autoUpload?: boolean;
    multipart?: any;
    method?: 'POST' | 'GET';
    customHeaders?: any;
    encodeHeaders?: boolean;
    authTokenPrefix?: string;
    authToken?: string;
    fieldName?: string;
    fieldReset?: boolean;
    previewUrl?: string;
    calculateSpeed?: boolean;
    filterExtensions?: boolean;
    allowedExtensions?: string[];
}