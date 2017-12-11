import { Component, Input, Inject, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Http, Headers } from "@angular/http";
import { NguiPopupComponent, NguiMessagePopupComponent } from "@ngui/popup/dist";
@Component({
    selector: 'product-list',
    template: require('./productlist.component.html'),
})
export class ProductListComponent {
    Settings : any;
    @ViewChild(NguiPopupComponent) popup: NguiPopupComponent;
    public data;
    public filterQuery = "";
    public rowsOnPage = 10;
    public sortBy = "name";
    public sortOrder = "asc";

    constructor(private http: Http) {
        const settings = require( '../../classes/settings' );
        this.Settings = settings.Settings;  
    }

    ngOnInit(): void {
        this.http.get(this.Settings.ORIGIN_URL + '/api/Products/').subscribe(result => {
            this.data = result.json();
        });
    }

    edit(item) {
        console.log(item);
    }

    delete(item) {
        this.popup.open(NguiMessagePopupComponent, {
            title: 'Please confirm',
            message: 'Are you sure you want to delete ' + item.name,
            buttons: {
                OK: () => {
                    console.log("deleted");
                    var index = this.data.indexOf(item);
                    this.data.splice(index, 1);
                    let headers = new Headers();
                    headers.append("Authorization", "Bearer " + sessionStorage.getItem("token"));
                    this.http.delete(this.Settings.ORIGIN_URL + '/api/Products/' + item.productID, { headers: headers }).subscribe(result => {
                        console.log(result);
                    });


                    this.popup.close();
                },
                CANCEL: () => {
                    console.log("canceled");
                    this.popup.close();
                }
            }
        });
    }

    
}