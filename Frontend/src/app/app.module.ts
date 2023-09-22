import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { BankStatemetComponent } from './components/bank-statemet/bank-statemet.component';
import { BalanceCardComponent } from './components/balance-card/balance-card.component';
import { FormsModule } from '@angular/forms';
import { ContractComponent } from './components/contract/contract.component';

@NgModule({
  declarations: [
    AppComponent,
    BankStatemetComponent,
    BalanceCardComponent,
    ContractComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
