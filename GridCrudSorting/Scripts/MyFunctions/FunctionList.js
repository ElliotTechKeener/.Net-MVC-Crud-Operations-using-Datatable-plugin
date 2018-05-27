var Popup;

function PopupForm(url) {

    var formDiv = $('<div/>');

    $.get(url)
        .done(function (response) {

            formDiv.html(response);

            Popup = formDiv.dialog({
                autoOpen: true,
                resizable: false,
                title: "Enter New Exercise Record",
                height: 350,
                width: 600,
                close: function () {
                    Popup.dialog('destroy').remove();
                }
            });
        });
}

function SubmitForm(form) {

    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        $.ajax({
            type: "POST",
            url: form.action,
            data: $(form).serialize(),
            success: function (data) {
                if (data.success) {
                    Popup.dialog('close');
                    dataTable.ajax.reload();
                    $.notify(data.message, {
                        globalPosition: "top center",
                        className: "success"
                    });
                }
            }
        });
    }
    return false;
}

function Delete(id) {

    if (confirm('Are you sure to delete this record?')) {

        $.ajax({
            type: "POST",
            url: '@Url.Action("Delete","Exercise")/' + id,
            success: function (data) {
                if (data.success) {

                    dataTable.ajax.reload();
                    $.notify(data.message, {
                        globalPosition: "top center",
                        className: "Selected record successfully removed"
                    });
                }
            }
        });
    }
}  

