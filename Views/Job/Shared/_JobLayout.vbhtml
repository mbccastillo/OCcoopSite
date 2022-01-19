@ModelType JobPostings.Models.ViewModels.BaseViewModel

@Code
    Layout = "~/Views/Shared/_AdminLayout.vbhtml"

    Dim controller = ViewContext.RouteData.GetRequiredString("Controller")
    Dim action = ViewContext.RouteData.GetRequiredString("action")
End Code

@section scripts
    <script src="~/Scripts/Site/Job.js"></script>
    @RenderSection("scripts", required:=False)
End Section

@section styles
     @RenderSection("styles", required:=False)
End Section

<div id="jobSecNav" class="navbar navbar-default">
    <div class="container">
        <div class="navbar-collapse collapse">
            <ul class="nav navbar-nav">
                <li @If (controller = "Post") Then @Html.AttributeEncode("class=active")  end If>
                    @Html.ActionLink("Pending", "Pending", "Post")
                </li>
                <li @If (action = "Index") Then @Html.AttributeEncode("class=active")  end If>
                    @Html.ActionLink("Search", "Index", "Job")
                </li>
                <li @If (action = "Create") Then @Html.AttributeEncode("class=active")  end If>
                    @Html.ActionLink("New", "Create", "Job")
                </li>
                <li @If (action = "Reports") Then @Html.AttributeEncode("class=active")  end If>
                    @Html.ActionLink("Reports", "Reports", "Job")
                </li></ul>
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


