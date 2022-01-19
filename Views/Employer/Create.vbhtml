@ModelType JobPostings.Models.ViewModels.EmployerViewModel

@Code
    ViewData("Title") = "Create"
    Layout = "~/Views/Employer/Shared/_EmployerLayout.vbhtml"
End Code


@If Not Model.EmployerDetails Is Nothing Then
    @Using Html.BeginForm()
        @Html.AntiForgeryToken()
        @Html.ValidationSummary(True)

        @<fieldset>
            <legend>Create Employer</legend>
            <div class="row">
                <div class="col-md-12">
                    <button class="btn btn-default pull-right" type="submit">
                        <span class="glyphicon glyphicon-ok"></span> Submit
                    </button>
                </div>
            </div>
             <div id="EmployerDetails">
                 @Html.Partial("~/Views/Employer/Shared/_EmployerForm.vbhtml", Model)
             </div>
        </fieldset>
    End Using
End If
