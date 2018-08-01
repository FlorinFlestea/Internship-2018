$(function () {


    $('[data-toggle="tooltip"]').tooltip();


    var input_email = "#Email";
    var input_password = "#Password";

    var error_email = true;
    var error_password = true;

    var tooltip_email_shown = 0;
    var color_correct = "greenyellow";
    var color_incorrect = "red";

    var button = "#btn_LogIn";


    $(input_password)
        .attr('data-original-title', "")
        .tooltip('_fixTitle').tooltip('hide');


    $(input_email)
        .attr('data-original-title', "")
        .tooltip('_fixTitle').tooltip('hide');


    //when starting the page, if the inputs are correct, do not disable the button(when refreshing the page)
    CheckEmail(1);
    CheckPassword(1);


    $(input_password).keyup(function () {
        CheckEmail(1);
        CheckPassword(0);
    });
    $(input_email).keyup(function () {
        CheckPassword(1);
        CheckEmail(0);
    });


    function SetTextTooltipEmail(text) {
        $(input_email)
            .attr('data-original-title', text)
            .tooltip('_fixTitle');
        $(input_email).css("border-color", color_incorrect);
    }


    function SetTextTooltipPassword(text) {
        $(input_password)
            .attr('data-original-title', text)
            .tooltip('_fixTitle');
        $(input_password).css("border-color", color_incorrect);
    }

    function DisableButton() {
        $(button).css('opacity', '0.5');
        $(button).attr("onClick", "return false");
    }

    function EnableButton() {
        $(button).css('opacity', '1');
        $(button).attr("onClick", "return true");
    }

  

    function HideTooltipEmail() {
        $(input_email)
            .attr('data-original-title', "")
            .tooltip('_fixTitle');
        $(input_email).css("border-color", color_correct);
        $(input_email).tooltip('hide');
    }

    function HideTooltipPassword() {
        $(input_password)
            .attr('data-original-title', "")
            .tooltip('_fixTitle');
        $(input_password).css("border-color", color_correct);
        $(input_password).tooltip('hide');
    }

    $(button).click(function () {
        CheckPassword(0);
        CheckEmail(0);
        
    });



    function CheckEmail(justCheck) {
        var temp_tooltip_email_shown = 0;
        var outputString = "";
        var email = $(input_email).val();
        var emailPattern = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);

        if (email === "") {
            temp_tooltip_email_shown = 1;
            outputString="Email field is required!";
        }
        else if (emailPattern.test(email) === false) {
            temp_tooltip_email_shown = 2;
            outputString = "Invalid Email Address!";
        }

        if (justCheck===0 && temp_tooltip_email_shown !== tooltip_email_shown) {
            SetTextTooltipEmail(outputString);
            $(input_email).tooltip('show');
            tooltip_email_shown = temp_tooltip_email_shown;
        }

        if (outputString.length > 0) {
            error_email = true;
            DisableButton();
        }
        else {
            tooltip_email_shown = 0;
            HideTooltipEmail();
            error_email = false;
            if (error_email === false && error_password === false) EnableButton();
        }
    }


    function CheckPassword(justCheck) {
       
        if ($(input_password).val() === "") {
            
            if (justCheck === 0) {
                SetTextTooltipPassword("Password field is required!");
                 $(input_password).tooltip('show');
            }
            DisableButton();
            error_password = true;
        }       
        else {
            error_password = false;
            HideTooltipPassword();
            if (error_email === false && error_password === false) EnableButton();
        }
    }

  
});