
Imports JobPostings.Filters
Imports JobPostings.Models.ViewModels
Imports JobPostings.Repositories

Namespace Controllers

    <XprsAuthorizationFilter(Roles:="Admin")>
    Public Class ManageController
        Inherits BaseController

        Private jobRepo As JobRepository
        Private manageVM As ManageViewModel

        Public Sub New()
            jobRepo = New JobRepository
            manageVM = New ManageViewModel

            manageVM.JobTypeList = jobRepo.GetJobTypes
            manageVM.CategoryList = jobRepo.GetCategories.OrderBy(Function(x) x.TypeId).ToList
            manageVM.EmploymentTypeList = jobRepo.GetEmploymentTypes
        End Sub


        '
        ' GET: /Manage

        Function Index() As ActionResult
            Return RedirectToAction("EmpTypes")
        End Function

        '
        ' /Job/Category/

        Function Categories() As ActionResult

            For Each c In manageVM.CategoryList
                manageVM.CategoryEditList.Add(New ManageViewModel.CategoryEdit(c, manageVM.JobTypeList))
            Next

            Return View(manageVM)
        End Function

        <HttpPost()>
        Function CatCreate(ByVal vm As ManageViewModel) As ActionResult
            Try
                manageVM.CategoryDetails = vm.CategoryDetails

                If ModelState.IsValid Then

                    jobRepo.InsertCategory(manageVM.CategoryDetails)

                    TempData("Message") = New StatusMessage("alert-success", "Category Created")

                    Return RedirectToAction("Categories")
                End If
                Return View("Categories", manageVM)
            Catch
                Return View("Categories", manageVM)
            End Try
        End Function

        <HttpPost()>
        Function CatEdit(ByVal vm As ManageViewModel) As ActionResult
            Try
                TryUpdateModel(manageVM.CategoryEditList, "CategoryEditList")

                If ModelState.IsValid Then

                    For Each c In vm.CategoryEditList 'emptypes
                        jobRepo.UpdateCategory(c.CategoryDetails)
                    Next

                    TempData("Message") = New StatusMessage("alert-success", "Categories Saved ")

                    Return RedirectToAction("Categories")
                End If

                Return View("Categories", manageVM)
            Catch
                Return View("Categories", manageVM)
            End Try
        End Function

        '
        ' /Job/EmpType/

        Function EmpTypes() As ActionResult
            Return View(manageVM)
        End Function

        <HttpPost()>
        Function EmpTypeCreate(ByVal vm As ManageViewModel) As ActionResult
            Try
                manageVM.EmploymentTypeDetails = vm.EmploymentTypeDetails

                If ModelState.IsValid Then

                    jobRepo.InsertEmploymentType(manageVM.EmploymentTypeDetails)

                    TempData("Message") = New StatusMessage("alert-success", "Employment Type Created")

                    Return RedirectToAction("EmpTypes")
                End If
                Return View("EmpTypes", manageVM)
            Catch
                Return View("EmpTypes", manageVM)
            End Try
        End Function

        <HttpPost()>
        Function EmpTypeEdit(ByVal vm As ManageViewModel) As ActionResult
            Try
                TryUpdateModel(manageVM.EmploymentTypeList, "EmploymentTypeList")

                If ModelState.IsValid Then

                    For Each e In vm.EmploymentTypeList 'emptypes
                        jobRepo.UpdateEmploymentType(e)
                    Next

                    TempData("Message") = New StatusMessage("alert-success", "Employment Types Saved ")

                    Return RedirectToAction("EmpTypes")
                End If

                Return View("EmpTypes", manageVM)
            Catch
                Return View("EmpTypes", manageVM)
            End Try
        End Function

        '
        ' /Job/JobType/

        Function JobTypes() As ActionResult
            Return View(manageVM)
        End Function

        <HttpPost()>
        Function JobTypeCreate(ByVal vm As ManageViewModel) As ActionResult
            Try
                manageVM.JobTypeDetails = vm.JobTypeDetails

                If ModelState.IsValid Then

                    jobRepo.InsertJobType(manageVM.JobTypeDetails)

                    TempData("Message") = New StatusMessage("alert-success", "Job Type Created")

                    Return RedirectToAction("JobType")
                End If
                Return View("JobTypes", manageVM)
            Catch
                Return View("JobTypes", manageVM)
            End Try
        End Function

        <HttpPost()>
        Function JobTypeEdit(ByVal vm As ManageViewModel) As ActionResult
            Try
                TryUpdateModel(manageVM.JobTypeList, "JobTypeList")

                If ModelState.IsValid Then

                    For Each e In vm.JobTypeList 'emptypes
                        jobRepo.UpdateEmploymentType(e)
                    Next

                    TempData("Message") = New StatusMessage("alert-success", "Job Types Saved ")

                    Return RedirectToAction("JobTypes")
                End If

                Return View("JobTypes", manageVM)
            Catch
                Return View("JobTypes", manageVM)
            End Try
        End Function

    End Class

End Namespace