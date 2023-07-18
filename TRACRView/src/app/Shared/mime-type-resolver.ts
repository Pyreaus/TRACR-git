import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MimeTypeResolver implements Resolve<boolean> {
  constructor(private router: Router) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
  const headers = route.data['headers'];
  if (headers && headers.has('X-Content-Type-Options')&& headers.get('X-Content-Type-Options') === 'nosniff') {
      this.router.navigateByUrl('/404'); // Redirect to NotFOund
      return of(false);
    }
    return of(true);
  }
}
