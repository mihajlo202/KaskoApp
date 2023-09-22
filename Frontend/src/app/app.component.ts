import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'AccountingFrontend';
  constructor(private router: Router) {}

  contractsClicked()
  {
    this.router.navigate([`./contract`]);
  }

  bankStatementClicked() {
    this.router.navigate([`./bankstatement`]);
  }

  balanceAccount() {
    this.router.navigate([`/invoice`]);
  }
}