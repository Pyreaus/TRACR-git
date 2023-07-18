import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'fontSize'
})
export class FontSizePipe implements PipeTransform {
  transform(value: string | undefined, size: string): any {
    return value ? { 'font-size': size, 'display': 'inline' } : null;
  }
}
