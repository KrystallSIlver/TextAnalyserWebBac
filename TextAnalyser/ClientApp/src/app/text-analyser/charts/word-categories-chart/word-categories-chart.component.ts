import { Component, OnInit, Input } from '@angular/core';
import { SemanticModel } from 'src/app/Models/SemanticModel';
import { Chart } from 'highcharts';

@Component({
  selector: 'word-categories-chart',
  templateUrl: './word-categories-chart.component.html',
  styleUrls: ['./word-categories-chart.component.css']
})
export class WordCategoriesChartComponent {

  data: any
  @Input() set seo(seo: SemanticModel) {
    this.data = [
      {
         name: "Кількість унікальних слів",
         y: seo.unicWordCount,
         color: "#009900"
      },
      {
         name: "Кількість значимих слів",
         y: seo.significantWordCount,
         color: "#ffd633"
      },
      {
         name: "Кількість стоп-слів",
         y: seo.stopWordCount,
         color: "#ff3333"
      }
    ]
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
        pointFormat: '{series.name}: <b>{point.percentage:.0f}%</b>'
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
    new Chart('word-category', chart);
  }

}
