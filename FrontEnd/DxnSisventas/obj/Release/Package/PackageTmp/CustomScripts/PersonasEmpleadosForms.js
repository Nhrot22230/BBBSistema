let labelDNI, errMsgDNI;
let labelNombre, errMsgNombre;
let labelApellidoPat, errMsgApellidoPat;
let labelApellidoMat, errMsgApellidoMat;
let labelSueldo, errMsgSueldo;

const specialChars = "<>@!#$%^&*()_+[]{}?:;|\"\\,./~`-=";
const regexSpecial = new RegExp("[" + specialChars + "]");

labelDNI = document.getElementById("ContentPlaceHolder1_" + "TxtDNI"); // "ContentPlaceHolder1_" + "TxtDNI
errMsgDNI = document.getElementById("errMsgDNI");
labelNombre = document.getElementById("ContentPlaceHolder1_" + "TxtNombre"); // "ContentPlaceHolder1_" + "TxtNombre
errMsgNombre = document.getElementById("errMsgNombre");
labelApellidoPat = document.getElementById("ContentPlaceHolder1_" + "TxtApellidoPat"); // "ContentPlaceHolder1_" + "TxtApellidoPat
errMsgApellidoPat = document.getElementById("errMsgApellidoPat");
labelApellidoMat = document.getElementById("ContentPlaceHolder1_" + "TxtApellidoMat"); // "ContentPlaceHolder1_" + "TxtApellidoMat
errMsgApellidoMat = document.getElementById("errMsgApellidoMat");
labelSueldo = document.getElementById("ContentPlaceHolder1_" + "TxtSueldo"); // "ContentPlaceHolder1_" + "TxtSueldo
errMsgSueldo = document.getElementById("errMsgSueldo");

function validarFormulario() {
  let validDNI = validateTxtDNI();
  let validNombre = validateTxtNombre();
  let validApellidoPat = validateTxtApellidoPat();
  let validApellidoMat = validateTxtApellidoMat();
  let validSueldo = validateTxtSueldo();

  return validDNI && validNombre && validApellidoPat && validApellidoMat && validSueldo;
}

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

function validateTxtSueldo() {
  if (labelSueldo.value === "") {
    labelSueldo.classList.add("is-invalid");
    errMsgSueldo.innerHTML = "El campo Sueldo no puede estar vacío";
    errMsgSueldo.style.display = "block";
    return false;
  }
  if (isNaN(labelSueldo.value)) {
    labelSueldo.classList.add("is-invalid");
    errMsgSueldo.innerHTML = "El campo Sueldo debe ser un número";
    errMsgSueldo.style.display = "block";
    return false;
  }
  if (labelSueldo.value < 0) {
    labelSueldo.classList.add("is-invalid");
    errMsgSueldo.innerHTML = "El campo Sueldo debe ser mayor o igual a 0";
    errMsgSueldo.style.display = "block";
    return false;
  }

  labelSueldo.classList.remove("is-invalid");
  errMsgSueldo.innerHTML = "";
  errMsgSueldo.style.display = "none";
  return true;
}

$(document).ready(function () {
  labelDNI = document.getElementById("ContentPlaceHolder1_" + "TxtDNI"); // "ContentPlaceHolder1_" + "TxtDNI
  errMsgDNI = document.getElementById("errMsgDNI");
  
  labelNombre = document.getElementById("ContentPlaceHolder1_" + "TxtNombre"); // "ContentPlaceHolder1_" + "TxtNombre
  errMsgNombre = document.getElementById("errMsgNombre");
  
  labelApellidoPat = document.getElementById("ContentPlaceHolder1_" + "TxtApellidoPat"); // "ContentPlaceHolder1_" + "TxtApellidoPat
  errMsgApellidoPat = document.getElementById("errMsgApellidoPat");
  
  labelApellidoMat = document.getElementById("ContentPlaceHolder1_" + "TxtApellidoMat"); // "ContentPlaceHolder1_" + "TxtApellidoMat
  errMsgApellidoMat = document.getElementById("errMsgApellidoMat");

  labelSueldo = document.getElementById("ContentPlaceHolder1_" + "TxtSueldo"); // "ContentPlaceHolder1_" + "TxtSueldo
  errMsgSueldo = document.getElementById("errMsgSueldo");
});
