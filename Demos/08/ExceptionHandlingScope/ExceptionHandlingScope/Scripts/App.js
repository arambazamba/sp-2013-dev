"use strict";

var Wingtip = window.Wingtip || {}

Wingtip.ErrorScope = function () {

    //private members
    var site,

    scope = function () {

        //Get Context
        var ctx = new SP.ClientContext.get_current();

        //Start Exception-Handling Scope
        var e = new SP.ExceptionHandlingScope(ctx);
        var s = e.startScope();

        //try
        var t = e.startTry();

        var list1 = ctx.get_web().get_lists().getByTitle("My List");
        ctx.load(list1);
        list1.set_description("A new description");
        list1.update();

        t.dispose();

        //catch
        var c = e.startCatch();

        var listCI = new SP.ListCreationInformation();

        listCI.set_title("My List");
        listCI.set_templateType(SP.ListTemplateType.announcements);
        listCI.set_quickLaunchOption(SP.QuickLaunchOptions.on);

        var list = ctx.get_web().get_lists().add(listCI);

        c.dispose();

        //finally
        var f = e.startFinally();

        var list2 = ctx.get_web().get_lists().getByTitle("My List");
        ctx.load(list2);
        list2.set_description("A new description");
        list2.update();

        f.dispose();

        //End Exception-Handling Scope
        s.dispose();

        //Execute
        ctx.executeQueryAsync(success, failure);

    },

    success = function () {
        alert("Success");
    },

    failure = function (sender, args) {
        alert(args.get_message());
    }

    //public interface
    return {
        execute: scope
    }
}();


$(document).ready(function () {
    Wingtip.ErrorScope.execute();
});
