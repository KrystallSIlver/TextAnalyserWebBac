import { Component, Input } from '@angular/core';
import { SemanticModel } from 'src/app/Models/SemanticModel';
import { Chart } from 'highcharts';

@Component({
  selector: 'unic-word',
  templateUrl: './unic-word.component.html',
  styleUrls: ['./unic-word.component.css']
})
export class UnicWordComponent {
  data: any
  @Input() set seo(seo: SemanticModel) {
    this.data = [
      {
         name: "Кількість унікальних слів",
         y: seo.unicWordCount,
         color: "green"
      },{
         name: "Кількість слів",
         y: seo.wordCount,
      }]
    this.draw();
  }

  draw() {
    var chart: any = {
      chart : {
        plotBorderWidth: null,
        plotShadow: false,
        backgroundColor: 'rgba(1,0,0,0)'
     },
     title : {
        text: null
     },
     tooltip : {
        pointFormat: '{series.name}: <b>{point.y}</b>'
     },
     plotOptions : {
        pie: {
           allowPointSelect: true,
           cursor: 'pointer',
     
           dataLabels: {
              enabled: false           
           },     
           showInLegend: true
        }
     },
     series : [{
        type: 'pie',
        data: this.data
     }]
    }
    new Chart('unic', chart);
  }}
