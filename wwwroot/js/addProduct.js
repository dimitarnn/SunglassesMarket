function loadImage(path) {
    return '<img src="../images/' + path + '" width="100" alt="image" />';
}

$(document).ready(function () {

    $('#submit').on('click', function () {

        //console.log('clicked');

        if ($('#create-form').valid()) {

            var designer = $('#designer').val();
            var brand = $('#brand').val();
            var title = $('#title').val();
            var type = $('#type').val();
            var color = $('#color').val();
            var price = $('#price').val().replace('.', ',');
            var imageTitle = $('#image-title').val();
            var imageFile = $('#image-file')[0].files[0];

            var model = {
                designer: designer,
                brand: brand,
                title: title,
                type: type,
                color: color,
                price: price,
                imageTitle: imageTitle,
                imageFile: imageFile,
            };

            var data = new FormData();
            data.append('Designer', designer);
            data.append('Brand', brand);
            data.append('Title', title);
            data.append('Type', type);
            data.append('Color', color);
            data.append('Price', price);
            data.append('ImageTitle', imageTitle);
            data.append('ImageFile', imageFile);

            console.log('model: ' + model);
            console.log('data: ' + data);

            $.ajax({
                type: 'POST',
                url: "/Product/AddProduct",
                data: data,
                cache: false,
                contentType: false,
                processData: false,
                success: function (response) {
                    console.log('successs');
                    console.log(response);
                    console.log(response.id);
                    var id = response.id;
                    var path = response.photo;
                    $('#datatable').DataTable().row.add([
                        id, loadImage(path), designer, brand, title, type, color, price
                    ]).draw();
                    $('#createModal').modal('hide');
                }
            });
        }
    });
});