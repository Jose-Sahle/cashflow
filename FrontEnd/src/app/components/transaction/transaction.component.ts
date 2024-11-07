import { Component, OnInit } from '@angular/core';
import { TransactionService } from './transaction.service';
import { Transaction, TransactionType } from './transaction.model';
import { AbstractControl, NgForm } from '@angular/forms';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.scss']
})

export class TransactionComponent implements OnInit 
{
  transactions: Transaction[] = [];
  selectedTransaction: Transaction | null = null;

  isEditing = false; // Flag para controle das edições
  TransactionType = TransactionType;

  constructor(private transactionService: TransactionService) { }

  // Validador personalizado para o campo Amount
  validateAmount(control: AbstractControl) 
  {
    const value = control.value;
    if (value && (!/^\d+(\.\d{1,2})?$/.test(value) || value <= 0)) 
    {
      return { invalidAmount: true };
    }
    return null;
  }

  formatAmount(event: any) 
  {
    let value = event.target.value;

    // Remove tudo que não é número ou ponto
    value = value.replace(/[^0-9.]/g, '');

    // Limita a duas casas decimais
    const parts = value.split('.');

    if (parts.length > 2) 
    {
      value = parts[0] + '.' + parts.slice(1).join('');
    }

    if (parts[1] && parts[1].length > 2) 
    {
      value = parts[0] + '.' + parts[1].substring(0, 2);
    }

    event.target.value = value;
    this.selectedTransaction!.amount = value; // Atualiza o modelo
  }

  formatDate(date: Date): string 
  {
    const day = String(date.getDate()).padStart(2, '0'); // Adiciona zero à esquerda se necessário
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Os meses começam em 0
    const year = date.getFullYear();

    return `${day}/${month}/${year}`;
  }

  ngOnInit(): void 
  {
    this.initTransaction();
    this.loadTransactions();
  }

  loadTransactions() 
  {
    this.transactionService.getAll().subscribe
      (
        data => 
        {
          this.transactions = data;
        },
        error => 
        {
          console.error('Error retrieving transactions', error);
        }
      );
  }

  addTransaction(transaction: Transaction) 
  {
    this.transactionService.add(transaction).subscribe
      (
        () => this.loadTransactions(),
        error => console.error('Error adding transaction', error)
      );
  }

  selectTransaction(transaction: Transaction) 
  {
    this.selectedTransaction = { ...transaction };
    this.isEditing = true; // Habilita edição ao selecionar transação
  }

  cancelSelectedTransaction() 
  {
    this.initTransaction();
    this.isEditing = false; // Desabilita edição ao limpar seleção
  }

  initTransaction() 
  {
    this.selectedTransaction =
    {
      amount: 0,
      type: TransactionType.None,
      description: '',
      date: this.formatDate(new Date())
    };
  }

  newTransaction() 
  {
    this.initTransaction();
    this.isEditing = true;
  }

  onSubmit(form: NgForm) 
  {
    if (this.validateForm(form)) 
    {
      this.addTransaction(this.selectedTransaction!);
      this.cancelSelectedTransaction();
    }
  }

  validateForm(form: NgForm): boolean 
  {
    let isValid = true;

    // Validação do campo Amount
    if (this.selectedTransaction!.amount <= 0) 
    {
      form.controls['amount'].setErrors({ invalidAmount: true });
      isValid = false;
    }

    // Validação do campo Type
    if (this.selectedTransaction!.type !== this.TransactionType.Credit && this.selectedTransaction!.type !== this.TransactionType.Debit) 
    {
      form.controls['type'].setErrors({ invalidType: true });
      isValid = false;
    }

    // Validação do campo Date
    if (!this.selectedTransaction!.date) 
    {
      form.controls['date'].setErrors({ required: true });
      isValid = false;
    }

    return isValid;
  }

  getTransactionType(type: number): string
  {
    return type === 0 ? 'Credit' : 'Debit';
  }

}
