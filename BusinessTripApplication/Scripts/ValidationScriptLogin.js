$(function () {


    $('[data-toggle="tooltip"]').tooltip();


    var input_email = "#Email";
    var input_password = "#Password";

    var error_email = true;
    var error_password = true;
    

    var color_correct = "greenyellow";
    var color_incorrect = "red";

    var button = "#btn_LogIn";


    $(input_password)
        .attr('data-original-title', "")
        .tooltip('fixTitle').tooltip('hide');


    $(input_email)
        .attr('data-original-title', "")
        .tooltip('fixTitle').tooltip('hide');


    $(input_password).keyup(function () {
        CheckPassword();
    });
    $(input_email).keyup(function () {
        CheckEmail();
    });


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
        if ($(button).css('onclick') !== null) {
            $(button).onclick = "return false";
        } else {
            $(button).attr("onclick", "return false");
            $(button).css("onclick", "return false");
        }
        $(button).css('opacity', '0.5');
    }

    function EnableButton() {
        $(button).css('opacity', '1');
        $(button).removeAttr('onclick');
        //$(':input[type="submit"]').prop('disabled', false);
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

    $(button).click(function () {
        CheckPassword();
        CheckEmail();
        
    });



    function CheckEmail() {
        if ($(input_email).val() === "") {
            SetTextTooltipEmail("Email field is required!");
            $(input_email).tooltip('show');
            DisableButton();
            error_email = true;
        }
        else {
            HideTooltipEmail();
            error_email = false;
            if (error_email === false && error_password === false) EnableButton();
        }
    }


    function CheckPassword() {
       
        if ($(input_password).val() === "") {
            SetTextTooltipPassword("Password field is required!");
            $(input_password).tooltip('show');
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