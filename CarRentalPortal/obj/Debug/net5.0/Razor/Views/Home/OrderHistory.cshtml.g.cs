#pragma checksum "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\OrderHistory.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0e43c44bbc784fc830ee0f46b3679926a3a0c743"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_OrderHistory), @"mvc.1.0.view", @"/Views/Home/OrderHistory.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0e43c44bbc784fc830ee0f46b3679926a3a0c743", @"/Views/Home/OrderHistory.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"75032d6b42beb35b56e3f4b3d9f7cd6f887be7e1", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_OrderHistory : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CarRentalPortal.Models.OrderTable>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\OrderHistory.cshtml"
  
    ViewData["Title"] = "OrderHistory";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div>\r\n    ");
#nullable restore
#line 7 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\OrderHistory.cshtml"
Write(Html.Partial("_NavBar"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>\r\n<h1 class=\"page-heading\">Your Order History</h1>\r\n\r\n<table class=\"table table-bordered table-striped\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 15 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\OrderHistory.cshtml"
           Write(Html.DisplayNameFor(model => model.OrderId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 18 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\OrderHistory.cshtml"
           Write(Html.DisplayNameFor(model => model.CarDetail.CarName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 21 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\OrderHistory.cshtml"
           Write(Html.DisplayNameFor(model => model.FromDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 24 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\OrderHistory.cshtml"
           Write(Html.DisplayNameFor(model => model.ToDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 27 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\OrderHistory.cshtml"
           Write(Html.DisplayNameFor(model => model.Total));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 30 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\OrderHistory.cshtml"
           Write(Html.DisplayNameFor(model => model.ExtraDays));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 33 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\OrderHistory.cshtml"
           Write(Html.DisplayNameFor(model => model.Completed));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>");
#nullable restore
#line 37 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\OrderHistory.cshtml"
              
        int index = 0;
        var list = ViewBag.OrderList;
        foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
#nullable restore
#line 44 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\OrderHistory.cshtml"
           Write(Html.DisplayFor(modelItem => item.OrderId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 47 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\OrderHistory.cshtml"
           Write(Html.DisplayFor(modelItem => item.CarDetail.CarName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 50 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\OrderHistory.cshtml"
           Write(Html.DisplayFor(modelItem => item.FromDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 53 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\OrderHistory.cshtml"
           Write(Html.DisplayFor(modelItem => item.ToDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 56 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\OrderHistory.cshtml"
           Write(Html.DisplayFor(modelItem => item.Total));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 59 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\OrderHistory.cshtml"
           Write(Html.DisplayFor(modelItem => item.ExtraDays));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n");
#nullable restore
#line 62 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\OrderHistory.cshtml"
                 if (list[index].Completed)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <button class=\"btn btn-primary wide-btn\" disabled>Completed</button>\r\n");
#nullable restore
#line 65 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\OrderHistory.cshtml"
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 66 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\OrderHistory.cshtml"
                 if (!list[index].Completed)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <button class=\"btn btn-danger wide-btn\"");
            BeginWriteAttribute("onclick", " onclick=\"", 2069, "\"", 2179, 4);
            WriteAttributeValue("", 2079, "location.href=\'", 2079, 15, true);
#nullable restore
#line 68 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\OrderHistory.cshtml"
WriteAttributeValue("", 2094, Url.Action("CompleteTrip", "home",new { orderId=list[index].OrderId}), 2094, 70, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2164, "\';return", 2164, 8, true);
            WriteAttributeValue(" ", 2172, "false;", 2173, 7, true);
            EndWriteAttribute();
            WriteLiteral(">Pending</button>\r\n");
#nullable restore
#line 69 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\OrderHistory.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </td>\r\n        </tr>\r\n");
#nullable restore
#line 72 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\OrderHistory.cshtml"
            ++index;
        }
        

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<CarRentalPortal.Models.OrderTable>> Html { get; private set; }
    }
}
#pragma warning restore 1591