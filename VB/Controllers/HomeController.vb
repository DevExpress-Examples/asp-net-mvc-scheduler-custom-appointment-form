Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports DevExpress.Web.Mvc
Imports DevExpressMvcApplication1.Models
Imports System.Collections

Namespace DevExpressMvcApplication1.Views
    Public Class HomeController
        Inherits Controller
        '
        ' GET: /Home/

        Public Function Index() As ActionResult
            Return View(SchedulerDataHelper.DataObject)
        End Function

        Public Function SchedulerPartial() As ActionResult
            Return PartialView("SchedulerPartial", SchedulerDataHelper.DataObject)
        End Function

        Public Function EditAppointment() As ActionResult
            UpdateAppointment()
            Return PartialView("SchedulerPartial", SchedulerDataHelper.DataObject)
        End Function

        Private Sub UpdateAppointment()
            Dim insertedAppt As CarScheduling = SchedulerExtension.GetAppointmentToInsert(Of CarScheduling)(
                "scheduler",
                SchedulerDataHelper.GetAppointments(),
                SchedulerDataHelper.GetResources(),
                SchedulerDataHelper.DefaultAppointmentStorage,
                SchedulerDataHelper.DefaultResourceStorage)

            SchedulerDataHelper.InsertAppointment(insertedAppt)

            ViewData("EditableSchedule") = insertedAppt

            Dim updatedAppt() As CarScheduling = SchedulerExtension.GetAppointmentsToUpdate(Of CarScheduling)(
                "scheduler",
                SchedulerDataHelper.GetAppointments(),
                SchedulerDataHelper.GetResources(),
                SchedulerDataHelper.DefaultAppointmentStorage,
                SchedulerDataHelper.DefaultResourceStorage)

            For Each appt In updatedAppt
                SchedulerDataHelper.UpdateAppointment(appt)
            Next appt

            Dim removedAppt() As CarScheduling = SchedulerExtension.GetAppointmentsToRemove(Of CarScheduling)(
                "scheduler",
                SchedulerDataHelper.GetAppointments(),
                SchedulerDataHelper.GetResources(),
                SchedulerDataHelper.DefaultAppointmentStorage,
                SchedulerDataHelper.DefaultResourceStorage)

            For Each appt In removedAppt
                SchedulerDataHelper.RemoveAppointment(appt)
            Next appt
        End Sub
    End Class
End Namespace
