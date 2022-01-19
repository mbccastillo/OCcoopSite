@ModelType JobPostings.Models.ViewModels.PendingPostingViewModel

@Code
    ViewData("Title") = "Posting Details"
    Layout = "~/Views/Job/Shared/_JobLayout.vbhtml"
End Code


@If Not Model.PostingDetails Is Nothing Then
    @Using Html.BeginForm("Approve", "Post")
        @Html.AntiForgeryToken()
        @Html.ValidationSummary(True)

        @<fieldset>
            <legend>Posting</legend>
            <div class="row">
                <div class="col-md-12 text-right">
                    <a Class="btn btn-default" href='@Url.Action("Deny", "Post", New With {.id = Model.PostingDetails.PostingId})'><span class="glyphicon glyphicon-ban-circle"></span> Denied</a>
                    <button class="btn btn-default" type="submit">
                        <span class="glyphicon glyphicon-ok"></span> Approve
                    </button>
                </div>
            </div>
            <div id="JobDetails">
                @Html.Partial("~/Views/Post/Shared/_PostForm.vbhtml", Model)
            </div>
        </fieldset>
    End Using
End If

