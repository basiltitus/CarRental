#pragma checksum "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4ebfc1b7783f03f240841166481651190502a81f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_CarModelList), @"mvc.1.0.view", @"/Views/Home/CarModelList.cshtml")]
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
#line 1 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4ebfc1b7783f03f240841166481651190502a81f", @"/Views/Home/CarModelList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"75032d6b42beb35b56e3f4b3d9f7cd6f887be7e1", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_CarModelList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CarRentalPortal.Models.CarModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_AdminNavBar", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "CreateCarModel", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\r\n<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "4ebfc1b7783f03f240841166481651190502a81f4781", async() => {
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
            WriteLiteral("\r\n</div>\r\n<div class=\"row p-4\">\r\n    <div class=\"col-4 text-center offset-4\">\r\n        <h3>Car Model List</h3>\r\n    </div>\r\n    <div class=\"col-4 text-right\">\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4ebfc1b7783f03f240841166481651190502a81f6081", async() => {
                WriteLiteral("<i class=\"fas fa-plus-square\"></i> Add Model");
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
            WriteLiteral("\r\n");
            WriteLiteral("<!--on>-->\r\n    </div>\r\n</div>\r\n<div class=\"sidePanel row p-3 border\">\r\n    <div class=\"col-4 offset-1\">\r\n        <b> Sort Order : </b><div class=\"switch-field\">\r\n");
#nullable restore
#line 20 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
             if (ViewBag.SortOrder == "ascending")
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <input type=\"radio\" name=\"orderRadio\" class=\"orderRadio btn-check\" id=\"ascendingRadio\" value=\"ascending\" autocomplete=\"off\" checked>\r\n");
#nullable restore
#line 23 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <input type=\"radio\" name=\"orderRadio\" class=\"orderRadio btn-check\" id=\"ascendingRadio\" value=\"ascending\" autocomplete=\"off\">\r\n");
#nullable restore
#line 27 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("            <label class=\"btn btn-outline-success\" for=\"ascendingRadio\"><i class=\"fas fa-sort-amount-down-alt\"></i> Low->High</label>\r\n\r\n\r\n");
#nullable restore
#line 31 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
             if (ViewBag.SortOrder == "descending")
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <input type=\"radio\" name=\"orderRadio\" class=\"orderRadio btn-check\" id=\"descendingRadio\" value=\"descending\" autocomplete=\"off\" checked>\r\n");
#nullable restore
#line 34 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <input type=\"radio\" name=\"orderRadio\" class=\"orderRadio btn-check\" id=\"descendingRadio\" value=\"descending\" autocomplete=\"off\">\r\n");
#nullable restore
#line 38 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral(" <label class=\"btn btn-outline-success\" for=\"descendingRadio\"><i class=\"fas fa-sort-amount-down\"></i> High->Low</label>\r\n        </div>\r\n    </div>\r\n    <div class=\"col-5 offset-1\">\r\n        <b> Sort By : </b><div class=\"switch-field\">\r\n");
#nullable restore
#line 43 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
             if (ViewBag.SortBy == "created")
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <input type=\"radio\" name=\"sortRadio\" id=\"createdRadio\" value=\"created\" class=\"sortbyradio\" checked>\r\n");
#nullable restore
#line 46 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <input type=\"radio\" name=\"sortRadio\" id=\"createdRadio\" class=\"sortbyradio\" value=\"created\">\r\n");
#nullable restore
#line 50 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("            <label class=\"form-check-label\" for=\"createdRadio\">\r\n                <i class=\"fas fa-calendar-day\"></i> Created On\r\n            </label>\r\n\r\n");
#nullable restore
#line 55 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
             if (ViewBag.SortBy == "charge")
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <input type=\"radio\" name=\"sortRadio\" value=\"charge\" class=\"sortbyradio\" id=\"chargeRadio\" checked>\r\n");
#nullable restore
#line 58 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <input type=\"radio\" name=\"sortRadio\" value=\"charge\" class=\"sortbyradio\" id=\"chargeRadio\">\r\n");
#nullable restore
#line 62 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("            <label class=\"form-check-label\" for=\"chargeRadio\">\r\n                <i class=\"fas fa-rupee-sign\"></i> Charge Per Day\r\n            </label>\r\n");
#nullable restore
#line 66 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
             if (ViewBag.SortBy == "seat")
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <input type=\"radio\" name=\"sortRadio\" value=\"seat\" class=\"sortbyradio\" id=\"seatRadio\" checked>\r\n");
#nullable restore
#line 69 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <input type=\"radio\" name=\"sortRadio\" value=\"seat\" class=\"sortbyradio\" id=\"seatRadio\">\r\n");
#nullable restore
#line 73 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <label class=""form-check-label"" for=""seatRadio"">
                <i class=""fas fa-chair""></i> Seat Count
            </label>
        </div>
    </div>
        <table class=""table table-bordered table-striped"">
            <thead>
                <tr>
");
            WriteLiteral("                <th>\r\n                    ");
#nullable restore
#line 87 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
               Write(Html.DisplayNameFor(model => model.CarName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
#nullable restore
#line 90 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
               Write(Html.DisplayNameFor(model => model.CarTransmission));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
#nullable restore
#line 93 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
               Write(Html.DisplayNameFor(model => model.CarType));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
#nullable restore
#line 96 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
               Write(Html.DisplayNameFor(model => model.SeatCount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
#nullable restore
#line 99 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
               Write(Html.DisplayNameFor(model => model.ChargePerDay));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th></th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n\r\n");
#nullable restore
#line 106 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
               foreach (var item in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n");
            WriteLiteral("                    <td>\r\n                        ");
#nullable restore
#line 115 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
                   Write(Html.DisplayFor(modelItem => item.CarName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 118 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
                   Write(Html.DisplayFor(modelItem => item.CarTransmission));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 121 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
                   Write(Html.DisplayFor(modelItem => item.CarType));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 124 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
                   Write(Html.DisplayFor(modelItem => item.SeatCount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 127 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
                   Write(Html.DisplayFor(modelItem => item.ChargePerDay));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        <button type=\"button\" class=\"btn btn-secondary\"");
            BeginWriteAttribute("onclick", " onclick=\"", 5491, "\"", 5596, 4);
            WriteAttributeValue("", 5501, "location.href=\'", 5501, 15, true);
#nullable restore
#line 130 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
WriteAttributeValue("", 5516, Url.Action("ViewCarModel", "home", new { id = item.CarModelId }), 5516, 65, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 5581, "\';return", 5581, 8, true);
            WriteAttributeValue(" ", 5589, "false;", 5590, 7, true);
            EndWriteAttribute();
            WriteLiteral("><i class=\"fas fa-eye\"></i></button>|\r\n");
#nullable restore
#line 131 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
                         if (Context.Session.GetInt32("_userId") == item.UserId || Context.Session.GetString("_userType") == "Super")
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <button type=\"button\" class=\"btn btn-primary edit-btn \"");
            BeginWriteAttribute("onclick", " onclick=\"", 5881, "\"", 5986, 4);
            WriteAttributeValue("", 5891, "location.href=\'", 5891, 15, true);
#nullable restore
#line 133 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
WriteAttributeValue("", 5906, Url.Action("EditCarModel", "home", new { id = item.CarModelId }), 5906, 65, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 5971, "\';return", 5971, 8, true);
            WriteAttributeValue(" ", 5979, "false;", 5980, 7, true);
            EndWriteAttribute();
            WriteLiteral("><i class=\"fas fa-pen-square\"></i></button>\r\n");
#nullable restore
#line 134 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
                                                                                                                                                      
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </td>\r\n                </tr>\r\n");
#nullable restore
#line 138 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
            }
            

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </tbody>\r\n    </table>\r\n    <script type=\"text/javascript\">\r\n\r\n    $(document).ready(function () {\r\n        $(function () {\r\n            $(\'input[name=\"orderRadio\"]\').click(function () {\r\n                var sortby = \"");
#nullable restore
#line 148 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
                         Write(ViewBag.SortBy.ToString());

#line default
#line hidden
#nullable disable
            WriteLiteral("\";\r\n                if ($(this).is(\':checked\')) {\r\n                    location.href = \"");
#nullable restore
#line 150 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
                                Write(Url.Action("CarModelList", "home"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\" + \"?sortby=\" + sortby + \"&sortOrder=\" + $(this).val();\r\n                }\r\n            });\r\n        });\r\n        $(function () {\r\n            $(\'input[name=\"sortRadio\"]\').click(function () {\r\n                var sortOrder = \"");
#nullable restore
#line 156 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
                            Write(ViewBag.SortOrder.ToString());

#line default
#line hidden
#nullable disable
            WriteLiteral("\";\r\n                if ($(this).is(\':checked\')) {\r\n                    location.href = \"");
#nullable restore
#line 158 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\CarModelList.cshtml"
                                Write(Url.Action("CarModelList", "home"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\" + \"?sortby=\" + $(this).val() + \"&sortOrder=\" + sortOrder;\r\n                }\r\n            });\r\n        });\r\n");
            WriteLiteral("\r\n");
            WriteLiteral("    });\r\n    </script>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<CarRentalPortal.Models.CarModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
