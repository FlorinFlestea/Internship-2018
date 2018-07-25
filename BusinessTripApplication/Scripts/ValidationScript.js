﻿$(function () {


    $('[data-toggle="tooltip"]').tooltip(); 


    var input_username = "#User_Name";
    var input_email = "#User_Email";
    var input_password = "#User_Password";
    var error_username = true;
    var error_password = true;
    var error_email = true;

    var tooltip_username_shown = 0;
    var tooltip_email_shown = false;
    var tooltip_password_shown = false;

    var color_correct = "greenyellow";
    var color_incorrect = "red";

    

    $(input_password)
        .attr('data-original-title', "")
        .tooltip('fixTitle').tooltip('hide');


    $(input_email)
        .attr('data-original-title', "")
        .tooltip('fixTitle').tooltip('hide');

    $(input_username)
        .attr('data-original-title', "")
        .tooltip('fixTitle').tooltip('hide');


   
    /*
    $('#btn_Create').click(function () {
        CheckUsername();
        CheckPassword();
        CheckEmail();
    });
    */

    $(input_username).keyup(function () {
        CheckUsername();
        //CheckPassword();
        //CheckEmail();
    });
    $(input_password).keyup(function () {
       // CheckUsername();
        CheckPassword();
       // CheckEmail();
    });
    $(input_email).keyup(function () {
       // CheckUsername();
       // CheckPassword();
        CheckEmail();
    });


    function SetTextTooltipUsername(text) {
        $(input_username)
            .attr('data-original-title', text)
            .tooltip('fixTitle');
        $(input_username).css("border-color", color_incorrect);
    }

    function SetTextTooltipEmail(text) {
        $(input_email)
            .attr('data-original-title', text)
            .tooltip('fixTitle');
        $(input_email).css("border-color", color_incorrect);
    }


    function SetTextTooltipPassword(text) {
        $(input_password)
            .attr('data-original-title', text)
            .tooltip('fixTitle');
        $(input_password).css("border-color", color_incorrect);
    }

    function DisableButton() {
        //$(':input[type="submit"]').prop('disabled', true);
        //
        if ($('#btn_Create').css('onclick') !== null) {
            $("#btn_Create").onclick = "return false";
        } else {
            $('#btn_Create').attr("onclick", "return false");
            $('#btn_Create').css("onclick", "return false");
        }
        $("#btn_Create").css('opacity', '0.5');
    }

    function EnableButton() {
        $("#btn_Create").css('opacity', '1');
        $("#btn_Create").removeAttr('onclick');
        //$(':input[type="submit"]').prop('disabled', false);
    }

    function HideTooltipUserName() {
        $(input_username)
            .attr('data-original-title', "")
            .tooltip('fixTitle');
        $(input_username).css("border-color", color_correct);
        $(input_username).tooltip('hide');
    }

    function HideTooltipEmail() {
        $(input_email)
            .attr('data-original-title', "")
            .tooltip('fixTitle');
        $(input_email).css("border-color", color_correct);
        $(input_email).tooltip('hide');
    }

    function HideTooltipPassword() {
        $(input_password)
            .attr('data-original-title', "")
            .tooltip('fixTitle');
        $(input_password).css("border-color", color_correct);
        $(input_password).tooltip('hide');
    }
   
    $('#btn_Create').click(function () {
        if ($(input_username).val() === "") {
            SetTextTooltipUsername("Username field is required!");
            $(input_username).tooltip('show');
        }
        if ($(input_email).val() === "") {
            SetTextTooltipEmail("Email field is required!");
            $(input_email).tooltip('show');
        }
        if ($(input_password).val() === "") {
            SetTextTooltipPassword("Password field is required!");
            $(input_password).tooltip('show');
        }
    });

    function CheckUsername() {
        var username = $(input_username).val();
        var usernamePattern = new RegExp("^[a-zA-Z0-9]+$");
        var outputString = "";
        var current_tooltip_username_shown = 0;
        $("#username_error_message").hide();


        if (usernamePattern.test(username) === false) {
            outputString += "Allowed characters: a-z, A-Z, 0-9";
            current_tooltip_username_shown = 1;
        } 
        else if (username.length < 5 || username.length > 20) {
            outputString = "The length should be between 5 - 20 characters! ";
            current_tooltip_username_shown = 2;
        }

        if (current_tooltip_username_shown !== tooltip_username_shown) {
            SetTextTooltipUsername(outputString);
            $(input_username).tooltip('show');
            tooltip_username_shown = current_tooltip_username_shown;
        }
        

        if (outputString.length > 0) {
            error_username = true;
            DisableButton();
        }
        else {
            tooltip_username_shown = 0;
            
            error_username = false;
            HideTooltipUserName();
            if (error_username === false && error_password === false && error_email === false) {
                EnableButton();
            }
        }
    }



    function CheckEmail() {
        var email = $(input_email).val();
        var emailPattern = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);
        $("#email_error_message").hide();
        if (emailPattern.test(email) === false) {
            error_email = true;
            
            DisableButton();
            
            if (tooltip_email_shown === false) {
                //alert("show");
                tooltip_email_shown = true;
                SetTextTooltipEmail("Invalid Email Address!");
                $(input_email).tooltip('show');
            }
        }
        else {
            //alert("hide");
            HideTooltipEmail();
            tooltip_email_shown = false;
        }

        error_email = false;
        if (error_username === false && error_password === false && error_email === false) {
            EnableButton();
        }
    }


    function CheckPassword() {
        var current_tooltip_password_shown = 0;
        var password = $(input_password).val();       
        var outputString = "";
        var passwordPattern1 = new RegExp(/[a-z]/);
        var passwordPattern2 = new RegExp(/[A-Z]/);
        var passwordPattern3 = new RegExp(/[0-9]/);
        $("#password_error_message").hide();

        if (passwordPattern1.test(password) === false ||
            passwordPattern3.test(password) === false ||
            passwordPattern2.test(password) === false) {
            current_tooltip_password_shown = 1;
            outputString = "The password must contain a number, a lower and upper case character!";
        }
        else if (password.length < 8 || password.length > 24) {
            current_tooltip_password_shown = 2;
            outputString = "The password should be between 8 - 24 characters! ";
        } 
       

        if (current_tooltip_password_shown !== tooltip_password_shown) {
            SetTextTooltipPassword(outputString);
            $(input_password).tooltip('show');
            tooltip_password_shown = current_tooltip_password_shown;
        }

        if (outputString.length > 0) {
            error_password = true;
            DisableButton();
        }
        else {
            tooltip_password_shown = 0;
            HideTooltipPassword();
            
            error_password = false;
            if (error_username === false && error_password === false && error_email === false) {
                EnableButton();
            }
        }
      
    }
});