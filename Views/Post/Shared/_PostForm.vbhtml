@ModelType JobPostings.Models.ViewModels.PendingPostingViewModel

@Html.HiddenFor(Function(model) model.PostingDetails.PostingId)
@Html.HiddenFor(Function(model) model.PostingDetails.IP)

@If Model.PostingDetails.Status <> JobPostings.Models.Entities.Jobs.Posting.StatusType.Approved Then
    @<div>
        <h4> Addtional details for approved job </h4>
        <hr />
        <div id="jobCategories" class="row row-form">
            <div class="col-md-2">
                @Html.ValidationMessageFor(Function(model) model.SelectedCategories)
                <div id="ddJobCats" class="btn-group">
                    <a class="btn dropdown-toggle" data-toggle="dropdown" href="#">
                        Add Categories
                        <span class="caret"></span>
                    </a>
                    <ul id="ulJobCategories" Class="dropdown-menu scrollable-menu">
                        @Html.Partial("~/Views/Job/Shared/_JobCategories.vbhtml", Model.JobTypesNextCategoryJobNumberList)
                    </ul>
                </div>
            </div>
        </div>
        <div class="row row-form">
            <div class="col-md-3">
                @Html.LabelFor(Function(model) model.DatePosted)<br />
                @Html.EditorFor(Function(model) model.DatePosted)
                @Html.ValidationMessageFor(Function(model) model.DatePosted)
            </div>
        </div>
        <div Class="row row-form">
            <div Class="col-md-6">
                @Html.LabelFor(Function(model) model.EmployerId)
                @Html.DropDownListFor(Function(model) model.EmployerId, Model.EmployerList, New With {.class = "form-control"})
                @Html.ValidationMessageFor(Function(model) model.EmployerName)
            </div>
            <div Class="col-md-6">
                <Label>Or enter an Employer Name </Label>
                @Html.TextBoxFor(Function(model) model.EmployerName, New With {.class = "form-control"})
            </div>
        </div>
        <div class="row row-form">
            <div Class="col-md-3">
                @Html.LabelFor(Function(model) model.EmploymentTypeId)
                @Html.DropDownListFor(Function(model) model.EmploymentTypeId, Model.EmploymentTypeList, New With {.class = "form-control"})
                @Html.ValidationMessageFor(Function(model) model.EmploymentTypeId)
                <br />

                @Html.LabelFor(Function(model) model.EmploymentTypeNote)
                @Html.TextBoxFor(Function(model) model.EmploymentTypeNote, New With {.class = "form-control"})
                @Html.ValidationMessageFor(Function(model) model.EmploymentTypeNote)

            </div>
            <div class="col-md-3 col-md-offset-3">
                @Html.LabelFor(Function(model) model.ActualNumberOfJobs)
                @Html.TextBoxFor(Function(model) model.ActualNumberOfJobs, New With {.class = "form-control"})
                @Html.ValidationMessageFor(Function(model) model.ActualNumberOfJobs)
            </div>
        </div>
    </div>  End If
<br />
<h4> Contact Information</h4>
<hr />
<div class="row row-form">
    <div class="col-md-6">
        @Html.LabelFor(Function(model) model.PostingDetails.CompanyName)<br />
        @Html.TextBoxFor(Function(model) model.PostingDetails.CompanyName, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.PostingDetails.CompanyName)
    </div>
    <div class="col-md-6">
        @Html.LabelFor(Function(model) model.PostingDetails.ContactName)<br />
        @Html.TextBoxFor(Function(model) model.PostingDetails.ContactName, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.PostingDetails.ContactName)
    </div>
</div>
<div class="row row-form">
    <div class="col-md-12">
        <label class="control-label">Company Address</label>
        @Html.TextBoxFor(Function(model) model.PostingDetails.Address, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.PostingDetails.Address)
    </div>
</div>
<div class="row row-form">
    <div class="col-md-4">
        <label class="control-label">City</label>
        @Html.TextBoxFor(Function(model) model.PostingDetails.City, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.PostingDetails.City)
    </div>
    <div class="col-md-4">
        <label class="control-label">Province</label>
        @Html.TextBoxFor(Function(model) model.PostingDetails.Province, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.PostingDetails.Province)
    </div>
    <div class="col-md-4">
        <label class="control-label">Postal Code</label>
        @Html.TextBoxFor(Function(model) model.PostingDetails.PostalCode, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.PostingDetails.PostalCode)
    </div>
</div>
<div class="row row-form">
    <div class="col-md-4">
        <label class="control-label">Phone</label>
        @Html.TextBoxFor(Function(model) model.PostingDetails.PhoneNumber, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.PostingDetails.PhoneNumber)
    </div>
    <div class="col-md-4">
        <label class="control-label">Fax</label>
        @Html.TextBoxFor(Function(model) model.PostingDetails.FaxNumber, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.PostingDetails.FaxNumber)
    </div>
    <div class="col-md-4">
        <label class="control-label">Email</label>
        @Html.TextBoxFor(Function(model) model.PostingDetails.Email, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.PostingDetails.Email)
    </div>
</div>
<br />
<h4> Details</h4>
<hr />
<div class="row row-form">
    <div class="col-md-3">
        @Html.LabelFor(Function(model) model.PostingDetails.ApplicationDeadline)<br />
        @Html.EditorFor(Function(model) model.PostingDetails.ApplicationDeadline)
        @Html.ValidationMessageFor(Function(model) model.PostingDetails.ApplicationDeadline)
    </div>
</div>
<div class="row row-form">
    <div class="col-md-12">
        @Html.LabelFor(Function(model) model.PostingDetails.JobTitle)
        @Html.TextBoxFor(Function(model) model.PostingDetails.JobTitle, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.PostingDetails.JobTitle)
    </div>
</div>
<div Class="row row-form">
    <div Class="col-md-3">
        @Html.LabelFor(Function(model) model.PostingDetails.EmploymentType)
        @Html.TextBoxFor(Function(model) model.PostingDetails.EmploymentType, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.PostingDetails.EmploymentType)
    </div>
    <div Class="col-md-3">
        @Html.LabelFor(Function(model) model.PostingDetails.Wage)
        @Html.TextBoxFor(Function(model) model.PostingDetails.Wage, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.PostingDetails.Wage)
    </div>
    <div class="col-md-3">
        @Html.LabelFor(Function(model) model.PostingDetails.HoursPerWeek)
        @Html.TextBoxFor(Function(model) model.PostingDetails.HoursPerWeek, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.PostingDetails.HoursPerWeek)
    </div>
    <div class="col-md-3">
        @Html.LabelFor(Function(model) model.PostingDetails.NumberOfJobs)
        @Html.TextBoxFor(Function(model) model.PostingDetails.NumberOfJobs, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.PostingDetails.NumberOfJobs)
    </div>
</div>
<div class="row row-form">
    <div class="col-md-12">
        @Html.LabelFor(Function(model) model.PostingDetails.Location)
        @Html.TextBoxFor(Function(model) model.PostingDetails.Location, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.PostingDetails.Location)
    </div>
</div>
<div class="row row-form">
    <div class="col-md-12">
        @Html.LabelFor(Function(model) model.PostingDetails.Url)
        @Html.TextBoxFor(Function(model) model.PostingDetails.Url, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.PostingDetails.Url)
    </div>
</div>
<div class="row row-form">
    <div class="col-md-12">
        @Html.LabelFor(Function(model) model.PostingDetails.JobDescription)
        @Html.TextAreaFor(Function(model) model.PostingDetails.JobDescription, New With {.class = "form-control text-editor", .style = "height:  200px"})
        @Html.ValidationMessageFor(Function(model) model.PostingDetails.JobDescription)
    </div>
</div>
<div class="row row-form">
    <div class="col-md-12">
        @Html.LabelFor(Function(model) model.PostingDetails.HowtoApply)
        @Html.DropDownListFor(Function(model) model.PostingDetails.HowtoApply, Model.HowToApplyList, "Other", New With {.Class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.PostingDetails.HowtoApply)
    </div>
</div>
<div class="row row-form">
    <div class="col-md-12">
        @Html.TextAreaFor(Function(model) model.PostingDetails.HowtoApply, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.PostingDetails.HowtoApply)
    </div>
</div>
<div class="row row-form">
    <div class="col-md-12">
        @Html.LabelFor(Function(model) model.PostingDetails.Comments) (Co-op comments will not be displayed)
        @Html.TextBoxFor(Function(model) model.PostingDetails.Comments, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.PostingDetails.Comments)
    </div>
</div>
@Html.HiddenFor(Function(model) model.PostingDetails.DateCreated)
