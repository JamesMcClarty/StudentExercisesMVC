#pragma checksum "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7f10495c2fd9358bd6ce399f41e71b80c53c8e9a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Cohorts_Details), @"mvc.1.0.view", @"/Views/Cohorts/Details.cshtml")]
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
#line 1 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\_ViewImports.cshtml"
using StudentExercisesMVC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\_ViewImports.cshtml"
using StudentExercisesMVC.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7f10495c2fd9358bd6ce399f41e71b80c53c8e9a", @"/Views/Cohorts/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bae3dac0dc5d195cfb606d7b7ac9ff8ae977575d", @"/Views/_ViewImports.cshtml")]
    public class Views_Cohorts_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<StudentExercisesMVC.Models.Cohort>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 3 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Details</h1>\r\n\r\n<div>\r\n    <h4>Cohort</h4>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 14 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 17 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml"
       Write(Html.DisplayFor(model => model.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 20 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 23 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml"
       Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n</div>\r\n\r\n<h4>Students</h4>\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 33 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml"
           Write(Html.DisplayNameFor(model => model.Students[0].Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 36 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml"
           Write(Html.DisplayNameFor(model => model.Students[0].FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 39 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml"
           Write(Html.DisplayNameFor(model =>  model.Students[0].LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 42 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml"
           Write(Html.DisplayNameFor(model => model.Students[0].SlackHandle));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n\r\n    <tbody>\r\n");
#nullable restore
#line 49 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml"
         foreach (var item in Model.Students)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 53 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml"
               Write(Html.DisplayFor(modelItem => item.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 56 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml"
               Write(Html.DisplayFor(modelItem => item.FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 59 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml"
               Write(Html.DisplayFor(modelItem => item.LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 62 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml"
               Write(Html.DisplayFor(modelItem => item.SlackHandle));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 65 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n\r\n<h4>Instructors</h4>\r\n<table class=\"table\">\r\n\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 75 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml"
           Write(Html.DisplayNameFor(model => model.Instructors[0].Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 78 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml"
           Write(Html.DisplayNameFor(model => model.Instructors[0].FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 81 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml"
           Write(Html.DisplayNameFor(model => model.Instructors[0].LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 84 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml"
           Write(Html.DisplayNameFor(model => model.Instructors[0].SlackHandle));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 87 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml"
           Write(Html.DisplayNameFor(model => model.Instructors[0].Specialty));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n\r\n    <tbody>\r\n");
#nullable restore
#line 94 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml"
         foreach (var item in Model.Instructors)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
#nullable restore
#line 98 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml"
           Write(Html.DisplayFor(modelItem => item.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 101 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml"
           Write(Html.DisplayFor(modelItem => item.FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 104 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml"
           Write(Html.DisplayFor(modelItem => item.LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 107 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml"
           Write(Html.DisplayFor(modelItem => item.SlackHandle));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 110 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml"
           Write(Html.DisplayFor(modelItem => item.Specialty));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n        </tr>\r\n");
#nullable restore
#line 113 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n\r\n\r\n<div>\r\n    ");
#nullable restore
#line 119 "C:\Users\James\workspace\StudentExercisesMVC\StudentExercisesMVC\Views\Cohorts\Details.cshtml"
Write(Html.ActionLink("Edit", "Edit", new { id = Model.Id}));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7f10495c2fd9358bd6ce399f41e71b80c53c8e9a13022", async() => {
                WriteLiteral("Back to List");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<StudentExercisesMVC.Models.Cohort> Html { get; private set; }
    }
}
#pragma warning restore 1591