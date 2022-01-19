@ModelType JobPostings.Models.ViewModels.EmployerViewModel

@Code
    ViewData("Title") = "Index"
    Layout = "~/Views/Employer/Shared/_EmployerLayout.vbhtml"
End Code

@section scripts
    <script src="~/Scripts/DataTables/jquery.dataTables.min.js"></script>
End Section

@section styles
    <link href="~/Content/DataTables/css/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="~/Content/DataTables/css/dataTables.bootstrap.min.css" rel="stylesheet" />
End Section

<div id="divSearchOptions">
    @Using Html.BeginForm("Index", "Employer", FormMethod.Post, New With {.id = "formSearch", .style = "margin:0px"})
        @<fieldset>
            <legend>Employer Search</legend>
            <div class="row">
                <div class="col-md-2">
                    @Html.DropDownListFor(Function(model) model.SelectedCity, Model.Cities, New With {.id = "ddlCities", .class = "form-control"})
                    @Html.HiddenFor(Function(model) model.SearchValue, New With {.id = "hidSearchValue"})
             </div>
            </div>
        </fieldset>
    End Using
</div>
<div>
    <table id="tblEmps" class="table table-striped table-hover">
        <thead>
            <tr>
                <th>Employer Name</th>
                <th>Contact Name</th>
                <th>City</th>
                <th>Province</th>
                <th>Phone</th>
                <th>Edit</th>
            </tr>
        </thead>
        <tbody>
             @For Each j In Model.EmployerList
                @<tr>
                    <td>@j.EmployerName</td>
                    <td>@j.ContactName</td>
                    <td>@j.City</td>
                    <td>@j.Province</td>
                    <td>@j.PhoneDisplay</td>
                    <td>@Html.ActionLink("Select", "Edit", "Employer", New With {.id = j.EmployerId}, Nothing)</td>   
                </tr>
             Next
        </tbody>
    </table>
</div>


