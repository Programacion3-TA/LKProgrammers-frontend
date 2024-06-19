function showModalForm() {
    var modalForm = new bootstrap.Modal(document.getElementById('modalForm'));
    modalForm.toggle();
}     

function showModalFormWarning() {
    var modalForm = new bootstrap.Modal(document.getElementById('modalWarning'));
    modalForm.show();
}

function showModalFormCursosDictados() {
    var modalForm = new bootstrap.Modal(document.getElementById('modalCursosDictados'));
    modalForm.show();
}

function validarFormulario() { }