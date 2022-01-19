Imports JobPostings.Filters
Imports JobPostings.Models.Entities.Jobs
Imports JobPostings.Models.ViewModels
Imports JobPostings.Repositories

Imports System.Net
Imports System.Collections.Generic
Imports System.Web.Script.Serialization
Imports JobPostings.Models.Entities.Jobs.Posting

Namespace Controllers

    <XprsAuthorizationFilter(Roles:="Admin")>
    Public Class PostController
        Inherits BaseController

        Dim _postRepo As New PostingRepository
        Dim _jobRepo As New JobRepository
        Dim _empRepo As New EmployerRepository
        Public Property gvLocation As Object

        '
        ' GET: /Post
        <AllowAnonymous>
        Function Index() As ActionResult
            Dim vM As New JobPostViewModel
            Return View(vM)
        End Function

        <AllowAnonymous>
        <HttpPost>
        Function Index(ByVal vm As JobPostViewModel) As ActionResult

            If ModelState.IsValid Then
                Dim postRepo As New PostingRepository
                vm.JobPost.IP = Request.UserHostAddress
                vm.JobPost.DateCreated = Date.Today

                postRepo.Insert(vm.JobPost)

                Return View("Confirmation")
            Else
                vm.AddModelErrors(ModelState.Values)
            End If

            Return View(vm)
        End Function

        Function Deny(ByVal id As Integer) As ActionResult

            _postRepo.UpdateStatus(id, Posting.StatusType.Denied)

            TempData("Message") = New StatusMessage("alert-success", "Posting Denied")

            Return RedirectToAction("Pending")
        End Function

        <HttpPost>
        Function Approve(ByVal vm As PendingPostingViewModel) As ActionResult

            If ModelState.IsValid Then
                Dim job As New Job

                job.ActualNumberOfJobs = vm.ActualNumberOfJobs
                job.ApplicationDeadline = vm.PostingDetails.ApplicationDeadline
                job.Comments = vm.PostingDetails.Comments
                job.DateCreated = Date.Today
                job.DatePosted = vm.DatePosted
                job.EmployerName = vm.EmployerName
                job.EmploymentTypeId = vm.EmploymentTypeId
                job.EmploymentTypeNote = vm.EmploymentTypeNote
                job.HowtoApply = vm.PostingDetails.HowtoApply
                job.JobDescription = vm.PostingDetails.JobDescription
                job.JobTitle = vm.PostingDetails.JobTitle
                job.Location = vm.PostingDetails.Location
                job.NumberOfJobs = vm.PostingDetails.NumberOfJobs
                job.URL = vm.PostingDetails.Url
                job.Wage = vm.PostingDetails.Wage
                job.CreatedBy = User.Identity.Name
                job.DateCreated = Date.Today
                job.Status = Job.StatusType.Published

                If vm.EmployerId = 0 Then
                    job.EmployerId = Nothing
                End If

                If Not vm.SelectedCategories Is Nothing Then
                    For Each cat In vm.SelectedCategories
                        Dim id = cat.Split(",")(0)
                        Dim num = cat.Split(",")(1)

                        job.AddCategory(New JobCategory(id, num))
                    Next
                End If

                If Not String.IsNullOrEmpty(vm.SelectedHowToApply) Then
                    job.HowtoApply = vm.SelectedHowToApply
                End If

                _jobRepo.Insert(job)

                vm.PostingDetails.ApprovedJobId = job.JobId
                vm.PostingDetails.Status = Posting.StatusType.Approved

                _postRepo.Update(vm.PostingDetails)

                TempData("Message") = New StatusMessage("alert-success", "Job approved")

                Return RedirectToAction("Pending")
            Else
                vm.AddModelErrors(ModelState.Values)
            End If

            SetViewModel(vm)

            Return View("Details", vm)
        End Function

        Function Pending() As ActionResult
            Dim vm As New PendingPostViewModel

            vm.SelectedYear = Date.Today.Year

            SetPendingViewModel(vm, Posting.StatusType.Pending)

            Return View("List", vm)
        End Function

        <HttpPost>
        Function Pending(vm As PendingPostViewModel) As ActionResult
            SetPendingViewModel(vm, Posting.StatusType.Pending)

            Return View("List", vm)
        End Function


        Function Approved() As ActionResult
            Dim vm As New PendingPostViewModel

            vm.SelectedYear = Date.Today.Year

            SetPendingViewModel(vm, Posting.StatusType.Approved)

            Return View("List", vm)
        End Function

        <HttpPost>
        Function Approved(vm As PendingPostViewModel) As ActionResult
            SetPendingViewModel(vm, Posting.StatusType.Approved)

            Return View("List", vm)
        End Function

        Function Denied() As ActionResult
            Dim vm As New PendingPostViewModel

            vm.SelectedYear = Date.Today.Year

            SetPendingViewModel(vm, Posting.StatusType.Denied)

            Return View("List", vm)
        End Function

        <HttpPost>
        Function Denied(vm As PendingPostViewModel) As ActionResult
            SetPendingViewModel(vm, Posting.StatusType.Denied)

            Return View("List", vm)
        End Function

        Function Details(ByVal id As Integer) As ActionResult
            Dim vm As New PendingPostingViewModel

            vm.PostingDetails = _postRepo.GetById(id)
            vm.DatePosted = Date.Today

            SetViewModel(vm)

            Return View(vm)
        End Function

        Private Sub SetPendingViewModel(ByRef vm As PendingPostViewModel, ByVal status As Posting.StatusType)

            If vm.SelectedYear = 0 Then
                vm.JobPostings = _postRepo.GetByStatus(status)
            Else
                vm.JobPostings = _postRepo.GetByStatusYear(status, vm.SelectedYear)
            End If

        End Sub

        Private Sub SetViewModel(ByRef vm As PendingPostingViewModel)
            vm.EmploymentTypeList = New SelectList(_jobRepo.GetEmploymentTypes, "EmploymentType", "Description")

            vm.JobTypesNextCategoryJobNumberList = _jobRepo.GetJobTypesNextJobCatogeries(vm.DatePosted.GetValueOrDefault.Year)

            Dim emplist = New List(Of SelectListItem)()
            emplist.Add(New SelectListItem() With {.Value = "0", .Text = "-- Select an Employer --"})
            emplist.AddRange(New SelectList(_empRepo.GetAll, "EmployerId", "FullDescription"))
            vm.EmployerList = emplist

            vm.HowToApplyList = New SelectList(_jobRepo.GetHowToApply, "Value", "Value")
        End Sub

        Private Class Local
        End Class
    End Class

End Namespace