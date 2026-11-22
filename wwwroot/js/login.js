
function validarLogin() {
    let usuario = document.getElementById("NombreUsuario").value.trim();
    let contrasena = document.getElementById("Contrasena").value;

    if (usuario === "" || contrasena === "") {
        alert("Completa todos los campos");
        return false;
    }

    return true;
}
