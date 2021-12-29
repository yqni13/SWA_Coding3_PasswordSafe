namespace PasswordSafeConsole
{
    internal class PasswordInfo
    {
        public string Password { get; private set; }
        public string PasswordName { get; private set; }

        public PasswordInfo(string password, string passwordName)
        {
            this.Password = password;
            this.PasswordName = passwordName;
        }
    }
}