let modalForm = new bootstrap.Modal(document.getElementById('modalClienteForm'));
function showModalForm() {
  modalForm.toggle();
}
function hideModalForm() {
  modalForm.hide();
}

let labelDNI, errMsgDNI;
let labelNombre, errMsgNombre;
let labelApellidoPat, errMsgApellidoPat;
let labelApellidoMat, errMsgApellidoMat;
let labelRUC, errMsgRUC;
let labelPuntos, errMsgPuntos;

labelDNI = document.getElementById("ContentPlaceHolder1_" + "TxtDNI"); // "ContentPlaceHolder1_" + "TxtDNI
errMsgDNI = document.getElementById("errMsgDNI");
labelNombre = document.getElementById("ContentPlaceHolder1_" + "TxtNombre"); // "ContentPlaceHolder1_" + "TxtNombre
errMsgNombre = document.getElementById("errMsgNombre");
labelApellidoPat = document.getElementById("ContentPlaceHolder1_" + "TxtApellidoPat"); // "ContentPlaceHolder1_" + "TxtApellidoPat
errMsgApellidoPat = document.getElementById("errMsgApellidoPat");
labelApellidoMat = document.getElementById("ContentPlaceHolder1_" + "TxtApellidoMat"); // "ContentPlaceHolder1_" + "TxtApellidoMat
errMsgApellidoMat = document.getElementById("errMsgApellidoMat");
labelRUC = document.getElementById("ContentPlaceHolder1_" + "TxtRUC"); // "ContentPlaceHolder1_" + "TxtRUC
errMsgRUC = document.getElementById("errMsgRUC");
labelPuntos = document.getElementById("ContentPlaceHolder1_" + "TxtPuntos"); // "ContentPlaceHolder1_" + "TxtPuntos
errMsgPuntos = document.getElementById("errMsgPuntos");

const specialChars = "<>@!#$%^&*()_+[]{}?:;|\"\\,./~`-=";
const regexSpecial = new RegExp("[" + specialChars + "]");
function validateTxtDNI() {
  if (labelDNI.value === "") {
    labelDNI.classList.add("is-invalid");
    errMsgDNI.innerHTML = "El campo DNI no puede estar vacío";
    errMsgDNI.style.display = "block";
    return false;
  }
  if (isNaN(labelDNI.value)) {
    labelDNI.classList.add("is-invalid");
    errMsgDNI.innerHTML = "El campo DNI debe ser un número";
    errMsgDNI.style.display = "block";
    return false;
  }
  if (labelDNI.value.length != 8) {
    labelDNI.classList.add("is-invalid");
    errMsgDNI.innerHTML = "El campo DNI debe tener 8 dígitos";
    errMsgDNI.style.display = "block";
    return false;
  }
  if (parseInt(labelDNI.value) < 0) {
    labelDNI.classList.add("is-invalid");
    errMsgDNI.innerHTML = "El campo DNI no puede ser negativo";
    errMsgDNI.style.display = "block";
    return false;
  }

  labelDNI.classList.remove("is-invalid");
  errMsgDNI.innerHTML = "";
  errMsgDNI.style.display = "none";
  return true;
}

function validateTxtNombre() {
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

function validateTxtApellidoPat() {
  if (labelApellidoPat.value === "") {
    labelApellidoPat.classList.add("is-invalid");
    errMsgApellidoPat.innerHTML = "El campo Apellido Paterno no puede estar vacío";
    errMsgApellidoPat.style.display = "block";
    return false;
  }

  if (regexSpecial.test(labelApellidoPat.value)) {
    labelApellidoPat.classList.add("is-invalid");
    errMsgApellidoPat.innerHTML = "El campo Apellido Paterno no puede contener caracteres especiales";
    errMsgApellidoPat.style.display = "block";
    return false;
  }

  labelApellidoPat.classList.remove("is-invalid");
  errMsgApellidoPat.innerHTML = "";
  errMsgApellidoPat.style.display = "none";
  return true;
}

function validateTxtApellidoMat() {
  if (labelApellidoMat.value === "") {
    labelApellidoMat.classList.add("is-invalid");
    errMsgApellidoMat.innerHTML = "El campo Apellido Materno no puede estar vacío";
    errMsgApellidoMat.style.display = "block";
    return false;
  }

  if (regexSpecial.test(labelApellidoMat.value)) {
    labelApellidoMat.classList.add("is-invalid");
    errMsgApellidoMat.innerHTML = "El campo Apellido Materno no puede contener caracteres especiales";
    errMsgApellidoMat.style.display = "block";
    return false;
  }

  labelApellidoMat.classList.remove("is-invalid");
  errMsgApellidoMat.innerHTML = "";
  errMsgApellidoMat.style.display = "none";
  return true;
}

function validateTxtPuntos() {
  if (labelPuntos.value === "") {
    labelPuntos.value = "0";
    return true;
  }
  if (isNaN(labelPuntos.value)) {
    labelPuntos.classList.add("is-invalid");
    errMsgPuntos.innerHTML = "El campo Puntos debe ser un número";
    errMsgPuntos.style.display = "block";
    return false;
  }
  if (regexSpecial.test(labelPuntos.value)) {
    labelPuntos.classList.add("is-invalid");
    errMsgPuntos.innerHTML = "El campo Puntos no puede contener caracteres especiales";
    errMsgPuntos.style.display = "block";
    return false;
  }
  if (parseInt(labelPuntos.value) < 0) {
    labelPuntos.classList.add("is-invalid");
    errMsgPuntos.innerHTML = "El campo Puntos no puede ser negativo";
    errMsgPuntos.style.display = "block";
    return false;
  }

  labelPuntos.classList.remove("is-invalid");
  errMsgPuntos.innerHTML = "";
  errMsgPuntos.style.display = "none";
  return true;
}

function validateTxtRUC() {
  if(labelRUC.value === "") {
    return true;
  }
  if(isNaN(labelRUC.value)) {
    labelRUC.classList.add("is-invalid");
    errMsgRUC.innerHTML = "El campo RUC debe ser un número";
    errMsgRUC.style.display = "block";
    return false;
  }
  if(labelRUC.value.length != 11) {
    labelRUC.classList.add("is-invalid");
    errMsgRUC.innerHTML = "El campo RUC debe tener 11 dígitos";
    errMsgRUC.style.display = "block";
    return false;
  }
  if (regexSpecial.test(labelRUC.value)) {
    labelRUC.classList.add("is-invalid");
    errMsgRUC.innerHTML = "El campo RUC no puede contener caracteres especiales";
    errMsgRUC.style.display = "block";
    return false;
  }

  labelRUC.classList.remove("is-invalid");
  errMsgRUC.innerHTML = "";
  errMsgRUC.style.display = "none";
}

function validateForm() {
  let validDNI = validateTxtDNI();
  let validNombre = validateTxtNombre();
  let validApellidoPat = validateTxtApellidoPat();
  let validApellidoMat = validateTxtApellidoMat();
  let validPuntos = validateTxtPuntos();
  let validRUC = validateTxtRUC();

  return validDNI && validNombre && validApellidoPat && validApellidoMat && validPuntos && validRUC;
}
$(document).ready(function () {
  
});