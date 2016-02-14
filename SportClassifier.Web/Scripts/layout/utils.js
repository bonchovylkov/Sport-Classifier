/// <reference path="utils.js" />
'use strict';
APP.namespace('UTILS');
APP.UTILS = function () {
    //constants
    var USA_COUNTRY_INDEX_DATABASE = 9,
        PROFILE_CONNECTION_FOLLOWING_TAB = 'following',
        PROFILE_CONNECTION_FOLLOWERS_TAB = 'followers',
        PROFILE_CONNECTION_CONNECTIONS_TAB = 'connections',
        PROFILE_CONNECTION_REQUESTS_TAB = 'requests',
        GET_COUTNTRY_CITIES_URL = '/api/EventApi/GetCountryCities',
        GET_AMATEUR_CLUBS_URL = "/api/AmateurApi/GetAmateurClubsByName",

        DAY_DROPDOWN_DEFAULT_VALUE = 'Choose day',
        MONTH_DROPDOWN_DEFAULT_VALUE = 'Choose month',
        YEAR_DROPDOWN_DEFAULT_VALUE = 'Choose year',
        NATIONALITY_DROPDOWN_DEFAULT_VALUE = 'Choose nationality',
        COUNTRY_DROPDOWN_DEFAULT_VALUE = 'Choose country',
        STATE_DROPDOWN_DEFAULT_VALUE = 'Choose state',

        //selectors
        loadingSelector = "#loading",
        browseSelector = '#browse',

        //variables
        keys = [36, 37, 38, 39, 40],

        //functions
        preventDefault = function (e) {
            e = e || window.event;
            if (e.preventDefault)
                e.preventDefault();
            e.returnValue = false;
        },

        keydown = function (e) {
            for (var i = keys.length; i--;) {
                if (e.keyCode === keys[i]) {
                    preventDefault(e);
                    return;
                }
            }
        },

        getImgSize = function (imgSrc)
        {
            var img = new Image(), curHeight, curWidth;
            img.onload = function () {
                curHeight = img.height;
                curWidth = img.width;

                return { height: curHeight, width: curWidth };
            }
            img.src = imgSrc;
        },

        showLoading = function () {
            $(loadingSelector).show();
        },

        hideLoading = function () {
            $(loadingSelector).hide();
        },
          onlyUniqueItems = function (value, index, self) {
              return self.map(function (e) { return e.label; }).indexOf(value.label) === index;
          },

          openIframeFileProfile = function () {
              try {
                  //$("#croping").show();
                  //$("#imgUploader", window.frames[0].document.body).trigger("click");
                  //$(browseSelector, window.frames[0].document.body).hide();
                  $("#imgUploader", $("#profilePicIFrame").contents()).trigger("click");
                  $(browseSelector, $("#profilePicIFrame").contents()).hide();

              }
              catch (e) {
                  $(loadingSelector, window.frames[0].document.body).hide();
                  $(browseSelector, window.frames[0].document.body).show();
              }
          },

        wheel = function (e) {
            preventDefault(e);
        },

        enableScroll = function () {
            if (window.removeEventListener) {
                window.removeEventListener('DOMMouseScroll', wheel, false);
            }
            window.onmousewheel = document.onmousewheel = document.onkeydown = null;
        },

        disableScroll = function () {
            if (window.addEventListener) {
                window.addEventListener('DOMMouseScroll', wheel, false);
            }
            window.onmousewheel = document.onmousewheel = wheel;
            document.onkeydown = keydown;
        },

        isNullUndefinedOrEmpty = function (input) {
            return (input === null || input === undefined || input === '');
        },

        toTitleCase = function (str) {
            return str.replace(/\w\S*/g, function (txt) { return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase(); });
        },

            afterRenderRegisterPopUp = function () {
                $("#edit-popup-close").on("click", function (e) {
                    var clickedItem = $(e.currentTarget);
                    var dontShowCheckBox = clickedItem.parent().find("#squaredTwo");
                  
                    clickedItem.parent().hide();
                });

                $("#pop-up-edit-trigger").on("click", function () {
                    $("#sideContainer #btnEdit")[0].click(); //.trigger("click");
                });
            },

        getAmateurClubsForAutocomplete = function (request, response) {
            if ($.trim(request.term).length > 3) {
                var dto = { Name: request.term };

                APP.HTTPREQUESTER.postJSON(GET_AMATEUR_CLUBS_URL, ko.toJS(dto))
                    .then(function (data) {
                        if (data.Success) {
                            var clubs = $.map(data.Clubs, function (item) {
                                return {
                                    label: APP.UTILS.toTitleCase(item.Name),
                                    value: APP.UTILS.toTitleCase(item.Name)
                                };
                            }).filter(onlyUniqueItems);

                            response(clubs);
                        }
                    }, function (err) {
                        //ex handler
                    });
            }
        },

        getCitiesForAutocomplete = function (request, response, countrySelectedId) {
            if ($.trim(request.term).length > 3) {
                var dto = { Name: request.term, fkCountry: APP.UTILS.isNullUndefinedOrEmpty(countrySelectedId) || countrySelectedId < 0 ? 0 : countrySelectedId };

                $.ajax({
                    type: 'POST',
                    url: GET_COUTNTRY_CITIES_URL,
                    contentType: "application/json",
                    dataType: 'json',
                    data: JSON.stringify(dto),
                    success: function (result) {
                        if (result.Success) {
                            var cities = $.map(result.Cities, function (item) {
                                return {
                                    label: APP.UTILS.toTitleCase(item.Name) + ', ' + item.Country,
                                    value: APP.UTILS.toTitleCase(item.Name)
                                };
                            }).filter(onlyUniqueItems);
                            response(cities);
                        }
                    },
                    error: function (request, status, error) {
                        console.log(request);
                    }
                });
            }
        },

        setDropDownEventHandler = function () {
            $(".drop-trigger").on("click", function (event) {

                if (!$(this).next().is(":visible")) {
                    $(this).next().slideDown();
                    event.stopPropagation();
                    $(document).click(function () {
                        $(".drop-down").slideUp();
                        $(document).unbind("click");
                    });
                }
                else {
                    $(this).next().slideUp();
                }
                preventDefault(event);
            });
        },

        setAutocompleteHandler = function (selector, dropdown) {
            $(document).keyup(function (event) {
                var completeSelector = selector + '_child';
                if ($(completeSelector).is(":visible")) {
                    var letterPressed = String.fromCharCode(event.which);
                    var countryName = $(selector + ' option').filter(function () { return $(this).html().lastIndexOf(letterPressed, 0) === 0; }).val();
                    if (countryName) {
                        dropdown.set('value', countryName);
                        var opt = $(completeSelector + ' li.selected');
                        if (opt.length > 0) {
                            opt.focus();
                            var pos = parseInt(($(opt).position().top));
                            var ch = parseInt($(completeSelector).height());
                            var top = pos + $(completeSelector).scrollTop() - (ch / 2);
                            $(completeSelector).animate({ scrollTop: top }, 500);
                        };
                    }
                }
            });
        },

        wait = function (time) {
            setTimeout(empty, time);
        },

        empty = function () {

        },

        getCustomDateFormat = function (date) {
            var m_names = new Array("January", "February", "March",
               "April", "May", "June", "July", "August", "September",
               "October", "November", "December");
            var d = new Date(date);
            var curr_date = d.getDate();
            var curr_month = d.getMonth();
            var curr_year = d.getFullYear();
            return (m_names[curr_month] + " " + curr_date + " " + curr_year);
            //  document.write(m_names[curr_month] + " " + curr_date + " " + curr_year);
        },

        stringFormat = function (format) {
            var args = Array.prototype.slice.call(arguments, 1);
            return format.replace(/{(\d+)}/g, function (match, number) {
                return typeof args[number] != 'undefined'
                  ? args[number]
                  : match
                ;
            });
        },

        deleteConfirm = function (event, saveCallback) {
            $("#dialog-delete").dialog({
                resizable: false,
                height: 140,
                modal: true,
                buttons: {
                    "Save": function () {
                        $(this).dialog("close");
                        saveCallback(event);
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                    }
                }
            });
        },


        startTagFBox = function (event) {
            var isSelectorOpened = APP.UTILS.FSTAGGING.isInitialized();
            if (!isSelectorOpened) {

                var id = $(event.currentTarget).attr("dataPictureId");

                var isPostPicture = !($(event.currentTarget).data("is-event-item"));

                APP.UTILS.FSTAGGING.init("img.fancybox-image", id, false, isPostPicture);
                $(event.currentTarget).find("span").text("Done tagging");
            } else {
                APP.UTILS.FSTAGGING.disable("img.fancybox-image");
                $(event.currentTarget).find("span").text("Tag photo");
            }

        },

        startTaggingVideoFBox = function (event) {
            var isSelectorOpened = APP.UTILS.FSTAGGING.isInitialized();
            if (!isSelectorOpened) {

                var id = $(event.currentTarget).attr("dataVideoId");
                APP.UTILS.FSTAGGING.init(".fancybox-iframe", id);
                APP.UTILS.FSTAGGING.initializeNewTagWindow(".fancybox-iframe", null, null, 5, 500);
                $(event.currentTarget).find("span").text("Done tagging");
            } else {
                APP.UTILS.FSTAGGING.disable(".fancybox-iframe");
                $(event.currentTarget).find("span").text("Tag video");
            }

        },


        showTagsFBox = function (event) {
            var areTagsShown = APP.UTILS.FSTAGGING.areTagsShown();
            if (areTagsShown) {
                APP.UTILS.FSTAGGING.hideTags();
                $(event.currentTarget).find("span").text("Show tags");
            } else {
                APP.UTILS.FSTAGGING.showTags("img.fancybox-image");
                $(event.currentTarget).find("span").text("Hide tags");
            }

        },

        showAllCommentButton = function (id) {
            var element = $(".fancybox-inner *[data-show-comment-id='" + id + "']");
            element.prev('a').hide();
            element.show();
        },

        closeTagBoxIfOpenedFBox = function () {
            var isSelectorOpened = APP.UTILS.FSTAGGING.isInitialized();
            if (isSelectorOpened) {
                APP.UTILS.FSTAGGING.disable("img.fancybox-image");
            }
        },

        setDropdownDefaultValueText = function (dropdownSelector, defaultValueText) {
            if ($(dropdownSelector).length > 0) {
                var defaultOption = $(dropdownSelector + ' option[data-id="-1"]');
                if (defaultOption.length == 0) {
                    $(dropdownSelector).prepend($('<option>', {
                        'data-id': '-1',
                        value: defaultValueText,
                        text: defaultValueText
                    }));

                    defaultOption = $(dropdownSelector + ' option[data-id="-1"]')
                }
                else {
                    defaultOption.text(defaultValueText);
                    defaultOption.attr('name', defaultValueText);
                }
            }
        },

        getParameterByName = function (name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        };

    return {
        startTagFBox: startTagFBox,
        setDropdownDefaultValueText: setDropdownDefaultValueText,
        startTaggingVideoFBox: startTaggingVideoFBox,
        showTagsFBox: showTagsFBox,
        closeTagBoxIfOpenedFBox:closeTagBoxIfOpenedFBox,
        preventDefault: preventDefault,
        getParameterByName: getParameterByName,
        enableScroll: enableScroll,
        disableScroll: disableScroll,
        isNullUndefinedOrEmpty: isNullUndefinedOrEmpty,
        toTitleCase: toTitleCase,
        setDropDownEventHandler: setDropDownEventHandler,
        setAutocompleteHandler: setAutocompleteHandler,
        getCustomDateFormat: getCustomDateFormat,
        openIframeFileProfile: openIframeFileProfile,
        getCitiesForAutocomplete: getCitiesForAutocomplete,
        getAmateurClubsForAutocomplete: getAmateurClubsForAutocomplete,
        showLoading: showLoading,
        hideLoading: hideLoading,
        stringFormat: stringFormat,
        deleteConfirm: deleteConfirm,
        getImgSize: getImgSize,
        afterRenderRegisterPopUp: afterRenderRegisterPopUp,
        showAllCommentButton: showAllCommentButton,

        USA_COUNTRY_INDEX_DATABASE: USA_COUNTRY_INDEX_DATABASE,
        PROFILE_CONNECTION_FOLLOWING_TAB: PROFILE_CONNECTION_FOLLOWING_TAB,
        PROFILE_CONNECTION_FOLLOWERS_TAB: PROFILE_CONNECTION_FOLLOWERS_TAB,
        PROFILE_CONNECTION_CONNECTIONS_TAB: PROFILE_CONNECTION_CONNECTIONS_TAB,
        PROFILE_CONNECTION_REQUESTS_TAB: PROFILE_CONNECTION_REQUESTS_TAB,
        DAY_DROPDOWN_DEFAULT_VALUE: DAY_DROPDOWN_DEFAULT_VALUE,
        MONTH_DROPDOWN_DEFAULT_VALUE: MONTH_DROPDOWN_DEFAULT_VALUE,
        YEAR_DROPDOWN_DEFAULT_VALUE: YEAR_DROPDOWN_DEFAULT_VALUE,
        NATIONALITY_DROPDOWN_DEFAULT_VALUE: NATIONALITY_DROPDOWN_DEFAULT_VALUE,
        COUNTRY_DROPDOWN_DEFAULT_VALUE: COUNTRY_DROPDOWN_DEFAULT_VALUE,
        STATE_DROPDOWN_DEFAULT_VALUE: STATE_DROPDOWN_DEFAULT_VALUE,
        wait: wait
    }
}();


