var ss = ss || {};
(function () {
    ss.utils = ss.utils || {};

    ss.utils.updateHTMLDirAndLangFromLocalStorage = function () {
        var isRtl = JSON.parse(localStorage.getItem("SmartSoftware.IsRtl"));
        var htmlTag = document.getElementsByTagName("html")[0];

        if (htmlTag) {
            var selectedLanguage = localStorage.getItem("SmartSoftware.SelectedLanguage");
            if (selectedLanguage) {
                htmlTag.setAttribute("lang", selectedLanguage);
            }

            if (isRtl) {
                htmlTag.setAttribute("dir", "rtl");
            }
        }
    }

    ss.utils.updateHTMLDirAndLangFromLocalStorage();
})();