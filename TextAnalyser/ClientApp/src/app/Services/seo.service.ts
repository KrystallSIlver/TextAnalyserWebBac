import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { SemanticModel } from '../Models/SemanticModel';

@Injectable({
  providedIn: 'root'
})
export class SeoService {
  baseApiUrl: string = '/Semantic/';
  constructor(private http: HttpClient) { }

  Semantic(text: string) {
    const params = new HttpParams().set('textForAnalysis', text);
    return this.http.get(this.baseApiUrl+"Semantic", {params});    
  }
  
  Zipf(text: string) {
    const params = new HttpParams().set('textForAnalysis', text);
    return this.http.get(this.baseApiUrl+"Zipf", {params});
  }
}
