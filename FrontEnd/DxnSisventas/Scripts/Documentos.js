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