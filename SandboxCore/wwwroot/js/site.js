// Write your Javascript code.

function ShowWaitingOverlay()
{
    $('#waitingOverlay').removeClass('hidden');
}

function HideWaitingOverlay()
{
    $('#waitingOverlay').addClass('hidden');
}

function notificationMessage(message, type, sticky)
{
    if (sticky == null || sticky == "") sticky = false;
    type = type.toLowerCase();

    var $notification = $("#notification")

    $("#notificationType").html(type.toUpperCase());
    $("#notificationMessage").html(message);
    
    $notification.removeClass("successMessage", "errorMessage").addClass(type + "Message");
    slideDOWN($notification);

    if (type && type == "error")
    {
        if (sticky == false)
            setTimeout(function () { slideUP($notification); }, 20000);
        else
            setTimeout(function () { slideUP($notification); }, 60000);
    }
    else if (type && type == "success")
    {
        if (sticky == false)
            setTimeout(function () { slideUP($notification); }, 2000);
        else
            setTimeout(function () { slideUP($notification); }, 30000);
    }

}

function slideUP($o)
{
    var messagesHeight = $o.outerHeight() + 30;
    $o.animate({ top: -messagesHeight }, 500);
}

function slideDOWN($o)
{
    $o.stop(true, false).animate({ top: "0" }, 500);
}