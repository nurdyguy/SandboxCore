

$(document).ready(function ()
{
    $('#promise').on('click', function ()
    {
        f1(10)
    });

    $('#loop').on('click', function ()
    {
        loop(10);
    });
});

function resolveAfter2Seconds(x)
{
    return new Promise(resolve =>
    {
        setTimeout(() =>
        {
            resolve(x);
        }, 2000);
    });
}

// https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Operators/await
// async function with await
async function f1()
{
    var x = await resolveAfter2Seconds(10);
    console.log(x); // 10
}


