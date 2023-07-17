import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'search',
})
export class SearchPipe implements PipeTransform {
  transform(properties: string[], searchInput: string): any[] {
    if (!searchInput) {
      return [];
    }
    searchInput = searchInput.toLowerCase();
    return properties.filter((p) => p.toLowerCase().includes(searchInput));
  }
}
