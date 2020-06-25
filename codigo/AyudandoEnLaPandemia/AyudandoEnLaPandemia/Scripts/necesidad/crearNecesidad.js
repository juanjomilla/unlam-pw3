$("#btnAgregarInsumo").on('click', function () {
    console.log($('#CrearNecesidadForm').serialize());
    $.ajax({
        async: true,
        data: $('#CrearNecesidadForm').serialize(),
        type: "POST",
        url: '/Necesidad/AgregarInsumoPartial',
        success: function (partialView) {
            console.log("partialView: " + partialView);
            $('#editorInsumos').html(partialView);
        }
    });
})

$(document).ready(function () {
    $("select").change(function () {
        $(this).find("option:selected").each(function () {
            var optionValue = $(this).attr("value");
            if (optionValue) {
                $(".tipoDonacion").not("." + optionValue).hide();
                $("." + optionValue).show();
            } else {
                $(".tipoDonacion").hide();
            }
        });
    }).change();
});

function show(input) {
    if (input.files && input.files[0]) {  
        var filerdr = new FileReader();  
        filerdr.onload = function (e) {  
            $('#user_img').attr('src', e.target.result);  
        }

        filerdr.readAsDataURL(input.files[0]);
    }
}