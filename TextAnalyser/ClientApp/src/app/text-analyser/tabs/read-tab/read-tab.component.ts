import { Component, OnInit, Input } from '@angular/core';
import { Readability } from 'src/app/Models/Readability';

@Component({
  selector: 'read-tab',
  templateUrl: './read-tab.component.html',
  styleUrls: ['./read-tab.component.css']
})
export class ReadTabComponent implements OnInit {
  @Input() readability: Readability;

  constructor() { }

  ngOnInit() {
  }

}
