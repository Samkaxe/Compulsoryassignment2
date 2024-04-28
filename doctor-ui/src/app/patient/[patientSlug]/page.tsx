"use client";

import MeasurementsTable from "@/app/components/MeasurementsTable";
import { Patient } from "@/app/page";
import { useParams } from "next/navigation";
import { useEffect, useState } from "react";

const getPatient = async (patientSlug: string) => {
  const response = await fetch(
    `${process.env.NEXT_PUBLIC_PATIENTS_BASE_API_URL}/api/patient/${patientSlug}`
  );
  if (response.ok) {
    return await response.json();
  }
  return [];
};

const getMeasurements = async (patientSlug: string) => {
  const response = await fetch(
    `${process.env.NEXT_PUBLIC_MEASUREMENTS_BASE_API_URL}/api/measurement`
  );
  if (response.ok) {
    return await response.json();
  }
  return [];
}

const PatientPage = () => {
  const { patientSlug } = useParams();
  const [patient, setPatient] = useState<Patient | null>(null);
  const [measurements, setMeasurements] = useState([]);

  useEffect(() => {
    const fetchPatient = async () => {
      const data = await getPatient(patientSlug as string);
      const measurements = await getMeasurements(patientSlug as string);
      setPatient(data);
      setMeasurements(measurements);
    };
    fetchPatient();
  }, [patientSlug]);

  if (!patient) {
    return;
  }

  return (
    <div>
      <div className="p-7">
        <h1>{patient.name}</h1>
        <h1>{patient.email}</h1>
      </div>
      <MeasurementsTable items={measurements} />
    </div>
  );
};

export default PatientPage;
