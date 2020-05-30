import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

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

  Check(text: string) {
    var languageCode = 'uk';
    return this.http.post(this.baseApiUrl+'Orthography',{ text: text, language: languageCode })
  }

  Map(text: string) {
    return this.http.post(this.baseApiUrl+'Map',{ textForAnalysis: text})
  }
}
