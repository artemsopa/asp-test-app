import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from '../app-injection-tokens';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(
    private http: HttpClient,
    @Inject(API_URL) private apiUrl: string
  ) { }

  getProducts(): Observable<Array<Product>> {
    return this.http.get<Array<Product>>(`${this.apiUrl}api/product/getallproducts`)
  }

  createProduct(product: Product): Observable<Product> {
    const myHeaders = new HttpHeaders().set("Content-Type", "application/json");
    return this.http.post<Product>(`${this.apiUrl}api/product/addproduct`, JSON.stringify(product), {headers:myHeaders}); 
  }

  updateProduct(product: Product) {
    const myHeaders = new HttpHeaders().set("Content-Type", "application/json");
    return this.http.put<Product>(`${this.apiUrl}api/product/updateproduct`, JSON.stringify(product), {headers:myHeaders});
  }

  deleteProduct(id: number){
    return this.http.delete<Product>(`${this.apiUrl}api/product/deleteproductbyid/` + id);
  }
}
