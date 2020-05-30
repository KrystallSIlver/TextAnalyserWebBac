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
  //Медод що відповідає за відображення графіків
  drawCharts() {
    //Налаштування для графіку цифрового відношення
    this.wordChartOptions = {   
      chart: {
        //Тип та фон графіку
         type: "spline",
         backgroundColor: 'rgba(1,0,0,0)'
      },
      title: {
        //Назва графіку
         text: "Кількісне відношення за законом Ципфа"
      },
      xAxis:{
        //Підписи по вісі Х
         categories: this._zipf.map(x=>x.phrase)
      },
      yAxis: {
        //Мінімальне значення по вісі У   
        min: 0,
        //Максимальне значення по вісі У
        max: Math.max(...this._zipf.map(x=>x.count)),   
        startOnTick: false,
        //Підпис для вісі У 
        title:{
            text:"Кількість повторів"
        },
      },
      //Налаштування відображення підказки
      tooltip: {
        formatter: function () {
          return "Слово " + this.points.reduce(function (s, point) {
              return s + '<br/>' + point.series.name + ': ' +
                  point.y;
          }, '<b>' + this.x + '</b>');
        },
        shared: true
      },
      //Дані для відображення
      series: [{
         name: 'Повторів у тексті',
         data: this._zipf.slice(0,50).map(x=>x.count),
         color: "#e6b82e"
      },{
        name: 'Рекомендоване за Ципфом',
        data: this._zipf.slice(0,50).map(x=>x.idealCount),
        color: "#27b91d"
      }]
   };

   //Налаштування для графіку процентного відношення
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
       data: this._zipf.slice(0,50).map(x=>x.currentPerc),
       color: "#e6b82e"
    },{
      name: 'Рекомендоване за Ципфом',
      data: this._zipf.slice(0,50).map(x=>x.idealPerc),
      color: "#27b91d"
      }]
    };

    this.wordChart = new Highcharts.Chart('word', this.wordChartOptions);
    this.percChart = new Highcharts.Chart('perc', this.percChartOptions);

  }

}
