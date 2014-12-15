tr.ajaxProxy = function () {

    var sendAjaxRequest = function (url, jsonRequest, onSuccessCallback, onFailureCallback, onComplete) {


        var opts = {lines: 9, length: 0, width: 15, radius: 30, corners: 1, rotate: 30, direction: 1, color: '#000', speed: 0.8, 
            trail: 60, shadow: false, hwaccel: false, className: 'spinner', zIndex: 2e9, top: '50%', left: '50%' 
        };

        var target = document.getElementById('body');
        var spinner = new Spinner(opts);
        spinner.spin(target);

        var onFailure = function (data) {
            if (data.status == 200) {
                window.location.href = tr.urls.mainPage;
            } else {
                var response = {
                    Message: tr.messages.technicalError
                };
                if (onFailureCallback) {
                    onFailureCallback(response);
                } else {
                    tr.alert(tr.messages.technicalError);
                }
            }
        };

        var onSuccess = function (response) {
            if (response && response.Status === true && onSuccessCallback) {
                onSuccessCallback(response);
            }
            else {
                tr.alert(response.Message);
            }
        };

        $.ajax({
            url: url,
            type: 'POST',
            data: jsonRequest,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: onSuccess,
            error: onFailure,
            complete: onComplete
        }).always(function () {
            spinner.stop();
        });

    };

    return {
        initReportControls: function (onSuccess, onFailure, onComplete) {
            sendAjaxRequest(tr.urls.initReportControls, null, onSuccess, onFailure, onComplete);
        },
        getDataSource: function (jsonData, onSuccess, onFailure, onComplete) {
            sendAjaxRequest(tr.urls.getDataSource, jsonData, onSuccess, onFailure, onComplete);
        },
        loadTemplate: function (onSuccess, onFailure, onComplete) {
            sendAjaxRequest(tr.urls.loadTemplate, null, onSuccess, onFailure, onComplete);
        },
        loadDemoTemplate: function (onSuccess, onFailure, onComplete) {
            sendAjaxRequest(tr.urls.loadDemoTemplate, null, onSuccess, onFailure, onComplete);
        },
        saveTemplate: function (jsonData, onSuccess, onFailure, onComplete) {
            sendAjaxRequest(tr.urls.saveTemplate, jsonData, onSuccess, onFailure, onComplete);
        },
        printReport: function (jsonData, onSuccess, onFailure, onComplete) {
            sendAjaxRequest(tr.urls.printReport, jsonData, onSuccess, onFailure, onComplete);
        },
    };
}();
