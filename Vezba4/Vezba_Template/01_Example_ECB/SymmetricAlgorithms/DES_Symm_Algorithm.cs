using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Drawing;
using Manager;


namespace SymmetricAlgorithms
{
	public class DES_Symm_Algorithm
	{
		/// <summary>
		/// Function that encrypts the plaintext from inFile and stores cipher text to outFile
		/// </summary>
		/// <param name="inFile"> filepath where plaintext is stored </param>
		/// <param name="outFile"> filepath where cipher text is expected to be stored </param>
		/// <param name="secretKey"> symmetric encryption key </param>
		public static void EncryptFile(string inFile, string outFile, string secretKey)
		{
			byte[] header = null;	//image header (54 byte) should not be encrypted
			byte[] body = null;     //image body to be encrypted

            byte[] image = File.ReadAllBytes(inFile); 
                //ImageHelper.ImageToByteArray(Image.FromFile(inFile));
            Formatter.Decompose(image, out header, out body);

            DESCryptoServiceProvider desCrypto = new DESCryptoServiceProvider();
            desCrypto.Key = ASCIIEncoding.ASCII.GetBytes(secretKey);
            desCrypto.IV = ASCIIEncoding.ASCII.GetBytes(secretKey);
            desCrypto.Mode = CipherMode.ECB;
            
            ICryptoTransform desEncrypt = desCrypto.CreateEncryptor();
            MemoryStream memoryStream = new MemoryStream();
             //memoryStream.Read(body, 0, body.Length);
             
                CryptoStream cryptoStream = new CryptoStream(memoryStream,
            desEncrypt, CryptoStreamMode.Write);


                
                cryptoStream.Write(body, 0, body.Length);
                //cryptoStream.Write(header, 0, header.Length);
            //cryptoStream.Close();
            //cryptoStream.FlushFinalBlock();

            int length = header.Length + body.Length;
            Formatter.Compose(header, body, length, outFile);
            cryptoStream.Close();
        }


		/// <summary>
		/// Function that decrypts the cipher text from inFile and stores as plaintext to outFile
		/// </summary>
		/// <param name="inFile"> filepath where cipher text is stored </param>
		/// <param name="outFile"> filepath where plain text is expected to be stored </param>
		/// <param name="secretKey"> symmetric encryption key </param>
		public static void DecryptFile(string inFile, string outFile, string secretKey)
		{
			byte[] header = null;		//image header (54 byte) should not be decrypted
			byte[] body = null;			//image body to be decrypted

			/// Formatter.Decompose();			
			
			DESCryptoServiceProvider desCrypto = new DESCryptoServiceProvider();
			/// desCrypto.Padding = PaddingMode.None;

			/// ICryptoTransform desDecrypt = desCrypto.CreateDecryptor();
			/// CryptoStream cryptoStream
						
			/// output = header + decrypted_body
			/// Formatter.Compose();					
		}
	}
}
