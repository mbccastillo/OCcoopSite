@ModelType JobPostings.Models.ViewModels.StudentViewModel

@Code
    ViewData("Title") = "Co-op Student"
    Layout = "~/Views/Shared/_CoopStudentLayout.vbhtml"
    Html.EnableClientValidation()
    Html.EnableUnobtrusiveJavaScript()

    Dim openJob As String
    Dim openApps As String

    If Model.ListOfJobs.Count > 0 Then
        openJob = "in"
        openApps = ""
    Else
        openJob = ""
        openApps = "in"
    End If

End Code

@If Not Model.StudentDetails Is Nothing Then

    @<div class="row">
        <div Class="col-md-2 col-md-offset-10">
            @if Model.AllowNewApp Then
                @<a href="@Url.Action("Submit", "Student")" Class="btn btn-default">
                    <span Class="glyphicon glyphicon-star"></span> Submit New Application
                </a>
            End If
        </div>
    </div>


    @<fieldset class="row-form">
        <legend>
            <label>Coop Student Personal Profile — </label>
            @Model.StudentDetails.FirstName @Model.StudentDetails.LastName / @Model.StudentDetails.StudentNumber
        </legend>
    </fieldset>

    @<div class="panel panel-default">
    <div class="panel-heading">
        <a data-toggle="collapse" href="#collapse0"><label>View your Jobs</label></a>
    </div>
    <div id="collapse0" class="panel-body panel-collapse collapse @openJob">
        <div class="alert alert-info">
            Note:To add jobs to your list of applied jobs, go to the details page of the job you have applied for and click 'Yes' on the 'Have you applied?' button.
        </div>
        <table class="table">
            <thead>
                <tr>
                    <th>Jobs you have applied for</th>
                    <th>Employer's Name</th>
                    <th>Activity Date</th>
                    <th class="text-right">Were you hired?</th>
                </tr>
            </thead>
            <tbody>
                @if Model.ListOfJobs.Count > 0 Then
                    @for each item In Model.ListOfJobs
                        @<tr>
                            <td>
                                @Html.ActionLink(item.Job.JobTitle, "Details", "Search", New With {.id = item.JobId}, New With {.target = "_blank"})
                            </td>
                            <td>
                                @item.Job.EmployerDisplay
                            </td>
                            <td>@item.ActivityDate.ToString("MMM dd, yyyy")</td>
                            <td class="text-right">
                                <div class="btn-group" role="group" aria-label="Were you hired?">
                                    <a @If item.Status = JobPostings.Models.Entities.Coop.AppJob.StatusType.Hired Then @: class="btn btn-success"
                                       Else @: class="btn btn-default"
                                       End If
                                       href='@Url.Action("Hire", "Student", New With {.jobId = item.JobId, .appId = item.AppId})'>
                                        Yes
                                    </a>
                                    <a @If item.Status = JobPostings.Models.Entities.Coop.AppJob.StatusType.Applied Then @: class="btn btn-gray"
                                       Else @: class="btn btn-default"
                                       End If
                                       href='@Url.Action("Hire", "Student", New With {.jobId = item.JobId, .appId = item.AppId})'>
                                        No
                                    </a>
                                </div>
                            </td>
                        </tr>
                    Next
                Else
                    @<tr>
                        <td align="center" colspan="3"> You have not applied for any jobs</td>
                    </tr>
                End If
            </tbody>
        </table>
    </div>
</div>
    @<hr />
    @<div class="panel panel-default">
        <div class="panel-heading">
            <a data-toggle="collapse" href="#collapse1"><label>View your Coop Applications and more details</label></a>
        </div>
        <div id="collapse1" class="panel-body panel-collapse collapse @openApps">
            @For Each item In Model.ListOfApplications
                @<div @If (item.AppStatus = JobPostings.Models.Entities.Coop.Application.StatusType.Approved Or item.AppStatus = JobPostings.Models.Entities.Coop.Application.StatusType.CoordinatorApproval) Then @: class="panel panel-success"
                        ElseIf (item.AppStatus = JobPostings.Models.Entities.Coop.Application.StatusType.Denied Or item.AppStatus = JobPostings.Models.Entities.Coop.Application.StatusType.Inactive) Then @: class="panel panel-danger"
                        ElseIf (item.AppStatus = JobPostings.Models.Entities.Coop.Application.StatusType.News Or item.AppStatus = JobPostings.Models.Entities.Coop.Application.StatusType.Pending) Then @: class="panel panel-warning"
                        End If>
                    <div Class="panel-heading">
                        <div class="row ">
                            <div class="col-md-4"><label>Application Date:</label> @item.DateCreated</div>
                            <div class="col-md-4"><label>Status:</label> @item.AppStatus</div>
                            <div class="col-md-4 text-right">
                                <label>Next Step:</label>
                                @if item.AppStatus = 2 Or item.AppStatus = 5 Then
                                    @if item.TestResult = 0 Then
                                        @<a href="@Url.Action("Question", "Quiz")" Class="btn btn-default">
                                            <span Class="glyphicon glyphicon-pencil"></span> Take Quiz
                                        </a>
                                    ElseIf item.TestResult = 2 Then
                                        @<a href="@Url.Action("Question", "Quiz")" Class="btn btn-default">
                                            <span Class="glyphicon glyphicon-repeat"></span> Redo Quiz
                                        </a>
                                    Else
                                        @<a href="@Url.Action("Coop", "Search")" Class="btn btn-default">
                                            <span Class="glyphicon glyphicon-thumbs-up"></span> View Co-op Jobs
                                        </a>
                                    End If
                                ElseIf item.AppStatus = 3 Or item.AppStatus = 4 Then
                                    @<span>Reapply</span>
                                Else
                                    @<span>Waiting for approval</span>
                                End If
                            </div>
                        </div>
                    </div>
                    <div Class="panel-body">
                        @If item.Comments.Where(Function(x) x.Type = "STU").Count > 0 Then
                            @<h5>Comments</h5>
                            @<div Class="scroll" style="max-height:200px; padding:10px">
                                @For Each com In item.Comments.Where(Function(x) x.Type = "STU").OrderByDescending(Function(x) x.ActivityDate)
                                    @<div class="panel panel-default">
                                        <div Class="panel-heading" style="font-weight: bold">
                                            @com.ActivityDate
                                        </div>
                                        <div Class="panel-body">
                                            @com.Comment
                                        </div>
                                    </div>
                                Next
                            </div>
                        Else
                            @<h5>No Comments</h5>
                        End If
                    </div>
                </div>

            Next
        </div>
    </div>


End If
