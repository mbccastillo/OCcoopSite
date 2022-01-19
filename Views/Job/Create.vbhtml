@ModelType JobPostings.Models.ViewModels.JobViewModel

@Code
    ViewData("Title") = "Job Create"
    Layout = "~/Views/Job/Shared/_JobLayout.vbhtml"
End Code

@If Not Model.JobDetails Is Nothing Then
    @Using Html.BeginForm()
        @Html.AntiForgeryToken()
        @Html.ValidationSummary(True)

        @<fieldset>
            <legend>Create Job</legend>
             <div class="row">
                 <div class="col-md-12">
                     <button class="btn btn-default pull-right" type="submit">
                         <span class="glyphicon glyphicon-ok"></span> Submit
                     </button>
                 </div>
             </div>
             <div id="JobDetails">
                 @Html.Partial("~/Views/Job/Shared/_JobForm.vbhtml", Model)
             </div>
        </fieldset>
    End Using
End If
