$(function () {


 
    $("#Name").keyup(function () {
        CheckUsername();
    });
    $("#Password").keyup(function () {
        CheckPassword();
    });
    $("#Email").keyup(function () {
        CheckEmail();
    });

    function CheckUsername() {
        var username = $("#Name").val();
        var usernamePattern = new RegExp(/^[a-zA-Z0-9]/);
        var outputString = "";
        $("#username_error_message").hide();

        if (username.length < 5 || username.length > 20) {
            var outputString = "The length should be between 5 - 20 characters\ ";
        }
        if (username.match(usernamePattern) === true) {
            outputString = " Allowed characters: [a-Z],[0-9]\ ";
        } 

        if (outputString.length > 0) {
            error_username = true;
            $("#username_error_message").html(outputString);
            $("#username_error_message").show();
        }
        else {
            error_username = false;
        }

           
    }
    function CheckPassword() {
        var password = $("#Password").val();       
        var outputString = "";
        var passwordPattern1 = new RegExp(/[a-z]/);
        var passwordPattern2 = new RegExp(/[A-Z]/);
        var passwordPattern3 = new RegExp(/[0-9]/);
        $("#password_error_message").hide();
        if (password.length < 8 || password.length > 24) {
            outputString += "should be between 8 - 24 characters\ ";
        } else {
        }
        if (passwordPattern1.test(password) === false) {
            outputString += "The string must contain at least 1 lowercase alphabetical character\ ";
        } else if (passwordPattern2.test(password)===false) {
            outputString += "The string must contain at least 1 uppercase alphabetical character\ ";
        
        } else if (passwordPattern3.test(password) === false) {
            outputString += "The string must contain at least 1 numeric character\ ";
        }
        if (outputString.length > 0) {
            error_password = true;
            $("#password_error_message").html(outputString);
            $("#password_error_message").show();
        }
        else {
            error_password = false;
        }
    }
    function CheckEmail() {
        var email = $("#Email").val();
        var emailPattern = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);
        $("#email_error_message").hide();
        if (emailPattern.test(email) === false) {
            error_email = true;
            $("#email_error_message").html("Invalid email address");
            $("#email_error_message").show();
        }
        else {
            error_email = false;
        }
    }

});