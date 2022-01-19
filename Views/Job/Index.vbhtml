@Imports JobPostings.Extensions

@ModelType JobPostings.Models.ViewModels.JobListViewModel

@Code   
    ViewData("Title") = "Jobs"
    Layout = "~/Views/Job/Shared/_JobLayout.vbhtml"
End Code

@section scripts
    <script src="~/Scripts/DataTables/jquery.dataTables.min.js"></script>
End Section

@section styles
    <link href="~/Content/DataTables/css/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="~/Content/DataTables/css/dataTables.bootstrap.min.css" rel="stylesheet" />
End Section

<div id="divSearchOptions">
    @Using Html.BeginForm("Index", "Job", FormMethod.Post, New With {.id = "formSearch", .style = "margin:0px"})
        @<fieldset>
            <legend>Job Search</legend>
                <div class="row">
                    <div class="col-md-2">
                        @Html.TextBoxFor(Function(model) model.SelectedYear, New With {.id = "txtYear", .placeholder = "All Years", .class = "form-control"}) 
                    </div>
                    <div class="col-md-2">
                        @Html.DropDownListFor(Function(model) model.SelectedJobType, Model.JobTypeList, New With {.id = "ddlJobType", .class = "form-control"})   
                    </div>
                    <div class="col-md-4">
                        @Html.DropDownListFor(Function(model) model.SelectedCategory, Model.CategoryList, New With {.id = "ddlCategory", .class = "form-control"})
                        @Html.HiddenFor(Function(model) model.SearchValue, New With {.id = "hidSearchValue"})
                    </div>
                    <div id="jobSearch" class="col-md-4">
                        <!--Placeholder-->
                    </div>
                </div>
             </fieldset>
    End Using
</div>
<div style="padding-top: 5px">
    @Html.Partial("~/Views/Job/Shared/_Jobs.vbhtml", Model.JobList)
</div>
