import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { LanguageToolResponseModel } from 'src/app/Models/LanguageToolResponseModel'

@Injectable({
  providedIn: 'root'
})
export class LanguageToolService {
  baseApiUrl: string;
  constructor(private http: HttpClient) { 
    this.baseApiUrl = 'https://languagetool.org/api/v2/';
  }


  Check(text: string): LanguageToolResponseModel {
    var data: LanguageToolResponseModel;
    var languageCode = 'uk';
    this.http.post(this.baseApiUrl+'check',{ text: text, language: languageCode }, { headers: { 'access-control-allow-origin':'*', 'mode': 'no-cors'} }).subscribe((res:LanguageToolResponseModel) => data = res)
    return data;
  }

}
