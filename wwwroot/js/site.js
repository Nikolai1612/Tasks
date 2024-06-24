$(function () {
    attachFormSubmit('#loginForm', '/Account/SignIn');
    attachFormSubmit('#registerForm', '/Account/SignUp');

    $("#myTab .nav-link").on('shown.bs.tab', function (e) {
        updateModalTitle();
    });

    $('#modal').on('shown.bs.modal', function (e) {
        updateModalTitle();
    });
});

function attachFormSubmit(formSelector, url) {
    $(document).off('submit', formSelector).on('submit', formSelector, function (event) {
        event.preventDefault();
        $.ajax({
            type: 'POST',
            url: url,
            data: $(this).serialize(),
            success: function (response) {
                if (response.success) {
                    $('#accountModal').modal('hide');
                    // Optionally, reload the page or update the UI accordingly
                    location.reload();
                } else {
                    $(formSelector).closest('.tab-pane').html(response);
                    $.validator.unobtrusive.parse(formSelector); // Reparse the form for validation
                    attachFormSubmit(formSelector, url); // Reattach submit handler
                }
            }
        });
    });
}
function updateModalTitle() {
    var activeTab = $('#myTab .nav-link.active').attr('id');
    var modalTitle = '';

    if (activeTab === 'login-tab') {
        modalTitle = 'Sign In';
    } else if (activeTab === 'register-tab') {
        modalTitle = 'Sing Up';
    }

    $('#modalLabel').text(modalTitle);
}
