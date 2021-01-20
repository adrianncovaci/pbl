import { FilterOperators } from './filter-operators';
import { Filter } from './filter';

export interface RequestFilters {
  logicalOperator: FilterOperators;
  filters: Filter[];
  startDate?: Date;
  endDate?: Date;
}
