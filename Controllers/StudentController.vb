Imports System.Net.Mail
Imports System.Runtime.InteropServices
Imports JobPostings
Imports JobPostings.Filters
Imports JobPostings.Models.Entities.Coop
Imports JobPostings.Models.ViewModels
Imports JobPostings.Repositories

Namespace Controllers

    <XprsAuthorizationFilter>
    Public Class StudentController
        Inherits BaseController
        Public Property SearchStuId() As String
        Private stuRepo As StudentRepository
        Private stuAppRepo As ApplicationRepository
        Private studentNumber As Object
        'Private hiredYesNo As Object
        'Private h As Integer
        Private hiredYesNo As Object


        Public Sub New()
            stuRepo = New StudentRepository
            stuAppRepo = New ApplicationRepository
        End Sub

        Function Details(ByVal id As String) As ActionResult
            Dim vm As New StudentDetailsViewModel

            If id Is Nothing Then
                Return RedirectToAction("Index", New With {.prog = vm.StudentDetails.Program, .id = ""})
            Else
                vm.ListOfApplications = stuAppRepo.getByStudentId(id)

                If vm.ListOfApplications.Count > 0 Then
                    vm.StudentDetails = stuRepo.GetById(id)
                    vm.PrefPhone = vm.ListOfApplications.Last.Phone
                    vm.PrefEmail = vm.ListOfApplications.Last.Email
                Else
                    vm.StudentDetails = Nothing
                    vm.DisplayMessage = New StatusMessage("alert-danger", "Student does Not have any applications")
                End If

                Return View(vm)
            End If
        End Function

        Function Applications() As ActionResult
            Dim vm As New StudentViewModel
            Dim stuAppRepo = New ApplicationRepository
            Dim stuRepo As New StudentRepository

            vm.StudentDetails = stuRepo.GetById(User.Identity.Name)
            vm.ListOfApplications = stuAppRepo.GetByStuId(User.Identity.Name)
            vm.ListOfApplications = vm.ListOfApplications.OrderByDescending(Function(x) x.DateCreated).ToList
            vm.ListOfJobs = stuAppRepo.GetAppJobsForStudent(User.Identity.Name)

            If vm.ListOfApplications.Where(Function(x) x.AppStatus = Application.StatusType.News Or x.AppStatus = Application.StatusType.Pending).Count = 0 Then 'No New or Pending apps
                vm.AllowNewApp = True
            End If

            If vm.ListOfApplications.Count = 0 Then
                Return RedirectToAction("Submit")
            End If

            Return View(vm)
        End Function

        Function Hire(ByVal JobId As Integer, ByVal AppId As Integer) As ActionResult
            Dim appJob = stuAppRepo.GetAppJob(AppId, JobId)

            If appJob IsNot Nothing Then
                If appJob.Status = AppJob.StatusType.Applied Then
                    appJob.Status = AppJob.StatusType.Hired
                Else
                    appJob.Status = AppJob.StatusType.Applied
                End If

                stuAppRepo.UpdateAppJob(appJob)
            End If

            Return RedirectToAction("Applications")
        End Function

        Function Submit() As ActionResult
            Dim vm As New StudentViewModel
            Dim id As String = User.Identity.Name

            vm.ListOfApplications = stuAppRepo.GetByStuId(User.Identity.Name)

            If vm.ListOfApplications.Where(Function(x) x.AppStatus = Application.StatusType.News Or x.AppStatus = Application.StatusType.Pending).Count = 0 Then 'No New or Pending apps
                vm.StudentDetails = stuRepo.GetById(id)
            Else
                TempData("Message") = New StatusMessage("alert-danger", "Application already submitted")
                Return RedirectToAction("Applications")
            End If

            vm.CoopPrograms = New SelectList(stuAppRepo.GetCoopPrograms, "Code", "Description")
            Return View(vm)
        End Function

        <HttpPost()>
        Function Submit(ByVal vm As StudentViewModel) As ActionResult

            If ModelState.IsValid Then
                vm.StudentDetails.StuApp.StudentNumber = vm.StudentDetails.StudentNumber
                vm.StudentDetails.StuApp.FirstName = vm.StudentDetails.FirstName
                vm.StudentDetails.StuApp.LastName = vm.StudentDetails.LastName

                Dim stuApps = stuAppRepo.GetByStuId(User.Identity.Name)

                If stuApps.Where(Function(x) x.AppStatus = Application.StatusType.News Or x.AppStatus = Application.StatusType.Pending).Count > 0 Then
                    vm.DisplayMessage = New StatusMessage("alert-danger", "Application already submitted")
                Else
                    stuAppRepo.Insert(vm.StudentDetails.StuApp)
                    Send_Confirmation(vm.StudentDetails.StuApp.Email)

                    TempData("Message") = New StatusMessage("alert-success", "Student Co-op Application Submitted")
                    Return RedirectToAction("Applications")
                End If

            Else
                vm.AddModelErrors(ModelState.Values)
            End If

            vm.CoopPrograms = New SelectList(stuAppRepo.GetCoopPrograms, "Code", "Description")

            Return View("Submit", vm)

        End Function

        Private Sub Send_Confirmation(ByVal email As String)
            Dim student_email As String = email
            Try
                Dim SmtpServer As New SmtpClient()
                Dim mail As New MailMessage()
                SmtpServer.Host = "smtprelay.okanagan.bc.ca"
                mail = New MailMessage()
                mail.From = New MailAddress("no-reply@okanagan.bc.ca")
                mail.To.Add(student_email)
                mail.IsBodyHtml = True
                mail.Subject = "Co-op Application Confirmation"
                mail.Body = "<p>Thank you for your interest in the co-op program. We will be in touch within 3-5 business days once your application has been reviewed.</p> " &
                        "<p>If you have any questions please contact <a href='mailto:coop@okanagan.bc.ca'>coop@okanagan.bc.ca</a> </p>" &
                        "<h3>NOTE: International Students Only</h3>" &
                        "<p>Before international students can be considered for the co-op program, we require the following documents to be provided to the co-op office, located in the Student Services Building (Kelowna) – Room A104:</p>" &
                        "<ol>" &
                        "<li>Passport</li>" &
                        "<li>Study Permit</li>" &
                        "<li>Resume</li>" &
                        "</ol>"

                SmtpServer.Send(mail)

            Catch ex As Exception
                TempData("Message") = New StatusMessage("alert-danger", "Student Co-op Application Successfully Submitted, however there was an error in sending the email confirmation to " & email)
            End Try

        End Sub


        ' JSON

        <AcceptVerbs(HttpVerbs.Get)>
        Function GetStudentDetails(ByVal stuId As String) As JsonResult
            Dim stuRepo As New StudentRepository

            Dim stu = stuRepo.GetById(stuId)

            Dim results = New With {.Phone = stu.Phone, .Email = stu.Email, .Program = stu.Program, .Name = stu.FirstName & " " & stu.LastName}

            Return Json(results, JsonRequestBehavior.AllowGet)
        End Function


    End Class




End Namespace

