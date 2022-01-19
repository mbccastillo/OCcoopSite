@ModelType JobPostings.Models.ViewModels.ManageViewModel

@Code
    ViewData("Title") = "Manage Employment Types"
    Layout = "~/Views/Manage/Shared/_ManageLayout.vbhtml"
End Code


@code
    Html.EnableClientValidation()
    Html.EnableUnobtrusiveJavaScript()
End Code


<div id="EmpTypeNew" class="row row-form">
    <div class="col-md-12">
        @Using Html.BeginForm("EmpTypeCreate", "Manage")

        @<fieldset>
            <legend>New Employment Type</legend>
            <div class="row row-form">
                <div class="col-md-3">
                    @Html.LabelFor(Function(model) model.EmploymentTypeDetails.Description)
                    @Html.TextBoxFor(Function(model) model.EmploymentTypeDetails.Description, New With {.class = "form-control"})
                    @Html.ValidationMessageFor(Function(model) model.EmploymentTypeDetails.Description)
                </div>
                <div Class="col-md-2 col-md-offset-7">
                    <Button Class="btn btn-default pull-right" type="submit"><span class="glyphicon glyphicon-plus"></span> Add</button>
                </div>
            </div>
        </fieldset>
        End Using
    </div>
</div>
<div Class="row row-form">
    <div Class="col-md-12">
        @Using Html.BeginForm("EmpTypeEdit", "Manage")

            @<fieldset>
                <legend> Edit</legend>
                <div Class="row">
                    <div Class="col-md-2 col-lg-offset-10">
                        <Button Class="btn btn-default pull-right" type="submit"><span class="glyphicon glyphicon-floppy-disk"></span> Save</button>
                    </div>
                </div>
                <div id = "EmpTypeEdit" Class="scroll row row-form">
                    <div Class="col-md-12">
                        @Html.EditorFor(Function(model) model.EmploymentTypeList, New ViewDataDictionary() From {{"HtmlPrefix", "EmploymentTypeList"}})
                    </div>
                </div>
            </fieldset>

        End Using
    </div>
</div>



