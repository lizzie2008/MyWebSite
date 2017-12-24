//必须复选框勾选验证
$.validator.addMethod("mustbetrue",
    function (value, element, parameters) {
        return value === "true";
    });

$.validator.unobtrusive.adapters.add("mustbetrue", [], function (options) {
    options.rules.mustbetrue = {};
    options.messages["mustbetrue"] = options.message;
});

//防止重复提交
$.validator.setDefaults({
    submitHandler: function (form) {
        $(form).find('[type="submit"]').attr('disabled', true);
        form.submit();
    }
});