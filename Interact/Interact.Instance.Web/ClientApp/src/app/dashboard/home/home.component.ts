import { Component, OnInit } from '@angular/core';
import { UserService } from '../../shared/services/user.service';


//import { HttpClientModule } from '@angular/http/src/http_module';
//import { Http } from '@angular/http';

@Component({
  selector: 'app-dashboard-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  constructor(
    private userService: UserService,
  ) { }

  ngOnInit() {

  }

  cards = [
    { title: 'Card 1', cols: 2, rows: 1 },
    { title: 'Card 2', cols: 1, rows: 1 },
    { title: 'Card 3', cols: 1, rows: 2 },
    { title: 'Card 4', cols: 1, rows: 1 }
  ];
}
