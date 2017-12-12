import { Component, Input, Inject, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Http, Headers } from "@angular/http";
import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';
import { NguiPopupComponent, NguiMessagePopupComponent } from "@ngui/popup/dist";
@Component({
    selector: 'orders-list',
    template: require('./orderslist.component.html'),
})
export class OrdersListComponent {
    Settings : any;
    @ViewChild(NguiPopupComponent) popup: NguiPopupComponent;
    public data;
    public filterQuery = "";
    public rowsOnPage = 10;
    public sortBy = "name";
    public sortOrder = "asc";

    constructor(private http: Http, private spinnerService: Ng4LoadingSpinnerService) {
        const settings = require( '../../classes/settings' );
        this.Settings = settings.Settings;
    }

    ngOnInit(): void {
        let headers = new Headers();
        headers.append("Authorization", "Bearer " + sessionStorage.getItem("token"));
        this.spinnerService.show();
        this.http.get(this.Settings.ORIGIN_URL + '/api/Sales/', { headers: headers }).subscribe(result => {            
            this.data = result.json();
            console.log(this.data);
            this.spinnerService.hide();
        });
    }

    edit(item) {
        console.log(item);
    }

    delete(item) {
        this.popup.open(NguiMessagePopupComponent, {
            title: 'Please confirm',
            message: 'Are you sure you want to delete ' + item.id +'sale?',
            buttons: {
                OK: () => {
                    console.log("deleted");
                    var index = this.data.indexOf(item);
                    this.data.splice(index, 1);
                    let headers = new Headers();
                    headers.append("Authorization", "Bearer " + sessionStorage.getItem("token"));
                    this.http.delete(this.Settings.ORIGIN_URL + '/api/Sales/' + item.id, { headers: headers }).subscribe(result => {
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