using PatientRepository.Interface;
using PatientViewer.SharedObjects;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace PatientRepository.SQL
{
    /// <summary>
    /// SQL Repository;
    /// Used Model First approach;
    /// Krzysztof Szczurowski
    /// Repo: https://github.com/kriss3/BCIT_WPF_COMP3608_Final_PatientApp.git
    /// </summary>
    public class SQLRepository : IPatientRepository
    {
        /// <summary>
        /// This project can be run on its own;
        /// It will generate Database: MedicalDb with Tbl: dbo.Patients;
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            DbSetup();
        }

        public SQLRepository()
        {
            DbSetup();
        }

        /// <summary>
        /// This method will call into DbSeeder class (below) that contains 
        /// overridden Seed method to populate dbo.Patients table;
        /// </summary>
        private static void DbSetup()
        {
            var ctx = new PatientModelContainer();
            SetDefaultDbContent(ctx);
        }

        public IEnumerable<Patient> GetPatients()
        {
            using (var ctx = new PatientModelContainer())
            {
                IEnumerable<Patient> patients = from p in ctx.DbPatients
                             select new Patient
                             {
                                 FirstName = p.FirstName,
                                 LastName = p.LastName,
                                 PhoneNumber = p.PhoneNumber,
                                 MobileNumber = p.MobileNumber,
                                 Address = p.Address
                             };

                return patients.ToList();
            }
        }

        public Patient GetPatient(string lastName)
        {
            Patient p = null;

            using (var context = new PatientModelContainer())
            {
                var patientFound = GetDbPatient(context, lastName);
                if (patientFound != null)
                    p = GetPatientFromDbPatient(patientFound);
            }
            return p;
        }
        public void AddPatient(Patient newPatient)
        {
            using (var context = new PatientModelContainer())
            {
                if (GetDbPatient(context, newPatient.LastName) != null)
                    return;

                var addPatient = new DbPatient()
                {
                    FirstName = newPatient.FirstName,
                    LastName = newPatient.LastName,
                    PhoneNumber = newPatient.PhoneNumber,
                    MobileNumber = newPatient.MobileNumber,
                    Address = newPatient.Address
                };
                context.DbPatients.Add(addPatient);
                context.SaveChanges();
            }
        }

        public void UpdatePatient(string lastName, Patient updatedPatient)
        {
            using (var context = new PatientModelContainer())
            {
                var patientFound = GetDbPatient(context, lastName);
                if (patientFound == null)
                    return;

                patientFound.FirstName = updatedPatient.FirstName;
                patientFound.LastName = updatedPatient.LastName;
                patientFound.PhoneNumber = updatedPatient.PhoneNumber;
                patientFound.MobileNumber = updatedPatient.MobileNumber;
                patientFound.Address = updatedPatient.Address;

                context.SaveChanges();
            }
        }

        public void DeletePatient(string lastName)
        {
            using (var context = new PatientModelContainer())
            {
                var foundPerson = GetDbPatient(context, lastName);
                if (foundPerson == null)
                    return;

                context.DbPatients.Remove(foundPerson);
                context.SaveChanges();
            }
        }

        public IEnumerable<Patient> ReloadPatients()
        {
            return GetPatients();
        }

        private DbPatient GetDbPatient(PatientModelContainer context, string lastName)
        {
            DbPatient dbPatientFound = null;
            dbPatientFound = (from p in context.DbPatients
                           where p.LastName == lastName
                           select p).FirstOrDefault();
            return dbPatientFound;
        }

        private Patient GetPatientFromDbPatient(DbPatient dbPatient)
        {
            var person = new Patient()
            {
                FirstName = dbPatient.FirstName,
                LastName = dbPatient.LastName,
                PhoneNumber = dbPatient.PhoneNumber,
                MobileNumber = dbPatient.MobileNumber,
                Address = dbPatient.Address
            };
            return person;
        }

        private static void SetDefaultDbContent(PatientModelContainer dbContext)
        {
            using (var context = new PatientModelContainer())
            {
                Database.SetInitializer(new DbSeeder());
                //without below database/table is created but data is not populated;
                var data = from d in context.DbPatients select d;
            }
        }
    }
    
    public class DbSeeder : DropCreateDatabaseIfModelChanges<PatientModelContainer>
    {
        protected override void Seed(PatientModelContainer context)
        {
            IList<DbPatient> pList = new List<DbPatient>
            {
                new DbPatient() { FirstName = "Wladysław", LastName = "Orlicz", PhoneNumber = "418-555-0155", MobileNumber = "604-123-456", Address = "101 Grange Street, Burnaby BC, V5H 1P1" },
                new DbPatient() { FirstName = "Stanisław", LastName = "Ulam", PhoneNumber = "418-555-0142", MobileNumber = "604-123-456", Address = "102 Main Street, Victoria BC, V5H 1P1" },
                new DbPatient() { FirstName = "Hugo", LastName = "Steinhaus", PhoneNumber = "418-555-0158", MobileNumber = "613-555-0176", Address = "103 Seymore Street, Burnaby BC, V5H 1P1" },
                new DbPatient() { FirstName = "Stanisław", LastName = "Ruziewicz", PhoneNumber = "709-555-0167", MobileNumber = "613-555-0100", Address = "104 Kingsway Street, Regina SK, V5H 1P1" },
                new DbPatient() { FirstName = "Stanisław", LastName = "Mazur", PhoneNumber = "709-555-0120", MobileNumber = "613-555-0141", Address = "105 Dunsmuir Street, Whistler BC, V5H 1P1" },
                new DbPatient() { FirstName = "Richard", LastName = "Bellman", PhoneNumber = "709-555-0160", MobileNumber = "613-555-0132", Address = "106 Thurlow Street, Calgary AL, V5H 1P1" },
                new DbPatient() { FirstName = "Felix", LastName = "Browder", PhoneNumber = "709-555-0132", MobileNumber = "613-555-0176", Address = "107 Butte Street, Montreal QC, V5H 1P1" },
                new DbPatient() { FirstName = "Erica", LastName = "Flapan", PhoneNumber = "709-555-0157", MobileNumber = "613-555-0150", Address = "108 Cambie Street, Edmonton AL, V5H 1P1" },
                new DbPatient() { FirstName = "James", LastName = "Lepowsky", PhoneNumber = "709-555-0185", MobileNumber = "418-555-0169", Address = "108 Expo Street, Ottawa ON, V5H 1P1" },
                new DbPatient() { FirstName = "Herbert", LastName = "Robbins", PhoneNumber = "506-555-0100", MobileNumber = "418-555-0128", Address = "110 Burrard Street, Winnipeg MT, V5H 1P1" },
                new DbPatient() { FirstName = "Frederick", LastName = "Mosteller", PhoneNumber = "506-555-0149", MobileNumber = "418-555-0126", Address = "111 Davie Street, Toronto ON, V5H 1P1" }
            };


            foreach (var p in pList)
            {
                context.DbPatients.Add(p);
            }
            base.Seed(context);
        }
    }
}


