// add document ready event listener
const usernameInput = document.getElementById("username");
const usernameError = document.getElementById("username-error");
const passwordInput = document.getElementById("password");
const passwordError = document.getElementById("password-error");
const loginForm = document.getElementById("loginForm");

function validateUsername() {
  if (usernameInput.value === "") {
    usernameError.style.display = "block";
    return false;
  }
  usernameError.style.display = "none";
  return true;
}

function validatePassword() {
  if (passwordInput.value === "") {
    passwordError.style.display = "block";
    return false;
  }
  passwordError.style.display = "none";
  return true;
}

function validateForm() {
  let validUsername = validateUsername();
  let validPassword = validatePassword();

  if (!validUsername || !validPassword) {
    alert("Los campos no pueden estar vacíos");
    return false;
  }

  return true;
}

loginForm.addEventListener("submit", (event) => {
  if (!validateForm()) {
    event.preventDefault();
  }
});

function showErrorPanel() {
  $(".error-panel").fadeIn().delay(3000).fadeOut();
}

$(document).ready(function () {
  console.log("ready!");
});
