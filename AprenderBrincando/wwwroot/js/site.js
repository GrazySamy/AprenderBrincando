window.addEventListener("load", function () {
    categoria_onchange();
}, false);

function categoria_onchange() {

    var selCategoriaValue = document.getElementById("categoria").value;
    var selSubcategoria = document.getElementById("subcategoria");

    for (var i = selSubcategoria.options.length - 1; i >= 0; i--) {
        selSubcategoria.remove(i);
    }

    if (selCategoriaValue == "1") {
        var option = document.createElement('option');
        option.text = "Alfabetização";
        option.value = "1";
        selSubcategoria.add(option);
        option = document.createElement('option');
        option.text = "Coordenação Motora";
        option.value = "2";
        selSubcategoria.add(option);
    }
    else if (selCategoriaValue == "2") {
        var option = document.createElement('option');
        option.text = "Português";
        option.value = "1";
        selSubcategoria.add(option);
        option = document.createElement('option');
        option.text = "Matemática";
        option.value = "2";
        selSubcategoria.add(option);
        option = document.createElement('option');
        option.text = "Ciências";
        option.value = "3";
        selSubcategoria.add(option);
    }
}
