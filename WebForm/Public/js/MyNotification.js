function showNotification(tipo, mensaje) {
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
    const icon = document.getElementById("my_notification_modal--icon");
    const span = document.getElementById("my_notification_modal--span");

    modal.classList.add("activo");
    modal.classList.add(modales[tipo]);
    icon.classList.add(iconos[tipo]);
    span.innerText = mensaje;
    setTimeout(() => {
        modal.classList.remove("activo");
        modal.classList.remove(modales[tipo]);
        icon.classList.remove(iconos[tipo]);
    }, 6000);
}