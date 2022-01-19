@ModelType JobPostings.Models.ViewModels.StudentViewModel

@Code
    Layout = "~/Views/Shared/_CoopStudentLayout.vbhtml"
    Html.EnableClientValidation()
    Html.EnableUnobtrusiveJavaScript()
End Code

@section scripts
    <script>
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

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Submit</title>
</head>
<body>

    @If Not Model.StudentDetails Is Nothing Then

        @Using Html.BeginForm()
            @Html.ValidationSummary(True)
            @<fieldset>
                <legend>Submit New Co-op Application</legend>
      
                 @Html.HiddenFor(Function(model) model.StudentDetails.StuApp.AppStatus)
                 @Html.HiddenFor(Function(model) model.StudentDetails.StuApp.TestResult)
                 @Html.HiddenFor(Function(model) model.StudentDetails.StuApp.DateCreated)
                 @Html.HiddenFor(Function(model) model.StudentDetails.StuApp.DateUpdated)
              

                 @Html.HiddenFor(Function(model) model.StudentDetails.FirstName)
                 @Html.HiddenFor(Function(model) model.StudentDetails.LastName)
                 
                 
                 @Html.HiddenFor(Function(model) model.isTrue)
                 <div Class="row row-form">
                     <div>
                         <div Class="col-md-12">
                             <h5><strong>CO-OP AGREEMENT</strong></h5>

                             <p>This agreement outlines key elements of participating in Okanagan College's Co-operative Education program.</p>

                             <p>I hereby make application For Co-op Work Term participation and agree to attend the Co-operative Education Employment Seminar Series. I understand that:</p>
                             <ul>
                                 <li>
                                     The Co-operative Education co-ordinators will make every reasonable effort to obtain suitable program-related Work Term employment opportunities;
                                     however, application for Work Term participation is NOT a guarantee of employment.
                                 </li>
                                 <li>
                                     All Co-op Work Term positions are posted on the Student, Graduate & Co-op Employment Centre website.
                                 </li>
                                 <li>
                                     Co-op student wages are established and paid by the employer.
                                 </li>
                                 <li>
                                     Employment opportunities will NOT be limited to the Okanagan Valley.
                                 </li>
                                 <li>
                                     A Co-operative Education Work Term fee for each four-month work term will be payable during the work term.
                                 </li>
                                 <li>
                                     Students returning to the same employer for successive Work Terms will be required to pay the Co-op component fee for EACH four-month Work Term.
                                 </li>
                                 <li>
                                     Upon students' return to OC, a Work Term report must be submitted to the Student, Graduate & Co-op Employment Centre.
                                 </li>
                                 <li>
                                     Students must satisfy a minimum academic requirement to qualify for Co-op Work Terms. See the OC Calendar or
                                     <a rel="author" target="_blank" href="http://www.okanagan.bc.ca/Page894.aspx">www.okanagan.bc.ca/coop.</a>
                                 </li>
                             </ul>

                             <h5><strong>STUDENT AUTHORIZATION TO RELEASE INFORMATION</strong></h5>
                             <p>
                                 Okanagan College regards the information pertaining to student enrolment or any information contained within the student records as private and confidential
                                 (see OC 'Calendar' for official confidentiality policy). The "Freedom of Information and Protection of Privacy Act" protects the student from the release of
                                 any personal information. Therefore, no information will be released without the student's written authorization.
                             </p>

                             <p>By clicking submit below, you may authorize the OC Student, Graduate & Co-op Employment Centre to release:</p>

                             <ol value="number">
                                 <li>
                                     Mark transcripts to employers for the purpose of obtaining Co-op Work Terms or graduate placement.
                                 </li>
                                 <li>
                                     Work Term Reports to be viewed by other OC students or staff.
                                 </li>
                                 <li>
                                     Photos or video images of me for promotional material in any media format.
                                 </li>
                             </ol>
                             <div Class="row row-form">
                                 <div Class="col-md-12">
                                     <p style="color:red; font-weight:bold">
                                         @Html.Label("Yes, I understand and accept these terms and conditions*: ")
                                         @Html.CheckBoxFor(Function(model) model.AcceptAgreement)
                                         @Html.ValidationMessageFor(Function(model) model.AcceptAgreement)
                                     </p>
                                 </div>
                             </div>

                             <p>
                                 <h5><strong>STUDENT AUTHORIZATION FOR CO-OP FEES</strong></h5>
                                 I hereby authorize Okanagan College to apply the one-time non-refundable <b>Co-operative Education Work Term Application fee of $91.42</b> payable to my Okanagan College
                                 student account. For each registered Work Term, a <b>Co-operative Education Work Term fee of $304.74 for each four-month Work Term</b> will be payable during the Work Term.
                                 <b>NOTE:</b> Fees subject to yearly review and possible change.
                             </p>

                             <div Class="row row-form">
                                 <div Class="col-md-12">
                                     <p style="color:red; font-weight:bold">
                                         @Html.Label("Yes, I understand and accept that I will be charged and will pay for a Co-operative Education Work Term Application Fee and if applicable Co-operative Education Work Term Fees for each registered four-month work term*: ")
                                         @Html.CheckBoxFor(Function(model) model.AcceptAgreement2)
                                         @Html.ValidationMessageFor(Function(model) model.AcceptAgreement2)
                                     </p>
                                 </div>
                             </div>
                             <p><b>*It is required that you have read and agree to the terms and conditions outlined on this online application before your application can be submitted.</b></p>
                             <div class="row row-form">
                                 <div class="col-md-12">
                                     <span style="color:red; font-weight:bold">Instructions will be emailed to this address upon approval</span>
                                 </div>
                             </div>
                             <div class="row row-form">
                                 <div class="col-md-4">
                                     @Html.LabelFor(Function(model) model.StudentDetails.StudentNumber)
                                     @Html.TextBoxFor(Function(model) model.StudentDetails.StudentNumber, New With {.class = "form-control", .readonly = "true"})
                                     @Html.ValidationMessageFor(Function(model) model.StudentDetails.StudentNumber)
                                 </div>
                             </div>
                             <div class="row row-form">
                                 <div class="col-md-4">
                                     @Html.LabelFor(Function(model) model.StudentDetails.StuApp.Phone)
                                     @Html.TextBoxFor(Function(model) model.StudentDetails.StuApp.Phone, New With {.class = "form-control"})
                                     @Html.ValidationMessageFor(Function(model) model.StudentDetails.StuApp.Phone)
                                 </div>
                                 <div class="col-md-4">
                                     @Html.LabelFor(Function(model) model.StudentDetails.StuApp.Email)
                                     @Html.TextBoxFor(Function(model) model.StudentDetails.StuApp.Email, New With {.class = "form-control"})
                                     @Html.ValidationMessageFor(Function(model) model.StudentDetails.StuApp.Email)
                                 </div>
                                 <div class="col-md-4">
                                     @Html.LabelFor(Function(model) model.StudentDetails.StuApp.ProgramCode)
                                     @Html.DropDownListFor(Function(model) model.StudentDetails.StuApp.ProgramCode, Model.CoopPrograms, "Select a Program", New With {.Class = "form-control"})
                                     @Html.ValidationMessageFor(Function(model) model.StudentDetails.StuApp.ProgramCode)
                                 </div>
                             </div>
                         </div>
                     </div>
                 </div>
                 <div class="row row-form">
                     <div class="col-md-12">
                         <button class="btn btn-default pull-right" type="submit">
                             <span class="glyphicon glyphicon-ok"></span> Submit
                         </button>
                     </div>
                 </div>
    

</fieldset>
        End Using

    End If
   
</body>
</html>
