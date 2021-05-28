function GetQueryString(key, default_) {
    if (default_ == null) default_ = "";
    key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
    var qs = regex.exec(window.location.href);
    if (qs == null)
        return default_;
    else
        return qs[1];
}

function OpenWindow(url, target)
{
    var screenWidth = parseFloat(screen.availWidth) - 10;
    var screenHeight = parseFloat(screen.availHeight) - 60;
    window.open(url, target, "location=no,resizable=yes,scrollbars=yes,width=" + screenWidth + ",height=" + screenHeight);
}
