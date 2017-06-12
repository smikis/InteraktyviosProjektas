import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Ng2UploaderModule } from 'ng2-uploader';
import { CustomFormsModule } from 'ng2-validation';
import { NguiPopupModule } from '@ngui/popup';

import { AuthService } from './_services/auth.service';

import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { NewProductComponent } from './components/newproduct/newproduct.component';
import { ProductDetailsComponent } from './components/details/productdetails.component';
import { ProductListItemComponent } from './components/productlistitem/ProductListItem.component';

export const sharedConfig: NgModule = {
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        RegisterComponent,
        LoginComponent,
        NewProductComponent,
        ProductDetailsComponent,
        ProductListItemComponent
    ],
    imports: [
        NguiPopupModule,
        FormsModule,
        CustomFormsModule,
        Ng2UploaderModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'register', component: RegisterComponent },
            { path: 'login', component: LoginComponent },
            { path: 'newProduct', component: NewProductComponent },
            { path: 'productDetails/:id', component: ProductDetailsComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
};
