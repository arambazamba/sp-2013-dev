
function Create(subject, feedback) {
    var listUrl = _spPageContextInfo.webServerRelativeUrl + "/_api/web/lists/getByTitle('Suggestions')/items";
    // Store the form digest.
    var formDigest = $('#__REQUESTDIGEST').val();
    $.ajax({
        url: listUrl,
        type: "POST",
        data: JSON.stringify({
            '__metadata': { 'type': 'SP.Data.SuggestionsListItem' },
            'Subject': subject,
            'Feedback': feedback
        }),
        headers: {
            "accept": "application/json;odata=verbose",
            "X‐RequestDigest": formDigest
        },
        success: function () {
            alert("Successfully created the suggestion");
        },
        error: function (err) {
            alert("Could not create the suggestion");
        }
    });
}

function update(id, subject, feedback) {
    var listUrl = _spPageContextInfo.webServerRelativeUrl + "/_api/web/lists/getByTitle('Suggestions')/getItemByStringId('" + id + "')";
    // Store the form digest.
    var formDigest = $('#__REQUESTDIGEST').val();
    $.ajax({
        url: listUrl,
        type: "POST",
        data: JSON.stringify({
            '__metadata': { 'type': 'SP.Data.SuggestionsListItem' },
            'Subject': subject,
            'Feedback': feedback
        }),
        headers: {
            "accept": "application/json;odata=verbose",
            "X‐RequestDigest": formDigest,
            "IF‐MATCH": "*",
            "X‐Http‐Method": "PATCH"
        },
        success: function() {
            alert("Successfully updated the suggestion");
        },
        error: function(err) {
            alert("Could not update the suggestion");
        }
    });
}

function deleteitem(id){
    // To delete an item, formulate a URL to the item itself.
    var listUrl = _spPageContextInfo.webServerRelativeUrl + "/_api/web/lists/getByTitle('Suggestions')/getItemByStringId('" + id + "')";
    // Store the form digest.
    var formDigest = $('#__REQUESTDIGEST').val();
    $.ajax({
        url: listUrl,
        type: "DELETE",
        headers: {
            "accept": "application/json;odata=verbose",
            "X‐RequestDigest": formDigest,
            "IF‐MATCH": "*"
        },
        success: function() {
            alert("Successfully updated the suggestion");
        },
        error: function(err) {
            alert("Could not update the suggestion");
        }
    });
}