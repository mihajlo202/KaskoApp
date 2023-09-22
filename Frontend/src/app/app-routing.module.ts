import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BankStatemetComponent } from './components/bank-statemet/bank-statemet.component';
import { BalanceCardComponent } from './components/balance-card/balance-card.component';
import { ContractComponent } from './components/contract/contract.component';

const routes: Routes = [
  {path: '', redirectTo: '/contract', pathMatch: 'full'},
  {path: 'bankstatement', component: BankStatemetComponent},
  {path: 'invoice', component: BalanceCardComponent },
  {path: 'contract', component: ContractComponent },
  {path: '**', redirectTo: 'contract', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
