namespace OMM.Services.SendGrid.Common
{
    public static class Constants
    {
        public const string REGISTRATION_SUBJECT = "Office Manager mini platform";

        public const string PLATFORM_PREFIX = "OMM - {0}";

        public const string DEFAULT_SENDER_EMAIL = "doncho.vasilev@gmail.com";

        public const string DEFAULT_SENDER_NAME = "Admin";

        public const string HTML_TEXT = "<div><strong><i>You have been granted with access to the Office Manager mini platform with:</i></strong></div><br><br><div><strong> Email:</strong> {0}</div><div><strong>Password:</strong> {1}</div><div><strong>LoginUrl: </strong> https://localhost:44318/employees/login </div><br><div><i>Later you can change your password from your profile page.</i></div>";
    }
}
