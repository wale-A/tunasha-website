$(function () {
    var $chatOpen = $('.chat-support');
    var $chatClose = $('.chat-close');
    var $chatPanel = $('.chat-support-panel');

    $chatOpen.click(function () {
        $chatPanel.css('display', 'block');
        $(this).css('display', 'none');
    });

    $chatClose.click(function () {
        $chatPanel.css('display', 'none');
        $chatOpen.css('display', 'block');
    });
});