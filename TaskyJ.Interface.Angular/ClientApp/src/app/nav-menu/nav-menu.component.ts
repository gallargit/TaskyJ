import { AuthenticationService } from '../services/authentication.service';
import { Component } from '@angular/core';
import { Router, ParamMap, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  public user: string;
  public showdeleted: boolean = true;

  constructor(
    private authenticationService: AuthenticationService,
    private route: ActivatedRoute
  ) {
      if (this.authenticationService.currentUserValue)
        this.user = this.authenticationService.currentUserValue.userName;
    this.route.queryParams.subscribe((params: ParamMap) => {
      this.showdeleted = (params['showdeleted'] === 'true');
    });
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
