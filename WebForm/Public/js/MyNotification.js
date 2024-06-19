function showNotification(tipo, mensaje, titulo = "", href="") {
    // Tipos: Ok, Bad, Info
    // Mensaje: Cualquiera que no sea una carta de odio al ruso, tmr rayita
    const iconos = {
        "Ok": "fa-check",
        "Bad": "fa-exclamation-triangle",
        "Info": "fa-circle-info"
    };

    const modales = {
        "Ok": "alert-success",
        "Bad": "alert-danger",
        "Info": "alert-primary"
    };

    const modal = document.getElementById("my_notification_modal");
    const h4 = document.getElementById("my_notification_modal--h4");
    const icon = document.getElementById("my_notification_modal--icon");
    const span = document.getElementById("my_notification_modal--span");
    const a = document.getElementById("my_notification_modal--ancle");

    h4.innerText = titulo;
    modal.classList.add(modales[tipo]);
    a.classList.add("d-none");
    icon.classList.add(iconos[tipo]);
    span.innerText = mensaje;
    if (href != "") {
        modal.classList.add("noFade");
        a.setAttribute("href", href);
        a.classList.remove("d-none");
    } else {
        modal.classList.add("activo");
        setTimeout(() => {
            modal.classList.remove("activo");
            modal.classList.remove(modales[tipo]);
            icon.classList.remove(iconos[tipo]);
            a.classList.remove("d-none");
        }, 6000);
    }
}