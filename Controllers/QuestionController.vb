Imports JobPostings.Filters
Imports JobPostings.Models.ViewModels
Imports JobPostings.Repositories

Namespace Controllers

    <XprsAuthorizationFilter(Roles:="Admin")>
    Public Class QuestionController
        Inherits BaseController
        Private questionRepo As QuestionRepository
        Private questionVM As QuestionViewModel


        '
        ' GET: /Quiz
        Public Sub New()
            questionRepo = New QuestionRepository
            questionVM = New QuestionViewModel
        End Sub

        Function Index() As ActionResult
            questionVM.QuestionList = questionRepo.GetAll
            Return View(questionVM)
        End Function

        Function Create() As ActionResult

            Return View(questionVM)

        End Function

        Function Edit(ByVal id As System.Nullable(Of Integer)) As ActionResult
            If Not id Is Nothing Then
                questionVM.QuestionDetails = questionRepo.GetById(id)
                questionVM.QuestionDetails.AnswerOptions = questionRepo.GetOptions(questionVM.QuestionDetails.QuestionId)

            Else
                Return RedirectToAction("Index")
            End If
            Return View(questionVM)
        End Function

        <HttpPost()>
        Function Edit(ByVal q As QuestionViewModel) As ActionResult
            questionVM.QuestionDetails = q.QuestionDetails
            questionVM.Answer = q.Answer
            If ModelState.IsValid Then

                questionRepo.Update(questionVM.QuestionDetails)
                Dim x As Integer = 0

                For Each j In questionVM.QuestionDetails.AnswerOptions
                    If x = questionVM.Answer Then
                        questionVM.QuestionDetails.AnswerOptions.Item(x).CorrectOption = 1
                    End If
                    questionVM.QuestionDetails.AnswerOptions.Item(x).QuestionId = questionVM.QuestionDetails.QuestionId
                    questionRepo.Update(questionVM.QuestionDetails.AnswerOptions.Item(x))

                    x = x + 1
                Next

                TempData("Message") = New StatusMessage("alert-success", "Question has been saved")

                Return RedirectToAction("Edit", New With {.id = questionVM.QuestionDetails.QuestionId})
            End If
            'Catch
            Return View(questionVM)
            'End Try
        End Function

        <HttpPost()>
        Function Index(ByVal q As QuestionViewModel) As ActionResult

            questionVM.QuestionList = questionRepo.GetAll

            Return View(questionVM)
        End Function

        <HttpPost()>
        Function Create(ByVal q As QuestionViewModel) As ActionResult
            questionVM.QuestionDetails = q.QuestionDetails
            questionVM.Answer = q.Answer
            If ModelState.IsValid Then

                questionRepo.Insert(questionVM.QuestionDetails)
                Dim x As Integer = 0

                For Each j In questionVM.QuestionDetails.AnswerOptions

                    If Not String.IsNullOrEmpty(j.OptionText) Then
                        If x = questionVM.Answer Then
                            questionVM.QuestionDetails.AnswerOptions.Item(x).CorrectOption = 1
                        End If
                        questionVM.QuestionDetails.AnswerOptions.Item(x).QuestionId = questionVM.QuestionDetails.QuestionId
                        questionRepo.Insert(questionVM.QuestionDetails.AnswerOptions.Item(x))
                    End If

                    x = x + 1
                Next

                TempData("Message") = New StatusMessage("alert-success", "Question Created")

                Return RedirectToAction("Index")
            End If
            'Catch
            Return View(questionVM)
            'End Try
        End Function

        <HttpPost()>
        Function Delete(ByVal id As Integer) As ActionResult
            'Try
            Dim quest = questionRepo.GetById(id)
            Dim oldnum = quest.QuestionNumber

            Dim opt = questionRepo.GetOptions(id)
            Dim i As Integer = 0
            For Each x In opt
                questionRepo.Delete(opt(i))
                i = i + 1
            Next

            questionRepo.Delete(quest)
            questionRepo.UpdateQuestionNumbers(oldnum)

            TempData("Message") = New StatusMessage("alert-success", "Question Deleted")

            Return RedirectToAction("Index")
            ' Catch
            '    Return RedirectToAction("Edit", New With {.id = id})
            'End Try
        End Function


    End Class

End Namespace