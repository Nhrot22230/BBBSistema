function showModalForm(modalId) {
    var modalForm = new bootstrap.Modal(document.getElementById(modalId));
    modalForm.toggle();
}

function validarFormulario() {
    return true;
}


function autoUpdate(input) {
    const min = 0;
    const max = 100;
    if (input.value < min) input.value = min;
    if (input.value > max) input.value = max;
    porcentajeDescuento = document.getElementById('ContentPlaceHolder1_TxtDescuento').value;
    var total = 0;
    var gv = document.getElementById('ContentPlaceHolder1_gvLineasOrdenVenta');
       
    if (gv) {
        for (var i = 1; i < gv.rows.length; i++){
            var subtotalCell = gv.rows[i].cells[4];
            var subtotal = parseFloat(subtotalCell.innerText) || 0;
            total += subtotal;
        }
    }
    total -= (total * porcentajeDescuento / 100);
    document.getElementById('ContentPlaceHolder1_txtTotal').value = total.toFixed(2);
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
