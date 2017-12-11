import { Component } from '@angular/core';
import { ShoppingCartService } from './_services/cart.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [ShoppingCartService]
})
export class AppComponent {
  title = 'app';
}
