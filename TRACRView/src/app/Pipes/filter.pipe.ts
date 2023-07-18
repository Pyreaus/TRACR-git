import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'filter'
  })
  export class FilterPipe implements PipeTransform {
    transform(items: any[] | null, idFilter: string, nameFilter: string): any[] {
      if (!items) return [];
      let filteredItems = [...items];
      if (idFilter) {
        filteredItems = filteredItems.filter(item => item.id.toLowerCase().includes(idFilter.toLowerCase()));
      }
      if (nameFilter) {
        filteredItems = filteredItems.filter(item => item.name.toLowerCase().includes(nameFilter.toLowerCase()));
      }
      return filteredItems;
    }
  }