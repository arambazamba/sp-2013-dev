(function () {
    try {
        var appweburl =
            decodeURIComponent(
                getQueryStringParameter("SPAppWebUrl")
        );

        var executor = new SP.RequestExecutor(appweburl);

        executor.executeAsync(
        {
            url: appweburl + "/_api/web/lists/getbytitle('Announcements')/items",
            method: "GET",
            headers: { "Accept": "application/json;odata=verbose" },
            success: successHandler,
            error: errorHandler
        });
    }
    catch (err) {
        alert(err.message);
    }
})();

function successHandler(data) {
    var jsonObject = JSON.parse(data.body);
    var announcementsHtml = "";

    var results = jsonObject.d.results;

    for (var i = 0; i < results.length; i++) {
        announcementsHtml = announcementsHtml +
            "<p><h1>" + results[i].Title +
            "</h1>" + results[i].Body +
            "</p><hr/>";
    }

    document.getElementById("renderAnnouncements").innerHTML = announcementsHtml;
}

function errorHandler(data, errorCode, errorMessage) {
    alert("failure");
    document.getElementById("renderAnnouncements").innerText =
        "Could not complete cross domain call: " + errorMessage;
}

// Function to retrieve a query string value.
function getQueryStringParameter(paramToRetrieve) {
    var params = document.URL.split("?")[1].split("&");
    var strParams = "";
    for (var i = 0; i < params.length; i = i + 1) {
        var singleParam = params[i].split("=");
        if (singleParam[0] == paramToRetrieve)
            return singleParam[1];
    }
}