@ModelType JobPostings.Models.ViewModels.EmployerViewModel

@code
    Html.EnableClientValidation()
    Html.EnableUnobtrusiveJavaScript()
   
End Code


@Html.HiddenFor(Function(model) model.EmployerDetails.EmployerId)

<div class="row row-form">
    <div class="col-md-12">
        @Html.LabelFor(Function(model) model.EmployerDetails.EmployerName)
        @Html.TextBoxFor(Function(model) model.EmployerDetails.EmployerName, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.EmployerDetails.EmployerName)
    </div>
</div>
<div class="row row-form">
    <div class="col-md-12">
        @Html.LabelFor(Function(model) model.EmployerDetails.ContactName)
        @Html.TextBoxFor(Function(model) model.EmployerDetails.ContactName, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.EmployerDetails.ContactName)
    </div>
</div>
<div class="row row-form">
    <div class="col-md-12">
        @Html.LabelFor(Function(model) model.EmployerDetails.Address, New With {.class = "control-label"})
        @Html.TextBoxFor(Function(model) model.EmployerDetails.Address, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.EmployerDetails.Address)
    </div>
</div>
<div class="row row-form">
   <div class="col-md-4">
       @Html.LabelFor(Function(model) model.EmployerDetails.City)
       @Html.TextBoxFor(Function(model) model.EmployerDetails.City, New With {.class = "form-control"})
       @Html.ValidationMessageFor(Function(model) model.EmployerDetails.City)
   </div>
    <div class="col-md-4">
        @Html.LabelFor(Function(model) model.EmployerDetails.Province)
        @Html.DropDownListFor(Function(model) model.EmployerDetails.Province, Model.Provinces, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.EmployerDetails.Province)
    </div>
    <div class="col-md-4">
        @Html.LabelFor(Function(model) model.EmployerDetails.PostalCode)
        @Html.TextBoxFor(Function(model) model.EmployerDetails.PostalCode, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.EmployerDetails.PostalCode)
    </div>
</div>
<div class="row row-form">
    <div class="col-md-4">
        @Html.LabelFor(Function(model) model.EmployerDetails.Phone)
        @Html.TextBoxFor(Function(model) model.EmployerDetails.Phone, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.EmployerDetails.Phone)
    </div>
    <div class="col-md-4">
        @Html.LabelFor(Function(model) model.EmployerDetails.Fax)
        @Html.TextBoxFor(Function(model) model.EmployerDetails.Fax, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.EmployerDetails.Fax)
    </div>
</div>
<div class="row row-form">
    <div class="col-md-4">
        @Html.LabelFor(Function(model) model.EmployerDetails.Email)
        @Html.TextBoxFor(Function(model) model.EmployerDetails.Email, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.EmployerDetails.Email)
    </div>
    <div class="col-md-8">
        @Html.LabelFor(Function(model) model.EmployerDetails.Website)
        @Html.TextBoxFor(Function(model) model.EmployerDetails.Website, New With {.class = "form-control"})
        @Html.ValidationMessageFor(Function(model) model.EmployerDetails.Website)
    </div>
</div>

