@Imports Jobpostings.Extensions
@ModelType IEnumerable(Of JobPostings.Models.Entities.Jobs.Job)

<table id="tblJobs" class="table table-striped table-hover">
    <thead>
        <tr>
            <td>Date Posted</td>
            <td>Job Type</td>
            <td>Employer</td>
            <td>Job Title</td>
            <td>Application Deadline</td>
            <td>Categories</td>
            <td>Status</td>
            <td>Edit</td>
        </tr>
    </thead>
    <tbody>
        @For Each j In Model
            @<tr>
                <td>@j.DatePosted.GetValueOrDefault.ToMinValString("MMM dd, yyyy")</td>
                <td>@j.JobTypesDisplay</td>
                <td>@j.EmployerDisplay</td>
                <td>@j.JobTitle</td>
                <td>@j.ApplicationDeadline.GetValueOrDefault.ToMinValString("MMM dd, yyyy")</td>
                <td>@j.JobCategoriesDisplay</td>
                <td>@j.Status</td>
                <td>@Html.ActionLink("Select", "Edit", "Job", New With {.id = j.JobId}, Nothing)</td>
            </tr>
        Next
    </tbody>
</table>