import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PatientService } from '../../services/patient.service';

@Component({
  selector: 'app-patient-input',
  templateUrl: './measurement-input.component.html',
})
export class MeasurementInputComponent implements OnInit {
  measurementForm: FormGroup; // Reactive form group
  
  minDate = new Date(2000, 0, 1); // January 1, 2000
  maxDate = new Date(2030, 11, 31); // December 31, 2030

  constructor(
    private formBuilder: FormBuilder,
    private patientService: PatientService // Service to handle form data
  ) {}

  ngOnInit(): void {
    this.measurementForm = this.formBuilder.group({
      date: ['', Validators.required], // Set to empty by default
      systolic: [0, Validators.required], // Default to 0
      diastolic: [0, Validators.required], // Default to 0
      patientSSN: ['', [Validators.required, Validators.pattern(/\d{3}-\d{2}-\d{4}/)]], // Validate SSN format
    });
  }

  onSubmit(): void {
    if (this.measurementForm.valid) {
      const formData = this.measurementForm.value; // Get form data
      this.patientService.sendMeasurement(formData).subscribe(
        (response) => {
          console.log('Data saved successfully:', response);
        },
        (error) => {
          console.error('Error saving data:', error);
        }
      );
    }
  }
}
