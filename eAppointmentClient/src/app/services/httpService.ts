import { HttpClient, HttpErrorResponse, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ResultModel } from '../models/result.model';
import { api } from '../constants';
import { AuthService } from './authService';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  
  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) {}

  post<T>(
    apiUrl: string, 
    body: any,
    callBack: (res:ResultModel<T>) => void,
    errorCallBack?: (err: HttpErrorResponse) => void
  ) {

    const token = localStorage.getItem('token');

    let headers = new HttpHeaders()

    if(token) {
      headers = headers.set('Authorization', `Bearer ${token}`);
    }
    
    this.http.post<ResultModel<T>>(`${api}/${apiUrl}`, body, { headers })
      .subscribe({
        next: (res => {

          if(res.data !== undefined || res.data !== null){
            callBack(res)
          }

        }),
        error: ((err:HttpErrorResponse) => {
          
          if(errorCallBack !== undefined) {
            errorCallBack(err)
          }

        })
      })

  }

}
