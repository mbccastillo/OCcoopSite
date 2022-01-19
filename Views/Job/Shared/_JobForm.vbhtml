@ModelType JobPostings.Models.ViewModels.JobViewModel

@Html.HiddenFor(Function(model) model.JobDetails.JobId)
<div class="row row-form">
    <div class="col-md-2">
        @Html.LabelFor(Function(model) model.JobDetails.Status)<br />
        @Html.EnumDropDownListFor(Function(x) x.JobDetails.Status, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.JobDetails.Status)
    </div>
</div>
<div class="row row-form">
    <div class="col-md-12">
        @For Each j In Model.JobDetails.JobTypes
            @<span class="badge" style="background-color:#3A87AD">@j.Description</span>
        Next
    </div>
</div>
<div id="jobCategories" class="row row-form">
    <div class="col-md-12">
        @Html.EditorFor(Function(model) model.JobDetails.JobCategories)
        @Html.ValidationMessageFor(Function(model) model.SelectedCategories)
        <div id="ddJobCats" class="btn-group">
            <a class="btn dropdown-toggle" data-toggle="dropdown" href="#">
                Add Categories
                <span class="caret"></span>
            </a>
            <ul id="ulJobCategories" class="dropdown-menu scrollable-menu">
                @Html.Partial("~/Views/Job/Shared/_JobCategories.vbhtml", Model.JobTypesNextCategoryJobNumberList)
            </ul>
        </div>
    </div>
</div>
<div class="row row-form">
    <div class="col-md-3">
        @Html.LabelFor(Function(model) model.JobDetails.DatePosted)<br />
        @Html.EditorFor(Function(model) model.JobDetails.DatePosted)
        @Html.ValidationMessageFor(Function(model) model.JobDetails.DatePosted)
    </div>
    <div class="col-md-3">
        @Html.LabelFor(Function(model) model.JobDetails.ApplicationDeadline)<br />
        @Html.EditorFor(Function(model) model.JobDetails.ApplicationDeadline)
        @Html.ValidationMessageFor(Function(model) model.JobDetails.ApplicationDeadline)
    </div>
</div>
<div class="row row-form">
    <div class="col-md-12">
        @Html.LabelFor(Function(model) model.JobDetails.JobTitle)
        @Html.TextBoxFor(Function(model) model.JobDetails.JobTitle, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.JobDetails.JobTitle)
    </div>
</div>

<div Class="row row-form">
    <div Class="col-md-6">
            @Html.LabelFor(Function(model) model.JobDetails.EmployerId)
            @Html.DropDownListFor(Function(model) model.JobDetails.EmployerId, Model.EmployerList, New With {.class = "form-control"})
            @Html.ValidationMessageFor(Function(model) model.JobDetails.EmployerName)
    </div>
    <div Class="col-md-6">
        <label>Or enter an Employer Name </label>
        @Html.TextBoxFor(Function(model) model.JobDetails.EmployerName, New With {.class = "form-control"})
    </div>
</div>


<div Class="row row-form">
    <div Class="col-md-3">
        @Html.LabelFor(Function(model) model.JobDetails.EmploymentTypeId)
        @Html.DropDownListFor(Function(model) model.JobDetails.EmploymentTypeId, Model.EmploymentTypeList, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.JobDetails.EmploymentTypeId)
        <br/>
      
        @Html.LabelFor(Function(model) model.JobDetails.EmploymentTypeNote)
        @Html.TextBoxFor(Function(model) model.JobDetails.EmploymentTypeNote, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.JobDetails.EmploymentTypeNote)  

    </div>
    <div Class="col-md-3">
        @Html.LabelFor(Function(model) model.JobDetails.Wage)
        @Html.TextBoxFor(Function(model) model.JobDetails.Wage, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.JobDetails.Wage)
    </div>
    <div class="col-md-3">
        @Html.LabelFor(Function(model) model.JobDetails.NumberOfJobs)
        @Html.TextBoxFor(Function(model) model.JobDetails.NumberOfJobs, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.JobDetails.NumberOfJobs)
    </div>
    <div class="col-md-3">
        @Html.LabelFor(Function(model) model.JobDetails.ActualNumberOfJobs)
        @Html.TextBoxFor(Function(model) model.JobDetails.ActualNumberOfJobs, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.JobDetails.ActualNumberOfJobs)
    </div>
</div>
<div class="row row-form">
    <div class="col-md-12">
        @Html.LabelFor(Function(model) model.JobDetails.Location)
        @Html.TextBoxFor(Function(model) model.JobDetails.Location, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.JobDetails.Location)
    </div>
</div>
<div class="row row-form">
    <div class="col-md-12">
        @Html.LabelFor(Function(model) model.JobDetails.URL)
        @Html.TextBoxFor(Function(model) model.JobDetails.URL, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.JobDetails.URL)
    </div>
</div>
<div class="row row-form"> 
    <div class="col-md-12">
        @Html.LabelFor(Function(model) model.JobDetails.JobDescription)
        @Html.TextAreaFor(Function(model) model.JobDetails.JobDescription, New With {.class = "form-control text-editor", .style = "height:  200px"})
        @Html.ValidationMessageFor(Function(model) model.JobDetails.JobDescription)
    </div>
</div>
<div class="row row-form">
    <div class="col-md-12">
        @Html.LabelFor(Function(model) model.JobDetails.HowtoApply)
        @Html.DropDownListFor(Function(model) model.SelectedHowToApply, Model.HowToApplyList, "Other", New With {.Class = "form-control", .id = "ddlHowToApply"})
        @Html.ValidationMessageFor(Function(model) model.JobDetails.HowtoApply)
    </div>
</div>
<div class="row row-form">
    <div class="col-md-12">
        @Html.TextAreaFor(Function(model) model.JobDetails.HowtoApply, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.JobDetails.HowtoApply)
    </div>
</div>
<div class="row row-form">
    <div class="col-md-12">
        @Html.LabelFor(Function(model) model.JobDetails.Comments) (Co-op comments will not be displayed)
        @Html.TextBoxFor(Function(model) model.JobDetails.Comments, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.JobDetails.Comments)
    </div>
</div>
