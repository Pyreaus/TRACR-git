import { Component, Input, OnInit } from '@angular/core';
import { Skill } from 'src/app/Interfaces/Skill';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
})
export class CHARTComponent implements OnInit {
  @Input() skillArray!: Skill[]; 
  @Input() occurences!: number[]; 

  public barChartOptions = { scaleShowVerticalLines: false,  responsive: true };
  public barChartLegend = true;
  public barChartType = 'bar';

  public barChartLabels: string[] = [];
  public barChartData: any[] = [];

  ngOnInit() {
    this.barChartLabels = this.skillArray.map(skill => skill.skilL_NAME!);
    this.barChartData = [
      {
        data: this.occurences,
        label: 'Skills Used',
        backgroundColor: this.skillArray.map(skill => skill.colour)
      }
    ];
  }
}


// import { Component, Input } from '@angular/core';

// @Component({
//   selector: 'app-chart',
//   templateUrl: './chart.component.html',
// })
// export class CHARTComponent {
//   public barChartOptions = {
//     scaleShowVerticalLines: false,
//     responsive: true
//   };

//   public barChartLabels = ['Label 1', 'Label 2', 'Label 3'];
//   public barChartType = 'bar';
//   public barChartLegend = true;

//   public barChartData = [
//     { data: [65, 59, 80], label: 'Series A' },
//     { data: [28, 48, 40], label: 'Series B' }
//   ];

// }