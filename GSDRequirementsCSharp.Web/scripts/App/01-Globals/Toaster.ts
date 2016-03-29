module Notification {
    declare var toastr: any, _: any

    function messagesUlLi(messages: Array<string>) {
        return '<ul>' +
                _.reduce(messages, (acc, item) => acc + '<li>' + item + '</li>', '') +
               '</ul>';
    }

    export function notifySuccess(title: string) {
        toastr.success('', title);
    }

    export function notifyError(title: string, messages: Array<string>) {
        toastr.error(messagesUlLi(messages), title);
    }

    export function notifyWarning(title: string, messages: Array<string>) {
        toastr.warning(messagesUlLi(messages), title);
    }

    export function notifyInfo(title: string, messages: Array<string>) {
        toastr.info(messagesUlLi(messages), title);
    }
}