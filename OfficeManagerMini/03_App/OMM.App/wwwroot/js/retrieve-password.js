function retrievePassword() {
    $.ajax({
        type: "Post",
        url: '/Employees/ForgotPassword',
        data: $("#forgot-password-form").serialize(),
        success: function (result) {
            if (result.success) {
                window.location.href = result.url;
            } else {
                $('#EmailRetrievePassword').html(result);
            }
        }
    });
    return false;
};