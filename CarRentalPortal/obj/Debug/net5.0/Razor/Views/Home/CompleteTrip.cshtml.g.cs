#pragma checksum "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CompleteTrip.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "85faf0abdd016f2e43b9e197b385e90b829d3d67"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_CompleteTrip), @"mvc.1.0.view", @"/Views/Home/CompleteTrip.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"85faf0abdd016f2e43b9e197b385e90b829d3d67", @"/Views/Home/CompleteTrip.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"75032d6b42beb35b56e3f4b3d9f7cd6f887be7e1", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_CompleteTrip : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_AdminNavBar", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "85faf0abdd016f2e43b9e197b385e90b829d3d673436", async() => {
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
            WriteLiteral("\r\n</div>\r\n<h1 class=\"page-heading\">Penalty Page</h1>\r\n<div class=\"row\">\r\n    <div class=\"col-1\"></div>\r\n    <div class=\"col-7\">\r\n        <div class=\"rentDiv wideBox\">\r\n            Customer Have exceed the rent duration by <span class=\"text-danger\">");
#nullable restore
#line 14 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CompleteTrip.cshtml"
                                                                           Write(ViewBag.ExtraDays);

#line default
#line hidden
#nullable disable
            WriteLiteral(" Days</span>.<br />So they have been charged with a fine of <span class=\"text-danger\">\r\n                Rs.");
#nullable restore
#line 15 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CompleteTrip.cshtml"
              Write(ViewBag.FineAmount);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </span>.<br />Please accept the payment..<br />\r\n            <button class=\"btn btn-primary btn-lg\"");
            BeginWriteAttribute("onclick", " onclick=\"", 677, "\"", 751, 4);
            WriteAttributeValue("", 687, "location.href=\'", 687, 15, true);
#nullable restore
#line 17 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CompleteTrip.cshtml"
WriteAttributeValue("", 702, Url.Action("AdminPortal", "home"), 702, 34, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 736, "\';return", 736, 8, true);
            WriteAttributeValue(" ", 744, "false;", 745, 7, true);
            EndWriteAttribute();
            WriteLiteral(@"><i class=""fas fa-home""></i> Back to home</button>
        </div>
    </div>
    <div class=""col-4""></div>
</div>
<div class=""row"">
    <div class=""col-8""></div>
    <div class=""col-4"">
        <img src=""https://image.freepik.com/free-vector/tiny-people-getting-paper-sheet-with-fine-flat-illustration_74855-11107.jpg"" class=""bottom-right-image height70"" />
    </div>
</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
