import { Component } from '@angular/core';
import { /*HttpClient*/ } from "@angular/common/http";
import { NgProgress } from 'ngx-progressbar';
import { Http } from '@angular/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';

  constructor(
    private progressService: NgProgress) { }

  ngOnInit() {
    //const sampleUrl = 'http://slowwly.robertomurray.co.uk/delay/6000/url/https://jsonplaceholder.typicode.com/posts/1';

    //this.progressService.start();
    //setTimeout(() => {
    //  this.progressService.set(0.1);
    //}, 1000);
    //setTimeout(() => {
    //  this.progressService.inc(0.2);
    //}, 2000);

    //this.http.get(sampleUrl)
    //  .subscribe((response) => {
    //    this.progressService.done();
    //    //this.posts = response.json();
    //  });
    //this.http.get(sampleUrl)
    //  .subscribe((response) => {
    //    this.progressService.done();
    //    //this.posts = response.json();
    //  });
  }
}
