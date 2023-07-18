import { Pipe, PipeTransform } from '@angular/core';
import { env } from 'src/environments/environment';

@Pipe({
    name: 'apiUrl'
})
export class ApiUrlPipe implements PipeTransform {
    transform(path: string): string{
        return `${env.ServerRoot}/${path}`;
    }
}