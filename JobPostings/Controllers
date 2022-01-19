Imports System.Net.Mail
Imports JobPostings
Imports JobPostings.Filters
Imports JobPostings.Models.Entities.Coop
Imports JobPostings.Models.Entities.Jobs
Imports JobPostings.Models.ViewModels
Imports JobPostings.Repositories

Namespace Controllers

    <XprsAuthorizationFilter(Roles:="Admin")>
    Public Class ApplicationController
        Inherits BaseController
        Public Property SearchStuId() As String
        Private stuRepo As StudentRepository
        Private stuAppRepo As ApplicationRepository
        Private jobRepo As JobRepository
        Private termRepo As WorkTermRepository
        Private banRepo As BannerRepository


        Dim REPORTSERVER As String = System.Configuration.ConfigurationManager.AppSettings("ReportServerHTTPS")

        Public Sub New()
            stuRepo = New StudentRepository
            stuAppRepo = New ApplicationRepository
            jobRepo = New JobRepository
            termRepo = New WorkTermRepository
            banRepo = New BannerRepository
        End Sub

        Function Index(ByVal id As String, ByVal prog As String, ByVal lname As String, ByVal status As String) As ActionResult
            Dim vm As New ApplicationListViewModel

            vm.ListOfApplications = stuAppRepo.getAll
            vm.CoopPrograms = New SelectList(stuAppRepo.GetCoopPrograms, "Code", "Description")

            If Not String.IsNullOrEmpty(id) Then
                vm.ListOfApplications = (From i In vm.ListOfApplications
                                         Where i.StudentNumber = id).ToList
            End If


            If String.IsNullOrEmpty(status) Then
                status = 0
            End If

            If status <> "All" Then
                vm.ListOfApplications = (From i In vm.ListOfApplications
                                         Where i.AppStatus = status).ToList

            End If

            If Not String.IsNullOrEmpty(lname) Then
                vm.ListOfApplications = (From i In vm.ListOfApplications
                                         Where i.LastName.ToUpper = lname.ToUpper).ToList

            End If

            If Not String.IsNullOrEmpty(prog) Then
                vm.ListOfApplications = (From i In vm.ListOfApplications
                                         Where i.ProgramCode = prog).ToList

            End If

            vm.ListOfApplications = vm.ListOfApplications.OrderBy(Function(x) x.LastName).ToList

            vm.SearchStuId = id
            vm.SearchProgram = prog
            vm.SearchLastName = lname
            vm.SearchStatus = status

            Return View(vm)

        End Function

        <HttpPost()>
        Function Index(ByVal vm As ApplicationListViewModel) As ActionResult
            If ModelState.IsValid Then
                Return RedirectToAction("Index", New With {.id = vm.SearchStuId, .prog = vm.SearchProgram, .lname = vm.SearchLastName, .status = vm.SearchStatus})
            End If

            Return View(vm)
        End Function



        <HttpPost()>
        Function Delete(ByVal id As Integer)
            Try
                Dim stuAp = stuAppRepo.GetById(id)
                stuAppRepo.Delete(stuAp)

                TempData("Message") = New StatusMessage("alert-success", "Student Application Deleted")

                Return RedirectToAction("Index")
            Catch ex As Exception
                TempData("Message") = New StatusMessage("alert-danger", "Student Application Not Deleted Error: " & ex.Message)
            End Try

            Return RedirectToAction("Edit", New With {.id = id})
        End Function

        Private Sub SetViewModel(ByRef vm As ApplicationViewModel)
            vm.CoopPrograms = New SelectList(stuAppRepo.GetCoopPrograms, "Code", "Description")

            Dim coorRepo As New CoordinatorRepository
            Dim coorlist = New List(Of SelectListItem)()
            coorlist.AddRange(New SelectList(coorRepo.GetAll, "Id", "Name"))
            vm.SelectedCoordinator = User.Identity.Name

            vm.Coordinators = coorlist
            vm.WorkTerm = New WorkTermViewModel
            vm.WorkTerms = termRepo.GetByAppId(vm.StuApDetails.AppId)
            Dim joblist = New List(Of SelectListItem)
            joblist.AddRange(New SelectList(vm.StuApDetails.Jobs.Where(Function(x) x.Status = AppJob.StatusType.Hired).Select(Function(x) New With {.jobId = x.JobId, .JobTitle = x.Job.JobTitle}).ToList(), "JobId", "JobTitle"))

            vm.WorkTerm.HiredList = joblist
            vm.WorkTerm.TermList = banRepo.GetTermsList()


        End Sub


        Function Edit(ByVal id As Integer)
            Dim vm As New ApplicationViewModel

            If Not id = 0 Then
                vm.StuApDetails = stuAppRepo.GetById(id)
                SetViewModel(vm)
            Else
                Return RedirectToAction("Index")
            End If

            Return View(vm)
        End Function

        <HttpPost()>
        Function Edit(ByVal stu As ApplicationViewModel) As ActionResult
            Dim vm As New ApplicationViewModel

            Dim prev_status As Integer
            Dim updated_status As Integer

            If ModelState.IsValid Then
                Dim stuAp = stuAppRepo.GetById(stu.StuApDetails.AppId)
                stuAp.DateUpdated = Date.Now
                prev_status = stuAp.AppStatus

                UpdateModel(stuAp, "StuApDetails")
                stuAppRepo.Update(stuAp)

                vm.StuApDetails = stuAp
                updated_status = vm.StuApDetails.AppStatus

                TempData("Message") = New StatusMessage("alert-success", "Student Application Updated")
                If prev_status <> updated_status And (updated_status = Application.StatusType.Approved Or updated_status = Application.StatusType.CoordinatorApproval) Then
                    'notification sent if status was updated form any status to approved
                    Send_Approval(vm.StuApDetails)
                    If stuAp.HasGraduated Then
                        Dim status As StatusMessage = CType(TempData("Message"), StatusMessage)
                        status.Status &= " <b>This student has graduated from " & stuAp.ProgramCode & ", please verify approval.</b>"
                        TempData("Message") = status
                    End If
                End If

                Return RedirectToAction("Edit", New With {.id = vm.StuApDetails.AppId})
            Else
                vm.AddModelErrors(ModelState.Values)
            End If

            SetViewModel(vm)

            Return View(vm)
        End Function

        <HttpPost()>
        Function AddComment(ByVal vm As ApplicationViewModel) As ActionResult
            vm.AppComment.AppId = vm.StuApDetails.AppId
            vm.AppComment.UserId = User.Identity.Name
            vm.AppComment.ActivityDate = Date.Now

            Try
                stuAppRepo.InsertAppComment(vm.AppComment)

                Return RedirectToAction("Edit", New With {.id = vm.AppComment.AppId})
            Catch ex As Exception
                vm.DisplayMessage = New StatusMessage("alert-danger", "Error: " & ex.Message)
            End Try

            SetViewModel(vm)

            Return View("Edit", vm)
        End Function

        <HttpPost()>
        Function EditComment(ByVal model As AppComment)
            Try
                model.ActivityDate = Date.Now
                model.UserId = User.Identity.Name
                stuAppRepo.UpdateAppComment(model)

                TempData("Message") = New StatusMessage("alert-success", "Comment Edited")
            Catch ex As Exception
                TempData("Message") = New StatusMessage("alert-danger", "Comment Not Edited Error: " & ex.Message)
            End Try

            Return RedirectToAction("Edit", New With {.id = model.AppId})
        End Function

        Function DeleteComment(ByVal id As Integer)
            Dim appId As Integer

            Try
                Dim com = stuAppRepo.GetAppComment(id)
                appId = com.AppId
                stuAppRepo.DeleteAppComment(com)
                TempData("Message") = New StatusMessage("alert-success", "Comment Deleted")
            Catch ex As Exception
                TempData("Message") = New StatusMessage("alert-danger", "Comment Not Deleted Error: " & ex.Message)
            End Try

            Return RedirectToAction("Edit", New With {.id = appId})
        End Function

        Function AddJob(ByVal JobId As Integer, ByVal AppId As Integer, ByVal status As Integer) As ActionResult
            Dim job As Job = jobRepo.GetById(JobId)

            If job IsNot Nothing Then
                Dim appjob As AppJob = stuAppRepo.GetAppJob(AppId, job.JobId)

                If appjob Is Nothing Then
                    appjob = New AppJob
                    appjob.JobId = job.JobId
                    appjob.AppId = AppId
                    appjob.Status = status
                    appjob.ActivityDate = Date.Now

                    stuAppRepo.InsertAppJob(appjob)
                Else
                    stuAppRepo.DeleteAppjob(appjob)
                End If
            End If

            Return RedirectToAction("Edit", New With {.id = AppId})
        End Function

        Function JobHire(ByVal JobId As Integer, ByVal AppId As Integer) As ActionResult
            Dim appJob = stuAppRepo.GetAppJob(AppId, JobId)

            If appJob IsNot Nothing Then
                If appJob.Status = AppJob.StatusType.Applied Then
                    appJob.Status = AppJob.StatusType.Hired
                Else
                    appJob.Status = AppJob.StatusType.Applied
                End If

                stuAppRepo.UpdateAppJob(appJob)
            End If

            Return RedirectToAction("Edit", New With {.id = AppId})
        End Function

        Function DeleteJob(ByVal JobId As Integer, ByVal AppId As Integer) As ActionResult
            Dim appJob = stuAppRepo.GetAppJob(AppId, JobId)
            Dim term = termRepo.GetByAppIdJobId(AppId, JobId)

            If term Is Nothing Then
                Try
                    stuAppRepo.DeleteAppjob(appJob)
                    TempData("Message") = New StatusMessage("alert-success", "Job Deleted")
                Catch ex As Exception
                    TempData("Message") = New StatusMessage("alert-danger", "Job Not Deleted Error: " & ex.Message)
                End Try
            Else
                TempData("Message") = New StatusMessage("alert-danger", "This Job Cannot Be Deleted Because It Has a Work Term Associated. Delete Work Term First. ")
            End If

            Return RedirectToAction("Edit", New With {.id = AppId})
        End Function

        <HttpPost()>
        Function AddWorkTerm(ByVal vm As ApplicationViewModel) As ActionResult

            Try
                vm.WorkTerm.WorkTermDetails.AppId = vm.StuApDetails.AppId
                termRepo.Insert(vm.WorkTerm.WorkTermDetails)

                Return RedirectToAction("Edit", New With {.id = vm.StuApDetails.AppId})
            Catch ex As Exception
                vm.DisplayMessage = New StatusMessage("alert-danger", "Error: " & ex.Message)
            End Try

            vm.StuApDetails = stuAppRepo.GetById(vm.StuApDetails.AppId)
            SetViewModel(vm)

            Return View("Edit", vm)
        End Function

        <HttpPost()>
        Function EditWorkTerm(ByVal vm As WorkTermViewModel)
            Try
                If ModelState.IsValid Then
                    termRepo.Update(vm.WorkTermDetails)
                End If

                TempData("Message") = New StatusMessage("alert-success", "Work Term Edited")
            Catch ex As Exception
                TempData("Message") = New StatusMessage("alert-danger", "Work Term Not Edited Error: " & ex.Message)
            End Try

            Return RedirectToAction("Edit", New With {.id = vm.WorkTermDetails.AppId})
        End Function

        Function DeleteWorkTerm(ByVal id As Integer)
            Dim appId As Integer

            Try
                Dim term = termRepo.GetById(id)
                appId = term.AppId

                termRepo.Delete(term)

                TempData("Message") = New StatusMessage("alert-success", "Work Term Deleted")
            Catch ex As Exception
                TempData("Message") = New StatusMessage("alert-danger", "Work Term Not Deleted Error: " & ex.Message)
            End Try

            Return RedirectToAction("Edit", New With {.id = appId})
        End Function

        <HttpPost()>
        Function GenerateLetter(ByVal AppId As Integer, ByVal SelectedCoordinator As String) As ActionResult
            Dim url = REPORTSERVER & "/mrr?report=POQD5Y6PS2I7OPA3FQUPJBVXQTYIZYLRZLTF3I3EJ3S4UQIY6IME3WPI7BDO356JMJCZPCT7ZXFWI&AppID=" & AppId & "&CoordinatorId=" & SelectedCoordinator

            Return Redirect(url)
        End Function

        Function Inactivate()

            stuAppRepo.InactivateGrads()

            Return RedirectToAction("Index")
        End Function

        Function Reports() As ActionResult
            Dim vm As New ReportViewModel
            vm.ReportStartDate = Date.Today
            vm.ReportEndDate = Date.Today

            Return View(vm)
        End Function

        <HttpPost()>
        Function Reports(ByVal vm As ReportViewModel) As ActionResult

            REPORTSERVER = "https://maps-test.okanagan.bc.ca"

            If ModelState.IsValid Then
                Select Case vm.ReportType
                    Case 1 'CO-OP APPLICATIONS
                        vm.ReportURL = REPORTSERVER & "/mrr?report=DNMI2FSY7OBUZZF3QZ355D5T6ATSSLYXWG6W3JJCHN3TGH733OJU34J4TDAWCEJCYA3KRUIBZH4DY" &
                            "&StartDate=" & vm.ReportStartDate.GetValueOrDefault.ToString("dd-MMM-yyyy") &
                            "&EndDate=" & vm.ReportEndDate.GetValueOrDefault.ToString("dd-MMM-yyyy")
                    Case 2 'STUDENT PLACEMENT REPORT
                        vm.ReportURL = REPORTSERVER & "/argos/index.html?report=KDM5M6EJUSJFRPGXS6ITZAO42A3PJP6RSZKJI6N2TRAV2YXJSBWE2NSSASZT2J2EHMGUFXCQRB4A6&AutoLogOn"
                    Case 3 'STUDENT PLACEMENT REPORT CSV
                        vm.ReportURL = REPORTSERVER & "/argos/index.html?report=EUHWWYSPWTAKNSL2Y3T2BQJFZIA7ZQPG2QHZHKDJGIJJ7I7YKROM2ZK3VPK2ZIOER2YO7RHQZE4FQ&AutoLogOn"
                    Case 4 'STUDENT INTERACTION REPORT
                        vm.ReportURL = REPORTSERVER & "/argos/index.html?report=EIWG3BJN44JO5MOTTUWDXS65XHJQPESHG2PY5VVGHWTN3ZCSQFLMRL7QC2FRDOUDMRTU34UJBM7R2&AutoLogOn"
                    Case 5 'STUDENT INTERACTION REPORT CSV
                        vm.ReportURL = REPORTSERVER & "/argos/index.html?report=UBS2QYBYQGSILNLXWSQUW75I7TQJXI7RITPRD47MBSQUOQTBLUZQ6GQ2OWGCG3KPI4V6MSY3L27Y4&AutoLogOn"
                    Case 6 'INTERNATIONAL STUDENT TRACKING
                        vm.ReportURL = REPORTSERVER & "/argos/index.html?report=HDUN3U43KPKFKKFSYWDTPS5YHYVUJDPKIH25UPSMWWIKLBDOXVCWHET2VIXV2VHEMIJ65KVSFRYN4&AutoLogOn"
                    Case 7 'INTERNATIONAL STUDENT TRACKING CSV
                        vm.ReportURL = REPORTSERVER & "/argos/index.html?report=XL2ZAWQ5U42CHE7GYKAF4RQ6DN7QFZJ4Z2ATMCWA74FSFBIMY7VFHCF76LXDCTWHCUTVDELYJ4RL2&AutoLogOn"
                End Select

            End If

            Return View(vm)
        End Function

        Function AdminSubmit() As ActionResult
            Dim vm As New AdminApplicationViewModel

            vm.CoopPrograms = New SelectList(stuAppRepo.GetCoopPrograms, "Code", "Description")
            Return View(vm)
        End Function

        <HttpPost()>
        Function AdminSubmit(ByVal vm As AdminApplicationViewModel) As ActionResult

            If ModelState.IsValid Then
                Dim stuApp As New Application
                Dim s = stuRepo.GetById(vm.StudentNumber)

                stuApp.StudentNumber = s.StudentNumber
                stuApp.FirstName = s.FirstName
                stuApp.LastName = s.LastName
                stuApp.Phone = vm.PrefPhone
                stuApp.Email = vm.PrefEmail
                stuApp.ProgramCode = vm.Program

                stuAppRepo.Insert(stuApp)

                TempData("Message") = New StatusMessage("alert-success", "Student Co-op Application Submitted")

                Return RedirectToAction("Index")
            Else
                Dim str As String = ""

                For Each modelState As ModelState In ViewData.ModelState.Values
                    For Each e As ModelError In modelState.Errors
                        str &= e.ErrorMessage
                    Next
                Next

                vm.DisplayMessage = New StatusMessage("alert-danger", str)
            End If

            vm.CoopPrograms = New SelectList(stuAppRepo.GetCoopPrograms, "Code", "Description")

            Return View("Submit", vm)

        End Function

        Private Sub Send_Approval(ByRef app As Application)
            Dim student_email As String = app.Email
            Dim server = String.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority)
            'Dim id As String = SearchStuId
            Try
                Dim SmtpServer As New SmtpClient()
                Dim mail As New MailMessage()
                SmtpServer.Host = "smtprelay.okanagan.bc.ca"
                mail = New MailMessage()
                mail.From = New MailAddress("no-reply@okanagan.bc.ca")
                mail.To.Add(student_email)
                mail.CC.Add("coop@okanagan.bc.ca")
                'mail.CC.Add("mcastillo@okanagan.bc.ca")
                mail.IsBodyHtml = True
                mail.Subject = "Co-op Notification for " & app.Name

                mail.Body = "<p>Your co-op application has been <strong>APPROVED.</strong></p>" &
                        "<ul>" &
                        "<li>Please read the <a href='http://www.okanagan.bc.ca/Student_Services/students/coop/Co-op/Student_Handbook.html'>Student Handbook</a> carefully as the ANSWERS to the QUIZ questions are found in there.</li>" &
                        "<li>Complete the co-op quiz found <a href='" & server & Url.Action("Question", "Quiz") & "'>HERE</a>.</li>" &
                        "<li>You need 100% to pass the quiz.</li>" &
                        "<li>You can redo the quiz as many times as needed to pass.</li>" &
                        "<li>Passing the quiz, you will receive access to view the <a href='" & server & Url.Action("Coop", "Search") & "'>Co-op Job Postings</a>.</li>" &
                        "<li>Your co-op coordinator would like to meet with you to discuss the program, your goals and the process. <br />
                             This meeting is mandatory for all co-op students and will take about 30 minutes or longer depending on your questions. <br />
                             If meeting in Kelowna, we are in the Student Services Building in Rm A104 at the Kelowna Campus.  <br />
                             If you attend a different campus we will make arrangements via telephone or in-person at your campus. <br /> 
                             We can help with resume and cover letter review and preparation and also provide mock interviews.<br />
                             Please email your co-op coordinator (based on program you are registered in) <br /> 
                             with 3 dates/times that fit with your schedule.</li>" &
                        "<li>Your co-op coordinator will be in touch to schedule a meeting.</li>" &
                        "<li>Review your personal information below, if not correct, please contact the Co-op department.</li>" &
                        "</ul>" &
                        "<p>If you have any questions please contact <a href='mailto:coop@okanagan.bc.ca'>coop@okanagan.bc.ca</a></p>" &
                        "<p>Student application personal information:</p>" &
                        "<ul>" &
                        "<li>Student Number:   " & app.StudentNumber & "   </li>" &
                        "<li>First Name:  " & app.FirstName & "  </li>" &
                        "<li>Last Name:  " & app.LastName & "  </li>" &
                        "<li>Address: " & app.Student.Address & ", " & app.Student.City & ", " & app.Student.Province & ", " & app.Student.Postalcode & " </li>" &
                        "<li>Email:  " & app.Student.Email & "  </li>" &
                        "<li>Phone:  " & app.Student.Phone & "  </li>" &
                        "<li>Program:  " & app.ProgramCode & "  </li>" &
                        "<li>Domestic/International:  " & app.Student.ResCode & "  </li>" &
                        "<li>Preferred email address:  " & app.Email & "  </li>" &
                        "<li>Preferred phone number: " & app.Phone & " </li> " & "</ul>"

                SmtpServer.Send(mail)
                TempData("Message") = New StatusMessage("alert-success", "Email Notification of approval has been sent")
            Catch ex As Exception
                TempData("Message") = New StatusMessage("alert-danger", "Updated Successfully, however there was an error in sending the email notification. Please email: " & student_email)
            End Try

        End Sub

        Public Function AppCommentDialog(ByVal id As Integer) As PartialViewResult
            Dim model As New AppComment

            model = stuAppRepo.GetAppComment(id)

            Return PartialView("~/Views/Application/Shared/_EditCommentDialog.vbhtml", model)
        End Function

        Public Function WorkTermDialog(ByVal id As Integer) As PartialViewResult
            Dim model As New WorkTermViewModel

            model.WorkTermDetails = termRepo.GetById(id)
            model.TermList = banRepo.GetTermsList()

            Dim jobs = stuAppRepo.GetAppJobsByAppId(model.WorkTermDetails.AppId)
            Dim joblist = New List(Of SelectListItem)
            joblist.AddRange(New SelectList(jobs.Where(Function(x) x.Status = AppJob.StatusType.Hired).Select(Function(x) New With {.jobId = x.JobId, .JobTitle = x.Job.JobTitle}).ToList(), "JobId", "JobTitle"))
            model.HiredList = joblist

            Return PartialView("~/Views/Application/Shared/_EditWorkTermDialog.vbhtml", model)
        End Function

        Public Function CoopJobs(ByVal value As String, ByVal year As String) As JsonResult
            Dim list As List(Of Job) = jobRepo.GetByTypeYear(1, year)

            Dim results = (From i In list
                           Where
                               i.JobTitle.ToUpper.Contains(value.ToUpper) OrElse
                               i.EmployerDisplay.ToUpper.Contains(value.ToUpper) OrElse
                               i.JobCategoriesDisplay.ToUpper.Contains(value.ToUpper)
                           Select New With {
                               .id = i.JobId,
                               .label = i.EmployerDisplay & " | " & i.JobTitle & " | " & i.JobCategoriesDisplay,
                               .value = i.EmployerDisplay & " | " & i.JobTitle & " | " & i.JobCategoriesDisplay}
                               ).ToList

            Return Json(results, JsonRequestBehavior.AllowGet)
        End Function

    End Class

End Namespace
