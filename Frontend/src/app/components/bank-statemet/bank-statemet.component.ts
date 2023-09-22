import {
  HttpClient,
  HttpEventType,
  HttpErrorResponse,
} from '@angular/common/http';
import { Component } from '@angular/core';
import { environmentVariables } from 'src/app/constants/url-constants';
import { BankStatement } from 'src/app/models/BankStatement';

@Component({
  selector: 'app-bank-statemet',
  templateUrl: './bank-statemet.component.html',
  styleUrls: ['./bank-statemet.component.css'],
})
export class BankStatemetComponent {
  progress: number = 0;
  message: string = '';
  bankStatements: BankStatement[] = [];
  private accountingURL = environmentVariables.ACCOUNTING;
  private importURL = environmentVariables.IMPORT_BS;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getBankStatements();
  }

  getBankStatements() {
    this.bankStatements = [];
    let url = this.accountingURL + 'GetBankStatements';
    return this.http.get<any>(url).subscribe({
      next: (event) => {
        this.bankStatements = event;
      },
      error: (err: HttpErrorResponse) => console.log(err),
    });
  }

  uploadFile = (files: any) => {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.uploadBankStatementFile(formData);
    this.getBankStatements();
  };

  uploadBankStatementFile(formData: FormData) {
    let url = this.importURL + 'ImportBankStatement';
    this.http
      .post<boolean>(url, formData, { reportProgress: true, observe: 'events' })
      .subscribe({
        next: (event) => {
          if (event.type === HttpEventType.UploadProgress)
            this.progress = Math.round(
              (100 * event.loaded) / (event.total ? event.total : 1)
            );
          else if (event.type === HttpEventType.Response) {
            this.message = 'Upload success.';
          }
          this.getBankStatements();
        },
        error: (err: HttpErrorResponse) => console.log(err),
      });
  }

  applyBankStatement(bs:BankStatement) {
    let url = this.accountingURL + 'ApplyBankStatementItem';
    this.http
      .post<boolean>(url, bs)
      .subscribe({
        next: (event) => {
          this.getBankStatements();
        },
        error: (err: HttpErrorResponse) => console.log(err),
      });
  }
}
