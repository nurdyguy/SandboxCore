"use strict";

$(document).ready(function () {
    setTimeout(function () {
        var a = document.querySelector("a.PostLogoutRedirectUri");
        if (a) {
            window.location = a.href;
        }
    }, 2500);
});

