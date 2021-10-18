using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string AddSuccess = "Successfully added";
        public static string AddFailure = "Failed to add";

        public static string DeleteSuccess = "Successfully deleted";
        public static string DeleteFailure = "Failed to delete";

        public static string UpdateSuccess = "Successfully updated";
        public static string UpdateFailure = "Failed to updated";

        public static string UserNotFound = "User not found";
        public static string PasswordError = "Wrong password";
        public static string UserAlreadyExists = "User already exists";

        public static string SuccessfulLogin = "Login successful";
        public static string SuccessfulRegister = "Successfully registered";
        public static string AccessTokenCreated = "Access token created";
    }
}
