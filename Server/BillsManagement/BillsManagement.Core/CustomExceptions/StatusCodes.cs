namespace BillsManagement.Core.CustomExceptions
{
    public enum StatusCodes
    {
        UnknownError = 0,
        ConnectionLost = 100,

        #region Authentication status codes

        SessionExpired = 600,
        RegistrationFailed = 601,
        LoginFailed = 602,

        #endregion

        #region Charges status codes

        FailedChargeGeneration = 650,
        FailedGettingCharges = 651

        #endregion
    }
}
