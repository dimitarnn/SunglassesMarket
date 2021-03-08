$(document).ready(function () {
    function readUrl(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#preview').attr('src', e.target.result);
                $('#preview').attr('border', '1px solid #000');
            }

            reader.readAsDataURL(input.files[0]);
        }
    }

    $('#picture').change(function () {
        readUrl(this);
    });
});