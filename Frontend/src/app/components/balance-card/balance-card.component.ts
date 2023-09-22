import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { environmentVariables } from 'src/app/constants/url-constants';
import { Invoice } from 'src/app/models/Invoice';
import { Transaction } from 'src/app/models/Transaction';

@Component({
  selector: 'app-balance-card',
  templateUrl: './balance-card.component.html',
  styleUrls: ['./balance-card.component.css']
})
export class BalanceCardComponent {
  items: Invoice[] = [];
  createAmount: string = "";
  idFrom: string = "";
  idTo: string = "";
  private accountingURL = environmentVariables.ACCOUNTING;
  private cancelItemURL = environmentVariables.CANCEL_ITEM;
  private transactionURL = environmentVariables.TRANSACTION_URL;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getInvoices();
  }

  getInvoices() {
    this.items = [];
    let url = this.accountingURL + 'GetInvoices';
    return this.http.get<any>(url).subscribe({
      next: (event) => {
        var pom: Invoice[] = event;
        pom.map(item => {
          if(item.status == 0)
            item.statusText = "";
          else
            item.statusText = "Canceled"
          if(item.type == 0)
            item.typeText = "Invoice";
          else
            item.typeText = "Bank Item"
        })
        this.items = event;
      },
      error: (err: HttpErrorResponse) => console.log(err),
    });
  }



  createItem() {
    let url = this.accountingURL + 'CreateInvoice';
    var amount = Number.parseInt(this.createAmount);
    this.http
      .post<boolean>(url, new Invoice(-1, 0, 0, amount, amount, 0, -1))
      .subscribe({
        next: (event) => {
          this.getInvoices();
          this.createAmount = "";
        },
        error: (err: HttpErrorResponse) => console.log(err),
      });
  };

  cancelInvoice(item: Invoice) {
    let url = this.cancelItemURL + 'CancelInvoice';
    item.status = 1;
    this.http
      .post<boolean>(url, item)
      .subscribe({
        next: (event) => {
          this.getInvoices();
        },
        error: (err: HttpErrorResponse) => console.log(err),
      });
  }

  transaction() {
    var idF = Number.parseInt(this.idFrom);
    var idT = Number.parseInt(this.idTo);
    var itemFrom:Invoice = new Invoice(-1,0,0,0,0,0,-1);
    var itemTo:Invoice = new Invoice(-1,0,0,0,0,0,-1);
    this.items.forEach((item) => {
      if(item.id == idF)
        itemFrom = item;
      if(item.id == idT)
        itemTo = item;
    })
    if(itemFrom.id == -1)
    {
      this.idFrom = "";
    }
    if(itemTo.id == -1)
      this.idTo = "";
    if(this.idFrom == "" || this.idTo == "")
      return;

    let url = this.transactionURL + 'ExecuteTransaction';
    this.http
      .post<boolean>(url, new Transaction(itemFrom, itemTo))
      .subscribe({
        next: (event) => {
          this.getInvoices();
          this.idFrom = "";
          this.idTo = "";
        },
        error: (err: HttpErrorResponse) => console.log(err),
      });
  }
}
