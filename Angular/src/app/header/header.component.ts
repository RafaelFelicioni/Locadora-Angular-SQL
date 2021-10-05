import { NONE_TYPE } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    
  }

 renderizarTabela() {
   if ($("#cardTabela").hasClass("d-none")) {
    $("#cardTabela").removeClass("d-none")
   } else {
    $("#cardTabela").addClass("d-none")
   }
  }
  renderizarGrafico() {
    if ($("#cardGrafico").hasClass("d-none")) {
      $("#cardGrafico").removeClass("d-none")
     } else {
      $("#cardGrafico").addClass("d-none")
     }
  }
}
