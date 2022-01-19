@imports JobPostings.Extensions

@ModelType JobPostings.Models.ViewModels.StudentDetailsViewModel

@Code
    ViewData("Title") = "Student Co-op Application"
    Layout = "~/Views/Shared/_CoopLayout.vbhtml"
    Dim test_res As String = ""
End Code


@If Not Model.StudentDetails Is Nothing Then

    @<fieldset>
        <legend>Student Application Details</legend>
        <div class="row row-form">
            <div class="col-md-3">
                @Html.LabelFor(Function(model) model.StudentDetails.StudentNumber)
            </div>
            <div class="col-md-3">
                @Model.StudentDetails.StudentNumber
            </div>
        </div>
        <div class="row row-form">
            <div class="col-md-3">
                @Html.LabelFor(Function(model) model.StudentDetails.FirstName)
            </div>
            <div class="col-md-3">
                @Model.StudentDetails.FirstName
            </div>
            <div class="col-md-3">
                @Html.LabelFor(Function(model) model.StudentDetails.LastName)
            </div>
            <div class="col-md-3">
                @Model.StudentDetails.LastName
            </div>
        </div>
        <div class="row row-form">
            <div class="col-md-3">
                @Html.LabelFor(Function(model) model.StudentDetails.Address)
            </div>
            <div class="col-md-3">
                @Model.StudentDetails.Address
            </div>
            <div class="col-md-3">
                @Html.LabelFor(Function(model) model.StudentDetails.City)
            </div>
            <div class="col-md-3">
                @Model.StudentDetails.City
            </div>
        </div>
        <div class="row row-form">
            <div class="col-md-3">
                @Html.LabelFor(Function(model) model.StudentDetails.Province)
            </div>
            <div class="col-md-3">
                @Model.StudentDetails.Province
            </div>
            <div class="col-md-3">
                @Html.LabelFor(Function(model) model.StudentDetails.Postalcode)
            </div>
            <div class="col-md-3">
                @Model.StudentDetails.Postalcode
            </div>
        </div>
        <div class="row row-form">
            <div class="col-md-3">
                @Html.LabelFor(Function(model) model.StudentDetails.Email)
            </div>
            <div class="col-md-3">
                @Model.StudentDetails.Email
            </div>
            <div class="col-md-3">
                @Html.LabelFor(Function(model) model.StudentDetails.Phone)
            </div>
            <div class="col-md-3">
                @Model.StudentDetails.Phone
            </div>
        </div>
        <div class="row row-form">
            <div class="col-md-3">
                @Html.LabelFor(Function(model) model.StudentDetails.Program)
            </div>
            <div class="col-md-3">
                @Model.StudentDetails.Program
            </div>
            <div class="col-md-3">
                @Html.LabelFor(Function(model) model.StudentDetails.ResCode)
            </div>
            <div class="col-md-3">
                @Model.StudentDetails.ResCode
            </div>
        </div>
        <div class="row row-form">
            <div class="col-md-3">
                @Html.Label("Preferred Email Address")
            </div>
            <div class="col-md-3">
                @Model.PrefEmail
            </div>
            <div Class="col-md-3">
                @Html.Label("Preferred Phone Number")
            </div>
            <div class="col-md-3">
                @Model.PrefPhone
            </div>
        </div>
    </fieldset>

    @*@<fieldset style="margin-top:20px">
        <legend>List of Coop Job Applications</legend>
        <table class="table table-striped table-hover">
            <thead>
                <tr>
                    <th>Employer</th>
                    <th>Job Title</th>
                    <th>Hired Yes/No</th>
                    <th>Comments</th>
                </tr>
            </thead>
            <tbody>
                @for each item In Model.HiredYesNoList
                    @<tr>
                        <td>@item.</td>
                        <td>@item.</td>
                        <td>
                            @if item.Then Then
                                @("Yes")
                            Else
                                @("No")
                            End If
                        </td>
                     </tr>
                Next
            </tbody>
        </table>

    </fieldset>*@


    @<fieldset style="margin-top:20px">
        <legend>List of Coop Applications</legend>
        <table class="table table-striped table-hover">
            <thead>
                <tr>
                    <th>Application Date</th>
                    <th>Co-op Program</th>
                    <th>Has Graduated</th>
                    <th>Status</th>
                    <th>Test Results</th>
                    <th>Edit</th>
                </tr>
            </thead>
            <tbody>
                @for each item In Model.ListOfApplications
                    @<tr>
                        <td>@item.DateCreated.ToString(“MM/dd/yyyy”)</td>
                        <td>@item.ProgramCode</td>
                        <td>
                            @if item.HasGraduated Then
                                @("Yes")
                            Else
                                @("No")
                            End If
                        </td>
                        <td>@item.AppStatus.DisplayName()</td>
                        <td>@item.TestResult</td>
                        <td>@Html.ActionLink("Edit", "Edit", "Application", New With {.id = item.AppId}, Nothing)</td>
                    </tr>
                Next
            </tbody>
        </table>

    </fieldset>
End If
