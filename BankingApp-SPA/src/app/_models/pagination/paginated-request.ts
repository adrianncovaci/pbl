import { RequestFilters } from './request-filters';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

export class PaginatedRequest {
  pageIndex: number;
  pageSize: number;
  requestFilters: RequestFilters[];
  columnNameForSorting?: string;
  sortDirection?: string;

  constructor(
    paginator: MatPaginator,
    filters: RequestFilters[],
    sort?: MatSort
  ) {
    this.pageIndex = paginator.pageIndex;
    this.pageSize = paginator.pageSize;
    this.requestFilters = filters;
    this.columnNameForSorting = sort.active;
    this.sortDirection = sort.direction;
  }
}
