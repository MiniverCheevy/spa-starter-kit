using Core;
using Core.Models.Scratch;
using System;
using System.Collections.Generic;
namespace Core.Operations.Members.Extras
{
    public struct MemberMessages
    {
        public const string AddOk= "Member added successfully.";
        public const string UpdateOk= "Member updated successfully.";
        public const string DeleteOk= "Member deleted successfully.";
        public const string NotFound = "Member not found.";
        
        public const string NameTooLong = "128 characters or less";
        public const string TitleTooLong = "128 characters or less";
        
    }
}
