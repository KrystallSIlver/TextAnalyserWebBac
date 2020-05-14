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
    return this.http.post(this.baseApiUrl+"Semantic", {textForAnalysis: text});    
  }
  
  Zipf(text: string) {
    return this.http.post(this.baseApiUrl+"Zipf", {textForAnalysis: text});
  }
}
