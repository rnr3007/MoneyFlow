// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function openModal(modalId) {
    $(`#${modalId}`).modal('show');
}

function closeModal(modalId) {
    $(`#${modalId}`).modal('hide');
}

function changeFilePreview(inputFileId, targetId) {
    const inputFile = document.getElementById(inputFileId).files;

    const targetElement = document.getElementById(targetId);
    console.log(inputFile[0].type);
    if (inputFile.length > 0) {
        switch (inputFile[0].type) {
            case 'image/png':
            case 'image/jpeg':
                const imageUrl = URL.createObjectURL(inputFile[0])
                targetElement.innerHTML = `
                    <div class="container m-2 p-4  rounded" style="border-radius: 1rem !important;">
                        <img src="${imageUrl}" class="img-fluid" style="max-height: 360p: max-width: 400p">
                    </div>`;
                break;
            case 'application/pdf':
                targetElement.innerHTML = `
                    <div class="container m-2 p-4 rounded" style="border-radius: 1rem !important;">
                        <div class="row justify-content-center">
                            <svg xmlns="http://www.w3.org/2000/svg" width="50" height="50" fill="currentColor" class="bi bi-filetype-pdf" viewBox="0 0 16 16">
                            <path fill-rule="evenodd" d="M14 4.5V14a2 2 0 0 1-2 2h-1v-1h1a1 1 0 0 0 1-1V4.5h-2A1.5 1.5 0 0 1 9.5 3V1H4a1 1 0 0 0-1 1v9H2V2a2 2 0 0 1 2-2h5.5L14 4.5ZM1.6 11.85H0v3.999h.791v-1.342h.803c.287 0 .531-.057.732-.173.203-.117.358-.275.463-.474a1.42 1.42 0 0 0 .161-.677c0-.25-.053-.476-.158-.677a1.176 1.176 0 0 0-.46-.477c-.2-.12-.443-.179-.732-.179Zm.545 1.333a.795.795 0 0 1-.085.38.574.574 0 0 1-.238.241.794.794 0 0 1-.375.082H.788V12.48h.66c.218 0 .389.06.512.181.123.122.185.296.185.522Zm1.217-1.333v3.999h1.46c.401 0 .734-.08.998-.237a1.45 1.45 0 0 0 .595-.689c.13-.3.196-.662.196-1.084 0-.42-.065-.778-.196-1.075a1.426 1.426 0 0 0-.589-.68c-.264-.156-.599-.234-1.005-.234H3.362Zm.791.645h.563c.248 0 .45.05.609.152a.89.89 0 0 1 .354.454c.079.201.118.452.118.753a2.3 2.3 0 0 1-.068.592 1.14 1.14 0 0 1-.196.422.8.8 0 0 1-.334.252 1.298 1.298 0 0 1-.483.082h-.563v-2.707Zm3.743 1.763v1.591h-.79V11.85h2.548v.653H7.896v1.117h1.606v.638H7.896Z"></path>
                            </svg>
                        </div>
                        <div class="row justify-content-center my-2">
                            ${inputFile[0].name}
                        </div>
                    </div>`
                break;
            default:
                targetElement.innerHTML = `
                    <div class="container m-2 p-4 rounded" style="border-radius: 1rem !important;">
                        <div class="row justify-content-center">
                            <svg xmlns="http://www.w3.org/2000/svg" width="50" height="50" fill="currentColor" class="bi bi-file-earmark-x" viewBox="0 0 16 16">
                                <path d="M6.854 7.146a.5.5 0 1 0-.708.708L7.293 9l-1.147 1.146a.5.5 0 0 0 .708.708L8 9.707l1.146 1.147a.5.5 0 0 0 .708-.708L8.707 9l1.147-1.146a.5.5 0 0 0-.708-.708L8 8.293 6.854 7.146z"/>
                                <path d="M14 14V4.5L9.5 0H4a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h8a2 2 0 0 0 2-2zM9.5 3A1.5 1.5 0 0 0 11 4.5h2V14a1 1 0 0 1-1 1H4a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1h5.5v2z"/>
                            </svg>
                        </div>
                        <div class="row justify-content-center my-2">File tidak didukung</div>
                    </div>`
                break;
        }
    }
}

function openNewTab(url) {
    window.open(
        url,
        '_blank'
    );
}

function inputExcel(inputFormId, inputContainerId) {
    var inputContainer = document.getElementById(inputContainerId);

    var inputElement = document.createElement('input');
    inputContainer.appendChild(inputElement);
    
    inputElement.name = 'formFile';
    inputElement.type = 'file';
    inputElement.className = 'form-control d-none';
    inputElement.required = true;
    inputElement.accept = '.xls, .xlsx, .csv';

    inputElement.onchange = function(event) {
        var formData = new FormData();
        formData.append('formFile', event.target.files[0]);
        document.getElementById(inputFormId).submit();
    };
    
    inputElement.click();
}

function submitUpdate(submitFormId) {
    const submitElement = document.getElementById(submitFormId);
    submitElement.submit();
}

function setupPopover() {
    $('[data-toggle="popover"]').popover();
}