@ModelType JobPostings.Models.ViewModels.ManageViewModel

@Code
    ViewData("Title") = "Job"
    Layout = "~/Views/Shared/_AdminLayout.vbhtml"
    
    Dim action = ViewContext.RouteData.GetRequiredString("action")
End Code

@section scripts
    <script src="~/Scripts/Site/Manage.js"></script>
    @RenderSection("scripts", required:=False)
End Section

@section styles
     @RenderSection("styles", required:=False)
End Section

<div id="manageSecNav" class="navbar navbar-default">
    <div class="container">
        <div class="navbar-collapse collapse">
            <ul class="nav navbar-nav">
                <li @If (action = "EmpTypes") Then @Html.AttributeEncode("class=active")  end If>
                    @Html.ActionLink("Employment Types", "EmpTypes", "Manage")
                </li>
                <li @If (action = "JobTypes") Then @Html.AttributeEncode("class=active")  end If>
                    @Html.ActionLink("Job Types", "JobTypes", "Manage")
                </li>
                <li @If (action = "Categories") Then @Html.AttributeEncode("class=active")  end If>
                    @Html.ActionLink("Categories", "Categories", "Manage")
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


