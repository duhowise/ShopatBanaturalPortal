﻿var keepSessionAlive = false;
var keepSessionAliveUrl = null;

function SetupSessionUpdater(actionUrl) {
    keepSessionAliveUrl = actionUrl;
    var container = $("#body");
    container.mousemove(function () { keepSessionAlive = true; });
    container.keydown(function () { keepSessionAlive = true; });
    CheckToKeepSessionAlive();
}

function CheckToKeepSessionAlive() {
    setTimeout("KeepSessionAlive()", 200000);
}

function KeepSessionAlive() {
    if (keepSessionAlive && keepSessionAliveUrl != null) {
        $.ajax({
            type: "POST",
            url: keepSessionAliveUrl,
            success: function () { keepSessionAlive = false; }
        });
    }
    CheckToKeepSessionAlive();
}