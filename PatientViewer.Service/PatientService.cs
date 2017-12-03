using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PatientViewer.SharedObjects;

namespace PatientViewer.Service
{
    public class PatientService : IPatientService
    {
        public void AddPatient(Patient newPatient)
        {
            throw new NotImplementedException();
        }

        public void DeletePatient(string lastName)
        {
            throw new NotImplementedException();
        }

        public Patient GetPatient(string lastName)
        {
            var patients = new List<Patient>()
            {
                new Patient() { FirstName="John", LastName="Koenig", PhoneNumber ="604-123-456",
                    MobileNumber ="604-123-456", Address ="1134 Grange Street, Burnby, BC, V5Y 1P1"},
                new Patient() { FirstName="Dylan", LastName="Hunt", PhoneNumber="604-123-456",
                    MobileNumber ="604-123-456", Address="1134 Grange Street, Burnby, BC, V5Y 1P1"},
                new Patient() { FirstName="John", LastName="Crichton", PhoneNumber="604-123-456",
                    MobileNumber ="604-123-456", Address="1134 Grange Street, Burnby, BC, V5Y 1P1"},
                new Patient() { FirstName="Dave", LastName="Lister", PhoneNumber="604-123-456",
                    MobileNumber ="604-123-456", Address="1134 Grange Street, Burnby, BC, V5Y 1P1"},
                new Patient() { FirstName="John", LastName="Sheridan", PhoneNumber="604-123-456",
                    MobileNumber ="604-123-456", Address="1134 Grange Street, Burnby, BC, V5Y 1P1"},
                new Patient() { FirstName="Dante", LastName="Montana", PhoneNumber="604-123-456",
                    MobileNumber ="604-123-456", Address="1134 Grange Street, Burnby, BC, V5Y 1P1" },
                new Patient() { FirstName="Isaac", LastName="Gampu", PhoneNumber="604-123-456",
                    MobileNumber ="604-123-456", Address="1134 Grange Street, Burnby, BC, V5Y 1P1"}
            };

            Patient myPatient = patients.Where(p => p.LastName == lastName).FirstOrDefault();
            return myPatient;
        }

        public List<Patient> GetPatients()
        {
            var patients = new List<Patient>()
            {
                new Patient() { FirstName="John", LastName="Koenig", PhoneNumber ="604-123-456",
                    MobileNumber ="604-123-456", Address ="1134 Grange Street, Burnby, BC, V5Y 1P1"},
                new Patient() { FirstName="Dylan", LastName="Hunt", PhoneNumber="604-123-456",
                    MobileNumber ="604-123-456", Address="1134 Grange Street, Burnby, BC, V5Y 1P1"},
                new Patient() { FirstName="John", LastName="Crichton", PhoneNumber="604-123-456",
                    MobileNumber ="604-123-456", Address="1134 Grange Street, Burnby, BC, V5Y 1P1"},
                new Patient() { FirstName="Dave", LastName="Lister", PhoneNumber="604-123-456",
                    MobileNumber ="604-123-456", Address="1134 Grange Street, Burnby, BC, V5Y 1P1"},
                new Patient() { FirstName="John", LastName="Sheridan", PhoneNumber="604-123-456",
                    MobileNumber ="604-123-456", Address="1134 Grange Street, Burnby, BC, V5Y 1P1"},
                new Patient() { FirstName="Dante", LastName="Montana", PhoneNumber="604-123-456",
                    MobileNumber ="604-123-456", Address="1134 Grange Street, Burnby, BC, V5Y 1P1" },
                new Patient() { FirstName="Isaac", LastName="Gampu", PhoneNumber="604-123-456",
                    MobileNumber ="604-123-456", Address="1134 Grange Street, Burnby, BC, V5Y 1P1"}
            };
            return patients;
        }

        public void UpdatePatient(string lastName, Patient updatedPatient)
        {
            throw new NotImplementedException();
        }

        public void UpdatePatient(List<Patient> updatedPatients)
        {
            throw new NotImplementedException();
        }
    }
}
