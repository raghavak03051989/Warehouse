import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { ColDef } from 'ag-grid-community';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Warehouse';
  rowData: any;
  Warehouse: any;
  rowSelection: any;
  $: any;
  private gridApi;
  private gridColumnApi;
  isRowSelectable : any;
  cart :any = [];
  total :any = 0;

  headers= new HttpHeaders()
  .set('content-type', 'application/json')
  .set('Access-Control-Allow-Origin', '*')
  .set('Access-Control-Allow-Methods','GET');

  constructor(private http:HttpClient)
  {
    this.rowSelection = "single";

    this.isRowSelectable = function (rowNode) {
      return rowNode.data.isLicensed;
    };
  }
  ngOnInit() {
    this.getCars();
  }

  private getCars() {

    this.http.get('https://localhost:5001/api/WarehouseDetails',{'headers': this.headers}).subscribe(response => {
     this.rowData = response;
    }, error => {
      console.log(error);
    });
  }
  
  ragRenderer(params) {
    return '<span class="rag-element"></span>';
  }

  ragCellClassRules = {
    'rag-green-outer': (params) => params.value === true,
    'rag-red-outer': (params) => params.value === false,
  };

  columnDefs: ColDef[] = [
    { field: 'isLicensed',headerName: 'Select', resizable: true, width: 90, suppressSizeToFit: true, cellClassRules: this.ragCellClassRules, cellRenderer: this.ragRenderer,checkboxSelection: true, },
    { field: 'id', resizable: true , width: 0, suppressSizeToFit: true  },
    { field: 'make', resizable: true , width: 150, suppressSizeToFit: true  },
    { field: 'model', resizable: true, width: 150, suppressSizeToFit: true,  },
    { field: 'price', resizable: true, width: 150, suppressSizeToFit: true,  },
    { field: 'yearModel', resizable: true, width: 150, suppressSizeToFit: true}
  ];

  AddToCart(){
    const selectedRows = this.gridApi.getSelectedRows();
    this.addItemToCart(selectedRows[0]);
  }
  Checkout(){

  }
  
  onSelectionChanged(event) {
    const selectedRows = this.gridApi.getSelectedRows();
    this.http.get('https://localhost:5001/api/VehicleWarehouseDetails', {params: {'id': selectedRows[0].id} , 'headers': this.headers }).subscribe(data => {
        this.Warehouse = data;
             }, error => {
       console.log(error);
     });
    document.querySelector('#selectedRows').innerHTML =
      selectedRows.length === 1 ? 'Name :' + this.Warehouse.name + '<br/>' + 'Latitude :' + this.Warehouse.latitude + '<br/>' +'Longitude :' + this.Warehouse.longitude : '';
  }

  onGridReady(params) {
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi;
  }

  addItemToCart = function(product){
		  
    if (this.cart.length === 0){
      product.count = 1;
      this.cart.push(product);
    } else {
      var repeat = false;
      for(var i = 0; i< this.cart.length; i++){
        if(this.cart[i].id === product.id){
          repeat = true;
          this.cart[i].count +=1;
        }
      }
      if (!repeat) {
        product.count = 1;
        this.cart.push(product);	
      }
    }
    this.total += product.price;
  };
}

