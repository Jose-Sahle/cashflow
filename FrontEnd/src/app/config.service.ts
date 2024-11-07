import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})

export class ConfigService
{
  private config: any;
  constructor(private http: HttpClient) { }

  loadConfig(): Observable<any>
  {
    return this.http.get('/assets/appconfig.json').pipe(
      map(config =>
      {
        this.config = config;
        return config;
      })
    );
  }

  get apiUrl(): string
  {
    return this.config?.apiUrl;
  }
}
