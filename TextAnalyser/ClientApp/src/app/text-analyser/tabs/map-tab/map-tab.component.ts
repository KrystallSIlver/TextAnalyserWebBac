import { Component, Input, ViewEncapsulation } from '@angular/core';
import { WordForms } from 'src/app/Models/WordForms';

@Component({
  selector: 'map-tab',
  templateUrl: './map-tab.component.html',
  styleUrls: ['./map-tab.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class MapTabComponent  {
  @Input() map: WordForms[];
  @Input() text: string;

  getMapTemplate() {
    var text = this.text
    this.map.forEach(e => {
      e.words.forEach(w => {
        text = text.replace(new RegExp('( )('+w+')( )','g'), '$1<span class="'+this.getCSSClass(this.map.indexOf(e))+'">$2</span>$3')
      });
    });
    return text
  }

  getCSSClass(index: number) {
    switch(index) {
      case 0:
        return "s1"
      case 1:
        return "s2"
      case 2:
        return "s3"
      case 3:
        return "s4"
      case 4:
        return "s5"
      default:
        return "s6"
    }
  }

}
