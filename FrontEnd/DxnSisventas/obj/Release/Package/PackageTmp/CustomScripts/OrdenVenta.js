function showModalForm(modalId) {
    var modalForm = new bootstrap.Modal(document.getElementById(modalId));
    modalForm.toggle();
}

function validarFormulario() {
    return true;
}





function avoidEnterKey(event) {
    if (event.keyCode === 13) {
        return false; // Evita que se propague el evento
    }
    return document.getElementById('ContentPlaceHolder1_TxtDescuento').value;
}

function validateRange(input) {
    const min = 0;
    const max = 100;
    if (input.value < min) input.value = min;
    if (input.value > max) input.value = max;
}

oninput = "verificarStock(this)"

function verificarStock(input) {
    let stockProducto = parseInt(document.getElementById('ContentPlaceHolder1_TxtStock').value, 10);
    let cantidad = parseInt(input.value, 10);


    if (cantidad > stockProducto) {
        input.value = stockProducto;
    } else if(cantidad < 0) {
        input.value = 0;
    }
}
