import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    constructor(
        private authService: AuthService,
        private alertify: AlertifyService,
        private router: Router
    ) {}

  canActivate(router: ActivatedRouteSnapshot, state: RouterStateSnapshot):
    Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      const roles = router.firstChild.data['roles'] as Array<string>;
      if(roles) {
        const access = this.authService.roleMatch(roles);
        if (access) {
          return true;
        }
        else {
          this.router.navigate(['home']);
          this.alertify.error("You can not access this area");
        }
      }
      if (this.authService.loggedIn) {
          return true;
      }

      this.router.navigate(['login'], { queryParams: { returnUrl: state.url } });
  }
}
