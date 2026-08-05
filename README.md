# Download Page
The Download Page allows users to browse available software and documents. Users can request a download by filling out a form. After successful verification, an email containing the download link is sent automatically.

## Tech Stack : 
- ASP.NET Core MVC
- SQL Server
- Bootstrap
- jQuery
- JavaScript
- Ajax

## Folder Structure 
- Controllers/
- Models/
  - Interface
  - Repository
- Services/
  - EmailService
  - SmsService
- EmailTemplates/
- ViewModels
- Views/
- wwwroot/

## Download Controller 
### Action Methods 

1. SoftwareDownloadPage [HttpGet]
2. InformationForm [HttpGet]
3. InfomationForm [HttpPost]
4. SendOTP [HttpPost]
5. verifyOTP [HttpPost]

