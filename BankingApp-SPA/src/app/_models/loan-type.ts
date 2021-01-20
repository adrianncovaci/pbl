export interface LoanType {
    id: number;
    type: string;
    interestRate: number;
    period: number;
    fixedRate: boolean;
}
