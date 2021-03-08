
$(document).ready(function () {
    var counter = 1;
    var all = 4;
    //console.log('start');

    setInterval(function () {

        var tmp = 1;

        for (var i = 1; i <= all; ++i) {
            if ($('#radio' + i).prop('checked')) {
                tmp = i;
                break;
            }
        }

        tmp++;

        if (tmp > all)
            tmp = 1;

        $('#radio' + tmp).prop('checked', true);
    }, 5000);

    $('.slide img').on('click', function () {
        var source = $(this).attr('src');
        $('#frame img').attr('src', source);
        $('#overlay').fadeIn();
        $('#frame').fadeIn();

    });

    $('#overlay').on('click', function () {
        $('#frame').fadeOut();
        $('#overlay').fadeOut();
    });

});