import { Component, OnInit, Input } from '@angular/core';
import { SemanticModel } from 'src/app/Models/SemanticModel';
import { Chart } from 'highcharts';

@Component({
  selector: 'water-chart',
  templateUrl: './water-chart.component.html',
  styleUrls: ['./water-chart.component.css']
})
export class WaterChartComponent {
  data: any
  @Input() set seo(seo: SemanticModel) {
    this.data = [
      {
         name: "Кількість води у тексті",
         y: seo.waterPercentage,
         color: "#1a75ff"
      },{
         name: "Інший текст",
         y: 100-seo.waterPercentage,
         color: "#909090"
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
    new Chart('water', chart);
  }
}
