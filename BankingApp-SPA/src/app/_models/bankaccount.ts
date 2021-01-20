export interface Bankaccount {
  id: number;
  accountNumber: string;
  balance: number;
  maintenanceFee: number;
  dateCreated: Date;
  bankAccountStatus: string;
  accountTypeId: number;
  interestRate?: number;
  monthlyPayment?: number;
  period?: number;
}
