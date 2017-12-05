using PatientRepository.Interface;
using PatientRepository.SQL;
using PatientRepository.StaticData;
using PatientViewer.SharedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace PatientViewer.Presentation
{
    /// <summary>
    /// Patient ViewModel class;
    /// Krzysztof Szczurowski
    /// Repo: https://github.com/kriss3/BCIT_WPF_COMP3608_Final_PatientApp.git
    /// </summary>
    public class PatientViewModel : INotifyPropertyChanged
    {
        #region Members

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

        #endregion Members

        //when I use this ctor application goes into error state
        public PatientViewModel(IPatientRepository repository)
        {
            Repository = repository;
        }

        //this ctor correctly disply records in the UI
        public PatientViewModel()
        {
            LoadData();
            LoadCommands();
        }

        //Trying to using SQL Repository but thowing error regarding lack of connection to DB?
        //handling this by loading data from Static Repository;
        //ToDo: check why SQLRepository cannot find connection to DB?

        #region Private Methods
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

        #endregion Private Methods
        
        #region Commands

        private bool CanAdd(object obj)
        {
            return true;
        }
        private void AddPatientRecord(object obj)
        {
            //this will run when Add button is called
            //for now inline editing of the row will Edit/Save data to the static List<Patient>
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