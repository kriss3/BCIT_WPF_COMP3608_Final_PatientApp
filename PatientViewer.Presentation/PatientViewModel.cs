using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientViewer.SharedObjects;
using PatientRepository.Interface;
using PatientRepository.SQL;
using PatientRepository.StaticData;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace PatientViewer.Presentation
{
    public class PatientViewModel : INotifyPropertyChanged
    {
        protected IPatientRepository Repository;
        private IEnumerable<Patient> _patients;
        public IEnumerable<Patient> Patients
        {
            get { return _patients; }
            set
            {
                if (_patients == value)
                    return;
                _patients = value;
                RaisePropertyChanged("Patients");
            }
        }
        private Patient selectedPatient;

        public ICommand AddPatientCmd { get; set; }
        public ICommand DeletePatientCmd { get; set; }
        public ICommand SavePatientCmd { get; set; }

        public Patient SelectedPatient
        {
            get
            {
                return selectedPatient;
            }
            set
            {
                selectedPatient = value;
                RaisePropertyChanged("SelectedPatient");
            }
        }

        //when I use this ctor application goes into error state
        public PatientViewModel(IPatientRepository repository)
        {
            Repository = repository;
        }

        //this ctor correctly disply record in the UI
        public PatientViewModel()
        {
            Repository = new StaticRepository();
            LoadData();
            LoadCommands();
        }

        private void LoadData()
        {
            try
            {
                var repo = new SQLRepository();
                _patients = repo.GetPatients();
            }
            catch (Exception ex)
            {
                //Log details of exception and proide meaningful information to the end user;
                var localRepo = new StaticRepository();
                _patients = localRepo.GetPatients();
            }
        }

        private void LoadCommands()
        {
            AddPatientCmd = new CustomCommand(AddPatientRecord, CanAdd);
            DeletePatientCmd = new CustomCommand(DeletePatientRecord, CanDelete);
            SavePatientCmd = new CustomCommand(SavePatientRecord, CanSave);
        }

        #region Commands

        private bool CanAdd(object obj)
        {
            return true;
        }
        private void AddPatientRecord(object obj)
        {
            //this will run when Add button is called
            SelectedPatient.FirstName = "";
        }

        private bool CanDelete(object obj)
        {
            return true;
        }

        private void DeletePatientRecord(object obj)
        {
            Repository.DeletePatient(SelectedPatient.LastName);
            _patients = Repository.ReloadPatients();
        }

        private bool CanSave(object obj)
        {
            return true;
        }

        private void SavePatientRecord(object obj)
        {
            Repository.UpdatePatient(SelectedPatient.LastName, SelectedPatient);
        }

        #endregion Commands

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}