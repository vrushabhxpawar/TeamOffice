# Script.js 

## SendOTP (type)
- Based on type, check if the value is not null or not empty.
  - If so, then fire a swal that fields cannot be empty. 
  - If not, check for the correct format for either email or phone number.
- Call an Ajax :
  - url : /DownloadForm/SendOTP
  - type : Post
  - data : type , medium (email address or mobile number).
  - success
    - if result is true, show the OTP box either emailOtpBox or mailOtpBox
    - toast notification
  - error : show a Internal server error
    
## VerifyOTP (type) 
- Based on type, check if the OTP value is not null or not empty.
  - If so, then fire a swal that OTP is required.
- Call an Ajax :
    - url : /DownloadForm/VerifyOTP
    - type : Post,
    - data : OTP, type, value (email address or phone number);
    - success :
      - if result is true, make the email or phone number field readonly and hide the OTP box and send OTP btn.
      - Send a toast notification.
    - error : show a Internal server error

## HandleFormSubmit 
- checks if all the fields are not null and not empty.
- checks if phone Number and email is readonly or not.
- calls Ajax :
  - url : /DownloadForm/InformationForm
  - type : post
  - success : display a ThankYou partial view.
  - error : Internal server error
    
