Imports JobPostings.Filters
Imports JobPostings.Models.Entities.Jobs
Imports JobPostings.Models.ViewModels
Imports JobPostings.Repositories

Namespace Controllers

    <XprsAuthorizationFilter(Roles:="Admin")>
    Public Class JobController
        Inherits BaseController

        Private jobRepo As JobRepository
        Private empRepo As EmployerRepository


        Dim REPORTSERVER As String = System.Configuration.ConfigurationManager.AppSettings("ReportServerHTTPS")

        Public Sub New()
            jobRepo = New JobRepository
            empRepo = New EmployerRepository
        End Sub

        Private Sub SetViewModel(ByRef vm As JobViewModel)
            vm.EmploymentTypeList = New SelectList(jobRepo.GetEmploymentTypes, "EmploymentType", "Description")

            Dim emplist = New List(Of SelectListItem)()
            emplist.Add(New SelectListItem() With {.Value = "0", .Text = "-- Select an Employer --"})
            emplist.AddRange(New SelectList(empRepo.GetAll, "EmployerId", "FullDescription"))
            vm.EmployerList = emplist

            vm.HowToApplyList = New SelectList(jobRepo.GetHowToApply, "Value", "Value")

            vm.JobTypesNextCategoryJobNumberList = jobRepo.GetJobTypesNextJobCatogeries(vm.JobDetails.DatePosted.GetValueOrDefault.Year)
        End Sub

        Private Sub setSelections(ByRef vm As JobListViewModel)

            vm.JobTypeList.AddRange(New SelectList(jobRepo.GetJobTypes(), "TypeId", "Description"))

            Dim catlist = New List(Of SelectListItem)()
            catlist.Add(New SelectListItem() With {.Value = "0", .Text = "All Categories"})
            catlist.AddRange(New SelectList(jobRepo.GetCategoriesByType(vm.SelectedJobType), "CategoryId", "Description"))
            vm.CategoryList = catlist

            If vm.SelectedCategory = 0 Then
                If vm.SelectedYear = 0 Then
                    vm.JobList = jobRepo.GetByType(vm.SelectedJobType)
                Else
                    vm.JobList = jobRepo.GetByTypeYear(vm.SelectedJobType, vm.SelectedYear)
                End If
            Else
                If vm.SelectedYear = 0 Then
                    vm.JobList = jobRepo.GetByCategory(vm.SelectedCategory)
                Else
                    vm.JobList = jobRepo.GetByCategoryYear(vm.SelectedCategory, vm.SelectedYear)
                End If
            End If

        End Sub

        '****************************************************************
        '* Actions
        '****************************************************************

        '
        ' /Job/Create

        ' GET: 

        Function Create() As ActionResult
            Dim vm As New JobViewModel

            SetViewModel(vm)

            vm.JobDetails.DatePosted = Date.Today
            vm.JobDetails.Status = Job.StatusType.Published


            Return View(vm)
        End Function

        ' POST: 

        <HttpPost()>
        Function Create(ByVal vm As JobViewModel) As ActionResult
            vm.JobDetails = vm.JobDetails

            If ModelState.IsValid Then
                vm.JobDetails.CreatedBy = User.Identity.Name

                If vm.JobDetails.EmployerId.GetValueOrDefault = 0 Then
                    vm.JobDetails.EmployerId = Nothing
                End If

                If Not String.IsNullOrEmpty(vm.SelectedHowToApply) Then
                    vm.JobDetails.HowtoApply = vm.SelectedHowToApply
                End If

                If Not vm.SelectedCategories Is Nothing Then
                        For Each cat In vm.SelectedCategories
                            Dim id = cat.Split(",")(0)
                            Dim num = cat.Split(",")(1)

                            vm.JobDetails.AddCategory(New JobCategory(id, num))
                        Next

                        jobRepo.Insert(vm.JobDetails)

                        TempData("Message") = New StatusMessage("alert-success", "Job saved")

                        Return RedirectToAction("Edit", New With {.id = vm.JobDetails.JobId})
                    Else
                        vm.DisplayMessage = New StatusMessage("alert-danger", "Category is required")
                    End If
                Else
                    vm.AddModelErrors(ModelState.Values)
            End If

            SetViewModel(vm)

            Return View(vm)
        End Function

        ' POST:

        <HttpPost()>
        Function Delete(ByVal id As Integer) As ActionResult
            Try
                Dim job = jobRepo.GetById(id)
                jobRepo.Delete(job)

                TempData("Message") = New StatusMessage("alert-success", "Job Deleted")

                Return RedirectToAction("Index")
            Catch
                Return RedirectToAction("Edit", New With {.id = id})
            End Try
        End Function

        '
        ' /Job/Edit/5

        ' GET: 

        Function Edit(ByVal id As System.Nullable(Of Integer)) As ActionResult
            Dim vm As New JobViewModel

            If Not id Is Nothing Then
                vm.JobDetails = jobRepo.GetById(id)

                SetViewModel(vm)

                vm.SelectedHowToApply = vm.JobDetails.HowtoApply

                vm.PrintURL = REPORTSERVER & "/mrr?report=NLTAFUGA4X6BLAKQMESI5CFWYOZOQL43GVENYW5TERNHYAFNA6NVHZTKHUOM7AUOY3VWLNBNXUEYE&JobIdText=" & vm.JobDetails.JobId
                vm.PrintAdressURL = REPORTSERVER & "/mrr?report=QPKYCZJF3CE46I7PO6MWUDOD37NDP5S5ISAP4DX4I6IEN4WP5LBGGI24P5HP5FSZB3NDPAGR6YPTM&JobIdText=" & vm.JobDetails.JobId

                'Remove categories already assigned to the job
                For Each c In vm.JobTypesNextCategoryJobNumberList
                    c.NextJobCategories.RemoveAll(Function(X) vm.JobDetails.JobCategories.Any(Function(i) i.CatgeroryId = X.CatgeroryId))
                Next

            Else
                Return RedirectToAction("Index")
            End If

            Return View(vm)
        End Function

        ' POST:

        <HttpPost()>
        Function Edit(ByVal vm As JobViewModel) As ActionResult
            'Try
            If ModelState.IsValid Then
                Dim job = jobRepo.GetById(vm.JobDetails.JobId)

                UpdateModel(job, "JobDetails", Nothing, {"JobCategories", "JobTypes"})

                If Not String.IsNullOrEmpty(vm.SelectedHowToApply) Then
                    job.HowtoApply = vm.SelectedHowToApply
                End If

                Dim deletedCats = job.JobCategories.Where(Function(j) Not vm.JobDetails.JobCategories.Any(Function(x) x.JobCategoryId = j.JobCategoryId)).ToList

                For Each d In deletedCats
                    If d.JobCategoryId <> 0 Then
                        job.JobCategories.Remove(d)
                    End If
                Next

                If Not vm.SelectedCategories Is Nothing Then
                    For Each cat In vm.SelectedCategories
                        Dim id = cat.Split(",")(0)
                        Dim num = cat.Split(",")(1)

                        job.AddCategory(New JobCategory(id, num))
                    Next
                End If

                If job.JobCategories.Count > 0 Then
                    If job.EmployerId.GetValueOrDefault = 0 Then
                        job.EmployerId = Nothing
                    End If

                    jobRepo.Update(job)

                    vm.JobDetails = job

                    TempData("Message") = New StatusMessage("alert-success", "Job saved")


                    Return RedirectToAction("Edit", New With {.id = vm.JobDetails.JobId})
                Else
                    vm.DisplayMessage = New StatusMessage("alert-danger", "Category is required")
                End If
            Else
                vm.AddModelErrors(ModelState.Values)
            End If

            SetViewModel(vm)

            Return View(vm)
            'Catch
            '    Return View(vm)
            'End Try
        End Function


        Function Index() As ActionResult
            Dim vm As New JobListViewModel

            vm.SelectedJobType = 1
            vm.SelectedCategory = 0
            vm.SelectedYear = Date.Today.Year

            setSelections(vm)

            Return View(vm)
        End Function

        <HttpPost()>
        Function Index(ByVal vm As JobListViewModel) As ActionResult

            setSelections(vm)

            Return View(vm)
        End Function

        Function Reports() As ActionResult
            Dim vm As New JobReportViewModel

            Dim catlist = New List(Of SelectListItem)()
            catlist.Add(New SelectListItem() With {.Value = "0", .Text = "-- Choose Category --"})
            catlist.AddRange(New SelectList(jobRepo.GetCategoriesByType(1), "CategoryId", "Description"))
            vm.CategoryList = catlist

            vm.JobTypeList = New SelectList(jobRepo.GetJobTypes, "TypeId", "Description")

            vm.ReportStartDate = Date.Today
            vm.ReportEndDate = Date.Today

            Return View(vm)
        End Function

        ' POST: 

        <HttpPost()>
        Function Reports(ByVal vm As JobReportViewModel) As ActionResult

            Dim catlist = New List(Of SelectListItem)()
            catlist.Add(New SelectListItem() With {.Value = "0", .Text = "-- Choose Category --"})
            catlist.AddRange(New SelectList(jobRepo.GetCategoriesByType(vm.SelectedJobType), "CategoryId", "Description"))
            vm.CategoryList = catlist

            vm.JobTypeList = New SelectList(jobRepo.GetJobTypes, "TypeId", "Description")

            If ModelState.IsValid Then
                vm.ReportType = vm.ReportType
                vm.ReportStartDate = vm.ReportStartDate
                vm.ReportEndDate = vm.ReportEndDate
                vm.SelectedJobType = vm.SelectedJobType
                vm.SelectedCategory = vm.SelectedCategory

                Select Case vm.ReportType
                    Case 1
                        vm.ReportURL = REPORTSERVER & "/mrr?report=JCIHIHQOQR3BGXU6WSYCZ7IRHSUMJ7K6MZ3FOVSUBXEFCQYQXJ5PDDI3TYVRBPUBJ2UR7V3KKIYQQ" &
                                 "&CatText=" & vm.SelectedCategory &
                                 "&FromDateText=" & vm.ReportStartDate.GetValueOrDefault.ToString("dd-MMM-yyyy") &
                                 "&ToDateText=" & vm.ReportEndDate.GetValueOrDefault.ToString("dd-MMM-yyyy")
                    Case 2
                        vm.ReportURL = REPORTSERVER & "/mrr?report=SOHIYSST3NFG5JNEHTK673QPVPOXSVHSJ2JEMXBMU77QCVYB4QW33DNOK2THQNTK5FNCFMT4AJ672" &
                                "&JobTypeText=" & vm.SelectedJobType &
                                "&FromDateText=" & vm.ReportStartDate.GetValueOrDefault.ToString("dd-MMM-yyyy") &
                                "&ToDateText=" & vm.ReportEndDate.GetValueOrDefault.ToString("dd-MMM-yyyy")
                    Case 3
                        vm.ReportURL = REPORTSERVER & "/mrr?report=XBGIMO75Y4E22ARRVJ5RDWNAYSUK46DERQR535ZQIYYJXTMLJQE6QNDJJ46OKL5HA2ILQUDGTJNRK" &
                                "&CatText=" & vm.SelectedCategory &
                                "&FromDateText=" & vm.ReportStartDate.GetValueOrDefault.ToString("dd-MMM-yyyy") &
                                "&ToDateText=" & vm.ReportEndDate.GetValueOrDefault.ToString("dd-MMM-yyyy")
                End Select

            End If

            Return View(vm)
        End Function

        ' POST:

        Function Repost(ByVal id As Integer) As ActionResult
            Dim vm As New JobViewModel

            'Try
            If ModelState.IsValid Then
                Dim job As Job = jobRepo.GetById(id)

                Dim jobcopy As Job = Me.CopyJob(job)

                jobRepo.Insert(jobcopy)

                vm.JobDetails = jobcopy

                TempData("Message") = New StatusMessage("alert-success", "Job Reposted")

                Return RedirectToAction("Edit", New With {.id = vm.JobDetails.JobId})
            Else
                vm.AddModelErrors(ModelState.Values)
            End If

            Return View(vm)
        End Function

        <HttpPost()>
        Public Function Schedule(ByVal vm As JobViewModel) As ActionResult

            Dim job As Job = jobRepo.GetById(vm.JobDetails.JobId)
            Dim jobs As List(Of Job) = Me.ScheduleRepost(job, vm.SelectedFrequency, vm.ScheduleRepostStartDate.GetValueOrDefault, vm.ScheduleRepostEndDate.GetValueOrDefault)

            For Each j In jobs
                jobRepo.Insert(j)
            Next

            TempData("Message") = New StatusMessage("alert-success", jobs.Count & " jobs scheduled till " & vm.ScheduleRepostEndDate.GetValueOrDefault.ToString("MM/dd/yyyy"))

            Return RedirectToAction("Edit", New With {.id = vm.JobDetails.JobId})
        End Function

        '****************************************************************
        '* Function for a creating schedulled reposts of a job
        '****************************************************************
        Function ScheduleRepost(ByVal job As Job, ByVal freq As Integer, ByVal startDate As Date, ByVal endDate As Date) As List(Of Job)
            Dim jobs As New List(Of Job)
            Dim startNum As Integer
            Dim endNum As Integer

            Select Case freq
                Case 1 'oneWeek
                    startNum = 0
                    endNum = (endDate - startDate).Days / 7
                Case 2 'twoWeek
                    startNum = 0
                    endNum = (endDate - startDate).Days / 14
                Case 3 'threeWeek
                    startNum = 0
                    endNum = (endDate - startDate).Days / 21
                Case 4 'fourWeek
                    startNum = 0
                    endNum = (endDate - startDate).Days / 28
                Case 5 'fiveWeek
                    startNum = 0
                    endNum = (endDate - startDate).Days / 35
                Case 6 'sixWeek
                    startNum = 0
                    endNum = (endDate - startDate).Days / 42
                Case Else
                    startNum = 0
                    endNum = 0
            End Select

            For i = startNum To endNum
                Dim d As Date

                Select Case freq
                    Case 1 'oneWeek
                        d = startDate.AddDays(i * 7)
                    Case 2 'twoWeeks
                        d = startDate.AddDays(i * 14)
                    Case 3 'threeWeeks
                        d = startDate.AddDays(i * 21)
                    Case 4 'fourWeeks
                        d = startDate.AddDays(i * 28)
                    Case 5 'fiveWeeks
                        d = startDate.AddDays(i * 35)
                    Case 6 'sixWeeks
                        d = startDate.AddDays(i * 42)
                End Select

                If d <= endDate Then
                    Dim j = Me.CopyJob(job)
                    j.DatePosted = d

                    If freq = 1 Then
                        j.ApplicationDeadline = d.AddDays(6)
                    ElseIf freq = 2 Then
                        j.ApplicationDeadline = d.AddDays(13)
                    End If

                    jobs.Add(j)
                End If
            Next

            Return jobs
        End Function

        '****************************************************************
        '* Function for a posted job copy
        '****************************************************************
        Private Function CopyJob(ByVal job As Job) As Job
            Dim jobcopy As New Job
            jobcopy.DatePosted = Date.Today
            jobcopy.ApplicationDeadline = Date.Today.AddDays(14)
            jobcopy.JobTitle = job.JobTitle & " - REPOST"
            jobcopy.Employer = job.Employer
            jobcopy.EmployerName = job.EmployerName
            jobcopy.EmploymentType = job.EmploymentType
            jobcopy.EmploymentTypeNote = job.EmploymentTypeNote
            jobcopy.Wage = job.Wage
            jobcopy.NumberOfJobs = job.NumberOfJobs
            jobcopy.ActualNumberOfJobs = job.ActualNumberOfJobs
            jobcopy.Location = job.Location
            jobcopy.URL = job.URL
            jobcopy.JobDescription = job.JobDescription
            jobcopy.HowtoApply = job.HowtoApply
            jobcopy.Comments = job.Comments
            'jobcopy.EmployerId = job.EmployerId
            'jobcopy.EmploymentTypeId = job.EmploymentTypeId
            'jobcopy.JobCategories = job.JobCategories
            jobcopy.JobId = 0
            jobcopy.CreatedBy = User.Identity.Name

            For Each c In job.JobCategories
                jobcopy.AddCategory(New JobCategory(c.CatgeroryId, c.JobNumber))
            Next

            Return jobcopy
        End Function

        ' POST:



        '****************************************************************
        '*  Actions - JSON
        '****************************************************************

        <AcceptVerbs(HttpVerbs.Get)>
        Function CategoriesByJobType(ByVal typeId As System.Nullable(Of Integer)) As JsonResult
            Dim catlist = New List(Of SelectListItem)()

            If Not typeId Is Nothing Then
                catlist.Add(New SelectListItem() With {.Value = "0", .Text = "-- Choose Category --"})
                catlist.AddRange(New SelectList(jobRepo.GetCategoriesByType(typeId), "CategoryId", "Description"))
            End If

            Return Json(catlist, JsonRequestBehavior.AllowGet)
        End Function

        '****************************************************************
        '*  Actions - Partial
        '****************************************************************

        Function JobCategoriesByYear(ByVal jobId As System.Nullable(Of Integer), ByVal postYear As System.Nullable(Of Integer)) As ActionResult
            Dim vm As New JobViewModel

            Dim job As Job = Nothing
            Dim year = postYear

            If postYear Is Nothing Then
                year = Date.Today.Year
            End If

            If Not jobId Is Nothing Then
                If jobId <> 0 Then
                    job = jobRepo.GetById(jobId)
                End If
            End If

            vm.JobTypesNextCategoryJobNumberList = jobRepo.GetJobTypesNextJobCatogeries(year)

            If Not job Is Nothing Then
                'Remove categories already assigned to the job
                For Each c In vm.JobTypesNextCategoryJobNumberList
                    c.NextJobCategories.RemoveAll(Function(X) job.JobCategories.Any(Function(i) i.CatgeroryId = X.CatgeroryId))
                Next
            End If

            Return PartialView("~/Views/Job/Shared/_JobCategories.vbhtml", vm.JobTypesNextCategoryJobNumberList)
        End Function

    End Class

    Friend Class Weeks
    End Class
End Namespace