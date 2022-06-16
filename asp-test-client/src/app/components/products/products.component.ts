import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Product } from 'src/app/models/product';
import { AuthService } from 'src/app/services/auth.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  constructor(
    private as: AuthService,
    private ps: ProductService
  ) { }

  ngOnInit(): void {
    this.getProducts();
    this.loadTemplate(false);
  }

  @ViewChild('addTemplate', {static: false}) addTemplate!: TemplateRef<any>|null;
  @ViewChild('editTemplate', {static: false}) editTemplate!: TemplateRef<any>|null;

  loadTemplate(value: boolean) {
    if (value == true) {
        return this.editTemplate;
    } else {
        return this.addTemplate;
    }
  }

  productName!: string;
  paginationNum: number = 1;
  isEdit: boolean = false;

  openEdit(product: Product){
    this.isEdit = true;
    this.product = product;
    this.addForm.controls["name"].setValue(product.name);
    this.addForm.controls["price"].setValue(product.price);
    this.addForm.controls["quantity"].setValue(product.quantity);
    this.addForm.controls["description"].setValue(product.description);
  }

  editProduct() {
    this.product.name = this.addForm.controls["name"].value;
    this.product.price = this.addForm.controls["price"].value;
    this.product.quantity = this.addForm.controls["quantity"].value;
    this.product.description = this.addForm.controls["description"].value;
    this.ps.updateProduct(this.product)
    .subscribe(() => {
      this.addForm.controls["name"].setValue("");
      this.addForm.controls["price"].setValue("");
      this.addForm.controls["quantity"].setValue("");
      this.addForm.controls["description"].setValue("");
      this.product = new Product();
      this.isEdit = false;
      this.getProducts();
    })
  }

  products: Array<Product> = new Array<Product>();

  getProducts(){
    this.ps.getProducts()
    .subscribe(data => {
      this.products = data
    })
  }

  public get isProductEmpty(){
    if(this.products.length > 0) return true;
    else return false;
  }

  addForm : FormGroup = new FormGroup({
    "name": new FormControl(),
    "price": new FormControl(),
    "quantity": new FormControl(),
    "description": new FormControl()
  });

  product: Product = new Product();

  addProduct(){
    this.product.name = this.addForm.controls["name"].value;
    this.product.price = this.addForm.controls["price"].value;
    this.product.quantity = this.addForm.controls["quantity"].value;
    this.product.description = this.addForm.controls["description"].value;
    this.ps.createProduct(this.product)
    .subscribe(() => {
      this.addForm.controls["name"].setValue("");
      this.addForm.controls["price"].setValue("");
      this.addForm.controls["quantity"].setValue("");
      this.addForm.controls["description"].setValue("");
      this.product = new Product();
      this.getProducts();
    });
  }

  deleteProduct(id: number){
    this.isEdit = false;
    this.ps.deleteProduct(id)
    .subscribe(() => {
      this.getProducts();
    });
  }

  cancel(){
    this.isEdit = false;
    this.addForm.controls["name"].setValue("");
    this.addForm.controls["price"].setValue("");
    this.addForm.controls["quantity"].setValue("");
    this.addForm.controls["description"].setValue("");
    this.product = new Product();
  }

  public get isAdmin(){
    if(this.as.isAdmin() == "Admin")
    return true;
    else return false;
  }

  logOut() {
    this.as.logOut();
  }

  search(){
    if(this.productName == ""){
      this.ngOnInit();
    } else{
      this.products = this.products.filter(res =>{
        return res.name.toLocaleLowerCase().match(this.productName.toLocaleLowerCase());
      })
    }
  }

  name: string = "";
  reverse: boolean = false;
  sort(name: string){
    this.name = name;
    this.reverse = !this.reverse;
  }
}
