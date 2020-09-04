$(document).on("change", "input[type='file']", function (e) {
    if (e.target.files[0] != undefined) {
        var fileName = e.target.files[0].name;
        $(this).next('.custom-file-label').html(fileName);
        var _formgroup = $(this).parent().parent();

        if ($(_formgroup).find(".ImageCancel").length == 0) {
            var _string = '<div class="mt-2 ImageCancel float-right">' +
                '<i class="fas fa-times text-danger mr-2" style = "color:red" ></i><label class="text-danger ">Resmi kaldır</label>' +
                '</div>';
            $(_formgroup).append(_string);
        }
        $(_formgroup).find(".ImageCancel").show();
    }
});

$(document).on("click", ".ImageCancel", function () {
    var custom_file = $(this).parent();
    custom_file.find("input[name='Image']").attr("value", "");
    custom_file.find(".custom-file-label").html("Resim Seçiniz");
    $(this).hide();
});

//Menu click (open or close)
$("#menu-toggle").click(function (e) {
    e.preventDefault();
    $("#wrapper").toggleClass("toggled");
});