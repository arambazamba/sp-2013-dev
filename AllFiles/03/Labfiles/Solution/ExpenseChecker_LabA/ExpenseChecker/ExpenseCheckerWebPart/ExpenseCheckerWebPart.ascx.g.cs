﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExpenseChecker.ExpenseCheckerWebPart {
    using System.Web.UI.WebControls.Expressions;
    using System.Web.UI.HtmlControls;
    using System.Collections;
    using System.Text;
    using System.Web.UI;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Microsoft.SharePoint.WebPartPages;
    using System.Web.SessionState;
    using System.Configuration;
    using Microsoft.SharePoint;
    using System.Web;
    using System.Web.DynamicData;
    using System.Web.Caching;
    using System.Web.Profile;
    using System.ComponentModel.DataAnnotations;
    using System.Web.UI.WebControls;
    using System.Web.Security;
    using System;
    using Microsoft.SharePoint.Utilities;
    using System.Text.RegularExpressions;
    using System.Collections.Specialized;
    using System.Web.UI.WebControls.WebParts;
    using Microsoft.SharePoint.WebControls;
    
    
    public partial class ExpenseCheckerWebPart {
        
        protected global::System.Web.UI.WebControls.ListView lstExpenses;
        
        protected global::System.Web.UI.WebControls.Button btnApprove;
        
        protected global::System.Web.UI.WebControls.Button btnReject;
        
        public static implicit operator global::System.Web.UI.TemplateControl(ExpenseCheckerWebPart target) 
        {
            return target == null ? null : target.TemplateControl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.HtmlControls.HtmlTableCell @__BuildControl__control9() {
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl;
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlTableCell("th");
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.HtmlControls.HtmlTableCell @__BuildControl__control10() {
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl;
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlTableCell("th");
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("Requestor"));
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.HtmlControls.HtmlTableCell @__BuildControl__control11() {
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl;
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlTableCell("th");
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("Category"));
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.HtmlControls.HtmlTableCell @__BuildControl__control12() {
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl;
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlTableCell("th");
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("Description"));
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.HtmlControls.HtmlTableCell @__BuildControl__control13() {
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl;
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlTableCell("th");
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("Amount"));
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControl__control8(System.Web.UI.HtmlControls.HtmlTableCellCollection @__ctrl) {
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl1;
            @__ctrl1 = this.@__BuildControl__control9();
            @__ctrl.Add(@__ctrl1);
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl2;
            @__ctrl2 = this.@__BuildControl__control10();
            @__ctrl.Add(@__ctrl2);
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl3;
            @__ctrl3 = this.@__BuildControl__control11();
            @__ctrl.Add(@__ctrl3);
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl4;
            @__ctrl4 = this.@__BuildControl__control12();
            @__ctrl.Add(@__ctrl4);
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl5;
            @__ctrl5 = this.@__BuildControl__control13();
            @__ctrl.Add(@__ctrl5);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.HtmlControls.HtmlTableRow @__BuildControl__control7() {
            global::System.Web.UI.HtmlControls.HtmlTableRow @__ctrl;
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlTableRow();
            this.@__BuildControl__control8(@__ctrl.Cells);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.HtmlControls.HtmlTableRow @__BuildControl__control14() {
            global::System.Web.UI.HtmlControls.HtmlTableRow @__ctrl;
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlTableRow();
            @__ctrl.ID = "itemPlaceholder";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControl__control6(System.Web.UI.HtmlControls.HtmlTableRowCollection @__ctrl) {
            global::System.Web.UI.HtmlControls.HtmlTableRow @__ctrl1;
            @__ctrl1 = this.@__BuildControl__control7();
            @__ctrl.Add(@__ctrl1);
            global::System.Web.UI.HtmlControls.HtmlTableRow @__ctrl2;
            @__ctrl2 = this.@__BuildControl__control14();
            @__ctrl.Add(@__ctrl2);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.HtmlControls.HtmlTable @__BuildControl__control5() {
            global::System.Web.UI.HtmlControls.HtmlTable @__ctrl;
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlTable();
            @__ctrl.TemplateControl = this;
            @__ctrl.ID = "tblExpenses";
            @__ctrl.CellPadding = 2;
            this.@__BuildControl__control6(@__ctrl.Rows);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.NumericPagerField @__BuildControl__control17() {
            global::System.Web.UI.WebControls.NumericPagerField @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.NumericPagerField();
            @__ctrl.ButtonCount = 10;
            @__ctrl.PreviousPageText = "<--";
            @__ctrl.NextPageText = "-->";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControl__control16(System.Web.UI.WebControls.DataPagerFieldCollection @__ctrl) {
            global::System.Web.UI.WebControls.NumericPagerField @__ctrl1;
            @__ctrl1 = this.@__BuildControl__control17();
            @__ctrl.Add(@__ctrl1);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.DataPager @__BuildControl__control15() {
            global::System.Web.UI.WebControls.DataPager @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.DataPager();
            @__ctrl.TemplateControl = this;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "dataPager";
            @__ctrl.PageSize = 10;
            this.@__BuildControl__control16(@__ctrl.Fields);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControl__control4(System.Web.UI.Control @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                    "));
            global::System.Web.UI.HtmlControls.HtmlTable @__ctrl1;
            @__ctrl1 = this.@__BuildControl__control5();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                    "));
            global::System.Web.UI.WebControls.DataPager @__ctrl2;
            @__ctrl2 = this.@__BuildControl__control15();
            @__parser.AddParsedSubObject(@__ctrl2);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                "));
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.CheckBox @__BuildControl__control22() {
            global::System.Web.UI.WebControls.CheckBox @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.CheckBox();
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "chkUpdate";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.HiddenField @__BuildControl__control23() {
            global::System.Web.UI.WebControls.HiddenField @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.HiddenField();
            @__ctrl.ID = "hiddenID";
            @__ctrl.DataBinding += new System.EventHandler(this.@__DataBinding__control23);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public void @__DataBinding__control23(object sender, System.EventArgs e) {
            System.Web.UI.WebControls.HiddenField dataBindingExpressionBuilderTarget;
            System.Web.UI.WebControls.ListViewDataItem Container;
            dataBindingExpressionBuilderTarget = ((System.Web.UI.WebControls.HiddenField)(sender));
            Container = ((System.Web.UI.WebControls.ListViewDataItem)(dataBindingExpressionBuilderTarget.BindingContainer));
            dataBindingExpressionBuilderTarget.Value = global::System.Convert.ToString( Eval("UniqueId") , global::System.Globalization.CultureInfo.CurrentCulture);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.HtmlControls.HtmlTableCell @__BuildControl__control21() {
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl;
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlTableCell("td");
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                            "));
            global::System.Web.UI.WebControls.CheckBox @__ctrl1;
            @__ctrl1 = this.@__BuildControl__control22();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                            "));
            global::System.Web.UI.WebControls.HiddenField @__ctrl2;
            @__ctrl2 = this.@__BuildControl__control23();
            @__parser.AddParsedSubObject(@__ctrl2);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                        "));
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Label @__BuildControl__control25() {
            global::System.Web.UI.WebControls.Label @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Label();
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lblRequestor";
            @__ctrl.DataBinding += new System.EventHandler(this.@__DataBinding__control25);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public void @__DataBinding__control25(object sender, System.EventArgs e) {
            System.Web.UI.WebControls.Label dataBindingExpressionBuilderTarget;
            System.Web.UI.WebControls.ListViewDataItem Container;
            dataBindingExpressionBuilderTarget = ((System.Web.UI.WebControls.Label)(sender));
            Container = ((System.Web.UI.WebControls.ListViewDataItem)(dataBindingExpressionBuilderTarget.BindingContainer));
            dataBindingExpressionBuilderTarget.Text = global::System.Convert.ToString( Eval("CapExRequestor") , global::System.Globalization.CultureInfo.CurrentCulture);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.HtmlControls.HtmlTableCell @__BuildControl__control24() {
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl;
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlTableCell("td");
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                            "));
            global::System.Web.UI.WebControls.Label @__ctrl1;
            @__ctrl1 = this.@__BuildControl__control25();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                        "));
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Label @__BuildControl__control27() {
            global::System.Web.UI.WebControls.Label @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Label();
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lblCategory";
            @__ctrl.DataBinding += new System.EventHandler(this.@__DataBinding__control27);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public void @__DataBinding__control27(object sender, System.EventArgs e) {
            System.Web.UI.WebControls.Label dataBindingExpressionBuilderTarget;
            System.Web.UI.WebControls.ListViewDataItem Container;
            dataBindingExpressionBuilderTarget = ((System.Web.UI.WebControls.Label)(sender));
            Container = ((System.Web.UI.WebControls.ListViewDataItem)(dataBindingExpressionBuilderTarget.BindingContainer));
            dataBindingExpressionBuilderTarget.Text = global::System.Convert.ToString( Eval("CapExCategory") , global::System.Globalization.CultureInfo.CurrentCulture);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.HtmlControls.HtmlTableCell @__BuildControl__control26() {
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl;
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlTableCell("td");
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                            "));
            global::System.Web.UI.WebControls.Label @__ctrl1;
            @__ctrl1 = this.@__BuildControl__control27();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                        "));
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Label @__BuildControl__control29() {
            global::System.Web.UI.WebControls.Label @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Label();
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lblDescription";
            @__ctrl.DataBinding += new System.EventHandler(this.@__DataBinding__control29);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public void @__DataBinding__control29(object sender, System.EventArgs e) {
            System.Web.UI.WebControls.Label dataBindingExpressionBuilderTarget;
            System.Web.UI.WebControls.ListViewDataItem Container;
            dataBindingExpressionBuilderTarget = ((System.Web.UI.WebControls.Label)(sender));
            Container = ((System.Web.UI.WebControls.ListViewDataItem)(dataBindingExpressionBuilderTarget.BindingContainer));
            dataBindingExpressionBuilderTarget.Text = global::System.Convert.ToString( Eval("CapExDescription") , global::System.Globalization.CultureInfo.CurrentCulture);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.HtmlControls.HtmlTableCell @__BuildControl__control28() {
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl;
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlTableCell("td");
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                            "));
            global::System.Web.UI.WebControls.Label @__ctrl1;
            @__ctrl1 = this.@__BuildControl__control29();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                        "));
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Label @__BuildControl__control31() {
            global::System.Web.UI.WebControls.Label @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Label();
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lblAmount";
            @__ctrl.DataBinding += new System.EventHandler(this.@__DataBinding__control31);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public void @__DataBinding__control31(object sender, System.EventArgs e) {
            System.Web.UI.WebControls.Label dataBindingExpressionBuilderTarget;
            System.Web.UI.WebControls.ListViewDataItem Container;
            dataBindingExpressionBuilderTarget = ((System.Web.UI.WebControls.Label)(sender));
            Container = ((System.Web.UI.WebControls.ListViewDataItem)(dataBindingExpressionBuilderTarget.BindingContainer));
            dataBindingExpressionBuilderTarget.Text = global::System.Convert.ToString( Eval("CapExAmount", "{0:C2}") , global::System.Globalization.CultureInfo.CurrentCulture);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.HtmlControls.HtmlTableCell @__BuildControl__control30() {
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl;
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlTableCell("td");
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                            "));
            global::System.Web.UI.WebControls.Label @__ctrl1;
            @__ctrl1 = this.@__BuildControl__control31();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                        "));
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControl__control20(System.Web.UI.HtmlControls.HtmlTableCellCollection @__ctrl) {
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl1;
            @__ctrl1 = this.@__BuildControl__control21();
            @__ctrl.Add(@__ctrl1);
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl2;
            @__ctrl2 = this.@__BuildControl__control24();
            @__ctrl.Add(@__ctrl2);
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl3;
            @__ctrl3 = this.@__BuildControl__control26();
            @__ctrl.Add(@__ctrl3);
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl4;
            @__ctrl4 = this.@__BuildControl__control28();
            @__ctrl.Add(@__ctrl4);
            global::System.Web.UI.HtmlControls.HtmlTableCell @__ctrl5;
            @__ctrl5 = this.@__BuildControl__control30();
            @__ctrl.Add(@__ctrl5);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.HtmlControls.HtmlTableRow @__BuildControl__control19() {
            global::System.Web.UI.HtmlControls.HtmlTableRow @__ctrl;
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlTableRow();
            @__ctrl.TemplateControl = this;
            this.@__BuildControl__control20(@__ctrl.Cells);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControl__control18(System.Web.UI.Control @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                    "));
            global::System.Web.UI.HtmlControls.HtmlTableRow @__ctrl1;
            @__ctrl1 = this.@__BuildControl__control19();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                "));
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.ListView @__BuildControllstExpenses() {
            global::System.Web.UI.WebControls.ListView @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.ListView();
            this.lstExpenses = @__ctrl;
            @__ctrl.TemplateControl = this;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.LayoutTemplate = new System.Web.UI.CompiledTemplateBuilder(new System.Web.UI.BuildTemplateMethod(this.@__BuildControl__control4));
            @__ctrl.ItemTemplate = new System.Web.UI.CompiledBindableTemplateBuilder(new System.Web.UI.BuildTemplateMethod(this.@__BuildControl__control18), null);
            @__ctrl.ID = "lstExpenses";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Button @__BuildControlbtnApprove() {
            global::System.Web.UI.WebControls.Button @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Button();
            this.btnApprove = @__ctrl;
            @__ctrl.TemplateControl = this;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "btnApprove";
            @__ctrl.Text = "Approve";
            @__ctrl.Width = new System.Web.UI.WebControls.Unit(100D, global::System.Web.UI.WebControls.UnitType.Pixel);
            @__ctrl.Click -= new System.EventHandler(this.btnApprove_Click);
            @__ctrl.Click += new System.EventHandler(this.btnApprove_Click);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Button @__BuildControlbtnReject() {
            global::System.Web.UI.WebControls.Button @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Button();
            this.btnReject = @__ctrl;
            @__ctrl.TemplateControl = this;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "btnReject";
            @__ctrl.Text = "Reject";
            @__ctrl.Width = new System.Web.UI.WebControls.Unit(100D, global::System.Web.UI.WebControls.UnitType.Pixel);
            @__ctrl.Click -= new System.EventHandler(this.btnReject_Click);
            @__ctrl.Click += new System.EventHandler(this.btnReject_Click);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControl__control3(System.Web.UI.Control @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n            <p>Select one or more items, and then click <b>Approve</b> or <b>Re" +
                        "ject</b>.</p>\r\n            "));
            global::System.Web.UI.WebControls.ListView @__ctrl1;
            @__ctrl1 = this.@__BuildControllstExpenses();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n            <br />\r\n            "));
            global::System.Web.UI.WebControls.Button @__ctrl2;
            @__ctrl2 = this.@__BuildControlbtnApprove();
            @__parser.AddParsedSubObject(@__ctrl2);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n            &nbsp;\r\n            "));
            global::System.Web.UI.WebControls.Button @__ctrl3;
            @__ctrl3 = this.@__BuildControlbtnReject();
            @__parser.AddParsedSubObject(@__ctrl3);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("       \r\n        "));
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.UpdatePanel @__BuildControl__control2() {
            global::System.Web.UI.UpdatePanel @__ctrl;
            @__ctrl = new global::System.Web.UI.UpdatePanel();
            @__ctrl.ContentTemplate = new System.Web.UI.CompiledTemplateBuilder(new System.Web.UI.BuildTemplateMethod(this.@__BuildControl__control3));
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControlTree(global::ExpenseChecker.ExpenseCheckerWebPart.ExpenseCheckerWebPart @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n<div id=\"expenseChecker\">\r\n    "));
            global::System.Web.UI.UpdatePanel @__ctrl1;
            @__ctrl1 = this.@__BuildControl__control2();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n</div>"));
        }
        
        private void InitializeControl() {
            this.@__BuildControlTree(this);
            this.Load += new global::System.EventHandler(this.Page_Load);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        protected virtual object Eval(string expression) {
            return global::System.Web.UI.DataBinder.Eval(this.Page.GetDataItem(), expression);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        protected virtual string Eval(string expression, string format) {
            return global::System.Web.UI.DataBinder.Eval(this.Page.GetDataItem(), expression, format);
        }
    }
}
