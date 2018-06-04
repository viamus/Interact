import { Component } from '@angular/core';
import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';

import { NgProgress } from 'ngx-progressbar';
import { Http } from '@angular/http';

@Component({
  selector: 'dashboard-root',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  title = 'app';

  constructor(
    private progressService: NgProgress) { }

  ngOnInit() {

  }
}
