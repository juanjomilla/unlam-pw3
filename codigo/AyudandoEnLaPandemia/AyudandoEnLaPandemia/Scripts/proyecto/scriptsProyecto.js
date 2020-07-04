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

    $("#btnAgregarInsumo").click(function () {
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
    });

    $("#btnBusqueda").click(function () {
        $.ajax({
            async: true,
            data: $('#busqueda').serialize(),
            type: "POST",
            url: '/Necesidad/BuscarNecesidad',
            success: function (partialView) {
                console.log("partialView: " + partialView);
                $('#resultadosBusqueda').html(partialView);
            }
        });
    });

    $("#chkMostrarTodas").click(function () {
        var chk = $("#chkMostrarTodas")[0].checked;
        $("#misNecesidades .necesidad").each(function () {
            if (chk) {
                $(".necesidadInactiva").hide();
            } else {
                $(".necesidad").show();
            }
        });
    });

    $("#contraerBtn").click(function () {
        var btnText = $("#contraerBtn")[0].text;
        var ocultarText = "Ocultar mis necesidades";
        var mostrarText = "Mostrar mis necesidades";
        
        if (btnText === mostrarText){
            $("#contraerBtn")[0].text = ocultarText;
        }
        else{
            $("#contraerBtn")[0].text = mostrarText;
        }
    });

    $("#message2").hide();
    $("#message1").hide();
    $("#message4").hide();
    let mensajeRegistro = $('#mensajeRegistro')[0].value;
    if (mensajeRegistro == 'OK')
    {
        $("#message1").show();
    };

    if (mensajeRegistro == 'NotOK')
    {
        $("#message2").show();
        $("#message4").show();
    };


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