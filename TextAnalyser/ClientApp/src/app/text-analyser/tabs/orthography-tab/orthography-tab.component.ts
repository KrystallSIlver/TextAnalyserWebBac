import { Component, OnInit, Input } from '@angular/core';
import { SemanticModel } from 'src/app/Models/SemanticModel';

@Component({
  selector: 'orthography-tab',
  templateUrl: './orthography-tab.component.html',
  styleUrls: ['./orthography-tab.component.css']
})
export class OrthographyTabComponent implements OnInit {
  @Input() seo: SemanticModel;

  constructor() { }

  ngOnInit() {
  }

}
