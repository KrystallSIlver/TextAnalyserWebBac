import { Component, OnInit, Input } from '@angular/core';
import { SemanticModel } from 'src/app/Models/SemanticModel';

@Component({
  selector: 'map-tab',
  templateUrl: './map-tab.component.html',
  styleUrls: ['./map-tab.component.css']
})
export class MapTabComponent implements OnInit {
  @Input() seo: SemanticModel;

  constructor() { }

  ngOnInit() {
  }

}
