@ModelType JobPostings.Models.ViewModels.PendingPostViewModel

@Imports JobPostings
@Imports Jobpostings.Extensions

@Code
    ViewData("Title") = "Postings"
    Layout = "~/Views/Job/Shared/_JobLayout.vbhtml"

    Dim action = ViewContext.RouteData.GetRequiredString("action")
End Code


@section scripts
    <script src="~/Scripts/DataTables/jquery.dataTables.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#tblPostJobs').dataTable({
                scrollY: height - 50,
                order: [
                    [2, "asc"]
                ],
                pageLength: 100,
                lengthChange: false,
                search: { search: $("#hidSearchValue").val() },
                language: {
                    search: "",
                    emptyTable: "No Job Postings",
                    zeroRecords: "No matching records found"
                },
                columnDefs: [
                    { "targets": 4, "orderable": false }
                ]
            });
            $('div.dataTables_filter input').attr('placeholder', 'Search').addClass("form-control").appendTo("#jobSearch");
        });

    </script>
End Section

@section styles
    <link href="~/Content/DataTables/css/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="~/Content/DataTables/css/responsive.bootstrap.min.css" rel="stylesheet" />
End Section

<fieldset>
    <legend>Postings</legend>
    @Using Html.BeginForm()
        @<fieldset>
            <legend>Job Search</legend>
            <div class="row">
                <div class="col-md-2">
                    @Html.TextBoxFor(Function(model) model.SelectedYear, New With {.id = "txtYear", .placeholder = "All Years", .class = "form-control", .onchange = "this.form.submit()"})
                </div>
            </div>
        </fieldset>
    End Using
    <div class="row row-form">
        <div class="col-md-3 col-md-offset-9">
            <ul class="nav nav-pills float-right">
                <li role="presentation" @If (action = "Pending") Then @Html.AttributeEncode("class=active") End If>@Html.ActionLink("Pending", "Pending", "Post")</li>
                <li role="presentation" @If (action = "Approved") Then @Html.AttributeEncode("class=active") End If>@Html.ActionLink("Approved", "Approved", "Post")</li>
                <li role="presentation" @If (action = "Denied") Then @Html.AttributeEncode("class=active") End If>@Html.ActionLink("Denied", "Denied", "Post")</li>
            </ul>
        </div>
    </div>

    @Html.HiddenFor(Function(model) model.SearchValue, New With {.id = "hidSearchValue"})
    <div>
        @Model.SelectedYear
        <table id="tblPostJobs" class="table table-striped table-hover">
            <thead>
                <tr>
                    <td>Employer Contact</td>
                    <td>Job Title</td>
                    <td>Application Deadline</td>
                    <td>IP</td>
                    <td> </td>
                </tr>
            </thead>
            <tbody>
                @For Each j In Model.JobPostings
                    @<tr>
                        <td>@j.CompanyName</td>
                        <td>@j.JobTitle</td>
                        <td>@j.ApplicationDeadline.GetValueOrDefault.ToMinValString("MMM dd, yyyy")</td>
                        <td>@j.IP</td>
                        <td align="right">
                            @Html.ActionLink("Select", "Details", "Post", New With {.id = j.PostingId}, Nothing)
                            @if j.Status = JobPostings.Models.Entities.Jobs.Posting.StatusType.Approved Then
                                @(" | ") @Html.ActionLink("Approved Job", "Edit", "Job", New With {.id = j.ApprovedJobId}, Nothing)
                            End If
                        </td>
                    </tr>
                Next
            </tbody>
        </table>
    </div>
</fieldset>