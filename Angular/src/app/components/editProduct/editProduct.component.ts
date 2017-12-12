import { Component, Inject, ViewChild } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { URLSearchParams } from "@angular/http"
import { Router, ActivatedRoute } from '@angular/router';
import { NguiPopupComponent, NguiMessagePopupComponent } from "@ngui/popup/dist";
@Component({
    selector: 'editProduct',
    template: require('./editProduct.component.html')
})
export class EditProductComponent {
    Settings : any;
    id: number;
    private sub: any;
    @ViewChild(NguiPopupComponent) popup: NguiPopupComponent;
    model = new Product();
    Errors = [

    ];
    constructor(private http: Http, private router: Router,private route: ActivatedRoute) { 
        const settings = require( '../../classes/settings' );
        this.Settings = settings.Settings;    
    }
   
    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.id = +params['id'];
            console.log(this.id);                    
            this.http.get(this.Settings.ORIGIN_URL + '/api/Products/' + this.id).subscribe(result => {
                this.model = result.json() as Product;
            });

        });
}

    onSubmit() {
        console.log(this.model);
        let headers = new Headers();
        headers.append("Authorization", "Bearer " + sessionStorage.getItem("token"));
        this.http.put(this.Settings.ORIGIN_URL + '/api/Products/'+ this.id, this.model, { headers: headers } ).subscribe(result => {
            this.popup.open(NguiMessagePopupComponent, {
                title: 'Success',
                message: 'Successfully updated product',
                buttons: {
                    OK: () => {
                        this.router.navigate(['/']);
                    }
                }              
            });
        });      
    } 
    onChange(event) {
        let reader = new FileReader();
        if(event.target.files && event.target.files.length > 0) {
          let file = event.target.files[0];
          reader.readAsDataURL(file);
          reader.onload = () => {
            this.model.file = ({
              filename: file.name,
              filetype: file.type,
              value: reader.result.split(',')[1]
            })
          };
        }
      }


}

class Product {
    Name: string;
    Description: string;
    Price: number;
    Quantity: number;
    file: string|any;
}
class Category {
    CategoryID: string;
    CategoryName: string;
}
