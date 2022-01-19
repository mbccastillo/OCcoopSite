@ModelType JobPostings.Models.ViewModels.ApplicationListViewModel

@imports JobPostings.Extensions

@Code
    ViewData("Title") = "Student Co-op Application"
    Layout = "~/Views/Shared/_CoopLayout.vbhtml"
    Dim status_val As String = ""
End Code


@section scripts
    <script src="~/Scripts/DataTables/jquery.dataTables.min.js"></script>
    <script src="~/Scripts/DataTables/dataTables.responsive.min.js"></script>

End Section

@section styles
    <link href="~/Content/DataTables/css/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="~/Content/DataTables/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="~/Content/DataTables/css/responsive.dataTables.min.css" rel="stylesheet" />
    <link href="~/Content/DataTables/css/responsive.bootstrap.min.css" rel="stylesheet" />
End Section


<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Index</title>
</head>
<body>
    @If Not Model.ListOfApplications Is Nothing Then

        @<div class="row">

            <div class="col-md-4 col-md-offset-8 text-right">
                <a href='@Url.Action("Inactivate", "Application")' class="btn btn-default">
                    <span class="glyphicon glyphicon-ban-circle"></span> Inactivate Grads
                </a>
                <a href='@Url.Action("AdminSubmit", "Application")' class="btn btn-default">
                    <span class="glyphicon glyphicon-star"></span> Add New Application
                </a>
            </div>
        </div>


        @Using Html.BeginForm()
            @<fieldset>
                <legend>Student Applications</legend>
                 <div class="row row-form">
                     <div class="col-md-8">
                         <p>Search by student number,last name <strong>or</strong> program</p>
                     </div>
                 </div>
                 <div class="row row-form">
                     <div class="col-md-3">
                         @Html.LabelFor(Function(model) model.SearchStuId)
                         @Html.TextBoxFor(Function(model) model.SearchStuId, New With {.class = "form-control"})
                     </div>
                     <div class="col-md-3">
                         @Html.LabelFor(Function(model) model.SearchLastName)
                         @Html.TextBoxFor(Function(model) model.SearchLastName, New With {.class = "form-control"})
                     </div>
                     <div class="col-md-3">
                         @Html.LabelFor(Function(model) model.SearchProgram)
                         @Html.DropDownListFor(Function(model) model.SearchProgram, Model.CoopPrograms, "All Programs", New With {.Class = "form-control"})
                     </div>
                    <div class="col-md-3">
                         @Html.LabelFor(Function(model) model.SearchStatus)
                         @Html.DropDownListFor(Function(model) model.SearchStatus, Model.AppStatus, New With {.Class = "form-control"})
                    </div>
                 </div>

            <div class="row row-form">
                     <div class="col-md-4 col-md-offset-8">
                         <button class="btn btn-default pull-right" type="submit">
                             <span class="glyphicon glyphicon-search"></span> Search
                         </button>
                     </div>
                 </div>


            </fieldset>
        End Using
    End If

        <div class="row row-form">
            <h4>STUDENT APPLICATIONS RECEIVED</h4>
            <div class="alert alert-info">
                  Search by student number or lastname to see all (including approved/denied) applications for student
            </div>
            <div>
                @Html.Partial("~/Views/Application/Shared/_IndexForm.vbhtml", Model)

            </div>
        </div>
</body>
</html>
