Imports JobPostings.Filters
Imports JobPostings.Models.ViewModels
Imports JobPostings.Repositories

Namespace Controllers

    <XprsAuthorizationFilter(Roles:="Admin")>
    Public Class EmployerController
        Inherits BaseController

        Private empRepo As EmployerRepository
        Private empVM As EmployerViewModel

        Public Sub New()
            empRepo = New EmployerRepository
            empVM = New EmployerViewModel
        End Sub

        '
        ' GET: /Employer

        Function Index() As ActionResult
            empVM.EmployerList = empRepo.GetAll

            Dim citylist = New List(Of SelectListItem)()
            citylist.Add(New SelectListItem() With {.Value = "0", .Text = "All Cities"})
            citylist.AddRange(New SelectList(empRepo.GetCities))
            empVM.Cities = citylist

            Return View(empVM)
        End Function

        <HttpPost()>
        Function Index(ByVal vm As EmployerViewModel) As ActionResult
            Dim citylist = New List(Of SelectListItem)()
            citylist.Add(New SelectListItem() With {.Value = "0", .Text = "All Cities"})
            citylist.AddRange(New SelectList(empRepo.GetCities))
            empVM.Cities = citylist

            empVM.SelectedCity = vm.SelectedCity

            If vm.SelectedCity = "0" Then
                empVM.EmployerList = empRepo.GetAll
            Else
                empVM.EmployerList = empRepo.GetByCity(empVM.SelectedCity)
            End If

            Return View(empVM)
        End Function

        '
        ' GET: /Employer/Create

        Function Create() As ActionResult
            Return View(empVM)
        End Function

        '
        ' POST: /Employer/Create

        <HttpPost()>
        Function Create(ByVal emp As EmployerViewModel) As ActionResult
            empVM.EmployerDetails = emp.EmployerDetails

            If ModelState.IsValid Then

                empRepo.Insert(empVM.EmployerDetails)

                TempData("Message") = New StatusMessage("alert-success", "Employer Created")

                Return RedirectToAction("Edit", New With {.id = empVM.EmployerDetails.EmployerId})
            End If
            'Catch
            Return View(empVM)
            'End Try
        End Function

        '
        ' GET: /Employer/Edit/5

        Function Edit(ByVal id As System.Nullable(Of Integer)) As ActionResult

            If Not id Is Nothing Then
                empVM.EmployerDetails = empRepo.GetById(id)
            Else
                Return RedirectToAction("Index")
            End If

            Return View(empVM)
        End Function

        '
        ' POST: /Employer/Edit/5

        <HttpPost()>
        Function Edit(ByVal vm As EmployerViewModel) As ActionResult

            If ModelState.IsValid Then
                Dim emp = empRepo.GetById(vm.EmployerDetails.EmployerId)

                UpdateModel(emp, "EmployerDetails")
                empRepo.Update(emp)

                empVM.EmployerDetails = emp

                TempData("Message") = New StatusMessage("alert-success", "Employer saved")

                Return RedirectToAction("Edit", New With {.id = empVM.EmployerDetails.EmployerId})
            End If
            'Catch
            Return View(empVM)
            'End Try
        End Function

        '
        ' POST: /Employer/Delete/5

        <HttpPost()>
        Function Delete(ByVal id As Integer) As ActionResult
            Try
                Dim emp = empRepo.GetById(id)
                empRepo.Delete(emp)

                TempData("Message") = New StatusMessage("alert-success", "Employer Deleted")

                Return RedirectToAction("Index")
            Catch
                Return RedirectToAction("Edit", New With {.id = id})
            End Try
        End Function
    End Class

End Namespace