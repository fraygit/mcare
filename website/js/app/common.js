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