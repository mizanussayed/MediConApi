'use strict';

document.addEventListener('DOMContentLoaded', function (e) {
    const formAuthentication = document.querySelector('#formAuthentication');
    if (!formAuthentication) return;

    const fv = FormValidation.formValidation(formAuthentication, {
        fields: {
            userName: {
                validators: {
                    notEmpty: {
                        message: 'Please enter email / username'
                    },
                    stringLength: {
                        min: 2,
                        message: 'Username must be more than 2 characters'
                    }
                }
            },
            password: {
                validators: {
                    notEmpty: {
                        message: 'Please enter your password'
                    },
                    stringLength: {
                        min: 2,
                        message: 'Password must be more than 2 characters'
                    }
                }
            },
        },
        plugins: {
            trigger: new FormValidation.plugins.Trigger(),
            bootstrap5: new FormValidation.plugins.Bootstrap5({
                eleValidClass: '',
                rowSelector: '.mb-3'
            }),
            submitButton: new FormValidation.plugins.SubmitButton(),
            defaultSubmit: new FormValidation.plugins.DefaultSubmit(),
            autoFocus: new FormValidation.plugins.AutoFocus()
        },
        init: instance => {
            instance.on('plugins.message.placed', function (e) {
                if (e.element.parentElement.classList.contains('input-group')) {
                    e.element.parentElement.insertAdjacentElement('afterend', e.messageElement);
                }
            });
        }
    });

    fv.on('core.form.valid', function () {
        $("#alert").hide();

        $(".card .card-body").block({
            message: '<div class="spinner-border text-primary" role="status"></div>',
            timeout: 1000,
            css: {
                backgroundColor: "transparent",
                border: "0"
            },
            overlayCSS: {
                backgroundColor: "#000",
                opacity: 0.25
            }
        })
    })
});
