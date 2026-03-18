import { inject } from '@angular/core';
import { CanActivateFn, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../services/auth';

export const authGuard: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  // Check if logged in
  if (!authService.isLoggedIn()) {
    router.navigate(['/login']);
    return false;
  }

  // Check role requirements from route data
  const expectedRoles: string[] = route.data['roles'] || [];

  if (expectedRoles.length > 0) {
    const userRole = authService.getRole();
    if (userRole && expectedRoles.includes(userRole)) {
      return true;
    } else {
      // User doesn't have the required role; redirect to home or unauthorized page
      router.navigate(['/']); 
      return false;
    }
  }

  // If logged in and no specific role required, allow access
  return true;
};
