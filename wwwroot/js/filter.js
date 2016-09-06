$('.hide-finished').click(function() {
    $(".note-item[data-finished='true']").toggleClass('hidden');
    $('.hide-finished').toggleClass('active');
});
