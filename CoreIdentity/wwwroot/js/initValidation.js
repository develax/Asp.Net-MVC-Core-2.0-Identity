///////////////////
// http://nadeemkhedr.com/how-the-unobtrusive-jquery-validate-plugin-works-internally-in-asp-net-mvc/
///////////////////
(function () {

    var defaultOptions = {
        highlight: function (element, errorClass, validClass) {
            var $formGroup = $(element).closest(".form-group");
            $formGroup.find("span[data-valmsg-for]").addClass('invalid-feedback');
            $formGroup.find(".form-control").addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            var $formGroup = $(element).closest(".form-group");
            $formGroup.find("span[data-valmsg-for]").removeClass('invalid-feedback');
            $formGroup.find(".form-control").removeClass('is-invalid');
        }
    };

    $.validator.setDefaults(defaultOptions);
})();