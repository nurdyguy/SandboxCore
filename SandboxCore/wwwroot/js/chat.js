

$(document).ready(function () 
{
    var protocol = location.protocol === "https:" ? "wss:" : "ws:";
    var wsUri = protocol + "//" + window.location.host;
    var socket = new WebSocket(wsUri);
    socket.isOpen = false;

    socket.onopen = e =>
    {
        console.log("socket opened", e);
        socket.isOpen = true;
    };

    socket.onclose = function (e)
    {
        console.log("socket closed", e);
        socket.isOpen = false;
    };

    socket.onmessage = function (e)
    {
        console.log(e);
        $('#msgs').append(e.data + '<br />');
    };

    socket.onerror = function (e)
    {
        console.error(e.data);
    };

    $('#msgInput').keypress(function (e)
    {
        if (e.which != 13)
            return;        

        e.preventDefault();

        var $this = $(this);
        var message = $this.val();
        //socket.send(message);
        $this.val('');

        $.ajax(
        {
            type: "POST",
            url: "/chat",
            data: { message: message },
            success: function (result) 
            {
                if (!result.success)
                    alert(result.message);
            },
            error: function () 
            {
            }
                
        });
    });

    setTimeout(function ()
    {
        if (!socket.isOpen)
            window.location = window.location
    }, 1000)
});