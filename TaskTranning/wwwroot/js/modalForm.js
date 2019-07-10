$(function () {
    var placeholderElement = $('#modal-placeholder');
    $('button[data-toggle="modal-password"]').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
        });
    });
    placeholderElement.on('click', '[data-save="modal"]', function (event) {
        event.preventDefault();

        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var dataToSend = form.serialize();

        $.post(actionUrl, dataToSend).done(function (data) {
            var newBody = $('.modal-body', data);
            placeholderElement.find('.modal-body').replaceWith(newBody);
            var isValid = newBody.find('[name="IsValid"]').val() === 'True';
            if (isValid) {
                placeholderElement.find('.modal').modal('hide');
                location.reload();
            }
        });
    });
});
$(function () {
    var placeholderElement = $('#notification');
    $('button[data-toggle="modal-picture"]').click(function () {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
        });
    });
    placeholderElement.on('click', '[data-save="modal"]', function (event) {
        event.preventDefault();
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var dataToSend = new FormData(form.get(0));
        $.ajax({ url: actionUrl, method: 'post', data: dataToSend, processData: false, contentType: false }).done(function (data) {
            var newBody = $('.modal-body', data);
            placeholderElement.find('.modal-body').replaceWith(newBody);
            var isValid = newBody.find('[name="IsValid"]').val() === 'True';
            if (isValid) {
                placeholderElement.find('.modal').modal('hide');
                location.reload();
            }
        });
    });
});