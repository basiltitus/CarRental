#pragma checksum "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\AdminSignUp.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9947c36a1b59564a738ba66e5dd7c8bccf987113"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_AdminSignUp), @"mvc.1.0.view", @"/Views/Home/AdminSignUp.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9947c36a1b59564a738ba66e5dd7c8bccf987113", @"/Views/Home/AdminSignUp.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"75032d6b42beb35b56e3f4b3d9f7cd6f887be7e1", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_AdminSignUp : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CarRentalPortal.Models.User>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "SignUp", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\AdminSignUp.cshtml"
 using (Html.BeginForm())
{
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\AdminSignUp.cshtml"
Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""loginScreen"">
        <div class=""row row-size"">
            <div class=""col-md-6 d-none d-lg-block"">
                <img src='https://c.wallhere.com/photos/ab/d7/1600x900_px_New_York_City_street_taxi_Tilt_Shift_Traffic-545545.jpg!d' id=""imgLogin"" />
            </div>
            <div class=""col-md-6 col-sm-12"">
                <div id=""loginFormDiv"" class=""card"">
                    <div id=""loginForm"" class=""card-body"">
                        <h2 class=""brandName"">
                            DriveWay Admin Portal
                        </h2>
                        <p>
                            Welcome .. Please create your accont.
                        </p>
                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9947c36a1b59564a738ba66e5dd7c8bccf9871135078", async() => {
                WriteLiteral("\r\n                            ");
#nullable restore
#line 22 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\AdminSignUp.cshtml"
                       Write(Html.ValidationSummary(true, "", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n\r\n                            <input type=\"text\" name=\"Name\" class=\"form-control formInput\" placeholder=\"Name\" />\r\n                            ");
#nullable restore
#line 25 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\AdminSignUp.cshtml"
                       Write(Html.ValidationMessageFor(model => model.Name, "", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n\r\n                            <input type=\"email\" name=\"EmailId\" id=\"userNameTextBox\" onchange=\"validateEmail()\" class=\"form-control formInput\" placeholder=\"Email ID\" />\r\n");
                WriteLiteral("                            ");
#nullable restore
#line 29 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\AdminSignUp.cshtml"
                       Write(Html.ValidationMessageFor(model => model.EmailId, "", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n\r\n                            ");
#nullable restore
#line 31 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\AdminSignUp.cshtml"
                       Write(Html.EditorFor(model => model.Password, new { htmlAttributes = new { placeholder = "Passsword", @class = "form-control formInput" } }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                            ");
#nullable restore
#line 32 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\AdminSignUp.cshtml"
                       Write(Html.ValidationMessageFor(model => model.Password, "", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n\r\n                            <input type=\"Date\" id=\"DOBPicker\" name=\"DOB\" class=\"form-control formInput\" placeholder=\"Date Of Birth\" />\r\n                            ");
#nullable restore
#line 35 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\AdminSignUp.cshtml"
                       Write(Html.ValidationMessageFor(model => model.DOB, "", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n\r\n                            <input type=\"text\" name=\"PhoneNumber\" class=\"form-control formInput\" placeholder=\"Phone Number\" />\r\n                            ");
#nullable restore
#line 38 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\AdminSignUp.cshtml"
                       Write(Html.ValidationMessageFor(model => model.PhoneNumber, "", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n\r\n                            <input type=\"hidden\" name=\"LicenseNumber\" value=\"0\" />\r\n                            <span id=\"errorMessageSpan\" class=\"text-danger\"> ");
#nullable restore
#line 41 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\AdminSignUp.cshtml"
                                                                        Write(ViewBag.ErrorMessage);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</span>
                            <div class=""d-grid gap-2 d-md-block"">
                                <button class=""btn btn-primary formSubmit"" type=""submit"" id=""btn-one"">Create</button>
                                Already an admin? <button type=""button"" class=""btn btn-link """);
                BeginWriteAttribute("onclick", " onclick=\"", 2908, "\"", 2982, 4);
                WriteAttributeValue("", 2918, "location.href=\'", 2918, 15, true);
#nullable restore
#line 44 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\AdminSignUp.cshtml"
WriteAttributeValue("", 2933, Url.Action("AdminSignIn", "home"), 2933, 34, false);

#line default
#line hidden
#nullable disable
                WriteAttributeValue("", 2967, "\';return", 2967, 8, true);
                WriteAttributeValue(" ", 2975, "false;", 2976, 7, true);
                EndWriteAttribute();
                WriteLiteral(">\r\n                                    Click Here\r\n                                </button>\r\n                            </div>\r\n                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n");
#nullable restore
#line 54 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\AdminSignUp.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<script>
    function validateEmail() {
        var email = document.getElementById(""userNameTextBox"").value;
        fetch(""https://localhost:44305/api/Auth/"" + email).then(response => response.json())
            .then((item) => {
                if (item) {
                    document.getElementById(""errorMessageSpan"").innerHTML = ""User Already Exists!"";
                    document.getElementById(""btn-one"").disabled = true;
                }
                else {
                    document.getElementById(""errorMessageSpan"").innerHTML = """"
                    document.getElementById(""btn-one"").disabled = false;
                }

            });
    }
</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CarRentalPortal.Models.User> Html { get; private set; }
    }
}
#pragma warning restore 1591
