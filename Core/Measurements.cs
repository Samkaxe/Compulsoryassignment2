﻿namespace Core;

public class Measurements
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int Systolic { get; set; }
    public int Diastolic { get; set; }
    public string PatientSSN { get; set; } 
}