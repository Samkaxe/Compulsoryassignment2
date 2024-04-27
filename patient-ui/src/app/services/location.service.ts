import { Injectable } from '@angular/core';
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class LocationService {

  private apiUrl = 'https://ipinfo.io/json'; // Example API endpoint

  private clientLocationCountry;

  constructor(private http: HttpClient) {
    this.getLocation().subscribe(value => {
      this.clientLocationCountry = value['country']
    })
  }

  getLocation(): Observable<any> {
    return this.http.get(this.apiUrl);
  }

  get isClientLocationDenmark() {
    return this.clientLocationCountry === 'DK'
  }
}
