namespace BlazorStandaloneApp.Domain

/// <summary>
/// Represents the user of the BK Bank.
/// Contains authentication used for local login.
/// </summary>

{
    public class User
    {
        public string Username { get; set; }
        public string Pin { get; set; }

        public User(string username, string pin)
        {
            Username = username;
            Pin = pin;
        }
    }
}  