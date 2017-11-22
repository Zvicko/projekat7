using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace Manager
{
    public class DigitalSignature
    {
        /// <summary>
        /// Metoda za generisanje digitalnog potpisa.
        /// </summary>
        /// <param name="message"> Tekst koji treba da se digitalno potpise. </param>
        /// <param name="hashAlgorithm"> Hash algoritam koji se koristi. </param>
        /// <param name="certificate"> Sertifikat korisnika koji pravi digitalni potpis. </param>
        /// <returns> Niz bajtova koji predstavlja digitalni potpis prosledjene poruke. </returns>
        public static byte[] Create(string message, string hashAlgorithm, X509Certificate2 certificate)
        {
            RSACryptoServiceProvider csp = null;

            // Pribavljanje privatnog kljuca sertifikata za potpisivanje poruke.
            csp = (RSACryptoServiceProvider)certificate.PrivateKey;

            if (csp == null)
            {
                throw new Exception("Valid certificate was not found.\n");
            }

            // Koristi se SHA-1 hash algoritam.
            SHA1Managed sha1 = new SHA1Managed();
            UnicodeEncoding encoding = new UnicodeEncoding();

            byte[] data = encoding.GetBytes(message);
            byte[] hash = sha1.ComputeHash(data);
            byte[] signature = csp.SignHash(hash, CryptoConfig.MapNameToOID("SHA1"));

            return signature;
        }

        /// <summary>
        /// Metoda za verifikovanje digitalnog potpisa.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="hashAlgorith"></param>
        /// <param name="signature"></param>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public static bool Verify(string message, string hashAlgorith, byte[] signature, X509Certificate2 certificate)
        {
            // Pribavljanje javnog kljuca sertifikata za verifikovanje poruke.
            RSACryptoServiceProvider csp = (RSACryptoServiceProvider)certificate.PublicKey.Key;

            SHA1Managed sha1 = new SHA1Managed();
            UnicodeEncoding encoding = new UnicodeEncoding();

            byte[] data = encoding.GetBytes(message);
            byte[] hash = sha1.ComputeHash(data);

            bool verified = csp.VerifyHash(hash, CryptoConfig.MapNameToOID("SHA1"), signature);

            return verified;
        }
    }
}
