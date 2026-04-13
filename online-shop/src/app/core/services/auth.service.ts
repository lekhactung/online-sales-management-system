import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface AuthResponse {
  Token: string;
  Username: string;
  FullName: string;
  Role: string; // SuperAdmin, ProductAdmin, OrderAdmin, CustomerAdmin
}

export interface LoginRequest {
  Username: string;
  Password: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = environment.apiUrl + '/Auth';
  private currentUserSubject = new BehaviorSubject<AuthResponse | null>(JSON.parse(localStorage.getItem('currentUser') || 'null'));
  public currentUser$ = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient) { }

  public get currentUserValue(): AuthResponse | null {
    return this.currentUserSubject.value;
  }

  public getRole(): string {
    return this.currentUserValue?.Role || '';
  }

  public isAuthenticated(): boolean {
    return !!this.currentUserValue && !!this.currentUserValue.Token;
  }

  public hasRole(role: string | string[]): boolean {
    const currentRole = this.getRole();
    if (!currentRole) return false;
    
    if (typeof role === 'string') {
      return currentRole === role;
    }
    
    return role.includes(currentRole);
  }

  login(credentials: LoginRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, credentials).pipe(
      tap(response => {
        localStorage.setItem('currentUser', JSON.stringify(response));
        this.currentUserSubject.next(response);
      })
    );
  }

  logout() {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }
}
