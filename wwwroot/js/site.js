// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function openModal(modalId) {
    $(`#${modalId}`).modal('show');
}

function closeModal(modalId) {
    $(`#${modalId}`).modal('hide');
}

function changeCursorToHand(elementId) {
    const element = document.getElementById(elementId);
    element.style.cursor = 'pointer';
}

function changeCursorToPointer(elementId) {
    const element = document.getElementById(elementId);
    element.style.cursor = 'auto';
}

function changeShownImage(inputFileId, targetId) {
    const inputFile = document.getElementById(inputFileId).files;

    const targetElement = document.getElementById(targetId);

    if (inputFile.length > 0 && inputFile[0].type.startsWith('image/')) {
        const imageUrl = URL.createObjectURL(inputFile[0])
        targetElement.innerHTML = `
            <div class="container m-2 p-4  rounded" style="border-radius: 1rem !important;">
                <img src="${imageUrl}" class="img-fluid" style="max-height: 360p: max-width: 400p">
            </div>`;
    } else {
        targetElement.innerHTML = `
            <div class="container m-2 p-4  rounded" style="background-color: rgb(235, 235, 235); border-radius: 1rem !important;">
                <div class="row d-flex my-2 justify-content-center align-items-center">
                    <svg xmlns="http://www.w3.org/2000/svg" width="100" height="100" fill="currentColor" class="bi bi-card-image" viewBox="0 0 16 16">
                        <path d="M6.002 5.5a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0z"/>
                        <path d="M1.5 2A1.5 1.5 0 0 0 0 3.5v9A1.5 1.5 0 0 0 1.5 14h13a1.5 1.5 0 0 0 1.5-1.5v-9A1.5 1.5 0 0 0 14.5 2h-13zm13 1a.5.5 0 0 1 .5.5v6l-3.775-1.947a.5.5 0 0 0-.577.093l-3.71 3.71-2.66-1.772a.5.5 0 0 0-.63.062L1.002 12v.54A.505.505 0 0 1 1 12.5v-9a.5.5 0 0 1 .5-.5h13z"/>
                    </svg>
                </div>
                <div class="row d-flex justify-content-center align-items-center text-center mt-1 mb-3">
                    Klik ini untuk tambah gambar
                </div>
            </div>`
    }
}