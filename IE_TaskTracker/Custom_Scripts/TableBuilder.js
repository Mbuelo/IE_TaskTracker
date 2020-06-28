$(document).ready(function () {

    $.ajax({
        url: '_Tasks/BuildTaskTable',
        success: function (result) {
            $('#tableDiv').html(result);
        }
    });

});