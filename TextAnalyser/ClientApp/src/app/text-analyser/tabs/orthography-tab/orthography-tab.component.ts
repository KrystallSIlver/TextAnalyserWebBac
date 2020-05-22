import { Component, OnInit, Input } from '@angular/core';
import { Error } from 'src/app/Models/Error';

@Component({
  selector: 'orthography-tab',
  templateUrl: './orthography-tab.component.html',
  styleUrls: ['./orthography-tab.component.css']
})
export class OrthographyTabComponent {
  @Input() errors: Error[];

  constructor() { }
  
}
