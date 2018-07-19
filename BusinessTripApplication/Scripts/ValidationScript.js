$(function () {


    var error_username = true;
    var error_password = true;
    var error_email = true;

    $('#btn_Create').click(function () {
        CheckUsername();
        CheckPassword();
        CheckEmail();
    //    HideClientMessages();
    });

    $("#User_Name").keyup(function () {
        CheckUsername();
    });
    $("#User_Password").keyup(function () {
        CheckPassword();
    });
    $("User_#Email").keyup(function () {
        CheckEmail();
    });

    function HideClientMessages() {
        //if ($("Name-error").length) {
            $("#username_error_message").hide();
       // }
        if ($("Email-error").val().length() !== 0) {
            $("#password_error_message").hide();
        }
        if ($("text-danger-password").val().length() !== 0) {
            $("#email_error_message").hide();
        }
    }

    function CheckUsername() {
        var username = $("#User_Name").val();
        var usernamePattern = new RegExp("^[a-zA-Z0-9]+$");
        var outputString = "";
        $("#username_error_message").hide();

        if (username.length < 5 || username.length > 20) {
            outputString = "The length should be between 5 - 20 characters";
        }
        else if (usernamePattern.test(username) === false) {
            outputString = "Allowed characters: a-z, A-Z, 0-9";
        } 

        if (outputString.length > 0) {
            error_username = true;
            $("#username_error_message").html(outputString);
            $("#username_error_message").show();
            $(':input[type="submit"]').prop('disabled', true);
        }
        else {
            error_username = false;
            if (error_username === false && error_password === false && error_email === false) {
                $(':input[type="submit"]').prop('disabled', false);
            }
        }
        //HideClientMessages();
    }
    function CheckPassword() {
        var password = $("#User_Password").val();       
        var outputString = "";
        var passwordPattern1 = new RegExp(/[a-z]/);
        var passwordPattern2 = new RegExp(/[A-Z]/);
        var passwordPattern3 = new RegExp(/[0-9]/);
        $("#password_error_message").hide();
        if (password.length < 8 || password.length > 24) {
            outputString = "should be between 8 - 24 characters";
        } 
        else if (passwordPattern1.test(password) === false) {
            outputString = "The string must contain at least 1 lowercase alphabetical character";
        } else if (passwordPattern2.test(password) === false) {
            outputString = "The string must contain at least 1 uppercase alphabetical character";
        } else if (passwordPattern3.test(password) === false) {
            outputString = "The string must contain at least 1 numeric character";
        }
        //console.log("error_password1" + error_password);
        if (outputString.length > 0) {
            error_password = true;
            $("#password_error_message").html(outputString);
            $("#password_error_message").show();
            $(':input[type="submit"]').prop('disabled', true);
        }
        else {
            //console.log("error_password2" + error_password);
            error_password = false;
            if (error_username === false && error_password === false && error_email === false) {
                $(':input[type="submit"]').prop('disabled', false);
            }
        }
        //HideClientMessages();
    }
    function CheckEmail() {
        var email = $("#User_Email").val();
        var emailPattern = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);
        $("#email_error_message").hide();
        if (emailPattern.test(email) === false) {
            error_email = true;
            $("#email_error_message").html("Invalid email address");
            $("#email_error_message").show();
            $(':input[type="submit"]').prop('disabled', true);
        }
        else {
            error_email = false;
            if (error_username === false && error_password === false && error_email === false) {
                $(':input[type="submit"]').prop('disabled', false);
            }
        }
        //HideClientMessages();
    }

});