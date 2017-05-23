using Newtonsoft.Json;

namespace Core.Security
{
    public static class Encryption
    {
        public static string Encrypt(string value)
        {
            var encrypted = new Encryptor().Encrypt(value);
            return encrypted;
        }

        public static string Encrypt<T>(T value)
        {
            return Encrypt(JsonConvert.SerializeObject(value));
        }

        public static string Decrypt(string value)
        {
            var decrypted = new Encryptor().Decrypt(value);
            return decrypted;
        }

        public static T Decrypt<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(Decrypt(value));
        }
    }
}