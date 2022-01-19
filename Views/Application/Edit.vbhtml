@Imports Jobpostings.Extensions

@ModelType JobPostings.Models.ViewModels.ApplicationViewModel

@Code
    ViewData("Title") = "Edit Co-op Application Status"
    Layout = "~/Views/Shared/_CoopLayout.vbhtml"
    'Layout = "~/Views/Student/Shared/_AdminLayout.vbhtml"
    Html.EnableClientValidation()
    Html.EnableUnobtrusiveJavaScript()
End Code

@section scripts
    <script src="~/Scripts/Site/Application.js"></script>
End Section

@If (Not Model.StuApDetails Is Nothing) Then
    @Using Html.BeginForm()
        @Html.AntiForgeryToken()
        @Html.ValidationSummary(True)

        @<fieldset>
            <legend>Edit Application Status</legend>
            <div Class="row row-form">
                <div class="col-md-2">
                    <a class="btn btn-default" href='@Url.Action("Details", "Student", New With {.id = Model.StuApDetails.StudentNumber})'>
                        View Student Details
                    </a>
                </div>
                <div Class="col-md-6 col-md-offset-4">
                    <div class="pull-right">
                        <a href="#modalDeleteConfirm" role="button" Class="btn btn-default" data-toggle="modal">
                            <span Class="glyphicon glyphicon-remove"></span> Delete Application
                        </a>
                        <a href="#modalLetter" role="button" Class="btn btn-default" data-toggle="modal">
                            <span Class="glyphicon glyphicon-file"></span> Generate Approval Letter
                        </a>
                        <Button Class="btn btn-default" type="submit" id="button">
                            <span Class="glyphicon glyphicon-floppy-disk"></span> Save Changes
                        </Button>
                    </div>
                </div>
            </div>
            &nbsp;
            &nbsp;
            @Html.Partial("~/Views/Application/Shared/_EditApplicationForm.vbhtml", Model)
            <br />
        </fieldset>
    End Using



    @<fieldset>
        <legend>Edit/Add Comments and Jobs</legend>
        <ul Class="nav nav-tabs" role="tablist">
            <li role="presentation" Class="active border-primary bg-success"><a href="#comments" aria-controls="comments" role="tab" data-toggle="tab">Comments</a></li>
            <li role="presentation" class="border-primary bg-success"> <a href="#jobs" aria-controls="jobs" role="tab" data-toggle="tab">Jobs</a></li>
            <li role="presentation" class="border-primary bg-success"> <a href="#workterms" aria-controls="workterms" role="tab" data-toggle="tab">Work Terms</a></li>
        </ul>
        <br />
        <div class="tab-content">
            <div role="tabpanel" class="tab-pane active" id="comments">
                @Using Html.BeginForm("AddComment", "Application")
                    @Html.HiddenFor(Function(m) m.StuApDetails.AppId)
                    @Html.AntiForgeryToken()
                    @Html.ValidationSummary(True)

                    @<div Class="row row-form">
                        <div Class="col-md-2">
                            @Html.DropDownListFor(Function(m) m.AppComment.Type, Model.CommentTypeList, New With {.Class = "form-control"})
                        </div>
                        <div Class="col-md-2 col-md-offset-8 text-right">
                            <Button Class="btn btn-primary" type="submit" id="button">
                                <span Class="glyphicon glyphicon-plus-sign"></span> Add Comment
                            </Button>
                        </div>

                    </div>

                    @<div Class="row row-form">
                        <div Class="col-md-12">
                            @Html.TextAreaFor(Function(m) m.AppComment.Comment, New With {.class = "form-control"})
                            @Html.ValidationMessageFor(Function(m) m.AppComment.Comment)
                        </div>
                    </div>
                End Using
                <div Class="row row-form">
                    <div Class="col-md-12">
                        @For Each item In Model.StuApDetails.Comments.GroupBy(Function(x) x.Type)
                            @<h4>
                                @If item.Key = "STU" Then
                                    @:Public Comments
                                Else
                                    @:Internal Comments
                                End If
                            </h4>
                            @<div class="scrollList" style="max-height:250px; padding:10px; margin-bottom: 25px; box-sizing: border-box;">
                                @For Each com In item.OrderByDescending(Function(x) x.ActivityDate)
                                    @<div class="panel panel-default">
                                        <div Class="panel-heading" style="font-weight: bold">
                                            <div Class="col-md-8">
                                                @com.ActivityDate - Comment added by: @com.Name - @com.UserId
                                            </div>
                                            <div Class="col-md-offset-4 text-right">
                                                <a href='@Url.Action("DeleteComment", "Application", New With {.id = com.AppComId})'><span class="glyphicon glyphicon-remove"></span> Delete Comment</a>&nbsp; &nbsp;
                                                <a href='@Url.Action("AppCommentDialog", "Application", New With {.id = com.AppComId})' class="app-comment-dialog"><span class="glyphicon glyphicon-edit "></span> Edit Comment</a>
                                            </div>
                                        </div>
                                        <div Class="panel-body">
                                            @com.Comment
                                        </div>
                                    </div>
                                Next
                            </div>
                        Next
                    </div>
                </div>
            </div>

            <div role="tabpanel" class="tab-pane" id="jobs">
                <div class="row row-form">
                    <div class="col-md-2 col-md-offset-10 text-right">
                        <a href="#modaljob" role="button" Class="btn btn-primary" data-toggle="modal">
                            <span Class="glyphicon glyphicon-plus-sign"></span> Add Job
                        </a>
                    </div>
                </div>
                <br />
                <table class="table">
                    <thead>
                        <tr>
                            <th>Jobs the Student has Applied for</th>
                            <th>Employer</th>
                            <th>Activity Date</th>
                            <th class="text-right">Was the Student Hired?</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        @if Model.StuApDetails.Jobs.Count > 0 Then
                            @for each item In Model.StuApDetails.Jobs.OrderByDescending(Function(x) x.ActivityDate)
                                @<tr>
                                    <td>
                                        @Html.ActionLink(item.Job.JobCategoriesDisplay & " " & item.Job.JobTitle, "Details", "Search", New With {.id = item.JobId}, New With {.target = "_blank"})
                                    </td>
                                    <td>
                                        @item.Job.EmployerDisplay
                                    </td>
                                    <td>@item.ActivityDate.ToString("MMM dd, yyyy")</td>
                                    <td class="text-right">
                                        <div class="btn-group" role="group" aria-label="Was the Student Hired?">
                                            <a @If item.Status = JobPostings.Models.Entities.Coop.AppJob.StatusType.Hired Then @: class="btn btn-success"
                                               Else @: class="btn btn-default"
                                               End If
                                               href='@Url.Action("JobHire", "Application", New With {.jobId = item.JobId, .appId = item.AppId})'>
                                                Yes
                                            </a>
                                            <a @If item.Status = JobPostings.Models.Entities.Coop.AppJob.StatusType.Applied Then @: class="btn btn-gray"
                                               Else @: class="btn btn-default"
                                               End If
                                               href='@Url.Action("JobHire", "Application", New With {.jobId = item.JobId, .appId = item.AppId})'>
                                                No
                                            </a>
                                        </div>
                                    </td>
                                    <td><a class="btn btn-danger" href='@Url.Action("DeleteJob", "Application", New With {.jobId = item.JobId, .appId = item.AppId})'><span class="glyphicon glyphicon-remove"></span> </a></td>
                                </tr>
                            Next
                        Else
                            @<tr>
                                <td align="center" colspan="3"> Student has not applied for any jobs</td>
                            </tr>
                        End If
                    </tbody>
                </table>
            </div>

            <div role="tabpanel" class="tab-pane" id="workterms">
                <div class="row row-form">
                    <div class="col-md-2 col-md-offset-10 text-right">
                        <a href="#modalAddWorkTerm" role="button" Class="btn btn-primary" data-toggle="modal">
                            <span Class="glyphicon glyphicon-plus-sign"></span> Add Work Term
                        </a>
                    </div>

                    <table class="table">
                        <thead>
                            <tr>
                                <th>Term</th>
                                <th>Program</th>
                                <th>Work Term #</th>
                                <th>Categories</th>
                                <th>Job Title</th>
                                <th>Employer</th>
                                <th>Location</th>
                                <th>Monitoring Visit</th>
                                <th>Evaluation</th>
                                <th>Employer Evaluation</th>
                                <th>Work Term Report</th>
                                <th>Comments</th>
                                <th> </th>
                            </tr>
                        </thead>
                        <tbody>
                            @if Model.WorkTerms.Count > 0 Then
                                @for each item In Model.WorkTerms.OrderByDescending(Function(x) x.Term).ThenBy(Function(x) x.WorkTermNumber)
                                    @<tr>
                                        <td>
                                            @item.Term
                                        </td>
                                        <td>
                                            @item.Program
                                        </td>
                                        <td>
                                            @item.WorkTermNumber
                                        </td>
                                        <td>
                                            @item.Job.JobCategoriesDisplay
                                        </td>
                                        <td>
                                            @item.Job.JobTitle
                                        </td>
                                        <td>
                                            @item.Job.EmployerDisplay
                                        </td>
                                        <td>
                                            @item.Job.Location
                                        </td>
                                        <td>
                                            @item.VisitDate.GetValueOrDefault.ToMinValString("MMM dd, yyyy")
                                        </td>
                                        <td>
                                            @item.StuEvalDate.GetValueOrDefault.ToMinValString("MMM dd, yyyy")
                                        </td>
                                        <td>
                                            @item.EmpEvalDate.GetValueOrDefault.ToMinValString("MMM dd, yyyy")
                                        </td>
                                        <td>
                                            @item.ReportDate.GetValueOrDefault.ToMinValString("MMM dd, yyyy")
                                        </td>
                                        <td>
                                            @item.Comment
                                        </td>
                                        <td>
                                            <a href='@Url.Action("WorkTermDialog", "Application", New With {.id = item.WorkId})' role="button" Class="btn btn-sm btn-default workterm-dialog" data-toggle="modal">
                                                <span Class="glyphicon glyphicon-pencil"></span>
                                            </a>
                                            <a data-wrkid="@item.WorkId" role="button" Class="btn btn-sm btn-danger" data-toggle="modal" data-target="#modalDeleteWorkTerm">
                                                <span Class="glyphicon glyphicon-remove"></span>
                                            </a>
                                        </td>
                                    </tr>
                                Next
                            Else
                                @<tr>
                                    <td align="center" colspan="13">No work terms.</td>
                                </tr>
                            End If
                        </tbody>
                    </table>

                </div>
            </div>
        </div>
    </fieldset>

    'Dialogs

    @<div id="modalDeleteConfirm" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="myModalLabel">Delete Application</h4>
                </div>
                <div class="modal-body">
                    <p>This student's co-op application will be permanently deleted.</p>
                    <p>Do you wish to continue?</p>
                </div>
                <div class="modal-footer">
                    @Using Html.BeginForm("Delete", "Application", New With {.id = Model.StuApDetails.AppId})
                        @<button class="btn  btn-primary cancel" type="submit" value="Delete" name="action">Yes</button>
                        @<button class="btn btn-default" data-dismiss="modal" aria-hidden="true">No</button>
                    end Using
                </div>
            </div>
        </div>
    </div>

    @Using Html.BeginForm("GenerateLetter", "Application", New With {.AppId = Model.StuApDetails.AppId}, FormMethod.Post, New With {.target = "_blank"})
        @<div id="modalLetter" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="myModalLabel">Approval Letter</h4>
                    </div>
                    <div class="modal-body">
                        <p>Choose a coordinator</p>
                        @Html.DropDownListFor(Function(model) model.SelectedCoordinator, Model.Coordinators, "--", New With {.Class = "form-control"})
                    </div>
                    <div class="modal-footer">
                        <button class="btn  btn-primary cancel" type="submit" value="Delete" name="action">Ok</button>
                        <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
                    </div>
                </div>
            </div>
        </div>
    End Using

    @Using Html.BeginForm("EditComment", "Application", New With {.AppId = Model.StuApDetails.AppId}, FormMethod.Post, New With {.Id = "form-edit-comment"})
        @<div id="modalEditComment" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="myModalLabel">Edit Comment</h4>
                    </div>
                    <div class="modal-body">
                    </div>
                    <div class="modal-footer">
                        <button class="btn btn-primary" type="submit">Save</button>
                        <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
                    </div>
                </div>
            </div>
        </div>
    End Using


    @Using Html.BeginForm("AddJob", "Application", New With {.AppId = Model.StuApDetails.AppId}, FormMethod.Post, New With {.Id = "formAddJob"})
        @<div id="modaljob" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="modalJoblabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="modalJobLabel">Add Job</h4>
                    </div>
                    <div class="modal-body">
                        <div Class="row">
                            <div Class="col-md-2">
                                <label>Year</label>
                                @Html.TextBox("Year", Date.Today.Year, New With {.id = "txtYear", .class = "form-control"})
                            </div>
                            <div Class="col-md-7">
                                <label>Search for Job</label>
                                <div Class="ui-widget">
                                    @Html.Hidden("JobId", "", New With {.id = "hidJobId"})
                                    @Html.TextBox("Job", "", New With {.id = "txtJobId", .class = "form-control"})
                                </div>
                            </div>
                            <div Class="col-md-3">
                                <label>Status</label>
                                <select class="form-control" name="Status">
                                    <option value="0">Applied</option>
                                    <option value="1">Hired</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button class="btn btn-primary" type="submit">Add</button>
                        <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
                    </div>
                </div>
            </div>
        </div>
    End Using


    @Using Html.BeginForm("AddWorkTerm", "Application", FormMethod.Post, New With {.Id = "formAddWorkTerm"})
        @<div id="modalAddWorkTerm" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="modalJoblabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="modalJobLabel">Add Work Term</h4>
                    </div>
                    <div class="modal-body">
                        @Html.HiddenFor(Function(model) model.StuApDetails.AppId)
                        <div Class="row">
                            <div Class="col-md-4">
                                <label>Term</label>
                                @Html.DropDownListFor(Function(model) model.WorkTerm.WorkTermDetails.Term, Model.WorkTerm.TermList, New With {.Class = "form-control"})
                            </div>

                            <div Class="col-md-2">
                                <label>Work Term #</label>
                                @Html.DropDownListFor(Function(model) model.WorkTerm.WorkTermDetails.WorkTermNumber, Model.WorkTerm.WorkTermList, New With {.Class = "form-control"})
                            </div>

                            <div Class="col-md-6">
                                <label>Job</label>
                                @Html.DropDownListFor(Function(model) model.WorkTerm.WorkTermDetails.JobId, Model.WorkTerm.HiredList, New With {.Class = "form-control"})
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button class="btn btn-primary" type="submit">Add</button>
                        <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
                    </div>
                </div>
            </div>
        </div>
    End Using


    @Using Html.BeginForm("EditWorkTerm", "Application", FormMethod.Post, New With {.Id = "form-edit-workterm"})
        @<div id="modalEditWorkTerm" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="myModalLabel">Edit Work Term</h4>
                    </div>
                    <div class="modal-body">
                    </div>
                    <div class="modal-footer">
                        <button class="btn btn-primary" type="submit">Save</button>
                        <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
                    </div>
                </div>
            </div>
        </div>
    End Using

    @<div id="modalDeleteWorkTerm" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="myModalLabel">Delete Work Term</h4>
                </div>
                <div class="modal-body">
                    <p>This work term will be permanently deleted.</p>
                    <p>Do you wish to continue?</p>
                </div>
                <div class="modal-footer">
                    @Using Html.BeginForm("DeleteWorkTerm", "Application", FormMethod.Post, New With {.Id = "form-delete-workterm"})
                        @Html.Hidden("Id", "", New With {.id = "hidWrkId"})
                        @<button class="btn  btn-primary cancel" type="submit" value="Delete" name="action">Yes</button>
                        @<button class="btn btn-default" data-dismiss="modal" aria-hidden="true">No</button>
                    end Using
                </div>
            </div>
        </div>
    </div>

End If


