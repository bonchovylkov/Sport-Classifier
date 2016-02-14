//how to use it
//https://github.com/bonchovylkov/VasMobile/blob/master/VasMobile/scripts/app/data-persister.js

'use strict';
APP.namespace('HTTPREQUESTER');
APP.HTTPREQUESTER = function () {
    var
    getJSON = function (requestUrl, headers) {
        var promise = new RSVP.Promise(function (resolve, reject) {
            $.ajax({
                url: requestUrl,
                type: "GET",
                dataType: "json",
                headers: headers,
                success: function (data) {
                    resolve(data);
                },
                error: function (err) {
                    reject(err);
                }
            });
        });
        return promise;
    },

 postJSON = function (requestUrl, data, headers) {
     var promise = new RSVP.Promise(function (resolve, reject) {
         $.ajax({
             url: requestUrl,
             type: "POST",
             contentType: "application/json",
             data: JSON.stringify(data),
             dataType: "json",
             headers: headers,
             success: function (data) {
                 resolve(data);
             },
             error: function (err) {
                 reject(err);
             }
         });
     });
     return promise;
 },

  putJSON = function (requestUrl, data, headers) {
      var promise = new RSVP.Promise(function (resolve, reject) {
          $.ajax({
              url: requestUrl,
              type: "PUT",
              contentType: "application/json",
              data: JSON.stringify(data),
              headers: headers,
              dataType: "json",
              success: function (data) {
                  resolve(data);
              },
              error: function (err) {
                  reject(err);
              }
          });
      });
      return promise;
  };
    return {
        getJSON: getJSON,
        postJSON: postJSON,
        putJSON: putJSON
    }
}();

