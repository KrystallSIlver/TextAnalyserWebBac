import { Component, OnInit, Input } from '@angular/core';
import { SemanticModel } from 'src/app/Models/SemanticModel';
import { Chart } from 'highcharts';

@Component({
  selector: 'sign-word',
  templateUrl: './sign-word.component.html',
  styleUrls: ['./sign-word.component.css']
})
export class SignWordComponent {
  data: any
  @Input() set seo(seo: SemanticModel) {
    this.data = [
      {
         name: "Кількість значимих слів",
         y: seo.significantWordCount,
         color: "yellow"
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
    new Chart('sign', chart);
  }
}
