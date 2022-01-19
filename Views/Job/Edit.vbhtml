@ModelType JobPostings.Models.ViewModels.JobViewModel

@Code
    ViewData("Title") = "Job Edit"
    Layout = "~/Views/Job/Shared/_JobLayout.vbhtml"
End Code

@If Not Model.JobDetails Is Nothing Then
    @Using Html.BeginForm()
        @Html.AntiForgeryToken()
        @Html.ValidationSummary(True)

        @<fieldset>
            <legend>Edit Job</legend>
            <div class="row row-form">
                <div class="col-md-12 text-right">
                    <a href="#modalPrint" role="button" Class="btn btn-default cancel" data-toggle="modal"><span Class="glyphicon glyphicon-print"></span> Print</a>
                    <a Class="btn btn-default" href='@Url.Action("Repost", "Job", New With {.id = Model.JobDetails.JobId})'><span class="glyphicon glyphicon-share-alt"></span> Repost</a>
                    <a href="#modalDeleteConfirm" role="button" Class="btn btn-default cancel" data-toggle="modal"><span Class="glyphicon glyphicon-remove"></span> Delete</a>
                    <Button Class="btn btn-default" type="submit"><span class="glyphicon glyphicon-floppy-disk"></span> Save</Button>
                </div>
            </div>
            <div class="row row-form">
                <div class="col-md-12 text-right">
                    <a href="#modalSchedule" role="button" Class="btn btn-default cancel" data-toggle="modal"><span Class="glyphicon glyphicon-calendar"></span> Schedule Repost</a>
                </div>
            </div>
            <div id="JobDetails" Class="container">
                @Html.Partial("~/Views/Job/Shared/_JobForm.vbhtml", Model)
            </div>
        </fieldset>
    End Using

    @Using Html.BeginForm("Schedule", "Job")
        @<div id="modalSchedule" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="myModalLabel">Schedule Repost</h4>
                    </div>
                    <div class="modal-body">
                        @Html.HiddenFor(Function(model) model.JobDetails.JobId)
                        <div class="row row-form">
                            <div class="col-md-12">
                                <label>Choose Repost Frequency:</label><br />
                                @Html.DropDownListFor(Function(model) model.SelectedFrequency, Model.FrequencyList, New With {.class = "form-control"})
                            </div>
                        </div>
                        <div class="row row-form">
                            <div class="col-md-12">
                                <label>Choose Date to Start Reposting</label><br />
                                @Html.EditorFor(Function(model) model.ScheduleRepostStartDate)
                                @Html.ValidationMessageFor(Function(model) model.ScheduleRepostStartDate)
                            </div>
                        </div>
                        <div class="row row-form">
                            <div class="col-md-12">
                                <label>Choose Date to End Reposting</label><br />
                                @Html.EditorFor(Function(model) model.ScheduleRepostEndDate)
                                @Html.ValidationMessageFor(Function(model) model.ScheduleRepostEndDate)
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button class="btn btn-primary" type="submit"><span class="glyphicon glyphicon-check"></span> Schedule</button>
                    </div>
                </div>
            </div>
        </div>
    End Using

        @<div id="modalPrint" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="myModalLabel">Print Job</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-3">
                                <a href='@Model.PrintAdressURL'>Address</a>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <a href='@Model.PrintURL'>Without Address</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        @<div id="modalDeleteConfirm" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="myModalLabel">Delete Job</h4>
                    </div>
                    <div class="modal-body">
                        <p>This job will be permanently deleted.</p>
                        <p>Do you wish to continue?</p>
                    </div>
                    <div class="modal-footer">
                        @Using Html.BeginForm("Delete", "Job", New With {.id = Model.JobDetails.JobId})
                            @<button class="btn  btn-primary cancel" type="submit" value="Delete" name="action">Yes</button>
                            @<button class="btn btn-default" data-dismiss="modal" aria-hidden="true">No</button>
                        end Using
                    </div>
                </div>
            </div>
        </div>
End If
