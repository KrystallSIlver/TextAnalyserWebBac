import { Injectable } from "@angular/core";
import { Error } from "../Models/Error";
import { LanguageToolResponseModel } from "../Models/LanguageToolResponseModel";

@Injectable({
    providedIn: 'root'
  })
  export class SpellHelperService {
     //Метод для отримання списку орфографічних помилок та варіантів їх виправлення
     getErrors(data: LanguageToolResponseModel):Error[] {
        //Перевірка на пустий об'єкт
        if(!data) return [];
        var result = data.matches.map(x=> {
            var e:Error = new Error();
            //Виділення слова з тексту
            e.word = x.context.text.substr(x.context.offset, x.context.length);
            //Отримання масиву варіантів виправлення
            e.replacements = x.replacements.map(x=>x.value).join(', ');
            return e;
        });
        return result.filter((item, index) => { return result.map(x=>x.word).indexOf(item.word) == index })
     }
  }