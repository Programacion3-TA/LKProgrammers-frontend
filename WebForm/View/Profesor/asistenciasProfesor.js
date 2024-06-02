
const titleModal = document.getElementById('asistenRegistradasModalLabel');
const contentModal = document.getElementById('modalBody');

function showModal(idmodal) {
    var modalForm = new bootstrap.Modal(document.getElementById(idmodal));
    modalForm.show();
}    
function showUpdateModal(idmodal) {

    titleModal.textContent = "Asistencias actualizadas";
    contentModal.textContent = "Se actualizaron exitosamente las asistencias";


    showModal(idmodal);
}     
function showInsertModal(idmodal) {

    titleModal.textContent = "Asistencias registrada";
    contentModal.textContent = "Se registraron exitosamente las asistencias para el dia de hoy!.";


    showModal(idmodal);
}    