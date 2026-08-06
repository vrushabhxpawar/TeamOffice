# DownloadController 

### SoftwareDownloadPage [*HttpGet*]
Return a view (SoftwareDownloadPage). 

### InformationForm [*HttpPost*]
The form data is received at this action method and further passed to saveData repository method and sendSoftwareLink repository method for IDownloadRepository.

### SendOTP [*HttpPost*]
The Action method is responsible to send OTP on Email or Mobile number depending upon the type. The OTP and email or Mobile n umber is passed to the respective repository method of IDownloadRepository.

### VerifyOTP [*HttpPost*]
The Action method verifies the OTP for both email and mobile Number. The OTP and email or Mobile number is passed to the corresponding repository method whihc checks the saved OTP corresponding to email or mobile number.
