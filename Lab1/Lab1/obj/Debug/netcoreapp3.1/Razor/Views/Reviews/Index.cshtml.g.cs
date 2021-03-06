#pragma checksum "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "943a6d87a7c3820a483bef29d41136f00cd08594"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Reviews_Index), @"mvc.1.0.view", @"/Views/Reviews/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\_ViewImports.cshtml"
using Lab1;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
using System.Security.Claims;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
using Lab1.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"943a6d87a7c3820a483bef29d41136f00cd08594", @"/Views/Reviews/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"59419ef1b1476a13ca8a84f44f57646c39f2efca", @"/Views/_ViewImports.cshtml")]
    public class Views_Reviews_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Lab1.Models.Review>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 6 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1> ");
#nullable restore
#line 10 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
Write(ViewBag.ProductName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" Reviews</h1>\r\n\r\n");
#nullable restore
#line 12 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
 if (User.Identity.IsAuthenticated && @ViewBag.Count == 0)
{
    var roles = ((ClaimsIdentity)User.Identity).Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

    if (roles.Contains("admin") || roles.Contains("creator") || roles.Contains("moderator") || roles.Contains("user"))
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <p>\r\n            ");
#nullable restore
#line 19 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
       Write(Html.ActionLink("Create new", "Create", new { productId = @ViewBag.ProductId, userId = @ViewBag.UserId, userName = @ViewBag.UserName, num = @ViewBag.Num, searchString = @ViewBag.SearchString }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </p>\r\n");
#nullable restore
#line 21 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
    }
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 24 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
 if (ViewBag.Num == 0)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <p>\r\n        ");
#nullable restore
#line 27 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
   Write(Html.ActionLink("Back to list", "Index", "Products", new { id = @ViewBag.SubcategoryId, name = @ViewBag.SubcategoryName, num = @ViewBag.Num, searchString = @ViewBag.SearchString }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </p>\r\n");
#nullable restore
#line 29 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <p>\r\n        ");
#nullable restore
#line 33 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
   Write(Html.ActionLink("Back to list", "Index1", "Products", new { num = @ViewBag.Num, searchString = @ViewBag.SearchString }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </p>\r\n");
#nullable restore
#line 35 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 41 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Info));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 44 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Date));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 47 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Rating));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>User Name</th>\r\n            <th>\r\n                ");
#nullable restore
#line 51 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Product));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 57 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 61 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Info));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 64 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Date));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 67 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Rating));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 70 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.UserName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 73 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Product.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n");
#nullable restore
#line 76 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                     if (User.Identity.IsAuthenticated)
                    {

                        var roles = ((ClaimsIdentity)User.Identity).Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                        if (!roles.Contains("creator") && !roles.Contains("moderator") && !roles.Contains("admin") && roles.Contains("user"))
                        {
                            if (item.UserId == @ViewBag.UserId)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <p>\r\n                                    ");
#nullable restore
#line 86 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                               Write(Html.ActionLink("Edit", "Edit", new { id = @item.Id, productId = @item.ProductId, userId = @item.UserId, userName = @item.UserName, num = @ViewBag.Num, searchString = @ViewBag.SearchString }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "943a6d87a7c3820a483bef29d41136f00cd0859410996", async() => {
                WriteLiteral("Delete");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 87 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                                                             WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 87 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                                                                                      WriteLiteral(ViewBag.Num);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["num"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-num", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["num"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 87 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                                                                                                                            WriteLiteral(ViewBag.SearchString);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["searchString"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-searchString", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["searchString"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                </p>\r\n");
#nullable restore
#line 89 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                            }
                        }
                        else
                        {
                            if (roles.Contains("creator"))
                            {
                                if (item.UserId == @ViewBag.UserId)
                                {
                                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 97 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                               Write(Html.ActionLink("Edit", "Edit", new { id = @item.Id, productId = @item.ProductId, userId = @item.UserId, userName = @item.UserName, num = @ViewBag.Num, searchString = @ViewBag.SearchString }));

#line default
#line hidden
#nullable disable
#nullable restore
#line 97 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                                                                                                                                                                                                                                    
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "943a6d87a7c3820a483bef29d41136f00cd0859416085", async() => {
                WriteLiteral("Delete");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 99 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                                                         WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 99 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                                                                                  WriteLiteral(ViewBag.Num);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["num"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-num", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["num"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 99 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                                                                                                                        WriteLiteral(ViewBag.SearchString);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["searchString"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-searchString", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["searchString"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 100 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                            }
                            if (!roles.Contains("creator") && roles.Contains("admin"))
                            {
                                var rolesArray = getRoles(item.UserId);
                                if (item.UserId != @ViewBag.UserId)
                                {
                                    if (!rolesArray.Result.Contains("creator") && !rolesArray.Result.Contains("admin"))
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "943a6d87a7c3820a483bef29d41136f00cd0859420481", async() => {
                WriteLiteral("Delete");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 108 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                                                                 WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 108 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                                                                                          WriteLiteral(ViewBag.Num);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["num"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-num", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["num"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 108 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                                                                                                                                WriteLiteral(ViewBag.SearchString);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["searchString"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-searchString", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["searchString"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 109 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                                    }
                                }
                                else
                                {
                                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 113 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                               Write(Html.ActionLink("Edit", "Edit", new { id = @item.Id, productId = @item.ProductId, userId = @item.UserId, userName = @item.UserName, num = @ViewBag.Num, searchString = @ViewBag.SearchString }));

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "943a6d87a7c3820a483bef29d41136f00cd0859424977", async() => {
                WriteLiteral("Delete");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 114 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                                                             WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 114 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                                                                                      WriteLiteral(ViewBag.Num);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["num"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-num", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["num"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 114 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                                                                                                                            WriteLiteral(ViewBag.SearchString);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["searchString"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-searchString", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["searchString"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 115 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                                }
                            }
                            if (!roles.Contains("creator") && !roles.Contains("admin") && roles.Contains("moderator"))
                            {
                                var rolesArray = getRoles(item.UserId);

                                if (item.UserId != @ViewBag.UserId)
                                {
                                    if (!rolesArray.Result.Contains("creator") && !rolesArray.Result.Contains("admin") && !rolesArray.Result.Contains("moderator"))
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "943a6d87a7c3820a483bef29d41136f00cd0859429501", async() => {
                WriteLiteral("Delete");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 125 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                                                                 WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 125 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                                                                                          WriteLiteral(ViewBag.Num);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["num"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-num", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["num"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 125 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                                                                                                                                WriteLiteral(ViewBag.SearchString);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["searchString"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-searchString", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["searchString"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 126 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                                    }
                                }
                                else
                                {
                                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 130 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                               Write(Html.ActionLink("Edit", "Edit", new { id = @item.Id, productId = @item.ProductId, userId = @item.UserId, userName = @item.UserName, num = @ViewBag.Num, searchString = @ViewBag.SearchString }));

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "943a6d87a7c3820a483bef29d41136f00cd0859433997", async() => {
                WriteLiteral("Delete");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 131 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                                                             WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 131 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                                                                                      WriteLiteral(ViewBag.Num);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["num"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-num", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["num"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 131 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                                                                                                                            WriteLiteral(ViewBag.SearchString);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["searchString"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-searchString", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["searchString"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 132 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
                                }
                            }

                        }
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 141 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n\r\n");
            WriteLiteral("\r\n");
        }
        #pragma warning restore 1998
#nullable restore
#line 145 "C:\Users\Admin\source\repos\Lab1\Lab1\Views\Reviews\Index.cshtml"
           
    async Task<List<string>> getRoles(string id)
    {
        UserManager<User> userManager = ViewBag.UserManager;
        var user = userManager.FindByIdAsync(id).Result;
        var roles = await userManager.GetRolesAsync(user);
        return roles.ToList();
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Lab1.Models.Review>> Html { get; private set; }
    }
}
#pragma warning restore 1591
