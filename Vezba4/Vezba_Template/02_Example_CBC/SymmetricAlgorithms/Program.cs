﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Manager;


namespace SymmetricAlgorithms
{
	public class Program
	{
		#region DES Alogirthm

		static void Test_DES_Encrypt(string inputFile, string outputFile, string secretKey)
		{
			///Perform DES encryption
		}

		static void Test_DES_Decrypt(string inputFile, string outputFile, string secretKey)
		{
			///Perform DES decryption
		}

		#endregion

		#region 3DES Alogirthm

		static void Test_3DES_Encrypt(string inputFile, string outputFile, string secretKey)
		{
			///Perform 3DES encryption
		}

		static void Test_3DES_Decrypt(string inputFile, string outputFile, string secretKey)
		{
			///Perform 3DES decryption
		}

		#endregion

		#region AES Alogirthm

		static void Test_AES_Encrypt(string inputFile, string outputFile, string secretKey)
		{
			///Perform AES encryption
		}

		static void Test_AES_Decrypt(string inputFile, string outputFile, string secretKey)
		{
			///Perform AES decryption
		}

		#endregion


		static void Main(string[] args)
		{
			/// string imgFile = "Penguin.bmp";				//source bitmap file
			/// string cipherFile = "Ciphered.bmp";			//result of encryption
			/// string plaintextFile = "Plaintext.bmp";		//result of decryption
			/// string keyFile = "SecretKey.txt";			//secret key storage

			Console.WriteLine("Symmetric Encryption Example - CBC mode");

			/// Generate secret key for appropriate symmetric algorithm and store it to 'keyFile' for further usage

			///Test_DES_Encrypt(imgFile, cipherFile, eSecretKey);
			///Test_AES_Encrypt(imgFile, cipherFile, eSecretKey);
			///Test_3DES_Encrypt(imgFile, cipherFile, eSecretKey);
			Console.WriteLine("Encryption is done.");
			Console.ReadLine();

			///Test_DES_Decrypt(cipherFile, plaintextFile, SecretKey.LoadKey(keyFile));
			///Test_AES_Decrypt(cipherFile, plaintextFile, SecretKey.LoadKey(keyFile));
			///Test_3DES_Decrypt(cipherFile, plaintextFile, SecretKey.LoadKey(keyFile));
			Console.WriteLine("Decryption is done.");
			Console.ReadLine();
		}
	}
}
