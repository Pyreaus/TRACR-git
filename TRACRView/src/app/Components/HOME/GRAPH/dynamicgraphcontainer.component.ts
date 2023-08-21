import { Component, Input, OnInit } from '@angular/core';
import { Skill } from 'src/app/Interfaces/Skill';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styles: []
})
export class CHARTComponent implements OnInit {
  @Input() skillsWithOccurrences!: { skill: Skill; occurrences: number }[];
  
  public barChartLabels: string[] = [];
  public barChartData: any[] = [];
  public barChartLegend = false;
  public barChartType = 'bar';
  
  ngOnInit() {
    this.barChartLabels = this.skillsWithOccurrences.map(entry => entry.skill.skilL_NAME!);
    this.barChartData = [
      {
        data: this.skillsWithOccurrences.map(entry => entry.occurrences),
        label: ' ',
        backgroundColor: this.skillsWithOccurrences.map(entry => {
          const transparency = 0.4; 
          return `rgba(${parseInt(entry.skill.colour!.slice(1, 3), 16)}, 
          ${parseInt(entry.skill.colour!.slice(3, 5), 16)}, 
          ${parseInt(entry.skill.colour!.slice(5, 7), 16)}, 
          ${transparency})`;
        })
      }
    ];
  }
  public barChartOptions = { 
    responsive: true,
    indexAxis: 'y',
    plugins: {  // 'legend' now within object 'plugins {}'
      legend: {
        labels: {
          color: "blue",  // not 'fontColor:' anymore
          // fontSize: 18  // not 'fontSize:' anymore
          font: {
            size: 18 // 'size' now within object 'font {}'
          }
        }
      }
    },
    scales: {
      x: {
        ticks: {
          beginAtZero: true,
          callback(value: string) {
            return Math.abs(parseInt(value, 10));
          },
          color: 'rgb(13, 202, 240)'
        }
      },
      y: {
        ticks: {
          reverse: true
        },
      }
    }
  };
}
