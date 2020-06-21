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

$("#btnAgregarReferencia").on('click', function () {
    $.ajax({
        async: true,
        data: $('#CrearNecesidadForm').serialize(),
        type: "POST",
        url: '/Necesidad/AgregarReferenciaPartial',
        success: function (partialView) {
            console.log("partialView: " + partialView);
            $('#editorReferencias').html(partialView);
        }
    });
})

function show(input) {
    if (input.files && input.files[0]) {  
        var filerdr = new FileReader();  
        filerdr.onload = function (e) {  
            $('#user_img').attr('src', e.target.result);  
        }

        filerdr.readAsDataURL(input.files[0]);  
    }
}