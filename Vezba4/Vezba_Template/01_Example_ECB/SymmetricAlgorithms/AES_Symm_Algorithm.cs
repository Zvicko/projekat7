using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace SymmetricAlgorithms
{
	public class AES_Symm_Algorithm
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
			byte[] body = null;		//image body to be encrypted

			/// Formatter.Decompose();

			AesCryptoServiceProvider aesCrypto = new AesCryptoServiceProvider();

			/// ICryptoTransform aesEncrypt = aesCrypto.CreateEncryptor();
			/// CryptoStream cryptoStream

			/// output = header + encrypted_body
			/// Formatter.Compose();
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

			AesCryptoServiceProvider desCrypto = new AesCryptoServiceProvider();
			/// aesCrypto.Padding = PaddingMode.None;

			/// ICryptoTransform aesDecrypt = aesCrypto.CreateDecryptor();
			/// CryptoStream cryptoStream

			/// output = header + decrypted_body
			/// Formatter.Compose();
		}
	}
}
