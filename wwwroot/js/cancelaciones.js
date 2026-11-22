
function cancelarReserva(idReserva) {
    let mensaje = document.getElementById("mensajeCancelacion");

    mensaje.textContent = "Cancelando reserva...";
    mensaje.style.color = "black";

    fetch("/Reservas/Cancelar", {
        method: "POST",
        headers: {
            "Content-Type": "application/x-www-form-urlencoded"
        },
        body: "idReserva=" + idReserva
    })
    .then(response => {
        if (!response.ok) {
            throw new Error("Error " + response.status);
        }

        return response.json();
    })
    .then(datos => {
        if (datos.resultado == 0) {
            mensaje.textContent = "Reserva cancelada correctamente";
            mensaje.style.color = "green";

            setTimeout(function () {
                location.reload();
            }, 1500);
        }
        else if (datos.resultado == 1) {
            mensaje.textContent = "No se puede cancelar esta reserva";
            mensaje.style.color = "red";
        }
        else {
            mensaje.textContent = "No se pudo cancelar la reserva";
            mensaje.style.color = "red";
        }
    })
    .catch(error => {
        mensaje.textContent = "Error al cancelar (" + error.message + ")";
        mensaje.style.color = "red";
    });
}
