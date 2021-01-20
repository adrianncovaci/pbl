export interface LoanRequest {
  id: number;
  loanId: number;
  loanName: string;
  dateIssued: Date;
  status: string;
  comments: string;
  customerId: number;
  customerName: string;
}
