import { Component, OnInit } from '@angular/core';
import { LanguageToolService } from '../Services/language-tool.service';
import { SeoService } from '../Services/seo.service';
import { SemanticModel } from '../Models/SemanticModel';
import { Zipf } from '../Models/ZipfModel';

@Component({
  selector: 'text-analyser',
  templateUrl: './text-analyser.component.html',
  styleUrls: ['./text-analyser.component.css'],
  providers: [LanguageToolService]
})
export class TextAnalyserComponent implements OnInit {

  constructor(private langToolSvc:LanguageToolService, private seoSvc:SeoService) { }
  
  text: string = '';
  isOrthography: boolean = true;
  isSeo: boolean = false;
  isRead: boolean = false;
  isZipf: boolean = false;
  isMap: boolean = false;
  seoModel: SemanticModel;
  zipf: Zipf;

  ngOnInit() {
  }

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

  disableAllTabs() {
    this.isOrthography = false
    this.isSeo = false
    this.isRead = false
    this.isZipf = false
    this.isMap = false

  }

  check() {
    //this.langToolSvc.Check(this.text);
    this.seoSvc.Semantic(this.text).subscribe((res:SemanticModel) => {
      this.seoModel = res
    });;
    this.seoSvc.Zipf(this.text).subscribe((res: Zipf) => {
      this.zipf = res;
    })
  }

}
