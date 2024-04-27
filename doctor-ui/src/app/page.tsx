'use client';
import Image from "next/image";
import { use, useEffect, useState } from "react";
import CreatePatientModal from "./components/CreatePatientModal";
import Table from "./components/Table";

interface Patient {
  name: string;
  email: string;
  ssn: string;
  measurements: [];
}

export default function Home() {
  const [isOpen, setIsOpen] = useState(false);
  const [patients, setPatients] = useState<Patient[]>([]);

  useEffect(() => {
    const getPatients = async () => {
      const response = await fetch(`${process.env.NEXT_PUBLIC_PATIENTS_BASE_API_URL}/api/patient/all`);
      if (response.ok) {
        const data = await response.json();
        setPatients(data);
      }
    }
    getPatients();

  }, []);

  const createPatient = async (ssn: string, email: string, name: string) => {
    console.log(ssn, email, name);
    const response = await fetch(`${process.env.NEXT_PUBLIC_PATIENTS_BASE_API_URL}/api/patient`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ ssn, email, name, measurements: []}),
    });
    const data: Patient = await response.json();
    if (response.ok) {
      setIsOpen(false);
      setPatients(prev => [...prev, data]);
    }
  }
  return (
    <main className="flex min-h-screen flex-col items-center p-24">
      {/* <button onClick={() => setIsOpen(true)}>click</button> */}
        <CreatePatientModal isOpen={isOpen} onClose={() => setIsOpen(false)} onSubmit={createPatient}/>
        <Table items={patients} openModal={() => setIsOpen(true)}/>
    </main>
  );
}
