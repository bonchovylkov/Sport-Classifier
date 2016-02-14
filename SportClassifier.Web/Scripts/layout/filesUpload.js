'use strict';
APP.namespace('EVENT.FILEUPLOADER');
APP.EVENT.FILEUPLOADER = function () {
    //constants..............//
    var selector = "",
        browse = "",
        input = "",
        //selectors..............//

        //variables..............//




        setEventHandlers = function (callback) {
            $(selector).on("drop", function (e) {

                //var fd = new FormData();
                //fd.append("file", e.dataTransfer.files[0]);
                // uploadFile(fd);
                //callback(fd);
                
                callback(e.originalEvent.dataTransfer.files);
                APP.UTILS.preventDefault(e);
            });

            $(selector).on("dragover", function (event) {
                APP.UTILS.preventDefault(event);
            });

            $(browse).on("click", function () {
                $(input).trigger("click");
            });

            $(input).on("change", function () {
               // var fd = new FormData(document.forms[0]);
                callback(document.forms[0].uploder.files);
            });
        },

      

        initialize = function (dropSelectror,browseSelector,inputSelector,callback) {
            selector = dropSelectror;
            browse = browseSelector;
            input = inputSelector;
            setEventHandlers(callback);

           
        };

    return {
        init: initialize
    };
}();
