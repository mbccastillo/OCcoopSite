@ModelType JobPostings.Models.ViewModels.ManageViewModel

@Code
    ViewData("Title") = "Manage Job Types"
    Layout = "~/Views/Manage/Shared/_ManageLayout.vbhtml"
End Code


@code
    Html.EnableClientValidation()
    Html.EnableUnobtrusiveJavaScript()
End Code

<div id="JobTypNew" class="row row-form">
    <div class="col-md-12">
        @Using Html.BeginForm("JobTypeCreate", "Manage")

        @<fieldset>
            <legend>New Job Type</legend>
            <div class="row row-form">
                <div class="col-md-3">
                    @Html.LabelFor(Function(model) model.JobTypeDetails.Description)
                    @Html.TextBoxFor(Function(model) model.JobTypeDetails.Description, New With {.class = "form-control"})
                    @Html.ValidationMessageFor(Function(model) model.JobTypeDetails.Description)
                </div>
                <div class="col-md-2 col-md-offset-7">
                    <button class="btn btn-default pull-right" type="submit"><span class="glyphicon glyphicon-plus"></span> Add</button>
                </div>
            </div>
        
        </fieldset>
        End Using
    </div>
</div>
<div class="row row-form">
    <div class="col-md-12">
        @Using Html.BeginForm("JobTypeEdit", "Manage")

                @<fieldset>
                    <legend>Edit</legend>
                    <div class="row row-form">
                        <div class="col-md-2 col-lg-offset-10">
                            <button class="btn btn-default pull-right" type="submit"><span class="glyphicon glyphicon-floppy-disk"></span> Save</button>
                        </div>
                    </div>
                    <div id="JobTypeEdit" class="scroll row row-form">
                        <div class="col-md-12">
                            @Html.EditorFor(Function(model) model.JobTypeList, New ViewDataDictionary() From {{"HtmlPrefix", "JobTypeList"}})
                        </div>
                    </div>
                </fieldset>
        End Using
    </div>
</div>



