$(function () {
    var url = window.location.href;
    var email = "";
    var position = 0;
    position = url.indexOf('email');
    if (position > 0) {
        email = url.substring(position + 6);
        $('#User_Email').val(email);
    }
});