tr = {
    messages: {},
    urls: {
        mainPage: "/Designer/Index/",
        initReportControls: "/Designer/InitReportControls/",
        getDataSource: "/Designer/GetDataSource/",
        loadTemplate: "/Designer/LoadTemplate/",
        loadDemoTemplate: "/Designer/LoadDemoTemplate/",
        saveTemplate: "/Designer/SaveTemplate/",
        printReport: "/Designer/PrintReport/",
        openReport: "/Designer/OpenReport/",
    },


    alert: function (message) {
        swal(message);
    },

    confirm: function (title, callback) {
        var titleText = title != null ? title : tr.messages.confirmQuestion;
        swal({
            title: titleText,
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: tr.messages.confirmButtonText,
        }, function (isConfirm) {
            if (isConfirm) {
                callback();
            }
        });

    },

    init: function () {

    }
};

$(tr.init);