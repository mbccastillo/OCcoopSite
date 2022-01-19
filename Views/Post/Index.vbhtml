@ModelType JobPostings.Models.ViewModels.JobPostViewModel

@Code
    ViewData("Title") = "Job Postings"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

@section scripts
    <script>
        $(document).ready(function () {
            $("input.datePicker").datepicker({
                dateFormat: "mm/dd/yy"
            });
        });

        // extend jquery range validator to work for required checkboxes
        var defaultRangeValidator = $.validator.methods.range;
        $.validator.methods.range = function (value, element, param) {
            if (element.type === 'checkbox') {
                // if it's a checkbox return true if it is checked
                return element.checked;
            } else {
                // otherwise run the default validation function
                return defaultRangeValidator.call(this, value, element, param);
            }
        }
    </script>
End Section

<div class="container body-content" style="margin-top:10px">
    @If Not Model.DisplayMessage Is Nothing Then
        @<div class='alert @Model.DisplayMessage.DisplayClass'>
            @Model.DisplayMessage.Status
        </div>
    End If
</div>

@If Model IsNot Nothing Then
    @<div Class="container center">
        @Using Html.BeginForm()
            @Html.AntiForgeryToken()
            @Html.ValidationSummary(True)

            @<fieldset>
        <legend>Submit your information and the job details</legend>
        <div class="control-group">
            <div class="controls">
                <div class="row row-form">
                    <div class="col-sm-12">
                        <label class="control-label">Company Name</label>
                        @Html.TextBoxFor(Function(model) model.JobPost.CompanyName, New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(model) model.JobPost.CompanyName)
                    </div>
                </div>
            </div>
        </div>

        <div class="control-group">
            <div class="controls">
                <div class="row row-form">
                    <div class="col-sm-12">
                        <label class="control-label">Contact Name</label>
                        @Html.TextBoxFor(Function(model) model.JobPost.ContactName, New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(model) model.JobPost.ContactName)
                    </div>
                </div>
            </div>
        </div>

        <div class="control-group">
            <div class="controls">
                <div class="row row-form">
                    <div class="col-sm-12">
                        <label class="control-label">Company Address</label>
                        @Html.TextBoxFor(Function(model) model.JobPost.Address, New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(model) model.JobPost.Address)
                    </div>
                </div>
            </div>
        </div>

        <div class="control-group">
            <div class="controls">
                <div class="row row-form">
                    <div class="col-sm-4">
                        <label class="control-label">City</label>
                        @Html.TextBoxFor(Function(model) model.JobPost.City, New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(model) model.JobPost.City)
                    </div>

                    <div class="col-sm-4">
                        <label class="control-label">Province</label>
                        @Html.TextBoxFor(Function(model) model.JobPost.Province, New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(model) model.JobPost.Province)
                    </div>

                    <div class="col-sm-4">
                        <label class="control-label">Postal Code</label>
                        @Html.TextBoxFor(Function(model) model.JobPost.PostalCode, New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(model) model.JobPost.PostalCode)
                    </div>
                </div>
            </div>
        </div>

        <div class="control-group">
            <div class="controls">
                <div class="row row-form">
                    <div class="col-sm-6">
                        <label class="control-label">Phone</label>
                        @Html.TextBoxFor(Function(model) model.JobPost.PhoneNumber, New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(model) model.JobPost.PhoneNumber)
                    </div>
                    <div class="col-sm-6">
                        <label class="control-label">Fax</label>
                        @Html.TextBoxFor(Function(model) model.JobPost.FaxNumber, New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(model) model.JobPost.FaxNumber)
                    </div>
                </div>
            </div>
        </div>

        <div class="control-group">
            <div class="controls">
                <div class="row row-form">
                    <div class="col-sm-12">
                        <label class="control-label">Email</label>
                        @Html.TextBoxFor(Function(model) model.JobPost.Email, New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(model) model.JobPost.Email)
                    </div>
                </div>
            </div>
        </div>

        <div class="control-group">
            <div class="controls">
                <div class="row row-form">
                    <div class="col-sm-12">
                        <label class="control-label">Website Address</label>
                        @Html.TextBoxFor(Function(model) model.JobPost.Url, New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(model) model.JobPost.Url)
                    </div>
                </div>
            </div>
        </div>

        <div class="control-group">
            <div class="controls">
                <div class="row row-form">
                    <div class="col-sm-12">
                        <label class="control-label">Posting Title</label>
                        @Html.TextBoxFor(Function(model) model.JobPost.JobTitle, New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(model) model.JobPost.JobTitle)
                    </div>
                </div>
            </div>
        </div>

        <div class="control-group">
            <div class="controls">
                <div class="row row-form">
                    <div class="col-sm-12">
                        <label class="control-label">Job Location</label>
                        @Html.TextBoxFor(Function(model) model.JobPost.Location, New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(model) model.JobPost.Location)
                    </div>
                </div>
            </div>
        </div>

        <div class="control-group">
            <div class="controls">
                <div class="row row-form">
                    <div class="col-sm-12">
                        <label class="control-label">Employment Type</label>
                        @Html.TextBoxFor(Function(model) model.JobPost.EmploymentType, New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(model) model.JobPost.EmploymentType)

                    </div>
                </div>
            </div>
        </div>

        <div class="control-group">
            <div class="controls">
                <div class="row row-form">
                    <div class="col-sm-4">
                        <label class="control-label">Numbers of Positions</label>
                        @Html.TextBoxFor(Function(model) model.JobPost.NumberOfJobs, New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(model) model.JobPost.NumberOfJobs)
                    </div>

                    <div class="col-sm-4">
                        <label class="control-label">Hours per Week</label>
                        @Html.TextBoxFor(Function(model) model.JobPost.HoursPerWeek, New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(model) model.JobPost.HoursPerWeek)
                    </div>

                    <div class="col-sm-4">
                        <label class="control-label">Wage</label>
                        @Html.TextBoxFor(Function(model) model.JobPost.Wage, New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(model) model.JobPost.Wage)
                    </div>
                </div>
            </div>
        </div>

        <div class="control-group">
            <div class="controls">
                <div class="row row-form">
                    <div class="col-sm-12">
                        <label class="control-label">Job Description and Qualifications</label>
                        @Html.TextAreaFor(Function(model) model.JobPost.JobDescription, New With {.class = "form-control text-editor", .style = "height:  200px"})
                        @Html.ValidationMessageFor(Function(model) model.JobPost.JobDescription)
                    </div>
                </div>
            </div>
        </div>

        <div class="control-group">
            <div class="controls">
                <div class="row row-form">
                    <div class="col-sm-12">
                        <label class="control-label">How to Apply</label>
                        @Html.TextAreaFor(Function(model) model.JobPost.HowtoApply, New With {.class = "form-control text-editor", .style = "height:  100px"})
                        @Html.ValidationMessageFor(Function(model) model.JobPost.HowtoApply)
                    </div>
                </div>
            </div>
        </div>

        <div class="control-group">
            <div class="controls">
                <div class="row row-form">
                    <div class="col-sm-12">
                        <label class="control-label">Comments</label>
                        @Html.TextAreaFor(Function(model) model.JobPost.Comments, New With {.class = "form-control text-editor", .style = "height:  100px"})
                        @Html.ValidationMessageFor(Function(model) model.JobPost.Comments)
                    </div>
                </div>
            </div>
        </div>
        <div class="control-group">
            <div class="controls">
                <div class="row row-form">
                    <div class="col-sm-4">
                        <label class="control-label">Application Deadline</label>
                        @Html.EditorFor(Function(model) model.JobPost.ApplicationDeadline)
                        @Html.ValidationMessageFor(Function(model) model.JobPost.ApplicationDeadline)
                    </div>
                </div>
            </div>
        </div>
        <br/>
        <div class="row row-form">
            <div class="col-sm-10">
                @Html.CheckBoxFor(Function(model) model.Confirm)       
                <label class="control-label">Confirm that the position meets <a href="http://www.bclaws.ca/civix/document/id/complete/statreg/96244_01">Provincial Labour Laws</a></label>
               
                @Html.ValidationMessageFor(Function(model) model.Confirm)
            </div>
            <div class="col-sm-2">
                <Button Class="btn btn-default pull-right" type="submit">
                    <span Class="glyphicon glyphicon-ok"></span> Submit
                </Button>
            </div>
        </div>
    </fieldset>

        End Using
     </div>
End If
