function isBlank(str) {
    return (!str || /^\s*$/.test(str));
}

function setTabClickEvent(numberOfTabs) {
    for (var i = 0; i < numberOfTabs; i++) {
        $("#section-shape-tab-" + i).click(function (e) {
            var parent = $("#")
            e.preventDefault();
        });
    }
}

function Pad(num, size) {
    var s = num + "";
    while (s.length < size) s = "0" + s;
    return s;
}

function setValueDatePicker(value, el) {
    if (value != undefined || !isBlank(value)) {
        value = new Date(value);
        if (value.getFullYear() != 1) {
            $(el).datepicker("update", new Date(value));
        }
        else {
            $(el).datepicker("update", "");
        }
    }
    return value;
}

