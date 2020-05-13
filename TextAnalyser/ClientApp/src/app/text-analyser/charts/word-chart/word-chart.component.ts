import { Component, OnInit, Input } from '@angular/core';
import * as Highcharts from 'highcharts';
import { SemanticModel } from 'src/app/Models/SemanticModel';

@Component({
  selector: 'word-chart',
  templateUrl: './word-chart.component.html',
  styleUrls: ['./word-chart.component.css']
})
export class WordChartComponent {

   _series: number[]
   chart: any

   @Input() set series(series: SemanticModel) {
      this._series = [series.wordCount, series.unicWordCount, series.significantWordCount, series.stopWordCount]
      if(this.chartOptions && this.chartOptions.series) {
         this.chartOptions.series[0].data = this._series;
      }
      this.drawChart()
  }

  chartOptions: any = {   
   title: {
      text: null
   },
   chart: {
       type: 'bar',
       backgroundColor: 'rgba(1,0,0,0)'
   },
   xAxis:{
      categories: ['Кількість слів', 'Кількість унікальних слів', 'Кількість значимих слів', 'Кількість стоп-слів'], 
      title: {
         text: null
      } 
    },
    yAxis : {
       min: 0
    },
    plotOptions : {
       bar: {
          dataLabels: {
             enabled: true
          }
       }
    },
    series: [
       {
          name: "Слова",
          data: this._series
       }
    ]
   };

  drawChart() {
   this.chart = new Highcharts.Chart('word-bar',this.chartOptions)
  }
}