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
    formData['id'] = 0;
    formData['date'] = new Date(formData['date']).toISOString();
    return this.http.post("http://localhost:7001/api/Measurement",formData);
  }
}
