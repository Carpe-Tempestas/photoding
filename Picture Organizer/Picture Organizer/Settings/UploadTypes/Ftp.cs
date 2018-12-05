using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Security;
using System.Security.Cryptography;
using System.IO;

namespace Trebuchet.Settings.UploadTypes
{
	[Serializable]
    public class Ftp : UploadBase
    {
        private string username;
        private string password;
        private string serverAddress;
        private int serverPort;
		private bool rememberPass = false;
		private const string ALPHA = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		private const string NUMERIC = "0123456789";
		private const string ALPHA_NUMERIC = ALPHA + NUMERIC;
		private string passPhrase = "le96xe0ih6gluecq42vnqrt9p8supfzg";        // can be any string
		private string saltValue = "wx2t34etw2soaukok38rnn5tr3j0q9w9";        // can be any string
		private string hashAlgorithm = "MD5";             // can be "MD5"
		private int passwordIterations = 2;                  // can be any number
		private string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
		private int keySize = 256;                // can be 192 or 128

        public override UploadSettings.UploadType GetMethod()
        {
            return UploadSettings.UploadType.Ftp;
        }

		public Ftp()
		{
			LoadDefaults();
		}

        public override void LoadSettings(object obj)
        {
            Ftp settings = obj as Ftp;
            if (settings != null)
            {
                this.Username = settings.Username;
                this.Password = settings.Password;
                this.ServerAddress = settings.ServerAddress;
				this.ServerPort = settings.ServerPort;
				this.RememberPass = settings.RememberPass;
				
            }
        }

        public override void LoadDefaults()
        {
            this.Username = "";
            this.Password = "";
            this.ServerAddress = "";
            this.ServerPort = 21;
			this.RememberPass = false;		
        }

        public string Username
        {
            get { return this.username; }
            set { this.username = value; }
        }

        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }

        public string ServerAddress
        {
            get { return this.serverAddress; }
            set { this.serverAddress = value; }
        }

        public int ServerPort
        {
            get { return this.serverPort; }
            set { this.serverPort = value; }
        }

		public bool RememberPass
		{
			get { return this.rememberPass; }
			set { this.rememberPass = value; }
		}

		public string Request(string text)
		{
			return Decrypt(text);
		}

		public string Tell(string text)
		{
			return Encrypt(text);
		}

		private string Encrypt(string plainText)
		{
			// Convert strings into byte arrays.
			// Let us assume that strings only contain ASCII codes.
			// If strings include Unicode characters, use Unicode, UTF7, or UTF8 
			// encoding.
			byte[] initVectorBytes = Encoding.ASCII.GetBytes(this.initVector);
			byte[] saltValueBytes = Encoding.ASCII.GetBytes(this.saltValue);

			// Convert our plaintext into a byte array.
			// Let us assume that plaintext contains UTF8-encoded characters.
			byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

			// First, we must create a password, from which the key will be derived.
			// This password will be generated from the specified passphrase and 
			// salt value. The password will be created using the specified hash 
			// algorithm. Password creation can be done in several iterations.
			PasswordDeriveBytes password = new PasswordDeriveBytes(
															this.passPhrase,
															saltValueBytes,
															this.hashAlgorithm,
															this.passwordIterations);

			// Use the password to generate pseudo-random bytes for the encryption
			// key. Specify the size of the key in bytes (instead of bits).
			byte[] keyBytes = password.GetBytes(this.keySize / 8);

			// Create uninitialized Rijndael encryption object.
			RijndaelManaged symmetricKey = new RijndaelManaged();

			// It is reasonable to set encryption mode to Cipher Block Chaining
			// (CBC). Use default options for other symmetric key parameters.
			symmetricKey.Mode = CipherMode.CBC;

			// Generate encryptor from the existing key bytes and initialization 
			// vector. Key size will be defined based on the number of the key 
			// bytes.
			ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
															 keyBytes,
															 initVectorBytes);

			// Define memory stream which will be used to hold encrypted data.
			MemoryStream memoryStream = new MemoryStream();

			// Define cryptographic stream (always use Write mode for encryption).
			CryptoStream cryptoStream = new CryptoStream(memoryStream,
														 encryptor,
														 CryptoStreamMode.Write);
			// Start encrypting.
			cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

			// Finish encrypting.
			cryptoStream.FlushFinalBlock();

			// Convert our encrypted data from a memory stream into a byte array.
			byte[] cipherTextBytes = memoryStream.ToArray();

			// Close both streams.
			memoryStream.Close();
			cryptoStream.Close();

			// Convert encrypted data into a base64-encoded string.
			string cipherText = Convert.ToBase64String(cipherTextBytes);

			// Return encrypted string.
			return cipherText;
		}

		/// <summary>
		/// Decrypts specified ciphertext using Rijndael symmetric key algorithm.
		/// </summary>
		/// <param name="cipherText">
		/// Base64-formatted ciphertext value.
		/// </param>
		/// <returns>
		/// Decrypted string value.
		/// </returns>
		/// <remarks>
		/// Most of the logic in this function is similar to the Encrypt
		/// logic. In order for decryption to work, all parameters of this function
		/// - except cipherText value - must match the corresponding parameters of
		/// the Encrypt function which was called to generate the
		/// ciphertext.
		/// </remarks>
		private string Decrypt(string cipherText)
		{
			// Convert strings defining encryption key characteristics into byte
			// arrays. Let us assume that strings only contain ASCII codes.
			// If strings include Unicode characters, use Unicode, UTF7, or UTF8
			// encoding.
			byte[] initVectorBytes = Encoding.ASCII.GetBytes(this.initVector);
			byte[] saltValueBytes = Encoding.ASCII.GetBytes(this.saltValue);

			// Convert our ciphertext into a byte array.
			byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

			// First, we must create a password, from which the key will be 
			// derived. This password will be generated from the specified 
			// passphrase and salt value. The password will be created using
			// the specified hash algorithm. Password creation can be done in
			// several iterations.
			PasswordDeriveBytes password = new PasswordDeriveBytes(
															this.passPhrase,
															saltValueBytes,
															this.hashAlgorithm,
															passwordIterations);

			// Use the password to generate pseudo-random bytes for the encryption
			// key. Specify the size of the key in bytes (instead of bits).
			byte[] keyBytes = password.GetBytes(this.keySize / 8);

			// Create uninitialized Rijndael encryption object.
			RijndaelManaged symmetricKey = new RijndaelManaged();

			// It is reasonable to set encryption mode to Cipher Block Chaining
			// (CBC). Use default options for other symmetric key parameters.
			symmetricKey.Mode = CipherMode.CBC;

			// Generate decryptor from the existing key bytes and initialization 
			// vector. Key size will be defined based on the number of the key 
			// bytes.
			ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
															 keyBytes,
															 initVectorBytes);

			// Define memory stream which will be used to hold encrypted data.
			MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

			// Define cryptographic stream (always use Read mode for encryption).
			CryptoStream cryptoStream = new CryptoStream(memoryStream,
														  decryptor,
														  CryptoStreamMode.Read);

			// Since at this point we don't know what the size of decrypted data
			// will be, allocate the buffer long enough to hold ciphertext;
			// plaintext is never longer than ciphertext.
			byte[] plainTextBytes = new byte[cipherTextBytes.Length];

			// Start decrypting.
			int decryptedByteCount = cryptoStream.Read(plainTextBytes,
													   0,
													   plainTextBytes.Length);

			// Close both streams.
			memoryStream.Close();
			cryptoStream.Close();

			// Convert decrypted data into a string. 
			// Let us assume that the original plaintext string was UTF8-encoded.
			string plainText = Encoding.UTF8.GetString(plainTextBytes,
													   0,
													   decryptedByteCount);

			// Return decrypted string.   
			return plainText;
		}
    }
}
