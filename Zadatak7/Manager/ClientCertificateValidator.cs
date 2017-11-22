using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Diagnostics;

namespace Manager
{
    public class ClientCertificateValidator : X509CertificateValidator
    {
        /// <summary>
        /// Funkcija za validaciju sertifikata sa klijenske strane.
        /// </summary>
        /// <param name="certificate"> Sertifikat koji je potrebno validirati. </param>
        public override void Validate(X509Certificate2 certificate)
        {
            X509Certificate2 cert = CertificateManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, Formatter.ParseName(WindowsIdentity.GetCurrent().Name));

            if (certificate.Subject != cert.Issuer)
            {
                Logger.AnnotateEvent("Authentication failed. Certificate is not self signed.\n");

                throw new Exception("Certificate is not self signed.\n");
            }
            else if (certificate.SubjectName != cert.SubjectName)
            {
                Logger.AnnotateEvent("Authentication failed. Invalid common name.\n");

                throw new Exception("Invaid common name.\n");
            }
            else if (certificate.NotAfter <= DateTime.Now)
            {
                Logger.AnnotateEvent("Authentication failed. Certificate has expired.\n");

                throw new Exception("Certificate has expired.\n");
            }
        }
    }
}
