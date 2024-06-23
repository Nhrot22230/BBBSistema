function showModalForm() {
  let modalForm = new bootstrap.Modal(document.getElementById("modalForm"));
  modalForm.toggle();
}

const specialChars = "<>@!#$%^&*()_+[]{}?:;|'\"\\,./~`-=";
const regexSpecial = new RegExp("[" + specialChars + "]");

let labelNombre, errMsgNombre;
let labelStock, errMsgStock;
let labelPrecio, errMsgPrecio;
let labelPuntos, errMsgPuntos;
let labelCapacidad, errMsgCapacidad;

labelNombre = document.getElementById("ContentPlaceHolder1_" + "TxtNombre");
errMsgNombre = document.getElementById("nombreErrorMessage");
labelStock = document.getElementById("ContentPlaceHolder1_" + "TxtStock");
errMsgStock = document.getElementById("stockErrorMessage");
labelPrecio = document.getElementById("ContentPlaceHolder1_" + "TxtPrecio");
errMsgPrecio = document.getElementById("precioErrorMessage");
labelPuntos = document.getElementById("ContentPlaceHolder1_" + "TxtPuntos");
errMsgPuntos = document.getElementById("puntosErrorMessage");
labelCapacidad = document.getElementById("ContentPlaceHolder1_" + "TxtCapacidad");
errMsgCapacidad = document.getElementById("capacidadErrorMessage");

function validatePoints() {
  if (labelPuntos.value === "") {
    labelPuntos.classList.add("is-invalid");
    errMsgPuntos.innerHTML = "El campo Puntos no puede estar vacío";
    errMsgPuntos.style.display = "block";
    return false;
  }
  if (isNaN(labelPuntos.value)) {
    labelPuntos.classList.add("is-invalid");
    errMsgPuntos.innerHTML = "El campo Puntos debe ser un número";
    errMsgPuntos.style.display = "block";
    return false;
  }
  if (labelPuntos.value < 0) {
    labelPuntos.classList.add("is-invalid");
    errMsgPuntos.innerHTML = "El campo Puntos debe ser mayor o igual a 0";
    errMsgPuntos.style.display = "block";
    return false;
  }

  labelPuntos.classList.remove("is-invalid");
  errMsgPuntos.innerHTML = "";
  errMsgPuntos.style.display = "none";
  return true;
}
function validateCapacidad() {
  if (labelCapacidad.value === "") {
    labelCapacidad.classList.add("is-invalid");
    errMsgCapacidad.innerHTML = "El campo Capacidad no puede estar vacío";
    errMsgCapacidad.style.display = "block";
    return false;
  }
  if (isNaN(labelCapacidad.value)) {
    labelCapacidad.classList.add("is-invalid");
    errMsgCapacidad.innerHTML = "El campo Capacidad debe ser un número";
    errMsgCapacidad.style.display = "block";
    return false;
  }
  if (labelCapacidad.value < 0) {
    labelCapacidad.classList.add("is-invalid");
    errMsgCapacidad.innerHTML = "El campo Capacidad debe ser mayor o igual a 0";
    errMsgCapacidad.style.display = "block";
    return false;
  }

  labelCapacidad.classList.remove("is-invalid");
  errMsgCapacidad.innerHTML = "";
  errMsgCapacidad.style.display = "none";
  return true;
}

function validatePrecio() {
  if (labelPrecio.value === "") {
    labelPrecio.classList.add("is-invalid");
    errMsgPrecio.innerHTML = "El campo Precio no puede estar vacío";
    errMsgPrecio.style.display = "block";
    return false;
  }
  if (isNaN(labelPrecio.value)) {
    labelPrecio.classList.add("is-invalid");
    errMsgPrecio.innerHTML = "El campo Precio debe ser un número";
    errMsgPrecio.style.display = "block";
    return false;
  }
  if (labelPrecio.value <= 0) {
    labelPrecio.classList.add("is-invalid");
    errMsgPrecio.innerHTML = "El campo Precio debe ser mayor a 0";
    errMsgPrecio.style.display = "block";
    return false;
  }

  labelPrecio.classList.remove("is-invalid");
  errMsgPrecio.innerHTML = "";
  errMsgPrecio.style.display = "none";
  return true;
}

function validateNombre() {
  if (labelNombre.value === "") {
    labelNombre.classList.add("is-invalid");
    errMsgNombre.innerHTML = "El campo Nombre no puede estar vacío";
    errMsgNombre.style.display = "block";
    return false;
  }
  if (regexSpecial.test(labelNombre.value)) {
    labelNombre.classList.add("is-invalid");
    errMsgNombre.innerHTML = "El campo Nombre no puede contener caracteres especiales";
    errMsgNombre.style.display = "block";
    return false;
  }

  labelNombre.classList.remove("is-invalid");
  errMsgNombre.innerHTML = "";
  errMsgNombre.style.display = "none";
  return true;
}
function validateStock() {
  if (labelStock.value === "") {
    labelStock.classList.add("is-invalid");
    errMsgStock.innerHTML = "El campo Stock no puede estar vacío";
    errMsgStock.style.display = "block";
    return false;
  }
  if (isNaN(labelStock.value)) {
    labelStock.classList.add("is-invalid");
    errMsgStock.innerHTML = "El campo Stock debe ser un número";
    errMsgStock.style.display = "block";
    return false;
  }
  if (labelStock.value < 0) {
    labelStock.classList.add("is-invalid");
    errMsgStock.innerHTML = "El campo Stock debe ser mayor o igual a 0";
    errMsgStock.style.display = "block";
    return false;
  }

  labelStock.classList.remove("is-invalid");
  errMsgStock.innerHTML = "";
  errMsgStock.style.display = "none";
  return true;
}

function validarFormulario() {
  // Llama a todas las funciones de validación individual

  let val_puntos = validatePoints();
  let val_capacidad = validateCapacidad();
  let val_precio = validatePrecio();
  let val_nombre = validateNombre();
  let val_stock = validateStock();

  if (val_puntos && val_capacidad && val_precio && val_nombre && val_stock) {
    return true;
  }

  return false;
}
$(document).ready(function () {
  // get elements by id again
  labelNombre = document.getElementById("ContentPlaceHolder1_" + "TxtNombre");
  errMsgNombre = document.getElementById("nombreErrorMessage");

  labelStock = document.getElementById("ContentPlaceHolder1_" + "TxtStock");
  errMsgStock = document.getElementById("stockErrorMessage");

  labelPrecio = document.getElementById("ContentPlaceHolder1_" + "TxtPrecio");
  errMsgPrecio = document.getElementById("precioErrorMessage");

  labelPuntos = document.getElementById("ContentPlaceHolder1_" + "TxtPuntos");
  errMsgPuntos = document.getElementById("puntosErrorMessage");

  labelCapacidad = document.getElementById("ContentPlaceHolder1_" + "TxtCapacidad");
  errMsgCapacidad = document.getElementById("capacidadErrorMessage");
});
