import { Component, OnInit } from '@angular/core';
import { LanguageToolService } from '../Services/language-tool.service';

@Component({
  selector: 'text-analyser',
  templateUrl: './text-analyser.component.html',
  styleUrls: ['./text-analyser.component.css'],
  providers: [LanguageToolService]
})
export class TextAnalyserComponent implements OnInit {

  constructor(private langToolSvc:LanguageToolService) { }
  
  text: string = '';
  isOrthography: boolean = true;
  isSeo: boolean = false;
  isRead: boolean = false;
  isMap: boolean = false;

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
        this.isMap = true
        break;
    }
  }

  disableAllTabs() {
    this.isOrthography = false
    this.isSeo = false
    this.isRead = false
    this.isMap = false

  }

  check() {
    this.langToolSvc.Check(this.text);
  }

}
