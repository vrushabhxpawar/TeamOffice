let currentType = "";
let selectedLink = "";

function sendOTP(type) {

    let value = "";

    // For Email 
    if (type == "Email") {

        //emailInput value
        value = $("#emailInput").val();

        // hcekcing if the emailInput value is null
        if (!value || value.trim() === "") {
            Swal.fire({
                title: "Missing Email Id",
                text: "Email field is mandatory.",
                icon: "warning",
                confirmButtonText: "OK"
            })
            return;
        }

        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

        //format checking 
        if (!emailRegex.test(value)) {
            Swal.fire({
                title: "Invalid Email",
                text: "Please enter a valid Email Address",
                icon: "error",
                confirmButtonText: "OK"
            })
            return;
        }

        //since OTP req initiated for email we are pausing, sending the concurrent req's
        $("#emailBtn")
            .html('<span class="spinner-border spinner-border-sm me-2"></span>Sending...')
            .prop("disabled", true);
    }

    // For Phone
    if (type == "Mobile") {
        //PhoneInput value
        value = $("#mobileInput").val();
        //checks for the empty or null value
        if (!value || value.trim() === "") {
            Swal.fire({
                title: "Missing Phone Number",
                text: "Phone Number field is mandatory.",
                icon: "warning",
                confirmButtonText: "OK"
            })
            return;
        }

        //since using ititelphone lib, using the build-in val methods for correct phone format
        if (!iti.isValidNumber()) {
            Swal.fire({
                title: "Invalid Mobile No.",
                text: "Please enter a valid Mobile Number.",
                icon: "error",
                confirmButtonText: "OK"
            })
            return;
        }
        //since OTP req initiated for mobile pausing, sending the concurrent req's
        $("#mobileBtn")
            .html('<span class="spinner-border spinner-border-sm me-2"></span>Sending...')
            .prop("disabled", true);
    }

    // OTP Sending, via calling API
    $.ajax({
        url: "/DownloadForm/SendOTP",
        type: "POST",
        data: {
            type: type,
            medium: value
        },
        // if the API hits success
        success: function (result) {
            // the API returns boolean value
            if (result) { // if(true)

                //for Email
                if (type == "Email") {
                    //display email verify OTP div
                    document.querySelector("#emailOtpBox").style.display = "flex";

                    //set a timer so that the concurrent resend OTP req can be prevented
                    let seconds = 60;
                    const emailBtn = document.querySelector("#emailBtn");
                    emailBtn.disabled = true;
                    emailBtn.value = false;

                    const timer = setInterval(() => {
                        emailBtn.innerHTML = 
                            `<i class="bi bi-arrow-clockwise me-2"></i>Resend OTP in ${seconds}s`;

                        seconds--;

                        if (seconds < 0) {
                            clearInterval(timer);
                            emailBtn.disabled = false;
                            emailBtn.value = true;
                            emailBtn.innerHTML = `<i class="bi bi-arrow-clockwise me-2"></i>Resend OTP`;
                        }
                    }, 1000)
                    //after succesfully OTP, send a toast noti...
                }

                //for mobile
                if (type == "Mobile") {
                    document.querySelector("#phoneOtpBox").style.display = "flex";
                    //set a timer, to prevent concurrent resend OTP req's
                    let seconds = 60;
                    const mobileBtn = document.querySelector("#mobileBtn");

                    mobileBtn.disabled = true;
                    mobileBtn.value = false;

                    const timer = setInterval(() => {
                        mobileBtn.innerHTML =
                            `<i class="bi bi-arrow-clockwise me-2"></i>Resend OTP in ${seconds}s`;

                        seconds--;

                        if (seconds < 0) {
                            clearInterval(timer);
                            mobileBtn.disabled = false;
                            mobileBtn.value = true;
                            mobileBtn.innerHTML =
                                `<i class="bi bi-arrow-clockwise me-2"></i>Resend OTP`;
                        }
                    }, 1000);
                    //toast notificcation after successfull OTP processing.
                }
                toastr.success(`OTP sent to ${value}`);
            }
                else{ //if(result) == false
                toastr.error("Something went wrong while sending OTP, Please try again later!")
            }
        },
        // if any error while hitting API
        error: function (error) {
            toastr.error("Internal Server Error");
        }
    });

}

//verify OTP section
function verifyOTP(type) {

    let value = ""
    let OTP = ""

    //For Email
    if (type === "Email") {
        //value and OTP for email
        value = $("#emailInput").val();
        OTP = $("#emailOtpInput").val();
        //checking the null and empty string constraints
        if (!OTP || OTP.trim() == "") {
            Swal.fire({
                title: "OTP Required",
                text: "Please enter a valid OTP",
                icon: "warning",
                confirmButtonText: "OK"
            })
            return;
        }
        //when initiated the verify OTP req blocking the user to send the req again and again untill res comes.
        $("#verifyEmailBtn")
            .html('<span class="spinner-border spinner-border-sm me-2"></span>Verifying...')
            .prop("disabled", true);
        //also blocking the user to send unnecessary resend OTP req 
        $("#emailBtn").prop("disabled", true);
    }

    //For Phone
    if (type == "Mobile") {
        //value and OTP for mobile field
        value = iti.getNumber();
        OTP = $("#mobileOtpInput").val();
        //validation checking 
        if (!OTP || OTP.trim() == "") {
            Swal.fire({
                title: "OTP Required",
                text: "Please enter a valid OTP",
                icon: "warning",
                confirmButtonText: "OK"
            })
            return;
        }
        //when initiated the verify OTP req blocking the user to send the req again and again untill res comes.
        $("#verifyMobileBtn")
            .html('<span class="spinner-border spinner-border-sm me-2"></span>Verifying...')
            .prop("disabled", true);
        //also blocking the user to send unnecessary resend OTP req 
        $("#mobileBtn").prop("disabled", true);
    }

    //Verifying
    $.ajax({
        url: "/DownloadForm/VerifyOTP",
        type: "POST",
        data: {
            OTP: OTP,
            type: type,
            value: value
        },
        success: function (result) {

            if (result) { // true

                if (type == "Email") { //email

                    document.querySelector("#emailOtpBox").style.display = "none"; //hide the OTP div
                    document.querySelector("#emailBtn").style.display = "none"; // hide the send OTP btn
                    document.querySelector("#successEmail").innerHTML = `<i class="bi bi-check-circle-fill"></i> Email Id Verified.`; // display a val
                    document.querySelector("#emailInput").readOnly = true; // make it readonly only after verifying
                    toastr.success(`Email Id Verified`);// toast notification
                }

                if (type == "Mobile") {//mobile

                    document.querySelector("#phoneOtpBox").style.display = "none";
                    document.querySelector("#mobileBtn").style.display = "none";
                    document.querySelector("#successMobile").innerHTML = `<i class="bi bi-check-circle-fill"></i> Phone Number Verified.`;
                    document.querySelector("#mobileInput").readOnly = true;
                    toastr.success(`Phone Number Verified`);
                }
            }
            else {// false
                if (type == "Email") { //email 
                    $("#emailOtpVal").text("Incorrect OTP. Please try again."); // display  a val for verify OTP
                    $("#verifyEmailBtn").html('<i class="bi bi-check-circle me-2"></i>Verify Email').prop("disabled", false);// toggling the disabled btn

                    const emailBtn = document.querySelector("#emailBtn");

                    if (emailBtn.value == "true") {
                        emailBtn.disabled = false; // so that the resend OTP doesn't get effected
                    }
                }
                if (type == "Mobile") {
                    $("#mobileOtpVal").text("Incorrect OTP. Please try again.");
                    $("#verifyMobileBtn").html('<i class="bi bi-check-circle me-2"></i>Verify Phone').prop("disabled", false);
                    const mobileBtn = document.querySelector("#mobileBtn");
                    
                    if (mobileBtn.value == "true") {
                        mobileBtn.disabled = false;
                    }
                }
            }
        },
        error: function () {
            toastr.error(`Internal server error`);
        }
    });
}

function handleFormSubmit(event) {
    event.preventDefault();
    //extracting the readonly value of email and phone
    let e = $("#emailInput").prop("readonly") 
    let m = $("#mobileInput").prop("readonly")
    // OName and OAddress values
    let on = $("#OrganizationName").val();
    let oa = $("#OrganizationAddress").val();

    //checking if OName or OAddress is not empty
    if (on.trim() == "" || oa.trim() == "") {
        Swal.fire({
            title: "Organization Details Missing",
            text: "Please enter your Organization Name and Address",
            icon: "info",
            confirmButtonText: "OK"
        })
        return;
    }

    // checking if the readonly only's of both email and phone is true
    if (!e || !m) {
        Swal.fire({
            title: "Verification Pending",
            text: "Please verify your Email and Mobile Number.",
            icon: "warning",
            confirmButtonText: "OK"
        })
        return;
    }

    //since submit form is requested let's block the submit btn to avoid duplicate req's
    $(".request").html(`<span class="spinner-border spinner-border-sm me-2"></span>Requesting...`).prop("disabled", true);

    const token = document.querySelector(
        'input[name="__RequestVerificationToken"]'
    ).value;

    $.ajax({
        url: "/DownloadForm/InformationForm",
        type: "POST",
        headers: {
            RequestVerificationToken: token
        },
        data: {
            email: $("#emailInput").val(),
            phone: iti.getNumber(),
            OName: on,
            OAddress: oa,
            link: $("#SelectedLink").val()
        },
        success: function (res) {
            //display the thankyou partial View
            $(".main").html(res);
        },
        error: function (error) {
            toastr.error("Internal server error.")
            $(".request").html(`<i class="bi bi-download"></i> Request`).prop("disabled", false);
        }
    })


}
