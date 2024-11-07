export enum TransactionType
{
  None = -1,
  Credit = 0,
  Debit = 1
}

export interface Transaction
{
  amount: number;
  type: TransactionType;
  description: string;
  date: string; // Usar string ou Date dependendo de como você quer trabalhar com datas
}
