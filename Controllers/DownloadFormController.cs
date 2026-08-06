using DemoDownloadPage.Models.Interface;
using DNTCaptcha.Core;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace DemoDownloadPage.Controllers
    {
    public class DownloadFormController : Controller
        {

        private readonly IDownloadRepository _downloadRepository;
        private readonly IConfiguration _config;
        private readonly IDataProtector _dataProtector;
        public DownloadFormController (IDownloadRepository downloadRepositoy, IConfiguration config, IDataProtectionProvider provider)
            {
            _downloadRepository = downloadRepositoy;
            _config = config;
            _dataProtector = provider.CreateProtector("SoftwareDownloadLinks");
            }

        [HttpGet]
        public IActionResult InformationForm ()
            {
            return View();
            }

        [HttpPost]
        [ValidateDNTCaptcha(ErrorMessage = "Please enter the correct security code.")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InformationForm (string email, string phone, string OName, string OAddress, string link)
            {
            try
                {
                string actualLink = _dataProtector.Unprotect(link);
                //await _downloadRepository.SaveData(email, phone, OName, OAddress, actualLink);
                //await _downloadRepository.SendSoftwareLink(email, actualLink);
                //await Task.Delay(3000);
                return PartialView("_ThankYou", ViewBag.link = actualLink);
                }
            catch (Exception ex)
                {
                Console.WriteLine(ex.ToString()); Console.WriteLine(ex);

                return StatusCode(500, ex.Message);
                }
            }

        [HttpGet]
        public IActionResult SoftwareDownloadPage ()
            {
            var softwareLinks = _config.GetSection("SoftwareLinks");
            var model = new SoftwareCardViewModel
                {
                Cards = new List<SoftwareCard>
     {
        new SoftwareCard
        {
            Title = "TEAM OFFICE WINDOWS SOFTWARE",
            Description = "Main and full setups for Windows desktops",
            Icon = "bi-pc-display",
            Downloads = new List<DownloadItem>
            {
                new() { Title = "Team Office Main Setup For Windows", Size = "125 MB", Url = _dataProtector.Protect(softwareLinks["Link1"]) },
                new() { Title = "Team Office Full Setup for Windows", Size = "18 MB", Url = _dataProtector.Protect(softwareLinks["Link2"]) },
                new() { Title = "Win Installer 3.1", Size = "18 MB", Url = _dataProtector.Protect(softwareLinks["Link3"]) }
            }
        },

        new SoftwareCard
        {
            Title = "DOT NET FRAMEWORK",
            Description = "Runtime frameworks required by the application",
            Icon = "bi-windows",
            Downloads = new List<DownloadItem>
            {
                new() { Title = "Dot Net 3.5", Size = "111 MB", Url = _dataProtector.Protect(softwareLinks["Link4"]) },
                new() { Title = "Dot Net 4.0", Size = "55 MB", Url = _dataProtector.Protect(softwareLinks["Link5"]) },
                new() { Title = "Dot Net 4.0 Client", Size = "58 MB", Url = _dataProtector.Protect(softwareLinks["Link6"]) }
            }
        },

        new SoftwareCard
        {
            Title = "SDK DOWNLOAD",
            Description = "Device SDKs for biometric and RFID hardware",
            Icon = "bi-box-seam",
            Downloads = new List<DownloadItem>
            {
                new() { Title = "UHF Reader SDK", Size = "82 MB", Url = _dataProtector.Protect(softwareLinks["Link7"]) },
                new() { Title = "Z500V2 SDK", Size = "34 MB", Url = _dataProtector.Protect(softwareLinks["Link8"]) },
                new() { Title = "Z902 SDK", Size = "6 MB", Url = _dataProtector.Protect(softwareLinks["Link9"]) },
                new() { Title = "Z300AC SDK", Size = "2 MB", Url = _dataProtector.Protect(softwareLinks["Link10"]) }
            }
        },

        new SoftwareCard
        {
            Title = "OTHER SOFTWARE",
            Description = "Support utilities and helper tools",
            Icon = "bi-puzzle",
            Downloads = new List<DownloadItem>
            {
                new() { Title = "Upload Name in Decice for Cloud Software", Size = "12 MB", Url = _dataProtector.Protect(softwareLinks["Link11"]) },
                new() { Title = "Ultra Viewer", Size = "26 MB", Url = _dataProtector.Protect(softwareLinks["Link12"]) },
                new() { Title = "Guard Tour System Software", Size = "15 MB", Url = _dataProtector.Protect(softwareLinks["Link13"]) },
                new() { Title = "Team Office Guard Tour Software For Win10", Size = "15 MB", Url = _dataProtector.Protect(softwareLinks["Link14"]) }
            }
        },

        new SoftwareCard
        {
            Title = "DOOR CONTROLLER SOFTWARE",
            Description = "Door controller management applications",
            Icon = "bi-door-open",
            Downloads = new List<DownloadItem>
            {
                new() { Title = "Door Controller(Black PCB) Software", Size = "95 MB", Url = _dataProtector.Protect(softwareLinks["Link15"]) },
                new() { Title = "Door Controller(Green PCB) Software", Size = "14 MB", Url = _dataProtector.Protect(softwareLinks["Link16"]) },
            }
        },

        new SoftwareCard
        {
            Title = "SQL SERVER AND STUDIO",
            Description = "Database server and management studio",
            Icon = "bi-database",
            Downloads = new List<DownloadItem>
            {
                new() { Title = "SQL Server 2005 Express(x86)", Size = "285 MB", Url = _dataProtector.Protect(softwareLinks["Link17"]) },
                new() { Title = "SQL Server 2005 Express(x64)", Size = "650 MB", Url = _dataProtector.Protect(softwareLinks["Link18"]) },
                 new() { Title = "Studio SQL 2005(x86)", Size = "285 MB", Url = _dataProtector.Protect(softwareLinks["Link19"])},
                new() { Title = "SQL Server 2005 Express(x86,x64)", Size = "3 MB", Url = _dataProtector.Protect(softwareLinks["Link20"]) },
                new() { Title = "Studio SQL 2005(x86)", Size = "3 MB", Url = _dataProtector.Protect(softwareLinks["Link21"]) },
                new() { Title = "Studio SQL 2005(x64)", Size = "3 MB", Url = _dataProtector.Protect(softwareLinks["Link22"]) },
            }
        },

        new SoftwareCard
        {
            Title = "REPORTS SOFTWARE",
            Description = "Reporting tools and report viewer",
            Icon = "bi-file-earmark-bar-graph",
            Downloads = new List<DownloadItem>
            {
                new() { Title = "Report Viewer For Windows Old Version", Size = "48 MB", Url = _dataProtector.Protect(softwareLinks["Link23"]) },
                new() { Title = "Report Viewer For Windows", Size = "75 MB", Url = _dataProtector.Protect(softwareLinks["Link24"]) }
            }
        },

        new SoftwareCard
        {
            Title = "PPT / CATALOG DOCUMENTS",
            Description = "Presentations, brochures and catalogs",
            Icon = "bi-file-earmark-ppt",
            Downloads = new List<DownloadItem>
            {
                new() { Title = "Team Office Access Control Accessories PPT", Size = "18 MB", Url = _dataProtector.Protect(softwareLinks["Link25"]) },
                new() { Title = "Team Office Catelog Download", Size = "11 MB", Url = _dataProtector.Protect(softwareLinks["Link26"]) },
                new() { Title = "Team Office Cloud Software Report", Size = "7 MB", Url = _dataProtector.Protect(softwareLinks["Link27"]) },
                new() { Title = "Sample Report of Team Office Attendnce Software", Size = "7 MB", Url = _dataProtector.Protect(softwareLinks["Link28"]) }
            }
        }
    }
                };

            return View(model);
            }

        [HttpPost]
        public async Task<bool> SendOTP (string type, string medium)
            {
            Random random = new Random();
            var otp = random.Next(1001, 9999).ToString();


            if (type == "Email")
                {
                await Task.Delay(3000);
                return true;
                //bool res = await _downloadRepository.SendEmailOTP(medium, otp);
                //return res;
                }
            else
                {
                await Task.Delay(3000);
                return true;
                //var value = medium.Replace("+", "");
                //bool res = await _downloadRepository.SendPhoneOTP(value, otp);
                //return res;
                }

            return false;
            }

        [HttpPost]
        public async Task<bool> VerifyOTP (string otp, string type, string value)
            {
            if (type == "Mobile")
                {
                await Task.Delay(3000);
                //var val = value.Replace("+", "");
                return true;
                //var res = await _downloadRepository.VerifyNumber(val, otp);
                //return res;
                }
            if (type == "Email")
                {
                await Task.Delay(3000);
                return true;
                //var res = await _downloadRepository.VerifyEmail(value, otp);
                //return res;
                }
            return false;
            }

        [HttpGet]
        public IActionResult ThankYou ()
            {
            return View("_ThankYou", ViewBag.Link = "https://vrushabh.info");
            }
        }
    }
