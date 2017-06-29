using Core;
using Core.Models.Identity;
using System;
using System.Collections.Generic;
namespace Core.Operations.Roles.Extras
{
    public struct RoleMessages
    {
        public const string AddOk= "Role added successfully.";
        public const string UpdateOk= "Role updated successfully.";
        public const string DeleteOk= "Role deleted successfully.";
        public const string NotFound = "Role not found.";
        
        public const string NameTooLong = "128 characters or less";
        
    }
}
