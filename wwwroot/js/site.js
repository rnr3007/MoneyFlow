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