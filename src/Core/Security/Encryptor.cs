using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Voodoo;

namespace Fernweh.Core.Security
{
    //http://stackoverflow.com/questions/165808/simple-two-way-encryption-for-c-sharp
    internal class Encryptor
    {
        //TODO: change this for each project, first 32 characters will form the key
        private const string EncryptionKey =
            @"Inversion of control is a common feature of frameworks, but it's something that comes at a price. 
            It tends to be hard to understand and leads to problems when you are trying to debug. 
            So on the whole I prefer to avoid it unless I need it. This isn't to say it's a bad thing, 
            just that I think it needs to justify itself over the more straightforward alternative. - Martin Fowler";

        private readonly UTF8Encoding encoder;
        private readonly byte[] key;
        private readonly Random random;
        private readonly RijndaelManaged rm;

        public Encryptor()
        {
            random = new Random();
            rm = new RijndaelManaged();
            encoder = new UTF8Encoding();

            var psuedoKey = Convert.FromBase64String(Objectifyer.Base64Encode(EncryptionKey));
            //TODO: allow 64 or 128 length keys
            if (psuedoKey.Length < 32)
                throw new Exception("Encryption key too short");
            key = psuedoKey.Take(32).ToArray();
        }

        public string Encrypt(string unencrypted)
        {
            randomize();
            var vector = new byte[16];
            random.NextBytes(vector);
            var cryptogram = vector.Concat(encrypt(encoder.GetBytes(unencrypted), vector));
            return Convert.ToBase64String(cryptogram.ToArray());
        }

        private void randomize()
        {
            var num = (random.NextDouble() * 6).To<int>();
            Thread.Sleep(num);
            var scratch = new byte[num];
            random.NextBytes(scratch);
        }

        public string Decrypt(string encrypted)
        {
            var cryptogram = Convert.FromBase64String(encrypted);
            if (cryptogram.Length < 17)
                throw new ArgumentException("Not a valid encrypted string", nameof(encrypted));

            var vector = cryptogram.Take(16).ToArray();
            var buffer = cryptogram.Skip(16).ToArray();
            return encoder.GetString(decrypt(buffer, vector));
        }

        private byte[] encrypt(byte[] buffer, byte[] vector)
        {
            var encryptor = rm.CreateEncryptor(key, vector);
            return transform(buffer, encryptor);
        }

        private byte[] decrypt(byte[] buffer, byte[] vector)
        {
            var decryptor = rm.CreateDecryptor(key, vector);
            return transform(buffer, decryptor);
        }

        private byte[] transform(byte[] buffer, ICryptoTransform transform)
        {
            var stream = new MemoryStream();
            using (var cs = new CryptoStream(stream, transform, CryptoStreamMode.Write))
            {
                cs.Write(buffer, 0, buffer.Length);
            }

            return stream.ToArray();
        }
    }
}