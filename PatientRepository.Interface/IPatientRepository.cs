using System.Collections.Generic;
using PatientViewer.SharedObjects;

namespace PatientRepository.Interface
{
    /// <summary>
    /// Interface shared across repositories;
    /// Krzysztof Szczurwoski
    /// Repo: https://github.com/kriss3/BCIT_WPF_COMP3608_Final_PatientApp.git
    /// </summary>
    public interface IPatientRepository
    {
        IEnumerable<Patient> GetPatients();

        Patient GetPatient(string lastName);

        void AddPatient(Patient newPatient);

        void UpdatePatient(string lastName, Patient updatedPatient);

        void DeletePatient(string lastName);

        IEnumerable<Patient> ReloadPatients();
    }
}
