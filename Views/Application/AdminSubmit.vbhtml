@ModelType JobPostings.Models.ViewModels.AdminApplicationViewModel

@Code
    Layout = "~/Views/Shared/_CoopLayout.vbhtml"
    Html.EnableClientValidation()
    Html.EnableUnobtrusiveJavaScript()
End Code

@section scripts
    <script src="~/Scripts/Site/Application.js"></script>
end section

        @Using Html.BeginForm()
            @Html.ValidationSummary(True)
            @<fieldset>
                <legend>Submit New Co-op Application</legend>

                 <div class="row row-form">
                     <div class="col-sm-4">
                         @Html.LabelFor(Function(model) model.StudentNumber) <span id="StudentName" ></span>
                         @Html.TextBoxFor(Function(model) model.StudentNumber, New With {.class = "form-control"})
                         @Html.ValidationMessageFor(Function(model) model.StudentNumber)
                     </div>

                 </div>
                 
                  <div class="row row-form">
                     <div class="col-md-4">
                         @Html.LabelFor(Function(model) model.PrefPhone)
                         @Html.TextBoxFor(Function(model) model.PrefPhone, New With {.class = "form-control"})
                         
                     </div>
                     <div class="col-md-4">
                         @Html.LabelFor(Function(model) model.PrefEmail)
                         @Html.TextBoxFor(Function(model) model.PrefEmail, New With {.class = "form-control"})
                         
                     </div>
                     <div class="col-md-4">
                         @Html.LabelFor(Function(model) model.Program)
                         @Html.DropDownListFor(Function(model) model.Program, Model.CoopPrograms, "Select a Program", New With {.Class = "form-control"})
                         
                     </div>
                 </div>

                 <div class="row row-form">
                     <div class="col-sm-2 col-sm-offset-10">
                        <button class="btn btn-default pull-right" type="submit">
                             <span Class="glyphicon glyphicon-floppy-disk"></span>Submit 
                         </button>

                     </div>
                 </div>



</fieldset>
        End Using




   


