$(function () {

    var input_email = "#User_Email";
    var input_username = "#User_Name";
    var input_password = "#User_Password";
    var error_username = true;
    var error_password = true;
    var error_email = true;

    var tooltip_email_shown = false;
    var tooltip_username_shown = false;
    var tooltip_password_shown = false;

    var color_correct = "greenyellow";
    var color_incorrect = "red";

    $('[data-toggle="tooltip"]').tooltip();

    $(input_password)
        .attr('data-original-title', "")
        .tooltip('fixTitle').tooltip('hide');


    $(input_email)
        .attr('data-original-title', "")
        .tooltip('fixTitle').tooltip('hide');

    $(input_username)
        .attr('data-original-title', "")
        .tooltip('fixTitle').tooltip('hide');




    $('#btn_Create').click(function () {
        CheckUsername();
        CheckPassword();
        CheckEmail();
        //    HideClientMessages();
    });

    $(input_username).keyup(function () {
        CheckUsername();
    });
    $(input_password).keyup(function () {
        CheckPassword();
    });
    $(input_email).keyup(function () {
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
        var username = $(input_username).val();


        var usernamePattern = new RegExp("^[a-zA-Z0-9]+$");
        var outputString = "";
        $("#username_error_message").hide();

        if (username.length < 5 || username.length > 20) {
            tooltip_username_shown = false;
            outputString = "The length should be between 5 - 20 characters! ";
        }
        if (usernamePattern.test(username) === false) {
            tooltip_username_shown = false;
            outputString += "Allowed characters: a-z, A-Z, 0-9";
        }

        if (outputString.length > 0) {
            error_username = true;
            //$("#username_error_message").html(outputString);
            //$("#username_error_message").show();
            $(':input[type="submit"]').prop('disabled', true);
            $(input_username)
                .attr('data-original-title', outputString)
                .tooltip('fixTitle');

            if (tooltip_username_shown === false) {
                $(input_username).tooltip('show');
                tooltip_username_shown = true;
            }
            $(input_username).css("border-color", color_incorrect);

        }
        else {
            tooltip_username_shown = false;
            $(input_username).css("border-color", color_correct);
            $(input_username)
                .attr('data-original-title', "")
                .tooltip('fixTitle').tooltip('hide');
            error_username = false;
            if (error_username === false && error_password === false && error_email === false) {
                $(':input[type="submit"]').prop('disabled', false);
            }
        }
        //HideClientMessages();
    }
    function CheckPassword() {
        var password = $(input_password).val();
        var outputString = "";
        var passwordPattern1 = new RegExp(/[a-z]/);
        var passwordPattern2 = new RegExp(/[A-Z]/);
        var passwordPattern3 = new RegExp(/[0-9]/);
        $("#password_error_message").hide();
        if (password.length < 8 || password.length > 24) {
            tooltip_password_shown = false;
            outputString = "The password should be between 8 - 24 characters! ";
        }
        if (passwordPattern1.test(password) === false ||
            passwordPattern3.test(password) === false ||
            passwordPattern2.test(password) === false) {
            tooltip_password_shown = false;
            outputString += "The password must contain a number, a lower and upper case character!";
        }


        if (outputString.length > 0) {
            error_password = true;
            //$("#password_error_message").html(outputString);
            //$("#password_error_message").show();
            $(':input[type="submit"]').prop('disabled', true);
            $(input_password)
                .attr('data-original-title', outputString)
                .tooltip('fixTitle');

            if (tooltip_password_shown === false) {
                $(input_password).tooltip('show');
                tooltip_password_shown = true;
            }

            $(input_password).css("border-color", color_incorrect);
        }
        else {
            tooltip_password_shown = false;
            //console.log("error_password2" + error_password);
            $(input_password).css("border-color", color_correct);
            error_password = false;
            $(input_password)
                .attr('data-original-title', "")
                .tooltip('fixTitle').tooltip('hide');
            if (error_username === false && error_password === false && error_email === false) {
                $(':input[type="submit"]').prop('disabled', false);
            }
        }
        //HideClientMessages();
    }
    function CheckEmail() {
        var email = $(input_email).val();
        var emailPattern = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);
        $("#email_error_message").hide();
        if (emailPattern.test(email) === false) {
            error_email = true;
            //$("#email_error_message").html("Invalid email address!");
            //$("#email_error_message").show();
            $(':input[type="submit"]').prop('disabled', true);
            $(input_email)
                .attr('data-original-title', "Invalid email address!")
                .tooltip('fixTitle');
            $(input_email).tooltip

            if (tooltip_email_shown === false) {
                tooltip_email_shown = true;
                $(input_email).tooltip('show');
            }
            $(input_email).css("border-color", color_incorrect);
        }
        else {
            $(input_email).css("border-color", color_correct);
            $(input_email)
                .attr('data-original-title', "")
                .tooltip('fixTitle').tooltip('hide');

            tooltip_email_shown = false;
        }

        error_email = false;
        if (error_username === false && error_password === false && error_email === false) {
            $(':input[type="submit"]').prop('disabled', false);
        }
    }
    //HideClientMessages();


});
