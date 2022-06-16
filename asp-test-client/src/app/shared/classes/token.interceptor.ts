import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AuthService } from "src/app/services/auth.service";

@Injectable()
export class TokenInterceptor implements HttpInterceptor{

    constructor(private auth: AuthService){}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>{
        if(this.auth.isAuthenticated()){
            req = req.clone({
                setHeaders: {
                    Authorization: 'Bearer ' + localStorage.getItem('accessToken')
                }
            })
        }
        return next.handle(req);
    }
}