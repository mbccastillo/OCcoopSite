Imports JobPostings.Filters
Imports JobPostings.Models.Entities.Coop
Imports JobPostings.Models.Entities.Jobs
Imports JobPostings.Models.ViewModels
Imports JobPostings.Repositories

Namespace Controllers
    Public Class SearchController
        Inherits BaseController

        Private jobRepo As JobRepository
        Private searchVM As SearchViewModel
        Private stuAppRepo As ApplicationRepository

        Private Const COOPTYPE = 1
        Private Const GRADTYPE = 2
        Private Const OCTYPE = 3
        Private Const STUTYPE = 4
        Private Const VOLTYPE = 5

        Public Sub New()
            jobRepo = New JobRepository
            searchVM = New SearchViewModel
            stuAppRepo = New ApplicationRepository

            searchVM.JobTypeList = jobRepo.GetJobTypes.Where(Function(x) x.TypeId <> COOPTYPE).ToList
        End Sub


        '
        ' GET: /Search/Details/5

        Function Details(ByVal id As System.Nullable(Of Integer)) As ActionResult
            If Not id Is Nothing Then
                searchVM.JobDetails = jobRepo.GetById(id)

                If searchVM.JobDetails.JobTypes.Exists(Function(x) x.TypeId = COOPTYPE) Then
                    Return RedirectToAction("CoopDetails", New With {.id = id})
                Else
                    SetSearchViewModel(searchVM)
                End If
            End If

            Return View(searchVM)
        End Function

        <XprsAuthorizationFilter>
        Function CoopDetails(ByVal id As System.Nullable(Of Integer)) As ActionResult
            searchVM.JobDetails = jobRepo.GetById(id)

            If accessGranted() Then
                Dim appJobs = stuAppRepo.GetAppJobsForStudent(User.Identity.Name)
                searchVM.HasApplied = appJobs.Exists(Function(x) x.JobId = id)

                SetSearchViewModel(searchVM)
            Else
                searchVM.DisplayMessage = New StatusMessage("alert-danger", "You must have an approved Co-op application and passed the quiz to view this job")
                searchVM.JobDetails = New Job
            End If

            Return View("Details", searchVM)
        End Function

        '
        ' GET: /Search

        Function Index() As ActionResult
            Return View(searchVM)
        End Function

        '
        ' GET: /Search/Advance

        Function Advance() As ActionResult
            searchVM.EmploymentTypeList = jobRepo.GetEmploymentTypes
            searchVM.CategoryList = jobRepo.GetActiveCategories
            searchVM.JobSearchList = jobRepo.GetActive.Where(Function(x) x.JobTypes.Exists(Function(y) y.TypeId <> COOPTYPE)).ToList

            Return View(searchVM)
        End Function


        <HttpPost()>
        Function Advance(ByVal vm As SearchViewModel) As ActionResult
            searchVM = vm

            If searchVM.IncludePastJobs Then
                searchVM.JobSearchList = jobRepo.GetPastYear(searchVM.PostedJobTypes, searchVM.PostedEmploymentTypes, searchVM.PostedCategories)
            Else
                searchVM.JobSearchList = jobRepo.GetActive(searchVM.PostedJobTypes, searchVM.PostedEmploymentTypes, searchVM.PostedCategories)
            End If

            searchVM.JobSearchList = searchVM.JobSearchList.Where(Function(x) x.JobTypes.Exists(Function(y) y.TypeId <> COOPTYPE)).ToList

            Return PartialView("~/Views/Search/Shared/_Jobs.vbhtml", searchVM)
        End Function


        '
        ' GET: /Search/Coop
        <XprsAuthorizationFilter>
        Function Coop() As ActionResult
            Dim stuRepo As New StudentRepository

            If accessGranted() Then
                searchVM.CategoryList = jobRepo.GetActiveCategories.Where(Function(x) x.TypeId = COOPTYPE).ToList
                searchVM.JobSearchList = jobRepo.GetActive(COOPTYPE)
            Else
                TempData("Message") = New StatusMessage("alert-danger", "You must have an approved Co-op application and passed the quiz to view these jobs")
                Return RedirectToAction("Applications", "Student")
            End If

            Return View("Coop", searchVM)
        End Function

        <XprsAuthorizationFilter>
        <HttpPost()>
        Function Coop(ByVal vm As SearchViewModel) As ActionResult
            If accessGranted() Then
                searchVM = vm

                If searchVM.IncludePastJobs Then
                    searchVM.JobSearchList = jobRepo.GetPastYear(COOPTYPE, searchVM.PostedCategories)
                Else
                    searchVM.JobSearchList = jobRepo.GetActive(COOPTYPE, searchVM.PostedCategories)
                End If


                Return PartialView("~/Views/Search/Shared/_Jobs.vbhtml", searchVM)
            Else
                TempData("Message") = New StatusMessage("alert-danger", "Sorry, you have no access to this page")
                Return RedirectToAction("Applications", "Student")
            End If

        End Function


        '
        ' GET: /Search/Postings/5

        Function Postings(ByVal id As System.Nullable(Of Integer)) As ActionResult
            If Not id Is Nothing Then

                If id = COOPTYPE Then
                    Return RedirectToAction("Coop")
                Else
                    searchVM.JobSearchList = jobRepo.GetActive(id)
                    searchVM.CategoryList = jobRepo.GetActiveCategories.Where(Function(x) x.TypeId = id).ToList

                    Return View("Postings", searchVM)
                End If
            End If

            Return View("Index", searchVM)
        End Function

        <HttpPost()>
        Function Postings(ByVal id As System.Nullable(Of Integer), ByVal vm As SearchViewModel) As ActionResult
            If Not id Is Nothing Then

                If id = COOPTYPE Then
                    Return RedirectToAction("Coop")
                Else
                    searchVM = vm

                    If searchVM.IncludePastJobs Then
                        searchVM.JobSearchList = jobRepo.GetPastYear(id, searchVM.PostedCategories)
                    Else
                        searchVM.JobSearchList = jobRepo.GetActive(id, searchVM.PostedCategories)
                    End If

                    Return PartialView("~/Views/Search/Shared/_Jobs.vbhtml", searchVM)
                End If
            End If

            Return View("Index", searchVM)
        End Function

        Function Apply(ByVal id As System.Nullable(Of Integer)) As ActionResult
            Dim job As Job = jobRepo.GetById(id)

            If job IsNot Nothing Then
                Dim stuapp = stuAppRepo.GetApprovedByStuId(User.Identity.Name)

                If stuapp IsNot Nothing Then
                    Dim appjob As AppJob = stuAppRepo.GetAppJob(stuapp.AppId, job.JobId)

                    If appjob Is Nothing Then
                        appjob = New AppJob
                        appjob.JobId = job.JobId
                        appjob.AppId = stuapp.AppId
                        appjob.Status = AppJob.StatusType.Applied
                        appjob.ActivityDate = Date.Now

                        stuAppRepo.InsertAppJob(appjob)
                    Else
                        stuAppRepo.DeleteAppjob(appjob)
                    End If
                End If

            End If

            Return RedirectToAction("CoopDetails", New With {.id = id})
        End Function

        Private Sub SetSearchViewModel(ByVal vm As SearchViewModel)
            If Not searchVM.JobDetails Is Nothing Then

                If Not vm.HasApplied Or Not User.IsInRole("Admin") Then

                    ' If the student has not applied or is not an admin, check if the jovb is valid to view
                    If searchVM.JobDetails.Status = Job.StatusType.Published Then
                        If searchVM.JobDetails.CalculatedApplicationDeadline.Year < Date.Today.Year Then
                            searchVM.DisplayMessage = New StatusMessage("alert-danger", "This job is no longer available")
                            searchVM.JobDetails = New Job
                        End If
                    Else
                        searchVM.DisplayMessage = New StatusMessage("alert-danger", "This job does not exist")
                        searchVM.JobDetails = New Job
                    End If
                End If
            Else
                searchVM.DisplayMessage = New StatusMessage("alert-danger", "This job does not exist")
                searchVM.JobDetails = New Job
            End If

        End Sub


        Function accessGranted(Optional errors As List(Of String) = Nothing) As Boolean
            Dim stuRepo As New StudentRepository
            Dim b As Boolean = False

            'user is Admin
            If User.IsInRole("Admin") Then
                b = True
            Else
                Dim stuapp = stuAppRepo.GetApprovedByStuId(User.Identity.Name)

                If stuapp IsNot Nothing Then
                    If stuapp.TestResult = Application.ResultType.Passed Then
                        If Not stuapp.HasGraduated Or stuapp.AppStatus = Application.StatusType.CoordinatorApproval Then
                            b = True
                        End If
                    End If
                End If
            End If


            Return b
        End Function

    End Class

End Namespace