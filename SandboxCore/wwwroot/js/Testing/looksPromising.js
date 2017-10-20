

$(document).ready(function ()
{
    $('#promise').on('click', function ()
    {
        PromiseTest(1);
    });

    $('#loop').on('click', function ()
    {
        loop(10);
    });
});

function PromiseTest(x)
{
    asyncTest1(x++).then(
        function (result)//resolve
        {
            PromiseTest(x)
        }).catch(
        function (err)
        {
            console.log('error')
        });
}

function ajax(url, type, data)
{
    return new Promise(function (resolve, reject)
    {
        $.ajax(
        {
            type: type,
            url: url,
            data: data,
            success: function (result)
            {
                resolve(result);
            },
            error: function (e)
            {
                reject(e);
            }
        });
    });
}


function asyncTest1(x)
{
    return new Promise(function (resolve, reject)
    {
        setTimeout(function ()
        {
            console.log(x++);
        }, 100);
    });
}

function asyncTest2(x)
{
    setTimeout(function ()
    {
        console.log(x);
    }, 100);
}


function loop(max)
{
    for (var i = 1; i <= max; i++)
        asyncTest2(i);
}

