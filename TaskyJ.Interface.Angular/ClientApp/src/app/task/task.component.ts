import { Router, ParamMap, ActivatedRoute, UrlSegment } from '@angular/router';
import { Component, OnInit, ChangeDetectorRef, AfterViewInit  } from '@angular/core';
import { DBTaskJ } from '../models/DBTaskJ';
import { DBCategoryJ } from '../models/DBCategoryJ';
import { GeneralService } from '../services/general.service';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms'
import { ReactiveFormsModule }    from '@angular/forms';
import { AppComponent } from '../app.component';
import { TaskPriority } from '../models/TaskPriority';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})
export class TaskComponent implements OnInit, AfterViewInit {
  public currenttaskid: number = null;
  public currenttask: DBTaskJ = new DBTaskJ();
  public loaded = false;
  public prioritieslist: Array<Object>;
  public categorieslist: Array<DBCategoryJ>;
  form: FormGroup = null;


  ngAfterViewInit() {

  }
  
  constructor(
    private cdr: ChangeDetectorRef,
    private generalService: GeneralService,
    private fctrl: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
    private appcomponent: AppComponent) {
    appcomponent.title = 'Task detail';
      this.prioritieslist = new Array<Object>();
      for (let item in TaskPriority) {
        if (isNaN(Number(item))) {
          var prio = new Object({
            id: Number(TaskPriority[item]),
            name: item.toString()
          });
          this.prioritieslist.push(prio);
        }
      }
  }

  ngOnInit(): void {
    //current task
    this.route.paramMap.subscribe((params: ParamMap) => {
      let id = params.get('id');
      if (id === 'new') {
        this.currenttask = new DBTaskJ();
        this.currenttask.id = 0;
        this.currenttask.idUser = this.authenticationService.currentUserValue.id;
        this.currenttask.creationDate = new Date();
        this.loaded = true;
        this.form = this.fctrl.group({
              id: this.currenttask.id,
              name: [this.currenttask.name, Validators.required],
              description: [this.currenttask.description, Validators.required],
              creationDate: this.currenttask.creationDate,
              deadline: this.currenttask.deadline,
              finishDate: this.currenttask.finishDate,
              priority: this.currenttask.priority,
              deleted: this.currenttask.deleted,
              idCategory: this.currenttask.idCategory,
              idUser: this.currenttask.idUser,
            });
      }
      else {
        this.currenttaskid = +id;    
        if (this.currenttaskid) {
          this.generalService.GetTask(this.currenttaskid).subscribe(task => {
            this.loaded = true;
            this.currenttask = <DBTaskJ>task;
            this.form = this.fctrl.group({
              id: this.currenttask.id,
              name: [this.currenttask.name, Validators.required],
              description: [this.currenttask.description, Validators.required],
              creationDate: this.currenttask.creationDate,
              deadline: this.currenttask.deadline,
              finishDate: this.currenttask.finishDate,
              priority: this.currenttask.priority,
              deleted: this.currenttask.deleted,
              idCategory: this.currenttask.idCategory,
              idUser: this.currenttask.idUser,
            });
          },
          err => this.loaded = true);
        }
      }
    });
   //categories
    this.generalService.GetCategories().subscribe(categories => {
        this.categorieslist = new Array<DBCategoryJ>();
        if (categories) {
          categories.forEach(element => this.categorieslist.push(<DBCategoryJ>element));
      }
    });
    this.cdr.detectChanges();
  }

  onSubmit() {
    this.generalService.putTask(this.form.value).subscribe(status => {
        this.router.navigate(['/tasklist'], { queryParams: {resultstatus: 'Success'} } );
    });
  }

  resetCurrentTask() {
    this.form = null;
    this.ngOnInit();
  }

  delete() {
    this.generalService.deleteTask(this.form.value).subscribe(
      status => {
        this.router.navigate(['/tasklist'], { queryParams: {resultstatus: 'Deleted'} } );
      },
      err =>{
        this.router.navigate(['/tasklist'], { queryParams: {resultstatus: 'Error'} } );
      });
  }

  back() {
    this.router.navigate(['/tasklist']);
  }
}
