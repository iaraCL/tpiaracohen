let cantidad = 0;

document.addEventListener("DOMContentLoaded", function () {
    let contenedor = document.getElementById("contenedorClases");
    cantidad = Number(contenedor.dataset.cantidad);
});

function verMas() {
    fetch("/Clases/VerMas?cantidad=" + cantidad)
        .then(response => {
            if (!response.ok) {
                throw new Error("Error al cargar clases");
            }
            return response.json();
        })
        .then(clases => {
            let contenedor = document.getElementById("contenedorClases");

            clases.forEach(clase => {
                let tarjeta = document.createElement("div");
                tarjeta.className = "card mb-3";

                let cuerpo = document.createElement("div");
                cuerpo.className = "card-body";

                let imagen = document.createElement("img");
                imagen.src = "/imagenes/" + clase.imagen;
                imagen.alt = clase.nombre;
                imagen.style.width = "250px";
                imagen.style.height = "150px";
                imagen.style.objectFit = "cover";

                let titulo = document.createElement("h3");
                titulo.textContent = clase.nombre;

                let descripcion = document.createElement("p");
                descripcion.textContent = clase.descripcion;

                let profesor = document.createElement("p");
                profesor.textContent = "Profesor: " + clase.profesor;

                let categoria = document.createElement("p");
                categoria.textContent = "Categoría: " + clase.categoria;


                cuerpo.appendChild(imagen);
                cuerpo.appendChild(titulo);
                cuerpo.appendChild(descripcion);
                cuerpo.appendChild(profesor);
                cuerpo.appendChild(categoria);

                let botonDetalle = document.createElement("a");
                botonDetalle.href = "/Clases/Detalle/" + clase.id;
                botonDetalle.className = "btn btn-primary";
                botonDetalle.textContent = "Ver detalle";

                cuerpo.appendChild(botonDetalle);


                tarjeta.appendChild(cuerpo);
                contenedor.appendChild(tarjeta);
            });

            cantidad += clases.length;

            if (clases.length < 8) {
                document.getElementById("btnVerMas").style.display = "none";
            }
        })
        .catch(error => {
            alert("No se pudieron cargar más clases");
        });
}
