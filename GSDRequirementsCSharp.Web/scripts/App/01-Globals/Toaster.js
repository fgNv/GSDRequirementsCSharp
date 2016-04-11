var Notification;
(function (Notification) {
    function messagesUlLi(messages) {
        return '<ul>' +
            _.reduce(messages, function (acc, item) { return acc + '<li>' + item + '</li>'; }, '') +
            '</ul>';
    }
    function notifySuccess(title) {
        toastr.success('', title);
    }
    Notification.notifySuccess = notifySuccess;
    function notifyError(title, messages) {
        toastr.error(messagesUlLi(messages), title);
    }
    Notification.notifyError = notifyError;
    function notifyWarning(title, messages) {
        toastr.warning(messagesUlLi(messages), title);
    }
    Notification.notifyWarning = notifyWarning;
    function notifyInfo(title, messages) {
        toastr.info(messagesUlLi(messages), title);
    }
    Notification.notifyInfo = notifyInfo;
})(Notification || (Notification = {}));
