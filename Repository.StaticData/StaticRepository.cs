using PatientRepository.Interface;
using System;
using System.Collections.Generic;
using PatientViewer.SharedObjects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientRepository.StaticData
{
    public class StaticRepository : IPatientRepository
    {
        private IEnumerable<Patient> pList;
        public StaticRepository()
        {
            pList = GetPatients();
        }

        public void AddPatient(Patient newPatient)
        {
            pList.ToList().Add(newPatient);
        }

        public void DeletePatient(string lastName)
        {
            var patientList = pList.ToList();
            foreach (var p in pList)
            {
                if (p.LastName.Equals(lastName))
                    patientList.Remove(p);
            }

            pList = patientList;
        }

        public Patient GetPatient(string lastName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Patient> GetPatients()
        {
            pList = new List<Patient>
            {
                new Patient() { FirstName = "Wladysław", LastName = "Orlicz", PhoneNumber = "418-555-0155", MobileNumber = "604-123-456", Address = "101 Grange Street, Burnaby BC, V5H 1P1" },
                new Patient() { FirstName = "Stanisław", LastName = "Ulam", PhoneNumber = "418-555-0142", MobileNumber = "604-123-456", Address = "102 Main Street, Victoria BC, V5H 1P1" },
                new Patient() { FirstName = "Hugo", LastName = "Steinhaus", PhoneNumber = "418-555-0158", MobileNumber = "613-555-0176", Address = "103 Seymore Street, Burnaby BC, V5H 1P1" },
                new Patient() { FirstName = "Stanisław", LastName = "Ruziewicz", PhoneNumber = "709-555-0167", MobileNumber = "613-555-0100", Address = "104 Kingsway Street, Regina SK, V5H 1P1" },
                new Patient() { FirstName = "Stanisław", LastName = "Mazur", PhoneNumber = "709-555-0120", MobileNumber = "613-555-0141", Address = "105 Dunsmuir Street, Whistler BC, V5H 1P1" },
                new Patient() { FirstName = "Richard", LastName = "Bellman", PhoneNumber = "709-555-0160", MobileNumber = "613-555-0132", Address = "106 Thurlow Street, Calgary AL, V5H 1P1" },
                new Patient() { FirstName = "Felix", LastName = "Browder", PhoneNumber = "709-555-0132", MobileNumber = "613-555-0176", Address = "107 Butte Street, Montreal QC, V5H 1P1" },
                new Patient() { FirstName = "Erica", LastName = "Flapan", PhoneNumber = "709-555-0157", MobileNumber = "613-555-0150", Address = "108 Cambie Street, Edmonton AL, V5H 1P1" },
                new Patient() { FirstName = "James", LastName = "Lepowsky", PhoneNumber = "709-555-0185", MobileNumber = "418-555-0169", Address = "108 Expo Street, Ottawa ON, V5H 1P1" },
                new Patient() { FirstName = "Herbert", LastName = "Robbins", PhoneNumber = "506-555-0100", MobileNumber = "418-555-0128", Address = "110 Burrard Street, Winnipeg MT, V5H 1P1" },
                new Patient() { FirstName = "Frederick", LastName = "Mosteller", PhoneNumber = "506-555-0149", MobileNumber = "418-555-0126", Address = "111 Davie Street, Toronto ON, V5H 1P1" }
            };

            return pList;
        }

        public void UpdatePatient(string lastName, Patient updatedPatient)
        {
            var patientList = pList.ToList();
            var currentPatient = patientList.Find(x => x.LastName.Equals(lastName));

            if (currentPatient != null)
            {
                currentPatient.FirstName = updatedPatient.FirstName;
                currentPatient.LastName = updatedPatient.LastName;
                currentPatient.PhoneNumber = updatedPatient.PhoneNumber;
                currentPatient.MobileNumber = updatedPatient.MobileNumber;
                currentPatient.Address = updatedPatient.Address;
            }
        }

        public IEnumerable<Patient> ReloadPatients()
        {
            return pList;
        }
    }
}
