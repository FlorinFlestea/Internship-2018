$(function () {
    //only save email between different pages
    var ref = document.referrer.toUpperCase();
    //alert(ref);
    //alert(ref.indexOf("LOGIN"));
    if (ref.indexOf("LOGIN") < 0 && ref!=="") {
        var url = window.location.href;
        var email = "";
        var position = 0;
        position = url.indexOf('email');
        if (position > 0) {
            email = url.substring(position + 6);
            $('#Email').val(email);
        }
    }
});