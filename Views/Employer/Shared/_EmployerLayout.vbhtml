@ModelType JobPostings.Models.ViewModels.EmployerViewModel

@Code
    ViewData("Title") = "Employer"
    Layout = "~/Views/Shared/_AdminLayout.vbhtml"
    
    Dim action = ViewContext.RouteData.GetRequiredString("action")
End Code

@section scripts
    <script src="~/Scripts/Site/Employer.js"></script>
    @RenderSection("scripts", required:=False)
End Section

@section styles
       @RenderSection("styles", required:=False)
End Section

<div id="empSecNav" class="navbar navbar-default">
    <div class="container">
        <div class="navbar-collapse collapse">
            <ul class="nav navbar-nav">
                <li @If (action = "Index") Then @Html.AttributeEncode("class=active")  end If>
                    @Html.ActionLink("Search", "Index", "Employer")
                </li>
                <li @If (action = "Create") Then @Html.AttributeEncode("class=active")  end If>
                    @Html.ActionLink("New", "Create", "Employer")
                </li>
            </ul>
        </div>
    </div>
</div>
<div class="container body-content" style="margin-top:10px">
    @If Not Model.DisplayMessage Is Nothing Then
        @<div class='alert @Model.DisplayMessage.DisplayClass'>
            @Model.DisplayMessage.Status
        </div>
    End If
    @RenderBody()
</div>


