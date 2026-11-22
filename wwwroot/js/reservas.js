
function reservarClase(idHorarioClase) {
    let mensaje = document.getElementById("mensajeReserva");
    mensaje.textContent = "Procesando reserva...";
    mensaje.style.color = "black";

    fetch("/Clases/Reservar", {
        method: "POST",
        headers: {
            "Content-Type": "application/x-www-form-urlencoded"
        },
        body: "idHorarioClase=" + idHorarioClase
    })
    .then(response => {
        if (!response.ok) {
            throw new Error("Error " + response.status);
        }
        return response.json();
    })
    .then(datos => {
        if (datos.resultado == 0) {
            mensaje.textContent = "Reserva realizada correctamente";
            mensaje.style.color = "green";

            setTimeout(function () {
                location.reload();
            }, 1500);
        }
        else if (datos.resultado == 1) {
            mensaje.textContent = "El horario no está disponible";
            mensaje.style.color = "red";
        }
        else if (datos.resultado == 2) {
            mensaje.textContent = "No podés reservar porque tenés dos cancelaciones este mes";
            mensaje.style.color = "red";
        }
        else if (datos.resultado == 3) {
            mensaje.textContent = "Ya reservaste este horario";
            mensaje.style.color = "red";
        }
        else if (datos.resultado == 4) {
            mensaje.textContent = "No quedan cupos disponibles";
            mensaje.style.color = "red";
        }
        else {
            mensaje.textContent = "No se pudo realizar la reserva";
            mensaje.style.color = "red";
        }
    })
    .catch(error => {
        mensaje.textContent = "No se pudo realizar la reserva (" + error.message + ")";
        mensaje.style.color = "red";
    });
}
