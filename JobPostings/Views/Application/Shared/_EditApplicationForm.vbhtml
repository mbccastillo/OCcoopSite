@ModelType JobPostings.Models.ViewModels.ApplicationViewModel

@code
    Html.EnableClientValidation()
    Html.EnableUnobtrusiveJavaScript()
    Dim test_res As String = ""
    Dim status_val As String = ""
End Code

@Html.HiddenFor(Function(model) model.StuApDetails.AppId)
<div class="row row-form">
    <div class="col-md-3">
        @Html.LabelFor(Function(model) model.StuApDetails.StudentNumber)
    </div>
    <div class="col-md-3">
        @Model.StuApDetails.StudentNumber
    </div>
</div>
<div class="row row-form">
    <div class="col-md-3">
        @Html.LabelFor(Function(model) model.StuApDetails.FirstName)
    </div>
    <div class="col-md-3">
        @Html.TextBoxFor(Function(model) model.StuApDetails.FirstName, New With {.class = "form-control"})
    </div>
    <div class="col-md-3">
        @Html.LabelFor(Function(model) model.StuApDetails.LastName)
    </div>
    <div class="col-md-3">
        @Html.TextBoxFor(Function(model) model.StuApDetails.LastName, New With {.class = "form-control"})
    </div>
</div>
<div class="row row-form">
    <div class="col-md-3">
        @Html.LabelFor(Function(model) model.StuApDetails.Phone)
    </div>
    <div class="col-md-3">
        @Html.TextBoxFor(Function(model) model.StuApDetails.Phone, New With {.class = "form-control"})
    </div>
    <div class="col-md-3">
        @Html.LabelFor(Function(model) model.StuApDetails.Email)
    </div>
    <div class="col-md-3">
        @Html.TextBoxFor(Function(model) model.StuApDetails.Email, New With {.class = "form-control"})
    </div>
</div>
<div class="row row-form">
    <div class="col-md-3">
        @Html.Label("Co-op Program")
    </div>
    <div class="col-md-3">
        @Html.DropDownListFor(Function(model) model.StuApDetails.ProgramCode, Model.CoopPrograms, New With {.class = "form-control"})
    </div>
    <div class="col-md-3">
        @Html.Label("Has Graduated")
    </div>
    <div class="col-md-3">
        @If Model.StuApDetails.HasGraduated Then
            @("Yes")
        Else
            @("No")
        End If
    </div>
</div>
<div class="row row-form">
    <div class="col-md-3">
        @Html.LabelFor(Function(model) model.StuApDetails.TestResult)
    </div>
    <div class="col-md-3">
        @Model.StuApDetails.TestResult
    </div>
    <div class="col-md-3">
        @Html.LabelFor(Function(model) model.StuApDetails.AppStatus)
    </div>
    <div class="col-md-3">
        @Html.EnumDropDownListFor(Function(model) model.StuApDetails.AppStatus, New With {.class = "form-control"})
    </div>
</div>
<hr />
<div class="row row-form">
    <div class="col-md-3">
        @Html.LabelFor(Function(model) model.StuApDetails.InitialDate)
    </div>
    <div class="col-md-3">
        @Html.EditorFor(Function(model) model.StuApDetails.InitialDate)
        @Html.ValidationMessageFor(Function(model) model.StuApDetails.InitialDate)
    </div>
    <div class="col-md-3">
        @Html.LabelFor(Function(model) model.StuApDetails.ResumeDate)
    </div>
    <div class="col-md-3">
        @Html.EditorFor(Function(model) model.StuApDetails.ResumeDate)
        @Html.ValidationMessageFor(Function(model) model.StuApDetails.ResumeDate)
    </div>
</div>
<div class="row row-form">
    <div class="col-md-3">
        @Html.LabelFor(Function(model) model.StuApDetails.InterviewDate)
    </div>
    <div class="col-md-3">
        @Html.EditorFor(Function(model) model.StuApDetails.InterviewDate)
        @Html.ValidationMessageFor(Function(model) model.StuApDetails.InterviewDate)
    </div>
</div>

@If Model.StuApDetails.Student.ResCode = "I" Then
    @<hr />
    @<h3>International</h3>
    @<hr />
    @<div Class="row row-form">
        <div Class="col-md-3">
            @Html.LabelFor(Function(model) model.StuApDetails.IntlDocDate)
        </div>
        <div Class="col-md-3">
            @Html.EditorFor(Function(model) model.StuApDetails.IntlDocDate)
            @Html.ValidationMessageFor(Function(model) model.StuApDetails.IntlDocDate)
        </div>
        <div Class="col-md-3">
            @Html.LabelFor(Function(model) model.StuApDetails.IntlLetterDate)
        </div>
        <div Class="col-md-3">
            @Html.EditorFor(Function(model) model.StuApDetails.IntlLetterDate)
            @Html.ValidationMessageFor(Function(model) model.StuApDetails.IntlLetterDate)
        </div>
    </div>
    @<div Class="row row-form">
        <div Class="col-md-3">
            @Html.LabelFor(Function(model) model.StuApDetails.IntlApplyPermitDate)
        </div>
        <div Class="col-md-3">
            @Html.EditorFor(Function(model) model.StuApDetails.IntlApplyPermitDate)
            @Html.ValidationMessageFor(Function(model) model.StuApDetails.IntlApplyPermitDate)
        </div>
        <div Class="col-md-3">
            @Html.LabelFor(Function(model) model.StuApDetails.IntlReceivePermitDate)
        </div>
        <div Class="col-md-3">
            @Html.EditorFor(Function(model) model.StuApDetails.IntlReceivePermitDate)
            @Html.ValidationMessageFor(Function(model) model.StuApDetails.IntlReceivePermitDate)
        </div>
    </div>

    @<div Class="row row-form col-md-offset-6">
    <div Class="col-md-6 ">
        @Html.LabelFor(Function(model) model.StuApDetails.IntlExpirePermitDate)
    </div>
    <div Class="col-md-6 ">
        @Html.EditorFor(Function(model) model.StuApDetails.IntlExpirePermitDate)
        @Html.ValidationMessageFor(Function(model) model.StuApDetails.IntlExpirePermitDate)
    </div>

</div>

End If
