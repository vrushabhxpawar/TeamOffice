namespace DemoDownloadPage.Models.Interface
    {
    public interface IDownloadRepository
        {
        public Task<bool> SendEmailOTP (string emailId, string OTP);

        public Task<bool> VerifyEmail (string email, string OTP);

        public Task<bool> SendPhoneOTP (string number, string OTP);

        public Task<bool> VerifyNumber (string mobile, string OTP);

        public Task<string> SaveData (string email, string phone, string OName, string OAddress, string link);

        public Task<string> SendSoftwareLink (string email, string link);
        }
    }
