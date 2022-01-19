@ModelType JobPostings.Models.ViewModels.ManageViewModel

@Code
    ViewData("Title") = "Manage Categories"
    Layout = "~/Views/Manage/Shared/_ManageLayout.vbhtml"
End Code


@code
    Html.EnableClientValidation()
    Html.EnableUnobtrusiveJavaScript()
End Code


    <div id="CatNew" class="row">
        <div class="col-md-12">
            @Using Html.BeginForm("CatCreate", "Manage")

            @<fieldset>
                <legend>New Category</legend>
                <div class="row row-form">
                    <div class="col-md-2">
                        @Html.LabelFor(Function(model) model.CategoryDetails.TypeId)
                        @Html.DropDownListFor(Function(model) model.CategoryDetails.TypeId, New SelectList(Model.JobTypeList, "TypeId", "Description"), New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(model) model.CategoryDetails.TypeId)
                    </div>
                    <div class="col-md-3">
                        @Html.LabelFor(Function(model) model.CategoryDetails.Description)
                        @Html.TextBoxFor(Function(model) model.CategoryDetails.Description, New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(model) model.CategoryDetails.Description)
                    </div>
                    <div class="col-md-3">
                        @Html.LabelFor(Function(model) model.CategoryDetails.ShortDescription)
                        @Html.TextBoxFor(Function(model) model.CategoryDetails.ShortDescription, New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(model) model.CategoryDetails.ShortDescription)
                    </div>
                    <div class="col-md-2">
                        @Html.LabelFor(Function(model) model.CategoryDetails.Active)<br />
                        @Html.CheckBoxFor(Function(model) model.CategoryDetails.Active, New With {.checked = "checked"})
                        @Html.ValidationMessageFor(Function(model) model.CategoryDetails.Active)
                    </div>
                    <div class="col-md-2">
                        <button class="btn btn-default pull-right" type="submit"><span class="glyphicon glyphicon-plus"></span> Add</button>
                    </div>
                </div>

            </fieldset>
            End Using
        </div>
    </div>
    <div class="row row-form">
        <div class="col-md-12">
            @Using Html.BeginForm("CatEdit", "Manage")

            @<fieldset>
                <legend>Edit</legend>
                <div class="row">
                    <div class="col-md-2 col-lg-offset-10">
                        <button class="btn btn-default pull-right" type="submit"><span class="glyphicon glyphicon-floppy-disk"></span> Save</button>
                    </div>
                </div>
                <div id="CatEdit" class="scroll row row-form">
                    <div class="col-md-12">
                        @Html.EditorFor(Function(model) model.CategoryEditList, New ViewDataDictionary() From {{"HtmlPrefix", "CategoryEditList"}})
                    </div>
                </div>
            </fieldset>

            End Using
        </div>
    </div>


