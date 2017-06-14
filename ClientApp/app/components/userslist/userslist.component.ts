import { Component, Input, Inject, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Http } from "@angular/http";
import { NguiPopupComponent, NguiMessagePopupComponent } from "@ngui/popup/dist";
@Component({
    selector: 'users-list',
    template: require('./userslist.component.html'),
})
export class UsersListComponent {
    @ViewChild(NguiPopupComponent) popup: NguiPopupComponent;
    public data;
    public filterQuery = "";
    public rowsOnPage = 10;
    public sortBy = "name";
    public sortOrder = "asc";

    constructor(private http: Http, @Inject('ORIGIN_URL') private originUrl: string,) {
    }

    ngOnInit(): void {
        this.http.get(this.originUrl + '/api/Account/GetAllUsers').subscribe(result => {
            console.log(result);
            this.data = result.json();
        });
    }

    edit(item) {
        console.log(item);
    }

    delete(item) {
        this.popup.open(NguiMessagePopupComponent, {
            title: 'Please confirm',
            message: 'Are you sure you want to delete ' + item.email,
            buttons: {
                OK: () => {
                    console.log("deleted");
                    var index = this.data.indexOf(item);
                    this.data.splice(index, 1);

                    this.http.delete(this.originUrl + '/api/Account/RemoveUser/' + item.id).subscribe(result => {
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