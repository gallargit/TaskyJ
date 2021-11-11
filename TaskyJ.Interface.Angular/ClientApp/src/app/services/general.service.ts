import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map, retry, catchError } from 'rxjs/operators';
import { BaseEntity } from '../models/BaseEntity';
import { Str2ObjectService } from './str2object.service';

@Injectable({ providedIn: 'root' })
export class GeneralService {
  //private MAIN_URL: string = 'http://localhost:49002/api/'
  private static MAIN_URL: string = 'https://localhost:44332/api/'
  private static MASTER_URL: string = GeneralService.MAIN_URL + 'taskyj/GetMaster';
  private static STATUS_URL: string = GeneralService.MAIN_URL + 'taskyj/Status';
  private static TASK_URL: string = GeneralService.MAIN_URL + 'taskyj';
  private static ALLTASKS_URL: string = GeneralService.MAIN_URL + 'taskyj/GetSync';
  private static CATEGORIES_URL: string = GeneralService.MAIN_URL + 'CategoryJ';

  public static LOGIN_URL: string = GeneralService.MAIN_URL + 'Auth/authenticate';
  public static LOGOFF_URL: string = GeneralService.MAIN_URL + 'Auth/logoff';

  constructor(private http: HttpClient, private str2ObjectService: Str2ObjectService) {
  }

  private handleError(operation: String, url: String) {
      return (err: any) => {
          let errMsg = `error in ${operation}() retrieving ${url}`;
          console.log(`${errMsg}:`, err)
          if(err instanceof HttpErrorResponse) {
              // you could extract more info about the error if you want, e.g.:
              console.log(`status: ${err.status}, ${err.statusText}`);
              // errMsg = ...
          }
          if (operation === 'getStatus')
              errMsg = 'Cannot get server status. CORS?'
          throw(errMsg);
      }
  }
      
  GetMaster(): Observable<BaseEntity[]> {
    return this.http.get<BaseEntity[]>(GeneralService.MASTER_URL)
      .pipe(map(itemArray => {
            var result = new Array<BaseEntity>();
            if (itemArray) {
                itemArray.forEach(element => {
                    let item = this.str2ObjectService.getDbObject('task');
                    for (var key in element){
                        item[key] = element[key];
                    }
                    result.push(item);
                });
            }
            return result;
        })
        ).pipe(
            retry(1),
          catchError(this.handleError('GetMaster', GeneralService.MASTER_URL))
        );
  }

  GetCategories(): Observable<BaseEntity[]> {
    return this.http.get<BaseEntity[]>(GeneralService.CATEGORIES_URL)
      .pipe(map(itemArray => {
            var result = new Array<BaseEntity>();
            if (itemArray) {
                itemArray.forEach(element => {
                    let item = this.str2ObjectService.getDbObject('category');
                    for (var key in element){
                        item[key] = element[key];
                    }
                    result.push(item);
                });
            }
            return result;
        })
        ).pipe(
            retry(1),
          catchError(this.handleError('GetCategories', GeneralService.CATEGORIES_URL))
        );
    }

  GetTask(id: number): Observable<BaseEntity> {
    return this.http.get<BaseEntity>(GeneralService.TASK_URL + '/' + id.toString())
      .pipe(map(element => {
        if (element) {
          let item = this.str2ObjectService.getDbObject('task');
          for (var key in element){
            item[key] = element[key];
          }
          return item;
        }
      })
      ).pipe(
        retry(1),
        catchError(this.handleError('GetTask', GeneralService.TASK_URL))
    );
  }

  GetAllTasks(includedeleted: boolean = false): Observable<BaseEntity[]> {
    return this.http.get<BaseEntity[]>(GeneralService.ALLTASKS_URL + "?includedeleted=" + includedeleted.toString())
      .pipe(map(itemArray => {
            var result = new Array<BaseEntity>();
            if (itemArray) {
                itemArray.forEach(element => {
                    let item = this.str2ObjectService.getDbObject('task');
                    for (var key in element){
                        item[key] = element[key];
                    }
                    result.push(item);
                });
            }
            return result;
        })
        ).pipe(
            retry(1),
          catchError(this.handleError('GetAllTasks', GeneralService.ALLTASKS_URL))
        );
  }

  putTask(object: BaseEntity) {
      return this.http.put<any>(GeneralService.TASK_URL  + '/' + object.id, object)
      .pipe(map(strResult => {
          return strResult;
      })
      ).pipe(
          catchError(this.handleError('putTask', GeneralService.TASK_URL))
      );
  }

  deleteTask(object: BaseEntity) {
      return this.http.delete<any>(GeneralService.TASK_URL  + '/' + object.id)
      .pipe(map(strResult => {
          return strResult;
      })
      ).pipe(
          catchError(this.handleError('deleteTask', GeneralService.TASK_URL))
      );
  }

  Status(): Observable<string> {
    const headers = new HttpHeaders().set('Content-Type', 'text/plain; charset=utf-8');
    return this.http.get(GeneralService.STATUS_URL, { headers, responseType: 'text'})
      .pipe(
          retry(1),
          catchError(this.handleError('Status', GeneralService.STATUS_URL))
      );
  }

  /*
    getById(what: string, id: any): Observable<DbObject[]> {
        var getUrl = this.URL_GET + `/${what}`;
        return this.http.post<any>(getUrl, id)
        .pipe(map(itemArray => {
            var result = new Array<DbObject>();
            if (itemArray) {
                itemArray.forEach(element => {
                    let item = this.str2ObjectService.getDbObject(what);
                    for (var key in element){
                        item[key] = element[key];
                    }
                    result.push(item);
                });
            }
            return result;
        })
        ).pipe(
            retry(1),
            catchError(this.handleError('getById', getUrl))
        );
    }

    putObject(what: string, object: DbObject) {
        var putUrl = this.URL_PUT + `/${what}`;
        return this.http.put<any>(putUrl, object)
        .pipe(map(strResult => {
            return strResult;
        })
        ).pipe(
            catchError(this.handleError('put', putUrl))       
        );
    }

    deleteObject(what: string, object: DbObject) {
        var deleteUrl = this.URL_DELETE + `/${what}`;
        
        let options = { params : new HttpParams().set('content', JSON.stringify(object)) };

        return this.http.delete<any>(deleteUrl, options)
        .pipe(map(strResult => {
            return strResult;
        })
        ).pipe(
            catchError(this.handleError('delete', deleteUrl))       
        );
    }*/
}
