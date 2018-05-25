import { Injectable } from '@angular/core';
import { Http, Response, URLSearchParams, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class EventoService {
    public headers: Headers;

    constructor(private http: Http) {
        this.headers = new Headers();
        this.headers.append("Content-Type", 'application/json');
    }

    getEventos(id: number) {
        return this.http.get('provisaoapi/api/evento/' + id)
            .map(this.extractData)
            .catch(this.handleError);
    }

    private extractData(res: Response) {
        let body = res.json();
        return body || {};
    }

    private handleError(error: Response) {
        console.error(error);
        let msg = `Error status code ${error.status} at ${error.url}`;
        return Observable.throw(msg);
    }
}
