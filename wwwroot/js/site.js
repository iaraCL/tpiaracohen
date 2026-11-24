function validarLogin()
{
    let nombreUsuario = document.getElementById("NombreUsuario").value;
    let contrasena = document.getElementById("Contrasena").value;

    if (nombreUsuario.trim() == "" || contrasena == "")
    {
        alert("Completa todos los campos");
        return false;
    }

    return true;
}




function validarRegistro()
{
    let nombre = document.getElementById("Nombre").value;
    let apellido = document.getElementById("Apellido").value;
    let nombreUsuario = document.getElementById("NombreUsuario").value;
    let contrasena = document.getElementById("Contrasena").value;

    let mensaje = document.getElementById("mensajeRegistro");

    mensaje.textContent = "";

    if (nombre.trim() == "" || apellido.trim() == "" ||
        nombreUsuario.trim() == "" || contrasena == "")
    {
        mensaje.textContent = "Completa todos los campos";
        return false;
    }

    if (nombre.length > 50 || apellido.length > 50 ||
        nombreUsuario.length > 50)
    {
        mensaje.textContent = "Los datos son demasiado largos";
        return false;
    }

    if (contrasena.length < 8)
    {
        mensaje.textContent = "La contraseña debe tener al menos 8 caracteres";
        return false;
    }

    return true;
}


let cantidad = 0;

let contenedorClases = document.getElementById("contenedorClases");

if (contenedorClases != null)
{
    cantidad = Number(contenedorClases.getAttribute("data-cantidad"));
}


function verMas()
{
    fetch("/Clases/VerMas?cantidad=" + cantidad)
    .then(response => response.json())
    .then(clases => {

        let contenido = "";

        for (let i = 0; i < clases.length; i++)
        {
            contenido += "<div class='card mb-3'>";
            contenido += "<div class='card-body'>";

            contenido += "<img src='/imagenes/" + clases[i].imagen +
                         "' width='250' height='150'>";

            contenido += "<h3>" + clases[i].nombre + "</h3>";

            contenido += "<p>" + clases[i].descripcion + "</p>";

            contenido += "<p>Profesor: " + clases[i].profesor + "</p>";

            contenido += "<p>Categoría: " + clases[i].categoria + "</p>";

            contenido += "<a class='btn btn-primary' href='/Clases/Detalle/" +
                         clases[i].id + "'>Ver detalle</a>";

            contenido += "</div>";
            contenido += "</div>";
        }

        document.getElementById("contenedorClases").innerHTML += contenido;

        cantidad = cantidad + clases.length;

        if (clases.length < 8)
        {
            document.getElementById("btnVerMas").style.display = "none";
        }

    })
    .catch(error => {
        console.error(error);
        alert("No se pudieron cargar las clases");
    });
}




function reservarClase(idHorarioClase)
{
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
    .then(response => response.json())
    .then(datos => {

        if (datos.resultado == 0)
        {
            mensaje.textContent = "Reserva realizada correctamente";
            mensaje.style.color = "green";

            setTimeout(function () {
                location.reload();
            }, 1500);
        }
        else if (datos.resultado == 1)
        {
            mensaje.textContent = "El horario no está disponible";
            mensaje.style.color = "red";
        }
        else if (datos.resultado == 2)
        {
            mensaje.textContent = "No podés reservar porque tenés dos cancelaciones este mes";
            mensaje.style.color = "red";
        }
        else if (datos.resultado == 3)
        {
            mensaje.textContent = "Ya reservaste este horario";
            mensaje.style.color = "red";
        }
        else if (datos.resultado == 4)
        {
            mensaje.textContent = "No quedan cupos disponibles";
            mensaje.style.color = "red";
        }
        else
        {
            mensaje.textContent = "No se pudo realizar la reserva";
            mensaje.style.color = "red";
        }

    })
    .catch(error => {
        console.error(error);
        mensaje.textContent = "Error al realizar la reserva";
        mensaje.style.color = "red";
    });
}




function cancelarReserva(idReserva)
{
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
    .then(response => response.json())
    .then(datos => {

        if (datos.resultado == 0)
        {
            mensaje.textContent = "Reserva cancelada correctamente";
            mensaje.style.color = "green";

            setTimeout(function () {
                location.reload();
            }, 1500);
        }
        else if (datos.resultado == 1)
        {
            mensaje.textContent = "No se puede cancelar esta reserva";
            mensaje.style.color = "red";
        }
        else
        {
            mensaje.textContent = "No se pudo cancelar la reserva";
            mensaje.style.color = "red";
        }

    })
    .catch(error => {
        console.error(error);
        mensaje.textContent = "Error al cancelar la reserva";
        mensaje.style.color = "red";
    });
}
