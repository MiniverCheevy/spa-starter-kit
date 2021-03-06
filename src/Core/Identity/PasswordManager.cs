﻿using System;
using System.Security.Cryptography;
using System.Text;
using Core.Models.Identity;

namespace Core.Identity
{
    public class PasswordManager
    {
        public string CreateSalt()
        {
            var buffer = new byte[32];
            new RNGCryptoServiceProvider().GetBytes(buffer);
            return Convert.ToBase64String(buffer);
        }

        public string CreateHash(string clearText, string salt)
        {
            var toBeHashed = clearText + salt;
            var bytValue = Encoding.UTF8.GetBytes(toBeHashed);

            var hashAlg = new SHA512CryptoServiceProvider();
            var bytHash = hashAlg.ComputeHash(bytValue);

            return Convert.ToBase64String(bytHash);
        }

        public bool Compare(string cleartext, string salt, string hash)
        {
            if (string.IsNullOrWhiteSpace(cleartext)
                || string.IsNullOrWhiteSpace(salt)
                || string.IsNullOrWhiteSpace(hash))
                return false;

            var newHash = CreateHash(cleartext, salt);
            return newHash == hash;
        }

        public User Create(string userName, string password)
        {
            var user = new User
            {
                UserName = userName,
                Salt = CreateSalt()
            };
            user.PasswordHash = CreateHash(password, user.Salt);
            return user;
        }


        public void BuildPasswordAndSalt(ref User model, string password)
        {
            model.Salt = CreateSalt();
            model.PasswordHash = CreateHash(password, model.Salt);
        }
    }
}