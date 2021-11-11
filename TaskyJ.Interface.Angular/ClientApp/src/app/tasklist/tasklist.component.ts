import { GeneralService } from '../services/general.service';
import { Router, ParamMap, ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { BaseEntity } from '../models/BaseEntity';
import { AppComponent } from '../app.component';

@Component({
  selector: 'app-tasklist',
  templateUrl: './tasklist.component.html',
  styleUrls: ['./tasklist.component.css']
})
export class TasklistComponent implements OnInit {
  public tasklist: BaseEntity[] = null;
  public resultstatus: string;
  public currenttaskid: any;
  public showdeleted: boolean = false;

  constructor(
    private generalService: GeneralService,
    private appcomponent: AppComponent,
    private route: ActivatedRoute,
    private router: Router) {
    appcomponent.title = 'Task list';
    this.route.queryParams.subscribe((params: ParamMap) => {
      this.tasklist = null;
      let resultstatus = params['resultstatus'];
      if (resultstatus)
        this.resultstatus = resultstatus;
      this.showdeleted = (params['showdeleted'] === true || params['showdeleted'] === 'true');
      this.generalService.GetAllTasks(this.showdeleted).subscribe(tasks => {
        this.tasklist = new Array<BaseEntity>();
        tasks.forEach(task => this.tasklist.push(task));
      });
    });
  }

  ngOnInit(): void {    
  }

  taskchanged(value) {
    this.router.navigate(['/task/' + value.toString()]);
  }

  editcurrenttask(){
    if (this.currenttaskid)
      this.taskchanged(this.currenttaskid);
  }
}
