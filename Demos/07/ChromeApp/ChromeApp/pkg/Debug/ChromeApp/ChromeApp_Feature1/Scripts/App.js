var hostweburl;

// Load the SharePoint resources.
$(document).ready(function () {
    // Load the js file and continue to the 
    //   success handler.
    $.getScript("../scripts/sp.ui.controls.js", renderChrome);
});

function renderChrome() {

    // The Help, Account, and Contact pages receive the 
    // same query string parameters as the main page.
var options = {
    "appIconUrl": "../Images/AppIcon.png",
    "appTitle": "Chrome control app",
    "appHelpPageUrl": "Help.html?"
        + document.URL.split("?")[1],
    "settingsLinks": [
        {
            "linkUrl": "Account.html?" + document.URL.split("?")[1], 
            "displayName": "Account settings"
        },
        {
            "linkUrl": "Contact.html?"+ document.URL.split("?")[1],
            "displayName": "Contact us"
        }
    ]
};
var nav = new SP.UI.Controls.Navigation("chrome_ctrl_container", options);
nav.setVisible(true);
}

