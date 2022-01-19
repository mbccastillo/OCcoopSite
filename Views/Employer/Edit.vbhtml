@ModelType JobPostings.Models.ViewModels.EmployerViewModel

@Code
    ViewData("Title") = "Edit"
    Layout = "~/Views/Employer/Shared/_EmployerLayout.vbhtml"
End Code


@If Not Model.EmployerDetails Is Nothing Then
    @Using Html.BeginForm()
        @Html.AntiForgeryToken()
        @Html.ValidationSummary(True)

        @<fieldset>
    <legend>Edit Employer</legend>
    <div class="row">
        <div class="col-md-12">
            <div class="pull-right">
                <button class="btn btn-default " type="submit" value="Edit" name="action">
                    <span class="glyphicon glyphicon-floppy-disk"></span> Save
                </button>
                <a href="#modalDeleteConfirm" role="button" class="btn btn-default" data-toggle="modal"><span class="glyphicon glyphicon-remove"></span> Delete</a>
            </div>
        </div>
    </div>
    <div id="EmployerDetails">
        @Html.Partial("~/Views/Employer/Shared/_EmployerForm.vbhtml", Model)
    </div>

    <div class="row row-form">
        <div class="col-md-12">

            <div id="EmployerJobs" class="span12" style="margin-left: 0px;">
                <label>Jobs</label>
                <div class="scrollList" style="width:100%;">
                    <ul style="margin-left:0px">
                        @For Each j In Model.EmployerDetails.Jobs
                            @<li>
                                @Html.ActionLink(j.JobCategoriesDisplay & " / Job title: " & j.JobTitle & " / Job posted on: " & j.DatePosted, "Edit", "Job", New With {.id = j.JobId}, Nothing)
                            </li>
                        Next
                    </ul>
                </div>
            </div>
        </div>
    </div>

</fieldset>
    End Using

    @<div id="modalDeleteConfirm" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="myModalLabel">Delete Employer</h4>
                </div>
                <div class="modal-body">
                    <p>This employer will be permanently deleted as well as any associated jobs.</p>
                    <p>Do you wish to continue?</p>
                </div>
                <div class="modal-footer">
                    @Using Html.BeginForm("Delete", "Employer", New With {.id = Model.EmployerDetails.EmployerId})
                        @<button class="btn  btn-primary cancel" type="submit" value="Delete" name="action">Yes</button>
                        @<button class="btn btn-default" data-dismiss="modal" aria-hidden="true">No</button>
                    end Using
                </div>
            </div>
        </div>
    </div>
End If