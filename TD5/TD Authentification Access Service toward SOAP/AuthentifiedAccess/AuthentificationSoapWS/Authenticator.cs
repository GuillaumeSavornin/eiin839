using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AuthentificationSoapWS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Authenticator : IAuthenticator
    {

        private Dictionary<string, string> Credentials = new Dictionary<string, string>();

        public Authenticator()
        {
            string[] logins = File.ReadAllLines("C:/Users/lacav/source/repos/TD_soc/TD5/TD Authentification Access Service toward SOAP/AuthentifiedAccess/passwd.csv");
            foreach (string login in logins)
            {
                string[] parts = login.Split(';');
                Credentials.Add(parts[0].Trim(), parts[1].Trim());
            }
        }

        public bool ValidateCredentials(string username, string password)
        {
            return Credentials.Any(entry => entry.Key == username && entry.Value == password);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
