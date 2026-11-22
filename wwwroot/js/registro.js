
function validarRegistro() {
    let nombre = document.getElementById("Nombre").value.trim();
    let apellido = document.getElementById("Apellido").value.trim();
    let usuario = document.getElementById("NombreUsuario").value.trim();
    let contrasena = document.getElementById("Contrasena").value;

    if (nombre === "" || apellido === "" ||
        usuario === "" || contrasena === "") {
        alert("Completa todos los campos");
        return false;
    }

    if (nombre.length > 50 || apellido.length > 50 ||
        usuario.length > 50) {
        alert("Los datos son demasiado largos");
        return false;
    }

    if (contrasena.length < 8) {
        alert("La contraseña debe tener al menos 8 caracteres");
        return false;
    }

    return true;
}
