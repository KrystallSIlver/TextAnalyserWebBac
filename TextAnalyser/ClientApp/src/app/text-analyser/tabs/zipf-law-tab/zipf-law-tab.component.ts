import { Component, OnInit, Input } from '@angular/core';
import { Zipf } from 'src/app/Models/ZipfModel';
import * as Highcharts from 'highcharts';


@Component({
  selector: 'zipf-law-tab',
  templateUrl: './zipf-law-tab.component.html',
  styleUrls: ['./zipf-law-tab.component.css']
})
export class ZipfLawTabComponent implements OnInit {
  _zipf: Zipf[];
  percChart: any;
  wordChart: any;
  wordChartOptions: any;
  percChartOptions: any;
  @Input() set zipf (zipf: Zipf[]) {
    this._zipf = zipf;
    this.drawCharts();
  };

  constructor() { }

  ngOnInit() {
  }

  drawCharts() {
    this.wordChartOptions = {   
      chart: {
         type: "spline",
         backgroundColor: 'rgba(1,0,0,0)'
      },
      title: {
         text: "Кількісне відношення за законом Ципфа"
      },
      xAxis:{
         categories: this._zipf.map(x=>x.phrase)
      },
      yAxis: {   
        min: 0,
        max: Math.max(...this._zipf.map(x=>x.count)),   
        startOnTick: false,
        title:{
            text:"Кількість повторів"
        } 
      },
      tooltip: {
        formatter: function () {
          return "Слово " + this.points.reduce(function (s, point) {
              return s + '<br/>' + point.series.name + ': ' +
                  point.y;
          }, '<b>' + this.x + '</b>');
        },
        shared: true
      },
      series: [{
         name: 'Повторів у тексті',
         data: this._zipf.map(x=>x.count),
         color: "#e6b82e"
      },{
        name: 'Рекомендоване за Ципфом',
        data: this._zipf.map(x=>x.idealCount),
        color: "#27b91d"
      }]
   };

   this.percChartOptions = {   
    chart: {
       type: "spline",
       backgroundColor: 'rgba(1,0,0,0)'
    },
    title: {
       text: "Відсоткове відношення за законом Ципфа"
    },
    xAxis:{
      categories: this._zipf.map(x=>x.rank)
    },
    yAxis: {   
      min: 0,
      //max: 0.5,   
      startOnTick: false
    },
    tooltip: {
      formatter: function () {
        return "Ранг " + this.points.reduce(function (s, point) {
            return s + '<br/>' + point.series.name + ': ' +
                point.y.toFixed(3);
        }, '<b>' + this.x + '</b>');
      },
      shared: true
    },
    series: [{
       name: 'Повторів у тексті',
       data: this._zipf.map(x=>x.currentPerc),
       color: "#e6b82e"
    },{
      name: 'Рекомендоване за Ципфом',
      data: this._zipf.map(x=>x.idealPerc),
      color: "#27b91d"
      }]
    };

    this.wordChart = new Highcharts.Chart('word', this.wordChartOptions);
    this.percChart = new Highcharts.Chart('perc', this.percChartOptions);

  }

}
