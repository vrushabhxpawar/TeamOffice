using DemoDownloadPage.Models.Interface;
using DemoDownloadPage.Services;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace DemoDownloadPage.Models.Repository
    {
    public class DownloadRepository : IDownloadRepository
        {

        public readonly MailService _mailService;
        public readonly SmsService _smsService;
        public readonly ConnString _conn;
        public DownloadRepository (MailService mailService, SmsService smsService, IOptions<ConnString> conn)
            {
            _mailService = mailService;
            _smsService = smsService;
            _conn = conn.Value;
            }

        public async Task<bool> SendEmailOTP (string emailId, string OTP)
            {
            try
                {
                using SqlConnection conn = new SqlConnection(_conn.dbString);
                using SqlCommand cmd = new SqlCommand();

                var subject = "Verify your email address for TeamOffice";
                await _mailService.SendEmailAsync(emailId, subject, "OtpEmail", new Dictionary<string, string> { { "OTP", OTP } });


                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = @"IF EXISTS (SELECT 1 FROM emailOtpTbl WHERE email = @email)
                                       BEGIN
                                       UPDATE emailOtpTbl
                                       SET otp = @otp,
                                       instDate = @instDate
                                       WHERE email = @email
                                       END
                                       ELSE
                                       BEGIN
                                       INSERT INTO emailOtpTbl (email, otp, instDate)
                                       VALUES (@email, @otp, @instDate)
                                       END";

                cmd.Parameters.AddWithValue("@email", emailId);
                cmd.Parameters.AddWithValue("@otp", OTP);
                cmd.Parameters.AddWithValue("@instDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                await conn.OpenAsync();
                int aff = await cmd.ExecuteNonQueryAsync();
                await conn.CloseAsync();

                if (aff == 1) return true;
                else return false;
                }
            catch (Exception ex)
                {
                Console.WriteLine(ex.ToString());
                return false;
                }
            }

        public async Task<bool> VerifyEmail (string email, string OTP)
            {
            try
                {
                using SqlConnection conn = new SqlConnection(_conn.dbString);
                using SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT otp FROM emailOtpTbl WHERE email = @email";

                cmd.Parameters.AddWithValue("@email", email);

                await conn.OpenAsync();

                object res = await cmd.ExecuteScalarAsync();

                if (res == null)
                    {
                    return false;
                    }
                var savedOTP = res.ToString();
                return savedOTP == OTP;
                }
            catch (Exception ex)
                {
                Console.WriteLine(ex.ToString());
                return false;
                }
            }

        public async Task<bool> SendPhoneOTP (string mobile, string OTP)
            {
            try
                {
                if (mobile == null || OTP == null)
                    {
                    Console.WriteLine("Number/OTP is null");
                    return false;
                    }

                await _smsService.SendSmsAsync(mobile, OTP);

                using SqlConnection conn = new SqlConnection(_conn.dbString);
                using SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = @"IF EXISTS (SELECT 1 FROM mobileOtpTbl WHERE mobile = @mobile)
                                       BEGIN
                                       UPDATE mobileOtpTbl
                                       SET otp = @otp,
                                       instDate = @instDate
                                       WHERE mobile = @mobile
                                       END
                                       ELSE
                                       BEGIN
                                       INSERT INTO mobileOtpTbl (mobile, otp, instDate)
                                       VALUES (@mobile, @otp, @instDate)
                                       END"; ;

                cmd.Parameters.AddWithValue("@mobile", mobile);
                cmd.Parameters.AddWithValue("@otp", OTP);
                cmd.Parameters.AddWithValue("@instDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                return true;
                }
            catch (Exception ex)
                {
                Console.WriteLine(ex);
                return false;
                }
            }

        public async Task<bool> VerifyNumber (string mobile, string OTP)
            {
            using SqlConnection conn = new SqlConnection(_conn.dbString);
            using SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT otp FROM mobileOtpTbl WHERE mobile = @mobile";

            cmd.Parameters.AddWithValue("@mobile", mobile);

            await conn.OpenAsync();
            object res = await cmd.ExecuteScalarAsync();
            var savedOTP = res.ToString();
            if (res == null) return false;
            return savedOTP == OTP;
            }

        public async Task<string> SaveData (string email, string phone, string OName, string OAddress, string link)
            {
            try
                {
                using SqlConnection conn = new SqlConnection(_conn.dbString);
                using SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "INSERT INTO formData ([org_name],[org_address],[mobile],[email],[link],[instDate]) VALUES (@OName, @OAddress, @mobile, @email, @link, @instDate)";

                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@OName", OName);
                cmd.Parameters.AddWithValue("@OAddress", OAddress);
                cmd.Parameters.AddWithValue("@mobile", phone);
                cmd.Parameters.AddWithValue("@link", link);
                cmd.Parameters.AddWithValue("@instDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                await conn.OpenAsync();
                var aff = await cmd.ExecuteNonQueryAsync();
                await conn.CloseAsync();

                if (aff == 0)
                    {
                    return "notSaved";
                    }
                return "saved";
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }

        public async Task<string> SendSoftwareLink (string email, string link)
            {
            var subject = "Download Link for TeamOffice Software.";
            var res = await _mailService.SendEmailAsync(email, subject, "SoftwareLinkEmail", new Dictionary<string, string> { { "link", link } });

            if (res) return "sent";
            return "notSent";
            }
        }
    }
