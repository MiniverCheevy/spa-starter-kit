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
        private readonly Random random;
        private readonly byte[] key;
        private readonly RijndaelManaged rm;
        private readonly UTF8Encoding encoder;
        //TODO: change this for each project, first 32 characters will form the key
        private const string EncryptionKey =
            @"Inversion of control is a common feature of frameworks, but it's something that comes at a price. 
            It tends to be hard to understand and leads to problems when you are trying to debug. 
            So on the whole I prefer to avoid it unless I need it. This isn't to say it's a bad thing, 
            just that I think it needs to justify itself over the more straightforward alternative. - Martin Fowler";

        public Encryptor()
        {
            this.random = new Random();
            this.rm = new RijndaelManaged();
            this.encoder = new UTF8Encoding();

            var psuedoKey = Convert.FromBase64String(Objectifyer.Base64Encode(EncryptionKey));
            if (psuedoKey.Length < 32)
                throw new Exception("Encryption key too short");
            this.key = psuedoKey.Take(32).ToArray();
        }

        public string Encrypt(string unencrypted)
        {
            randomize();
            var vector = new byte[16];
            this.random.NextBytes(vector);
            var cryptogram = vector.Concat(this.encrypt(this.encoder.GetBytes(unencrypted), vector));
            return Convert.ToBase64String(cryptogram.ToArray());
        }

        private void randomize()
        {
            var num = (random.NextDouble() * 6).To<int>();
            Thread.Sleep(num);
            var scratch = new Byte[num];
            this.random.NextBytes(scratch);
        }

        public string Decrypt(string encrypted)
        {
            var cryptogram = Convert.FromBase64String(encrypted);
            if (cryptogram.Length < 17)
            {
                throw new ArgumentException("Not a valid encrypted string", nameof(encrypted));
            }

            var vector = cryptogram.Take(16).ToArray();
            var buffer = cryptogram.Skip(16).ToArray();
            return this.encoder.GetString(this.decrypt(buffer, vector));
        }

        private byte[] encrypt(byte[] buffer, byte[] vector)
        {
            var encryptor = this.rm.CreateEncryptor(this.key, vector);
            return this.transform(buffer, encryptor);
        }

        private byte[] decrypt(byte[] buffer, byte[] vector)
        {
            var decryptor = this.rm.CreateDecryptor(this.key, vector);
            return this.transform(buffer, decryptor);
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