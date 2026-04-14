import { inject } from '@angular/core';
import { Router, CanActivateFn } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const roleGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const authService = inject(AuthService);

  const currentUser = authService.currentUserValue;
  if (!currentUser) {
    router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
    return false;
  }

  const expectedRoles = route.data['roles'] as Array<string>;
  if (expectedRoles && expectedRoles.length > 0) {
    if (!authService.hasRole(expectedRoles)) {
      // Role not authorized! Redirect to their specific main page to prevent infinite loops
      const currentUserRole = authService.getRole();
      if (currentUserRole === 'OrderAdmin') {
        router.navigate(['/orders']);
      } else if (currentUserRole === 'CustomerAdmin') {
        router.navigate(['/customers']);
      } else if (currentUserRole === 'ProductAdmin' || currentUserRole === 'SuperAdmin') {
        router.navigate(['/products']);

        router.navigate(['/login']);
      }
      return false;
    }
  }

  return true;
};
