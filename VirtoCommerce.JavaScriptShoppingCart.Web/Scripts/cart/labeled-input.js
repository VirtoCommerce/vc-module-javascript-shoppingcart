cartModule.component('vcLabeledInput', {
    templateUrl: "labeled-input.tpl.html",
    bindings: {
        value: '=',
        form: '=',
        name: '@',
        placeholder: '@',
        type: '@?',
        required: '<',
        requiredError: '@?',
        autofocus: '<',
        readonly: '<'
    },
    controller: [function () {
        var $ctrl = this;
        
        $ctrl.validate = function () {
            $ctrl.form.$setSubmitted();
            return $ctrl.form.$valid;
        };

    }]
});