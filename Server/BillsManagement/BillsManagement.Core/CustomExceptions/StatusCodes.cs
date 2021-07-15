namespace BillsManagement.Core.CustomExceptions
{
    public enum StatusCodes
    {
        UnknownError = 0,
        ConnectionLost = 100,
        SessionExpired = 101,
        RegistrationFailed = 700,
        LoginFailed = 750
    }
}
