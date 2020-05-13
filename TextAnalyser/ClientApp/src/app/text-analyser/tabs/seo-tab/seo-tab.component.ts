import { Component, OnInit, Input } from '@angular/core';
import { SemanticModel } from 'src/app/Models/SemanticModel';

@Component({
  selector: 'seo-tab',
  templateUrl: './seo-tab.component.html',
  styleUrls: ['./seo-tab.component.css']
})
export class SeoTabComponent implements OnInit {


  @Input() seo: SemanticModel;
  wordChart: number[];

  constructor() { 

  }
 
  ngOnInit() {
    this.wordChart = [this.seo.wordCount, this.seo.unicWordCount, this.seo.significantWordCount, this.seo.stopWordCount]

  }

}
