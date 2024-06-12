
/*const cursoContainer = document.getElementById("cursosContainer");

const dataCurso = {
    curso: "Nombre curso",
    profesor: "Nombre profesor",
    codigo: "CodigoCurso"
} 
const colorPrueba = [
     "#6691FF",
     "#C6FF4E",
    "#FFDD66",
    "#FF6666"
]
const obtenerColor = () => { 
    let indice = Math.floor(Math.random() * colorPrueba.length);
    return colorPrueba[indice];
}

window.addEventListener("load", e => {
    dataArray = [];
    for (let i = 0; i < 8; i++) {
        dataArray.push(dataCurso);
    }

    cursoContainer.innerHTML += dataArray.map(data => {
        const color = obtenerColor();
        return `<a href="/View/CursoAlumno/CursoAlumno.aspx" class="d-flex flex-column cursoCaja" style="text-decoration:none;"> 
                <div class="h-50" style="background-color:${color}">
                </div>
                <div class="p-2 infoCaja">
                     <p>${data.curso}</p>
                     <div class="line"></div>
                     <p>Profesor: ${data.profesor}</p>
                     <p>Código curso: ${data.codigo}</p>
                </div>
            </a>`;
    }).join('');

});*/

function showModal(idmodal) {
    var modalForm = new bootstrap.Modal(document.getElementById(idmodal));
    modalForm.show();
}  