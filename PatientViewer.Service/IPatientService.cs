using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PatientViewer.SharedObjects;

namespace PatientViewer.Service
{
    [ServiceContract]
    public interface IPatientService
    {
        [OperationContract]
        List<Patient> GetPatients();

        [OperationContract]
        Patient GetPatient(string lastName);

        [OperationContract]
        void AddPatient(Patient newPatient);

        [OperationContract]
        void UpdatePatient(string lastName, Patient updatedPatient);

        [OperationContract]
        void DeletePatient(string lastName);

        [OperationContract]
        void UpdatePatient(List<Patient> updatedPatients);
    }
}
