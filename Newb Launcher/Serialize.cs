using System.IO;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;

namespace Newb_Launcher
{
    class Serialize
    {
        // Serialized "Password"
        private static readonly byte[] SALT = new byte[] { 0x26, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc5, 0xfe, 0x07, 0xaf, 0x4d, 0x08, 0x22, 0x3c };


        // Encrypt Function.
        public static byte[] Encrypt(byte[] plain, string password)
        {
            MemoryStream memoryStream;
            CryptoStream cryptoStream;

            Rijndael rijndael = Rijndael.Create();
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, SALT);

            rijndael.Key = pdb.GetBytes(32);
            rijndael.IV = pdb.GetBytes(16);

            memoryStream = new MemoryStream();
            cryptoStream = new CryptoStream(memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write);

            cryptoStream.Write(plain, 0, plain.Length);
            cryptoStream.Close();

            return memoryStream.ToArray();
        }

        // Decrypt Function.
        public static byte[] Decrypt(byte[] cipher, string password)
        {
            MemoryStream memoryStream;
            CryptoStream cryptoStream;

            Rijndael rijndael = Rijndael.Create();
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, SALT);

            rijndael.Key = pdb.GetBytes(32);
            rijndael.IV = pdb.GetBytes(16);

            memoryStream = new MemoryStream();
            cryptoStream = new CryptoStream(memoryStream, rijndael.CreateDecryptor(), CryptoStreamMode.Write);

            cryptoStream.Write(cipher, 0, cipher.Length);
            cryptoStream.Close();

            return memoryStream.ToArray();
        }


        // Serialize Function.
        public static byte[] accountSerialize(Account account)
        {
            BinaryFormatter bFormat = new BinaryFormatter();
            MemoryStream mStream = new MemoryStream();

            bFormat.Serialize(mStream, account);

            return mStream.GetBuffer();
        }

        // Deserialize Function.
        public static Account accountDeserialize(byte[] buffer)
        {
            Account account;

            BinaryFormatter bFormat = new BinaryFormatter();
            MemoryStream mStream = new MemoryStream(buffer);

            account = (Account)bFormat.Deserialize(mStream);

            return account;
        }


        // Load Account from File Function.
        public static Account loadAccount(string filename)
        {
            byte[] buffer = File.ReadAllBytes(filename);

            buffer = Decrypt(buffer, "cYjo3QUvPuBu8mKR90qF");

            return accountDeserialize(buffer);
        }

        // Save Account to File Function.
        public static void saveAccount(Account account, string filename)
        {
            byte[] buffer = accountSerialize(account);

            buffer = Encrypt(buffer, "cYjo3QUvPuBu8mKR90qF");

            File.WriteAllBytes(filename, buffer);
        }
    }
}
