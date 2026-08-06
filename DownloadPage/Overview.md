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
  - DownloadFormController.cs
- Models/
  - Interface/
    - IDownloadRepository.cs
  - Repository/
    - DownloadRepository.cs
- Services/
  - MailService.cs
  - SmsService.cs
- EmailTemplates/
  - OtpEmail.html
  - SoftwareLinkEmail.html
- ViewModels
- Views/
  - DownloadForm/
      - SoftwareDownloadPage.cshtml
      - _ThankYou.cshtml
  - Shared
      - _Layout.cshtml
      - _Header.cshtml
      - _Footer.cshtml
- wwwroot/
    - css
        - Stylesheet.css
    - js
        - Script.js
    - html
