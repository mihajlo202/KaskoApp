export class BankStatement {
    id:number;
    fileName: string;
    content: string;
    error: string;
    applied: boolean;
    createdOn: Date;

    constructor(id: number,name: string, content: string, error: string, applied: boolean, createdOn: Date) {
        this.id = id;
        this.fileName = name;
        this.content = content;
        this.error = error;
        this.applied = applied
        this.createdOn = createdOn;
    }
}