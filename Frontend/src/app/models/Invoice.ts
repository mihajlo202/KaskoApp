export class Invoice {
    id:number;
    type: number;
    status: number;
    statusText: string = ""
    typeText: string = "";
    amount: number;
    openAmount: number;
    closedAmount: number;
    contractId: number;

    constructor(id: number, type: number,status: number,amount: number, openAmount: number, closedAmount: number, contractId: number) {
        this.id = id;
        this.type = type;
        this.status = status;
        this.amount = amount;
        this.openAmount = openAmount;
        this.closedAmount = closedAmount;
        this.contractId = contractId;
    }
}