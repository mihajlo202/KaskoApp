import { Invoice } from "./Invoice";

export class Transaction {
   itemFrom: Invoice;
   itemTo: Invoice;

    constructor(itemFrom: Invoice, itemTo: Invoice) {
        this.itemFrom = itemFrom;
        this.itemTo = itemTo;
    }
}