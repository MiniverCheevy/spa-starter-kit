using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Fernweh.Core.Identity;
using Fernweh.Core.Security;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.TestData;

namespace Fernweh.Tests.Core.Security
{
    [TestClass]
    public class EncryptionTests
    {
        [TestMethod]
        public void StringValue_EncryptDecrypt_IsOk()
        {
            var testString = "Jenkies";
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var encrypted = Encryption.Encrypt(testString);
            stopwatch.Stop();
            Debug.WriteLine($"{stopwatch.ElapsedMilliseconds}ms to encrypt");
            testString.Should().NotBe(encrypted);
            stopwatch.Reset();
            stopwatch.Start();
            var decrypted = Encryption.Decrypt(encrypted);
            stopwatch.Stop();
            Debug.WriteLine($"{stopwatch.ElapsedMilliseconds}ms to decrypt");
            testString.Should().Be(decrypted);
        }

        [TestMethod]
        public void StringValue_EncryptTenTime_AtLeast2DifferentResultsk()
        {
            var testString = "Jenkies";
            var values = new List<string>();
            foreach (var value in Enumerable.Range(1, 10))
                values.Add(Encryption.Encrypt(testString));

            values = values.Distinct().ToList();
            var distinctValues = values.Count();

            distinctValues.Should().BeGreaterOrEqualTo(2);
            Debug.WriteLine($"{distinctValues} distinct values");
        }

        [TestMethod]
        public void Principal_EncryptedDecrypted_IsOk()
        {
            var principal = new AppPrincipal();
            TestHelper.Randomizer.Randomize(principal);

            var encryped = Encryption.Encrypt(principal);
            var decrypted = Encryption.Decrypt<AppPrincipal>(encryped);
        }
    }
}