using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientViewer.SharedObjects;
using PatientRepository.Interface;
using System.Windows.Input;

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

        private readonly ICommand _addPatientCmd;
        private readonly ICommand _deletePatientCmd;
        private readonly ICommand _savePatientCmd;

        public PatientViewModel(IPatientRepository repository)
        {
            Repository = repository;
        }

        public PatientViewModel()
        {
            IList<Patient> ps = new List<Patient>()
            {
                new Patient(){ FirstName = "Frederick", LastName = "Mosteller", PhoneNumber = "506-555-0149", MobileNumber = "418-555-0126", Address = "111 Davie Street, Toronto ON, V5H 1P1"}
            };
            _patients = ps;
        }

        #region Commands

        public ICommand AddPatientCmd { get { return _addPatientCmd; } }
        public ICommand DeletePatientCmd { get { return _deletePatientCmd; } }
        public ICommand SavePatientCmd { get { return _savePatientCmd; } }

        #region Refresh command

        private RefreshCommand _refreshPatientCommand = new RefreshCommand();
        public RefreshCommand RefreshPatientCommand
        {
            get
            {
                if (_refreshPatientCommand.ViewModel == null)
                    _refreshPatientCommand.ViewModel = this;
                return _refreshPatientCommand;
            }
        }

        public class RefreshCommand : ICommand
        {
            public PatientViewModel ViewModel { get; set; }
            public event EventHandler CanExecuteChanged;
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                ViewModel.Patients = ViewModel.Repository.GetPatients();
            }
        }

        #endregion Refresh command

        #region Clear commands

        private ClearCommand _clearPatientCommand = new ClearCommand();
        public ClearCommand ClearPatientCommand
        {
            get
            {
                if (_clearPatientCommand.ViewModel == null)
                    _clearPatientCommand.ViewModel = this;
                return _clearPatientCommand;
            }
        }
        public class ClearCommand : ICommand
        {
            public PatientViewModel ViewModel { get; set; }
            public event EventHandler CanExecuteChanged;
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                ViewModel.Patients = new List<Patient>();
            }
        }

        #endregion Clear commands

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