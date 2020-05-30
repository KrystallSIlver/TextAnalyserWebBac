import { Component, OnInit } from '@angular/core';
import { LanguageToolService } from '../Services/language-tool.service';
import { SeoService } from '../Services/seo.service';
import { SemanticModel } from '../Models/SemanticModel';
import { Zipf } from '../Models/ZipfModel';
import { LanguageToolResponseModel } from '../Models/LanguageToolResponseModel';
import { SpellHelperService } from '../helpers/SpellHelper';
import { Error } from '../Models/Error';
import { ExtendedSemanticCore } from '../Models/ExtendedSemanticCore';
import { NgxSpinnerService } from 'ngx-spinner';


@Component({
  selector: 'text-analyser',
  templateUrl: './text-analyser.component.html',
  styleUrls: ['./text-analyser.component.css'],
  providers: [LanguageToolService, SpellHelperService]
})
export class TextAnalyserComponent implements OnInit {

  constructor(private spell:SpellHelperService, private seoSvc:SeoService, private spinner: NgxSpinnerService) { }
  
  text: string = '';
  errorPercentage: number;
  isOrthography: boolean = true;
  isSeo: boolean = false;
  isRead: boolean = false;
  isZipf: boolean = false;
  isMap: boolean = false;
  showLangError: boolean = false;
  seoModel: SemanticModel;
  zipf: Zipf;
  errors: Error[];
  map: ExtendedSemanticCore[]

  ngOnInit() {
  }
  //Метод для переключання вкладок
  makeActiveTab(id: number) {
    this.disableAllTabs();
    switch(id)
    {
      case 1:
        this.isOrthography = true
        break;
      case 2:
        this.isSeo = true
        break;
      case 3:
        this.isRead = true
        break;
      case 4:
        this.isZipf = true
        break;
      case 5:
        this.isMap = true
        break;
    }
  }
  //Відключення усіх вкладок
  disableAllTabs() {
    this.isOrthography = false
    this.isSeo = false
    this.isRead = false
    this.isZipf = false
    this.isMap = false
    this.showLangError = false;
  }
  //Метод для відображення помилки пов'язаної з мовою введеного тексту
  showLanguageError() {
    this.disableAllTabs();
    this.showLangError = true;
    this.isOrthography = true;
    this.spinner.hide();
  }
  //Очищення об'єктів при повторному аналізі
  restoreTabs() {
    this.seoModel = null;
    this.zipf = null;
    this.errors = null;
    this.map = null;
  }
  //Виконується при натисканні на кнопку перевірити. Слугує для відправки запитів на сервер за допомогою SeoService
  check() {
    this.restoreTabs();
    //Відображення анімації загрузки
    this.spinner.show();
    //Відправка запиту на орфографічний аналіз
    this.seoSvc.Check(this.text).subscribe((res:LanguageToolResponseModel) => {
      //Перевірка на введену мову
      if(res && res.language.detectedLanguage.name && res.language.detectedLanguage.name != 'Ukrainian')
      {
        //Відображення помилки
        this.showLanguageError();
        return;
      }
      //Отримання списку помилок для відображення на вкладці результатів орфографічного аналізу
      this.errors = this.spell.getErrors(res)
      //Відправка запиту на семантичний аналіз
      this.seoSvc.Semantic(this.text).subscribe((res:SemanticModel) => {
        this.seoModel = res
        //Визначення відсотку офографічних помилок
        this.errorPercentage = (this.errors.length / res.words.length) * 100
        //Приховати анімацію загрузки
        this.spinner.hide();
      });;
      //Відправка запиту на аналіз методом Ципфа
      this.seoSvc.Zipf(this.text).subscribe((res: Zipf) => {
        this.zipf = res;
      });
      //Відправка запиту на отримання даних для побудови карти тексту
      this.seoSvc.Map(this.text).subscribe((res: ExtendedSemanticCore[]) => this.map = res);  
      this.showLangError = false;
    });
  }

}
