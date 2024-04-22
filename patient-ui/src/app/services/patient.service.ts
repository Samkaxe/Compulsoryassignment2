import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class PatientService {

  constructor(
    private http: HttpClient
  ) { }

  sendMeasurement(formData: any) {
    return this.http.post("http://localhost:7000/api/Patient",null);
  }
}
