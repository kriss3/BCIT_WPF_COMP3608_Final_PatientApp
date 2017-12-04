using System.Collections.Generic;
using PatientViewer.SharedObjects;

namespace PatientRepository.Interface
{
    public interface IPatientRepository
    {
        IEnumerable<Patient> GetPatients();

        Patient GetPatient(string lastName);

        void AddPatient(Patient newPatient);

        void UpdatePatient(string lastName, Patient updatedPatient);

        void DeletePatient(string lastName);
    }
}
