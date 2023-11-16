// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const requiredMaterialize = (form = 'formAttach') => {
    var requireds = document.getElementById(form).querySelectorAll('input');
    requireds.forEach(input => {
        if (input.dataset.hasOwnProperty('valRequired') && input.type != "checkbox") {
            input.required = true;
            var divSuperior = input.closest('div');
            var span = divSuperior.querySelector('span');
            if (span != undefined) {
                span.dataset.error = input.dataset.valRequired;
            }
            return;
        }
    })
}

const validateMaterialize = (form = 'formAttach') => {
    let inputs = document.getElementById(form).querySelectorAll('.input-validation-error');
    inputs.forEach(input => {
        input.classList.add('invalid');
        if (input.dataset.hasOwnProperty('valRequired') && input.value == '') {
            input.required = true;
            var divSuperior = input.closest('div');
            var span = divSuperior.querySelector('span');
            if (span != undefined) {
                span.dataset.error = input.dataset.valRequired;
            }
            return;
        }
    })
}