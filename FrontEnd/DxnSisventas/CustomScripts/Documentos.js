let labelFechaComprobante, errMsgFechaComprobante;
let labelFechaOrden;
let labelIdOrden, errMsgOrdenAsociada;
labelFechaComprobante = document.getElementById("ContentPlaceHolder1_" + "TxtFechaComprobante");
errMsgFechaComprobante = document.getElementById("fechaComprobanteErrorMessage");
labelFechaOrden = document.getElementById("ContentPlaceHolder1_" + "TxtFechaOrden");
//errMsgFechaOrden = document.getElementById("fechaErrorMessage");
labelIdOrden = document.getElementById("ContentPlaceHolder1_" + "TxtIdOrden");
errMsgOrdenAsociada = document.getElementById("ordenAsociadaErrorMessage");


function HolaMundo() {
    alert('Hola Mundo');
}

function showModalDetallesORV() {
    var modalORV = new bootstrap.Modal(document.getElementById('modalDetallesORV'));
    modalORV.toggle();
}

function showModalDetallesORC() {
    var modalORC = new bootstrap.Modal(document.getElementById('modalDetallesORC'));
    modalORC.toggle();
}

function showModalDetallesCOM() {
    var modalCOM = new bootstrap.Modal(document.getElementById('modalDetallesCOM'));
    modalCOM.toggle();
}

function showModalFormProducto() {
    var modalFormProducto = new bootstrap.Modal(document.getElementById('form-modal-producto'));
    modalFormProducto.toggle();
}

function showModalFormEnviar() {
    var modalFormProducto = new bootstrap.Modal(document.getElementById('form-modal-enviar'));
    modalFormProducto.toggle();
}

function showModalFormOrdenes(){
    var modalFormOrdenes = new bootstrap.Modal(document.getElementById('form-modal-ordenes'));
    modalFormOrdenes.toggle();
}


function formatoFecha(fechaInput) {

    // Verificar que el formato es dd/mm/yyyy usando una expresión regular
    const regex = /^(\d{2})\/(\d{2})\/(\d{4})$/;
    const match = fechaInput.match(regex);
    // Extraer los componentes de la fecha
    if (!match) {
        throw new Error("Formato de fecha inválido");
    }

    const day = parseInt(match[1], 10);
    const month = parseInt(match[2], 10);
    const year = parseInt(match[3], 10);
    // Formatear a yyyy-mm-dd
    const fecha_formato = `${String(year).padStart(4, '0')}-${String(month).padStart(2, '0')}-${String(day).padStart(2, '0')}`;
    return fecha_formato;
}
function validateFechaComprobante() {
    if (!labelFechaComprobante.value) {
        labelFechaComprobante.classList.add("is-invalid");
        errMsgFechaComprobante.innerHTML = "El campo Fecha no puede estar vacío";
        errMsgFechaComprobante.style.display = "block";
        return false;
    }
    
    if (labelFechaOrden.value && labelFechaComprobante.value) {

        const fechaComprobante_formato = labelFechaComprobante.value;
        const fechaOrden_formato = formatoFecha(labelFechaOrden.value);

        const date_comprobante = new Date(fechaComprobante_formato);
        const date_orden = new Date(fechaOrden_formato);

        if (date_comprobante < date_orden) {
            labelFechaComprobante.classList.add("is-invalid");
            errMsgFechaComprobante.innerHTML = "La fecha de comprobante no puede ser menor a la de orden";
            errMsgFechaComprobante.style.display = "block";
            return false;
        }
    }
    
    labelFechaComprobante.classList.remove("is-invalid");
    errMsgFechaComprobante.innerHTML = "";
    errMsgFechaComprobante.style.display = "none";
    return true;
}

function validateOrdenAsociada() {
    if (!labelIdOrden.value) {
        labelIdOrden.classList.add("is-invalid");
        errMsgOrdenAsociada.innerHTML = "La orden asociada no puede estar vacía";
        errMsgOrdenAsociada.style.display = "block";
        return false;
    }


    labelIdOrden.classList.remove("is-invalid");
    errMsgOrdenAsociada.innerHTML = "";
    errMsgOrdenAsociada.style.display = "none";
    return true;
}

function validarFormularioComprobante() {
    let val_fecha = validateFechaComprobante();
    let val_orden = validateOrdenAsociada();
    if (val_fecha && val_orden) {
        return true;
    }

    return false;
}

function openInNewTab() {
    window.document.forms[0].target = '_blank';
    setTimeout(function () { window.document.forms[0].target = ''; }, 0);
}