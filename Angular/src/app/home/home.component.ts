import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { UsuarioService } from '../usuario.service';
import { ChartType, Row } from "angular-google-charts";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {
  tableUsuarios?: any[]
  tableDataSrc: any;
  @Input('tableColumns') tableCols?: string[];
  @Input() tableData: {}[] = [];

  @ViewChild(MatSort, { static: true }) sort?: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator?: MatPaginator;
  
  myType = ChartType.Bar;
  title = "Tempo de estudo";
  type = 'Bar';
  data = [
    ['Angular', -1],
    ['C#', 2],
    ['.Net', 2],
    ['Javascript', 2],
    ['HTML', 2],
    ['CSS', 2]
  ];
  chartColumns = ['APRENDIZADO', 'ANOS'];
  options = { colors: ['#92A8D1'] };
  width = 500;
  height = 300;

  constructor(private usuarioService: UsuarioService) { }

  ngOnInit() {
    const resultado = this.getUsuario();
  }

  getUsuario() {
    this.usuarioService.getUsuarios().subscribe((usuarios: any[]) => {

      this.tableDataSrc = new MatTableDataSource(usuarios);
      this.tableDataSrc.sort = this.sort;
      this.tableDataSrc.paginator = this.paginator;
      this.tableCols =  Object.keys(usuarios[0])
    });
  }
  onSearchInput(ev?: any) {
    const searchTarget = ev.target.value;
    this.tableDataSrc.filter = searchTarget.trim().toLowerCase();
  }
}
