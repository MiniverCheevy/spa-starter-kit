using Core;
using Core.Models;
using System;
namespace Core.Operations.ApplicationSettings.Extras
{
    public struct ApplicationSettingMessages
    {
        public const string AddOk= "Application Setting added successfully.";
        public const string UpdateOk= "Application Setting updated successfully.";
        public const string DeleteOk= "Application Setting deleted successfully.";
        public const string NotFound = "Application Setting not found.";
        
        public const string NameTooLong = "128 characters or less";
        public const string ValueTooLong = "128 characters or less";
        
    }
}
