using System;

namespace Constants.Authorization
{
    public class Authorization
    {
        public enum Roles
        {
            Administrator,
            Manager,
            Editor,
            User
        }
        public const string defatul_firstName = "Bintang";
        public const string default_lastname = "Adesa";
        public const string default_username = "user";
        public const string default_email = "bintang@gmail.com";
        public const string default_password = "Pa$$w0rd";
        public const Roles default_role = Roles.User;
    }
}