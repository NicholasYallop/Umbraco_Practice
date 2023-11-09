(function () {
    angular.module('umbraco.services').factory('stringService', function () {
        return {
            removeParagraphTags: function (html) {
                if (!html) {
                    return "";
                }
                html = html.trim();
                if (html.startsWith("<p>") && html.endsWith("</p>")) {
                    return html.substring(3, html.length - 4);
                }
                return html;
            },
        };
    });
})();
