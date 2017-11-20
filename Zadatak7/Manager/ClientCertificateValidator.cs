using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;

namespace Manager
{
    public class ClientCertificateValidator : X509CertificateValidator
    {
        public override void Validate(X509Certificate2 certificate)
        {
            X509Certificate2 cert = CertificateManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, Formatter.ParseName(WindowsIdentity.GetCurrent().Name));

            if (certificate.Subject != cert.Issuer)
            {
                throw new Exception("Certificate is not self signed.\n");
            }
            else if (certificate.SubjectName != cert.SubjectName)
            {
                throw new Exception("Invaid common name");
            }
        }
    }
}
