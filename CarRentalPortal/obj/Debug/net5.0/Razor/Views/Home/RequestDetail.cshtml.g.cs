#pragma checksum "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\RequestDetail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "23b782dbb666de5082293b75a4182bb4d154d02a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_RequestDetail), @"mvc.1.0.view", @"/Views/Home/RequestDetail.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"23b782dbb666de5082293b75a4182bb4d154d02a", @"/Views/Home/RequestDetail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"75032d6b42beb35b56e3f4b3d9f7cd6f887be7e1", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_RequestDetail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CarRentalPortal.Models.ViewModels.AdminRequestVM>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_AdminNavBar", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AdminPortal", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "23b782dbb666de5082293b75a4182bb4d154d02a4582", async() => {
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
            WriteLiteral("\r\n</div>\r\n<div class=\"row p-4\">\r\n    <div class=\"col-4 text-center offset-4\">\r\n        <h3>Validate Request</h3>\r\n    </div>\r\n    <div class=\"col-3 text-right\">\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "23b782dbb666de5082293b75a4182bb4d154d02a5884", async() => {
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
            WriteLiteral("\r\n    </div>\r\n</div>\r\n\r\n");
#nullable restore
#line 15 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\RequestDetail.cshtml"
 if (Model == null)
{
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"maxWidthVW border p-5 receiptFont\">\r\n        <div class=\"row\">\r\n            <div class=\"col-7 offset-1\">\r\n                <b>Customer Name : </b> ");
#nullable restore
#line 23 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\RequestDetail.cshtml"
                                   Write(Html.DisplayFor(m => m.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n                <b>Customer Phone : </b> ");
#nullable restore
#line 24 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\RequestDetail.cshtml"
                                    Write(Html.DisplayFor(m => m.PhoneNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n            </div>\r\n            <div class=\"col-4\">\r\n                <img");
            BeginWriteAttribute("src", " src=\"", 817, "\"", 838, 1);
#nullable restore
#line 27 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\RequestDetail.cshtml"
WriteAttributeValue("", 823, ViewBag.ImgUrl, 823, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""mainImg border50"" alt=""Car Image"" />
            </div>
        </div>
        <div class=""row"">
            <table class=""ninetypercentTable"">
                <tr class=""backgroundPrimary"">
                    <th>Booking Id</th>
                    <th>Booking Date</th>
                    <th>Booking From</th>
                    <th>Booking To</th>
                </tr>
                <tr>
                    <td> ");
#nullable restore
#line 39 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\RequestDetail.cshtml"
                    Write(Html.DisplayFor(m => m.OrderId));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td> ");
#nullable restore
#line 40 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\RequestDetail.cshtml"
                    Write(Html.DisplayFor(m => m.OrderDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td> ");
#nullable restore
#line 41 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\RequestDetail.cshtml"
                    Write(Html.DisplayFor(m => m.FromDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td> ");
#nullable restore
#line 42 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\RequestDetail.cshtml"
                    Write(Html.DisplayFor(m => m.ToDate));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</td>
                </tr>
            </table>
        </div>
        <div class=""row"">
            <table class=""ninetypercentTable"">
                <tr class=""backgroundPrimary"">
                    <th>Registration Number</th>
                    <th>Car Name</th>
                    <th>Car Colour</th>
                </tr>
                <tr>
                    <td> ");
#nullable restore
#line 54 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\RequestDetail.cshtml"
                    Write(Html.DisplayFor(m => m.RegNo));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td> ");
#nullable restore
#line 55 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\RequestDetail.cshtml"
                    Write(Html.DisplayFor(m => m.CarName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td> ");
#nullable restore
#line 56 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\RequestDetail.cshtml"
                    Write(Html.DisplayFor(m => m.Colour));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</td>
                </tr>
            </table>
        </div>
        <br />
        <div class=""row"">
            <div class=""col-4 offset-1"">
               Car Returned Date :
            </div>
            <div class=""col-2"">
                <input type=""date"" id=""returnedDatePicker"" class=""form-control formInput"" />
            </div>
            <div class=""col-3 offset-1"">
                <b>Todays Date : ");
#nullable restore
#line 69 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\RequestDetail.cshtml"
                            Write(ViewBag.TodaysDate);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</b>
            </div>
            <br /><br />
            <div class=""row"">
                <div class=""col-4 offset-1"">
                    &nbsp;Additional Fine Amount (If applicable) :
                </div>
                <div class=""col-2"">
                    <input type=""number"" id=""fineAmountInput"" class=""form-control formInput"" />
                </div>
            </div>
        </div><br />
        <div class=""row"">
            <div class=""col-3 offset-3"">
                <button onclick=""approveFn()"" class=""btn btn-success btn-block""><i class=""fas fa-thumbs-up""></i> Approve</button>
            </div>
            <div class=""col-3"">
                <button onclick=""rejectFn()"" class=""btn btn-danger btn-block""><i class=""fas fa-thumbs-down""></i> Reject Request</button>
            </div>
        </div>
    </div>
");
#nullable restore
#line 90 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\RequestDetail.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<script type=""text/javascript"">
    function approveFn() {
        if (document.getElementById(""returnedDatePicker"").value == """") {
            Swal.fire('Please choose a return date')
            return;
        }
        var ReturnedDate = document.getElementById(""returnedDatePicker"").value;
        var returnedDateObj = new Date(ReturnedDate);
        var fromDateObj =new Date('");
#nullable restore
#line 100 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\RequestDetail.cshtml"
                              Write(Model.FromDate.Year);

#line default
#line hidden
#nullable disable
            WriteLiteral("\',parseInt(\'");
#nullable restore
#line 100 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\RequestDetail.cshtml"
                                                              Write(Model.FromDate.Month.ToString());

#line default
#line hidden
#nullable disable
            WriteLiteral("\')-1,\'");
#nullable restore
#line 100 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\RequestDetail.cshtml"
                                                                                                    Write(Model.FromDate.Day);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"');

        if (returnedDateObj <fromDateObj) {
            Swal.fire(
                'Failed!',
                'Car return date cannot be before the booking start date!.',
                'error'
            );
            return;
        }
        var fineAmount = document.getElementById(""fineAmountInput"").value;
        Swal.fire({
            title: 'Are you sure?',
            text: ""You won't be able to revert this!"",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, Approve!'
        }).then((result) => {
            if (result.isConfirmed) {
          Swal.fire(
                    'Approved!',
                    'Request was successfully approved.',
                    'success'
                )
                location.href = """);
#nullable restore
#line 126 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\RequestDetail.cshtml"
                            Write(Url.ActionLink("RequestApproved","home"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\" + \"?returndate=\" + ReturnedDate + \"&fineamount=\" + fineAmount + \"&orderid=\" +");
#nullable restore
#line 126 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\RequestDetail.cshtml"
                                                                                                                                                    Write(Model.OrderId);

#line default
#line hidden
#nullable disable
            WriteLiteral("+\"&userid=\" +");
#nullable restore
#line 126 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\RequestDetail.cshtml"
                                                                                                                                                                               Write(Model.UserId);

#line default
#line hidden
#nullable disable
            WriteLiteral("+\"&carmodelid=\" +");
#nullable restore
#line 126 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\RequestDetail.cshtml"
                                                                                                                                                                                                             Write(Model.CarModelId);

#line default
#line hidden
#nullable disable
            WriteLiteral(@";
            }
        })
    }
    function rejectFn() {
        Swal.fire({
            title: 'Are you sure?',
            text: ""You won't be able to revert this!"",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, Reject!'
        }).then((result) => {
            if (result.isConfirmed) {
                Swal.fire(
                    'Deleted!',
                    'Request has been rejected.',
                    'danger'
                )
                location.href = """);
#nullable restore
#line 146 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\RequestDetail.cshtml"
                            Write(Url.ActionLink("RequestRejected","home"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\"+\"?orderid=\"+");
#nullable restore
#line 146 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\RequestDetail.cshtml"
                                                                                   Write(Model.OrderId);

#line default
#line hidden
#nullable disable
            WriteLiteral("+\"&userid=\"+");
#nullable restore
#line 146 "C:\Users\basil\source\repos\CarRentalPortal\CarRentalPortal\Views\Home\RequestDetail.cshtml"
                                                                                                             Write(Model.UserId);

#line default
#line hidden
#nullable disable
            WriteLiteral(";\r\n            }\r\n        })\r\n    }\r\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CarRentalPortal.Models.ViewModels.AdminRequestVM> Html { get; private set; }
    }
}
#pragma warning restore 1591
