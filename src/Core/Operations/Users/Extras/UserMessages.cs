namespace Core.Operations.Users.Extras
{
    public struct UserMessages
    {
        public const string AddOk = "User added successfully.";
        public const string UpdateOk = "User updated successfully.";
        public const string DeleteOk = "User deleted successfully.";
        public const string NotFound = "User not found.";

        public const string UserNameTooLong = "128 characters or less";
        public const string FirstNameTooLong = "128 characters or less";
        public const string LastNameTooLong = "128 characters or less";
        public const string SaltTooLong = "128 characters or less";
        public const string PasswordHashTooLong = "128 characters or less";
        public const string RoleRequired = "All users must have at least 1 role";
    }
}