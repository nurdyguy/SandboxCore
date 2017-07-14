
var HasFormErrors = false;

var errorMessages = {
    FirstName: {
        Required: 'First Name is required'
    },
    LastName: {
        Required: 'Last Name is required'
    },
    Email: {
        Required: 'Email is required',
        Invalid: 'Invalid Email address',
        Unavailable: 'That Email address is unavailable'
    },
    Password: {
        MinLength: 'Your Password must be at least 8 characters long.',
        MaxLength: 'Your Password cannot exceed 20 characters.',
        WhiteSpace: 'Your Password cannot contain spaces.'
    },
    ConfirmPassword: {
        Mismatch: 'Your confirm password does not match'
    }

};

$(document).ready(function ()
{
    $('#inputFirstName').on('focusout', function ()
    {
        ValidateFirstName();
    });

    $('#inputLastName').on('focusout', function ()
    {
        ValidateLastName();
    });

    $('#inputEmail').on('focusout', function ()
    {
        ValidateEmail();
    });

    $('#inputPassword').on('focusout', function ()
    {
        ValidatePassword();
    });

    $('#inputConfirmPassword').on('focusout', function ()
    {
        ValidateConfirmPassword();
    });

    InitialValidation();
})

function InitialValidation()
{
    ValidateFirstName();
    ValidateLastName();
}

function ValidateFirstName()
{
    var $input = $('#inputFirstName');
    var val = $input.val();
    if (val.trim().length == 0)
        ShowValidationErrorMessage($input, errorMessages.FirstName.Required);
    else
        RemoveValidationErrorMessage($input);
}

function ValidateLastName()
{
    var $input = $('#inputLastName');
    var val = $input.val();
    RemoveValidationErrorMessage($input);

    if (val.trim().length == 0)
        ShowValidationErrorMessage($input, errorMessages.LastName.Required);
}

function ValidateEmail()
{
    var $input = $('#inputLastName');
    var val = $input.val();
    RemoveValidationErrorMessage($input);
    
    if (val.trim().length == 0)
    {
        ShowValidationErrorMessage($input, errorMessages.Email.Required);
        return;
    }

    if (!IsValidEmailFormat(val))
    {
        ShowValidationErrorMessage($input, errorMessages.Email.Invalid);
        return;
    }
    
    $.ajax({
        async: false,
        type: 'POST',
        url: '/Account/CheckEmailAvailable',
        data: { email: val },
        success: function (data) 
        {
            if (!data.Available)
                ShowValidationErrorMessage($input, errorMessages.Email.Unavailable);
                
        },
        error: function (e1, e2, e3) 
        {
        }
    });
    
}

function IsValidEmailFormat()
{
    var re = /^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$/;    
    return !re.test(email);    
}

function ValidatePassword()
{
    var $input = $('#inputNewPassword');
    var val = $input.val();
    RemoveValidationErrorMessage($input);

    if (password.replace(/ /g, '') != password)
    {
        ShowValidationErrorMessage($input, errorMessages.Password.WhiteSpace);
        return;
    }

    if (val.length > 0 && val.length < 8)
    {
        ShowValidationErrorMessage($input, errorMessages.Password.MinLength);
        return;
    }
    
    if (val.length > 20)
    {
        ShowValidationErrorMessage($input, errorMessages.Password.MaxLength);
        return;
    }
}

function ValidateConfirmPassword()
{
    var $input = $('#inputNewPassword');
    var val = $input.val();
    RemoveValidationErrorMessage($input);

    if (password.replace(/ /g, '') != password)    
        ShowValidationErrorMessage($input, errorMessages.ConfirmPassword.Mismatch);
        
}

function ShowValidationErrorMessage($input, message)
{
    $input.next('span.text-danger').text(message).removeClass('hidden');
    $input.closest('div.form-group').addClass('has-error');
    HasFormErrors = true;
}

function RemoveValidationErrorMessage($input)
{
    $input.next('span.text-danger').addClass('hidden');
    $input.closest('div.form-group').removeClass('has-error');
    HasFormErrors = $('div.form-group.has-error').length > 0;
}