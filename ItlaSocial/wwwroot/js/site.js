// Site.js

(function () {

    var $sidebarAndWrapper = $("#sidebar, #wrapper");
    var $icon = $("#sidebarToggle i.fa");

    $("#sidebarToggle").on("click", function () {
        $sidebarAndWrapper.toggleClass("hide-sidebar");
        $icon.removeClass(!$sidebarAndWrapper.hasClass("hide-sidebar") ? "fa-align-justify" : "fa-arrow-left");
        $icon.addClass($sidebarAndWrapper.hasClass("hide-sidebar") ? "fa-align-justify" : "fa-arrow-left");
    });
})();