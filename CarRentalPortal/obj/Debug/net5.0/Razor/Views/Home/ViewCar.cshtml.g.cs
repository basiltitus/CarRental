#pragma checksum "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b3b96e281e4fe0a1868bdcfa46de7b86d2b0f007"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_ViewCar), @"mvc.1.0.view", @"/Views/Home/ViewCar.cshtml")]
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
#line 1 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\_ViewImports.cshtml"
using CarRentalPortal;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\_ViewImports.cshtml"
using CarRentalPortal.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b3b96e281e4fe0a1868bdcfa46de7b86d2b0f007", @"/Views/Home/ViewCar.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"75032d6b42beb35b56e3f4b3d9f7cd6f887be7e1", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_ViewCar : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CarRentalPortal.Models.ViewModels.CarListVM>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_AdminNavBar", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "CarList", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "CarBookingHistory", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-secondary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "UserCarList", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "b3b96e281e4fe0a1868bdcfa46de7b86d2b0f0075714", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n<div class=\"row p-4\">\r\n    <div class=\"col-4 text-center offset-4\">\r\n        <h3>View Car</h3>\r\n    </div>\r\n    <div class=\"col-3 text-right\">\r\n");
#nullable restore
#line 11 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
         if (Context.Session.GetString("_userType") == "Admin" || Context.Session.GetString("_userType") == "Super")
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b3b96e281e4fe0a1868bdcfa46de7b86d2b0f0077339", async() => {
                WriteLiteral("<i class=\"fas fa-hand-point-left\"></i> Back to list");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b3b96e281e4fe0a1868bdcfa46de7b86d2b0f0078842", async() => {
                WriteLiteral("<i class=\"fas fa-calendar-times\"></i> See Bookings");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 14 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
                                                                          WriteLiteral(ViewBag.CarId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 15 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b3b96e281e4fe0a1868bdcfa46de7b86d2b0f00711413", async() => {
                WriteLiteral("<i class=\"fas fa-hand-point-left\"></i> Back to list");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 19 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n</div>\r\n<div class=\"row\">\r\n    <div class=\"col-4 offset-1\">\r\n        <div class=\"container p-4\">\r\n\r\n            <h2> ");
#nullable restore
#line 26 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
            Write(Html.DisplayFor(model => model.CarModelDetails.CarName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2> <br />\r\n");
#nullable restore
#line 27 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
             if (Context.Session.GetString("_userType") == "Admin" || Context.Session.GetString("_userType") == "Super")
            {



#line default
#line hidden
#nullable disable
            WriteLiteral("                <span class=\"carLabel\">Status : </span>\r\n");
#nullable restore
#line 32 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
                 if (ViewBag.Active == true)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <span>ACTIVE</span>\r\n");
#nullable restore
#line 35 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
                }

                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <span> INACTIVE </span >\r\n");
#nullable restore
#line 40 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n");
#nullable restore
#line 41 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
           Write(Html.LabelFor(model => model.RegNo, new { @class = "carLabel" }));

#line default
#line hidden
#nullable disable
#nullable restore
#line 42 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
           Write(Html.DisplayFor(model => model.RegNo));

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n");
#nullable restore
#line 43 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("            ");
#nullable restore
#line 44 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
       Write(Html.LabelFor(model => model.Colour, new { @class = "carLabel" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 45 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
       Write(Html.DisplayFor(model => model.Colour));

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n\r\n            ");
#nullable restore
#line 47 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
       Write(Html.LabelFor(model => model.CarModelDetails.CarTransmission, new { @class = "carLabel" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 48 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
       Write(Html.DisplayFor(model => model.CarModelDetails.CarTransmission));

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n\r\n            ");
#nullable restore
#line 50 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
       Write(Html.LabelFor(model => model.CarModelDetails.CarType, new { @class = "carLabel" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 51 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
       Write(Html.DisplayFor(model => model.CarModelDetails.CarType));

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n\r\n            ");
#nullable restore
#line 53 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
       Write(Html.LabelFor(model => model.CarModelDetails.SeatCount, new { @class = "carLabel" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 54 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
       Write(Html.DisplayFor(model => model.CarModelDetails.SeatCount));

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n\r\n            ");
#nullable restore
#line 56 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
       Write(Html.LabelFor(model => model.CarModelDetails.ChargePerDay, new { @class = "carLabel" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 57 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
       Write(Html.DisplayFor(model => model.CarModelDetails.ChargePerDay));

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n");
#nullable restore
#line 58 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
             if (Context.Session.GetString("_userType") == "Admin" || Context.Session.GetString("_userType") == "Super")
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 60 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
           Write(Html.LabelFor(model => model.CreatedOn, new { @class = "carLabel" }));

#line default
#line hidden
#nullable disable
#nullable restore
#line 61 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
           Write(Model.CreatedOn.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral("<br /><br />\r\n");
            WriteLiteral("                <span class=\"carLabel\">Created By</span>\r\n");
#nullable restore
#line 64 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
           Write(Html.DisplayFor(model => model.UserDetails.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n");
#nullable restore
#line 65 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n</div>\r\n<div class=\"col-5\">\r\n    <img");
            BeginWriteAttribute("src", " src=\'", 3051, "\'", 3072, 1);
#nullable restore
#line 69 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\ViewCar.cshtml"
WriteAttributeValue("", 3057, ViewBag.ImgUrl, 3057, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"maxWidth\" />\r\n</div>\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CarRentalPortal.Models.ViewModels.CarListVM> Html { get; private set; }
    }
}
#pragma warning restore 1591
