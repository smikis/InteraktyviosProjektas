import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import {HttpModule} from '@angular/http';
import { FormsModule } from '@angular/forms';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { NguiPopupModule } from '@ngui/popup';
import { DataTableModule } from "angular2-datatable";

import { ShoppingCartService } from './_services/cart.service';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { NewProductComponent } from './components/newproduct/newproduct.component';
import { ProductDetailsComponent } from './components/details/productdetails.component';
import { ProductListItemComponent } from './components/productlistitem/ProductListItem.component';
import { ProductListComponent } from './components/productslist/productlist.component';
import { UsersListComponent } from './components/userslist/userslist.component';
import { ShoppingCartComponent } from './components/shoppingcart/shoppingcart.component';
import { OrdersListComponent } from './components/orederslist/orderslist.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    RegisterComponent,
    LoginComponent,
    NewProductComponent,
    ProductDetailsComponent,
    ProductListItemComponent,
    ProductListComponent,
    UsersListComponent,
    ShoppingCartComponent,
    OrdersListComponent  
  ],
  imports: [
    BrowserModule,
    HttpModule,
    FormsModule,
    NgbModule,
    DataTableModule,
    NguiPopupModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: 'home', component: HomeComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'login', component: LoginComponent },
      { path: 'newProduct', component: NewProductComponent },
      { path: 'productList', component: ProductListComponent },
      { path: 'usersList', component: UsersListComponent },
      { path: 'cart', component: ShoppingCartComponent },
      { path: 'orders', component: OrdersListComponent },
      { path: 'productDetails/:id', component: ProductDetailsComponent },
      { path: '**', redirectTo: 'home' }
  ])   
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
