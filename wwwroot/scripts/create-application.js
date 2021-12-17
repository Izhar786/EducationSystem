"use strict";
var KTCreateAccount = (function () {
   
    return {
        init: function () {
            // Stepper lement
            var element = document.querySelector("#kt_create_application");

            // Initialize Stepper
            var stepper = new KTStepper(element);

            // Handle next step
            stepper.on("kt.stepper.next", function (stepper) {
                stepper.goNext(); // go next step
                KTUtil.scrollTop();
            });

            // Handle previous step
            stepper.on("kt.stepper.previous", function (stepper) {
                stepper.goPrevious(); // go previous step
                KTUtil.scrollTop();
            });
        },
    };
})();
KTUtil.onDOMContentLoaded(function () {
    KTCreateAccount.init();
});
