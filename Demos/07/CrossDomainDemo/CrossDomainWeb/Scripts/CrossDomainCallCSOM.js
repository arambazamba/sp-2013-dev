var allAnnouncements;

$(document).ready(function () {
    var appweburl =
            decodeURIComponent(
                getQueryStringParameter("SPAppWebUrl")
        );

    var context = new SP.ClientContext(appweburl);
    var factory = new SP.ProxyWebRequestExecutorFactory(appweburl);
    context.set_webRequestExecutorFactory(factory);
    var web = context.get_web();
    var list = web.get_lists().getByTitle("Announcements");
    var camlString =
        "<View><ViewFields>" +
            "<FieldRef Name='Title' />" +
            "<FieldRef Name='Body' />" +
        "</ViewFields></View>";
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml(camlString);

    allAnnouncements = list.getItems(camlQuery);

    context.load(allAnnouncements, "Include(Title,Body)");

    context.executeQueryAsync(
        Function.createDelegate(this, successHandler),
        Function.createDelegate(this, errorHandler)
    );

    var executor = new SP.RequestExecutor(appweburl);
});


function successHandler(data, req) {
    var announcementsHtml = "";
    var enumerator = allAnnouncements.getEnumerator();

    while (enumerator.moveNext()) {
        var announcement = enumerator.get_current();
        announcementsHtml = announcementsHtml +
            "<p><h1>" + announcement.get_item("Title") +
            "</h1>" + announcement.get_item("Body") +
            "</p><hr/>";
    }

    document.getElementById("renderAnnouncements").innerHTML = announcementsHtml;
}

function errorHandler(data, error) {
    alert("failure");
    document.getElementById("renderAnnouncements").innerText =
        "Could not complete cross domain call: " + args.get_message();
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