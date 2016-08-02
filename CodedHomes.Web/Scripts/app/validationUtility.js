var ValidationUtility = function () {
    var validationElements = $('[data-role="validate"]');
    var elementCount = 0; //show popover for first element that is invalid

    validationElements.popover({
        placement: 'top'
    });

    validationElements.on('invalid', function () { //html for fires invalid event
        if (elementCount === 0) {
            $('#' + this.id).popover('show');
            elementCount++;
        }
    });

    validationElements.on('blur', function () {
        $('#' + this.id).popover('hide');
    });

    var validate = function (formSelector) {
        elementCount = 0;

        if (formSelector.indexOf('#') === -1) {
            formSelector = '#' + formSelector;
        }

        return $(formSelector)[0].checkValidity();
    };

    return {
        validate: validate
    };
};