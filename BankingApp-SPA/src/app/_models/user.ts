export interface User {
  id: number;
  userId: number;
  firstName: string;
  lastName: string;
  email: string;
  cnp: string;
  dateJoined: Date;
  city: string;
  address: string;
  zipCode: string;
  creditScore?: number;
}
