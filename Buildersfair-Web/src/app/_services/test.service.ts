import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TestService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  rekognitionTest(body): Observable<any> {
    return this.http.post(this.baseUrl + 'test/rekognition', body);
  }

  textractTest(body): Observable<any> {
    return this.http.post(this.baseUrl + 'test/textract', body);
  }
}
