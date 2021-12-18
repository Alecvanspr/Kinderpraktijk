#pragma checksum "C:\Users\alecv\Desktop\Project\Kinderpraktijk\src\Views\DashBoard\Chat.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7e6166f7608416e49e237bc62ab90ec93aa0f236"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_DashBoard_Chat), @"mvc.1.0.view", @"/Views/DashBoard/Chat.cshtml")]
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
#line 1 "C:\Users\alecv\Desktop\Project\Kinderpraktijk\src\Views\_ViewImports.cshtml"
using src;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\alecv\Desktop\Project\Kinderpraktijk\src\Views\_ViewImports.cshtml"
using src.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7e6166f7608416e49e237bc62ab90ec93aa0f236", @"/Views/DashBoard/Chat.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ca6abdd9730bf1b4f2a327dc2e2bda1bd41f892c", @"/Views/_ViewImports.cshtml")]
    public class Views_DashBoard_Chat : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Chat>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("onsubmit", new global::Microsoft.AspNetCore.Html.HtmlString("sendMessage(event)"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Chat", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "CreateMessage", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/signalr/dist/browser/signalr.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/messageBuilder.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n    <div class=\"chat-body\">\r\n");
#nullable restore
#line 4 "C:\Users\alecv\Desktop\Project\Kinderpraktijk\src\Views\DashBoard\Chat.cshtml"
     if(Model!=null){
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\alecv\Desktop\Project\Kinderpraktijk\src\Views\DashBoard\Chat.cshtml"
         foreach (var message in Model.Messages)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"message\">\r\n                <header>");
#nullable restore
#line 8 "C:\Users\alecv\Desktop\Project\Kinderpraktijk\src\Views\DashBoard\Chat.cshtml"
                   Write(message.Naam);

#line default
#line hidden
#nullable disable
            WriteLiteral("</header>\r\n                <p>");
#nullable restore
#line 9 "C:\Users\alecv\Desktop\Project\Kinderpraktijk\src\Views\DashBoard\Chat.cshtml"
              Write(message.Text);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                <footer>\r\n                    ");
#nullable restore
#line 11 "C:\Users\alecv\Desktop\Project\Kinderpraktijk\src\Views\DashBoard\Chat.cshtml"
               Write(message.timestamp.ToShortTimeString());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </footer>\r\n        </div>\r\n");
#nullable restore
#line 14 "C:\Users\alecv\Desktop\Project\Kinderpraktijk\src\Views\DashBoard\Chat.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\Users\alecv\Desktop\Project\Kinderpraktijk\src\Views\DashBoard\Chat.cshtml"
         
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n<div class=\"chat-input\">\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7e6166f7608416e49e237bc62ab90ec93aa0f2367166", async() => {
                WriteLiteral("\r\n        <input type=\"hidden\" name=\"chatId\"");
                BeginWriteAttribute("value", " value=\"", 577, "\"", 594, 1);
#nullable restore
#line 19 "C:\Users\alecv\Desktop\Project\Kinderpraktijk\src\Views\DashBoard\Chat.cshtml"
WriteAttributeValue("", 585, Model.Id, 585, 9, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\r\n        <input type=\"hidden\" name=\"roomName\"");
                BeginWriteAttribute("value", " value=\"", 642, "\"", 661, 1);
#nullable restore
#line 20 "C:\Users\alecv\Desktop\Project\Kinderpraktijk\src\Views\DashBoard\Chat.cshtml"
WriteAttributeValue("", 650, Model.Naam, 650, 11, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\r\n        <input class=\"form-control form-control-lg\" type=\"text\" name=\"Message\">\r\n        <input class=\"btn btn-primary\" type=\"submit\" />\r\n    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n\r\n");
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n<script src=\"https://unpkg.com/axios/dist/axios.min.js\"></script>\r\n");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7e6166f7608416e49e237bc62ab90ec93aa0f23610260", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7e6166f7608416e49e237bc62ab90ec93aa0f23611356", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
<script>
        //DIt is voor het aanmaken van een connectie met de ChatHub
    //De naam van de Chathub is case sensitive
    const connection = new signalR.HubConnectionBuilder()
                                    .withUrl(""https://localhost:5001/chathub"")
                                    .configureLogging(signalR.LogLevel.Information)
                                    .build();

        //In de methode hieronder wordt bij het ontvangen van een bericht het bericht toegevoegd aan de html
    connection.on(""ReceiveMessage"", (data ) => {
        var message = messageBuilder()
            .createMessage()
            .withHeader(data.naam)
            .withParagraph(data.text)
            .withFooter(data.timestamp)
            .build();
        document.querySelector('.chat-body').append(message);
    });

        var _connectionId = '';

        var joinroom = () =>{
            var url = '/Chat/JoinRoom/' + _connectionId +'/");
#nullable restore
#line 52 "C:\Users\alecv\Desktop\Project\Kinderpraktijk\src\Views\DashBoard\Chat.cshtml"
                                                      Write(Model.Naam);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"';
            axious.post(url,null)
                .then(res => {
                    console(""Room Joined!"", res);
                })
                .catch(err => {
                    console.err(""Failed to join Room!"", res);
                })
        }

    async function start() {
        try {
            await connection.start();
            console.log(""SignalR Connected."");
        } catch (err) {
            console.log(err);
            setTimeout(start, 5000);
        }
    };

    connection.onclose(async () => {
        await start();
    });

    // Start the connection.
    start();

    var form = null;

    //hier wordt de hele form uit de html gepakt door gerbruik van onsubmit
     var sendMessage = function (event) {
        event.preventDefault();

        var data = new FormData(event.target);
        //Hieronder wordt de data gepost naar de method in chatController.cs
        axios.post('/Chat/SendMessage',data)
            .then(res => {
        ");
                WriteLiteral("        console.log(\"Message send!\")\r\n            })\r\n            .catch(err => {\r\n                console.log(\"Iets ging fout!\")\r\n            })\r\n    }\r\n</script>\r\n");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Chat> Html { get; private set; }
    }
}
#pragma warning restore 1591
