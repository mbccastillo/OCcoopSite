Imports System.Net.Mail
Imports JobPostings.Filters
Imports JobPostings.Models.Entities.Coop
Imports JobPostings.Models.ViewModels
Imports JobPostings.Repositories

Namespace Controllers

    <XprsAuthorizationFilter>
    Public Class QuizController
        Inherits BaseController

        Private questionRepo As QuestionRepository
        Private questionVM As QuestionViewModel
        Private stuAppRepo As ApplicationRepository

        Private quizScore(15) As Integer
        ' Private TotalQuestions As Integer


        '
        ' GET: /Quiz
        Public Sub New()
            questionRepo = New QuestionRepository
            questionVM = New QuestionViewModel

            stuAppRepo = New ApplicationRepository
        End Sub

        Function Question(id As System.Nullable(Of Integer)) As ActionResult
            Dim sturepo As New StudentRepository
            Dim quizVM As New QuizViewModel

            If id Is Nothing Then
                Session("answers") = Nothing

                Return RedirectToAction("Question", New With {.id = 1})
            End If

            Dim stuApp = stuAppRepo.GetApprovedByStuId(User.Identity.Name)

            If stuApp IsNot Nothing Or User.IsInRole("Admin") Then

                If User.IsInRole("Admin") OrElse stuApp.TestResult <> Application.ResultType.Passed Then
                    quizVM.QuestionDetails = questionRepo.GetByQuestionNumber(id)

                    If quizVM.QuestionDetails IsNot Nothing Then
                        quizVM.QuestionDetails.AnswerOptions = questionRepo.GetOptions(quizVM.QuestionDetails.QuestionId)

                        Dim answers As Nullable(Of Integer)() = Session("answers")

                        If answers IsNot Nothing Then
                            quizVM.SelectedAnswer = answers(id - 1)
                        End If

                    Else
                        quizVM.QuestionDetails = Nothing
                        quizVM.DisplayMessage = New StatusMessage("alert-danger", "Question does not exist")
                    End If
                Else
                    quizVM.QuestionDetails = Nothing
                    quizVM.DisplayMessage = New StatusMessage("alert-success", "You have already successfully completed the quiz. You now have access to the <a href='" & Url.Action("Coop", "Search") & "' >Co-op Job Search</a>")
                End If
            Else
                TempData("Message") = New StatusMessage("alert-danger", "Your applicaton has not yet been approved")
                Return RedirectToAction("Applications", "Student")
            End If

            Return View(quizVM)

        End Function

        <HttpPost>
        Function Question(ByVal vm As QuizViewModel) As ActionResult
            If ModelState.IsValid Then
                Dim answers As Nullable(Of Integer)() = Session("answers")

                If answers Is Nothing Then
                    answers = New Nullable(Of Integer)(questionRepo.GetAll.Count - 1) {}
                End If

                answers(vm.QuestionDetails.QuestionNumber - 1) = vm.SelectedAnswer

                Session("answers") = answers

                If answers.Count = vm.QuestionDetails.QuestionNumber Then
                    Return RedirectToAction("Summary")
                End If

                Return RedirectToAction("Question", New With {.id = vm.QuestionDetails.QuestionNumber + 1})
            End If

            Return View(vm)
        End Function

        Function Summary() As ActionResult
            Dim vm As New QuizViewModel
            Dim answers As Nullable(Of Integer)() = Session("answers")

            vm.QuestionList = New List(Of Question)
            vm.TotalQuestions = answers.Count

            For i = 0 To answers.Count - 1
                Dim ques = questionRepo.GetByQuestionNumber(i + 1)

                If ques IsNot Nothing Then
                    ques.AnswerOptions = questionRepo.GetOptions(ques.QuestionId)
                    Dim selectedAns = answers(i)

                    If Not ques.AnswerOptions(selectedAns).CorrectOption = 1 Then
                        'Answer wrong, store to display
                        vm.QuestionList.Add(ques)
                    End If

                End If

                vm.QuizScore = vm.TotalQuestions - vm.QuestionList.Count
            Next

            If Not User.IsInRole("Admin") Then
                If vm.QuizScore = vm.TotalQuestions Then
                    Me.SaveScore(Application.ResultType.Passed)
                Else
                    Me.SaveScore(Application.ResultType.Failed)
                End If
            End If

            Return View(vm)
        End Function

        Private Sub SaveScore(ByVal status As Integer)
            Dim studentnumber As String = User.Identity.Name
            Dim msg As String = ""
            Dim stuAp As Application
            Try
                stuAp = stuAppRepo.GetApprovedByStuId(studentnumber)

                stuAp.DateUpdated = Date.Now
                stuAp.TestResult = status
                stuAppRepo.Update(stuAp)

            Catch ex As Exception
                TempData("Message") = New StatusMessage("alert-danger", "An error occured. Your test result was not saved")
            End Try

        End Sub

    End Class

End Namespace