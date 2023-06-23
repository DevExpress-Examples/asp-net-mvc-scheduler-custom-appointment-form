' Developer Express Code Central Example:
' Scheduler - How to implement a custom Edit Appointment Form with custom fields
' 
' This example illustrates how to implement a custom Appointment Form and display
' it instead of the default one.
' 
' To include a custom Appointment Form to the
' SchedulerPartial view, the
' MVCxSchedulerOptionsForms.SetAppointmentFormTemplateContent Method
' (ms-help://DevExpress.NETv12.2/DevExpress.AspNet/DevExpressWebMvcMVCxSchedulerOptionsForms_SetAppointmentFormTemplateContenttopic.htm)
' should be handled.
' To add custom fields to the Appointment Form, implement a
' custom AppointmentFormTemplateContainer
' (ms-help://DevExpress.NETv12.2/DevExpress.AspNet/clsDevExpressWebASPxSchedulerAppointmentFormTemplateContainertopic.htm)
' and substitute the default container with your custom one by handling the
' ASPxScheduler.AppointmentFormShowing Event
' (ms-help://DevExpress.NETv12.2/DevExpress.AspNet/DevExpressWebASPxSchedulerASPxScheduler_AppointmentFormShowingtopic.htm).
' See
' Also:
' http://www.devexpress.com/scid=E2924
' http://www.devexpress.com/scid=E3984
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E4520
Imports System.Web.Mvc
Imports DevExpress.Web.Mvc
Imports DevExpressMvcApplication1.Models

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
            Dim insertedAppt As CarScheduling = SchedulerExtension.GetAppointmentToInsert(Of CarScheduling)("scheduler", SchedulerDataHelper.GetAppointments(), SchedulerDataHelper.GetResources(), SchedulerDataHelper.DefaultAppointmentStorage, SchedulerDataHelper.DefaultResourceStorage)
            SchedulerDataHelper.InsertAppointment(insertedAppt)
            ViewData("EditableSchedule") = insertedAppt
            Dim updatedAppt As CarScheduling() = SchedulerExtension.GetAppointmentsToUpdate(Of CarScheduling)("scheduler", SchedulerDataHelper.GetAppointments(), SchedulerDataHelper.GetResources(), SchedulerDataHelper.DefaultAppointmentStorage, SchedulerDataHelper.DefaultResourceStorage)
            For Each appt In updatedAppt
                SchedulerDataHelper.UpdateAppointment(appt)
            Next

            Dim removedAppt As CarScheduling() = SchedulerExtension.GetAppointmentsToRemove(Of CarScheduling)("scheduler", SchedulerDataHelper.GetAppointments(), SchedulerDataHelper.GetResources(), SchedulerDataHelper.DefaultAppointmentStorage, SchedulerDataHelper.DefaultResourceStorage)
            For Each appt In removedAppt
                SchedulerDataHelper.RemoveAppointment(appt)
            Next
        End Sub
    End Class
End Namespace
