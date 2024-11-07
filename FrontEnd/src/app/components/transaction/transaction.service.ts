import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Transaction, TransactionType } from './transaction.model';
import { ConfigService } from '../../config.service';

@Injectable({
  providedIn: 'root'
})

export class TransactionService
{
  private baseUrl: string;

  constructor(private http: HttpClient, private configService: ConfigService)
  {
    this.baseUrl = this.configService.apiUrl; 
  }

  getAll(): Observable<Transaction[]>
  {
    var addURL = this.baseUrl + "/GetBankTransactions";
    return this.http.get<Transaction[]>(addURL);
  }

  add(transaction: Transaction): Observable<Transaction>
  {
    var addURL = this.baseUrl + "/AddBankTransaction";
    return this.http.post<Transaction>(addURL, transaction);
  }
}
