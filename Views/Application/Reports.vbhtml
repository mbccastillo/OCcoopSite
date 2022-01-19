@ModelType JobPostings.Models.ViewModels.ReportViewModel

@Code   
    ViewData("Title") = "Reports"
    Layout = "~/Views/Shared/_CoopLayout.vbhtml"
End Code

@section Scripts
    <script>
        $(document).ready(function () {

            $("input.datePicker:not(:disabled)").datepicker({
                dateFormat: "mm/dd/yy",
                onSelect: function (dateText, inst) {
                    $(this).nextAll('.enddate').val(dateText);
                }
            });
        });
    </script>
end section


<div class="row">
    <div class="col-md-3">
        @Using Html.BeginForm()
                @<fieldset>
                <legend>Report Paramaters</legend>
                    <div class="row row-form">
                        <div class="col-md-12">
                            <label>Report Type</label><br />
                            @Html.DropDownListFor(Function(model) model.ReportType, Model.ReportList, New With {.class = "form-control"})
                        </div>
                    </div>
                <div class="row row-form">
                    <div class="col-md-12">
                        <label>Start Date</label><br />
                        @Html.EditorFor(Function(model) model.ReportStartDate)
                        @Html.ValidationMessageFor(Function(model) model.ReportStartDate)
                    </div>
                </div>
                <div class="row row-form">
                    <div class="col-md-12">
                        <label>End Date</label><br />
                        @Html.EditorFor(Function(model) model.ReportEndDate)
                        @Html.ValidationMessageFor(Function(model) model.ReportEndDate)
                    </div>
                </div>
                <div class="row row-form">
                    <div class="col-md-12">
                        <button class="btn btn-primary" type="submit"><span class="glyphicon glyphicon-file"></span> Generate Report</button>
                    </div>
                </div>
            </fieldset>
        End Using
    </div>
    <div class="col-md-9">
        <div id="divReport" style="border: 1px solid #CCCCCC;overflow: hidden;padding: 5px;height: 80vh;">
            <iframe id="frameReport" src='@Model.ReportURL' scrolling="auto" frameborder="0" width="100%" height="100%"></iframe>
        </div>
    </div>
</div>
