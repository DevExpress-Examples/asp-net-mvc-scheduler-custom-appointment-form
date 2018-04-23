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

Imports System.Collections
Imports System.Linq
Imports DevExpressMvcApplication1
Imports DevExpress.Web.Mvc
Imports System.ComponentModel.DataAnnotations
Imports System
Imports DevExpressMvcApplication1.Models
Imports DevExpress.Web
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.XtraScheduler

Public Class SchedulerDataObject
    Public Property Appointments() As IEnumerable
    Public Property Resources() As IEnumerable
End Class

Public Class SchedulerDataHelper
    Public Shared Function GetResources() As IEnumerable
        Dim db As New CarsDataContext()
        Return From res In db.Cars _
               Select res
    End Function
    Public Shared Function GetAppointments() As IEnumerable
        Dim db As New CarsDataContext()
        Return From apt In db.CarSchedulings _
               Select apt
    End Function
    Public Shared Iterator Function GetReminders(ByVal rawDataSource As IEnumerable) As IEnumerable
        For Each item As ListEditItem In rawDataSource
            Yield New With {Key .Value = item.Value, Key .Text = item.Text}
        Next item
    End Function
    Public Shared ReadOnly Property DataObject() As SchedulerDataObject
        Get
            Return New SchedulerDataObject() With {.Appointments = GetAppointments(), .Resources = GetResources()}
        End Get
    End Property

    Private Shared defaultAppointmentStorage_Renamed As MVCxAppointmentStorage
    Public Shared ReadOnly Property DefaultAppointmentStorage() As MVCxAppointmentStorage
        Get
            If defaultAppointmentStorage_Renamed Is Nothing Then
                defaultAppointmentStorage_Renamed = CreateDefaultAppointmentStorage()
            End If
            Return defaultAppointmentStorage_Renamed
        End Get
    End Property
    Private Shared Function CreateDefaultAppointmentStorage() As MVCxAppointmentStorage
        Dim appointmentStorage As New MVCxAppointmentStorage()
        appointmentStorage.Mappings.AppointmentId = "ID"
        appointmentStorage.Mappings.Start = "StartTime"
        appointmentStorage.Mappings.End = "EndTime"
        appointmentStorage.Mappings.Subject = "Subject"
        appointmentStorage.Mappings.Description = "Description"
        appointmentStorage.Mappings.Location = "Location"
        appointmentStorage.Mappings.AllDay = "AllDay"
        appointmentStorage.Mappings.Type = "EventType"
        appointmentStorage.Mappings.RecurrenceInfo = "RecurrenceInfo"
        appointmentStorage.Mappings.ReminderInfo = "ReminderInfo"
        appointmentStorage.Mappings.Label = "Label"
        appointmentStorage.Mappings.Status = "Status"
        appointmentStorage.Mappings.ResourceId = "CarId"
        appointmentStorage.CustomFieldMappings.Add("Price", "Price")
        appointmentStorage.CustomFieldMappings.Add("ContactInfo", "ContactInfo")
        appointmentStorage.CustomFieldMappings.Add("CarId", "CarId")
        Return appointmentStorage
    End Function

    Private Shared defaultResourceStorage_Renamed As MVCxResourceStorage
    Public Shared ReadOnly Property DefaultResourceStorage() As MVCxResourceStorage
        Get
            If defaultResourceStorage_Renamed Is Nothing Then
                defaultResourceStorage_Renamed = CreateDefaultResourceStorage()
            End If
            Return defaultResourceStorage_Renamed
        End Get
    End Property
    Private Shared Function CreateDefaultResourceStorage() As MVCxResourceStorage
        Dim resourceStorage As New MVCxResourceStorage()
        resourceStorage.Mappings.ResourceId = "ID"
        resourceStorage.Mappings.Caption = "Model"
        Return resourceStorage
    End Function
    Public Shared Sub InsertAppointment(ByVal appt As CarScheduling)
        If appt Is Nothing Then
            Return
        End If
        Dim db As New CarsDataContext()
        appt.ID = appt.GetHashCode()
        db.CarSchedulings.InsertOnSubmit(appt)
        db.SubmitChanges()
    End Sub
    Public Shared Sub UpdateAppointment(ByVal appt As CarScheduling)
        If appt Is Nothing Then
            Return
        End If
        Dim db As New CarsDataContext()
        Dim query As CarScheduling = CType(( _
            From carSchedule In db.CarSchedulings _
            Where carSchedule.ID = appt.ID _
            Select carSchedule).SingleOrDefault(), CarScheduling)

        query.ID = appt.ID
        query.StartTime = appt.StartTime
        query.EndTime = appt.EndTime
        query.AllDay = appt.AllDay
        query.Subject = appt.Subject
        query.Description = appt.Description
        query.Location = appt.Location
        query.RecurrenceInfo = appt.RecurrenceInfo
        query.ReminderInfo = appt.ReminderInfo
        query.Status = appt.Status
        query.EventType = appt.EventType
        query.Label = appt.Label
        query.CarId = appt.CarId
        query.ContactInfo = appt.ContactInfo
        query.Price = appt.Price
        db.SubmitChanges()
    End Sub
    Public Shared Sub RemoveAppointment(ByVal appt As CarScheduling)
        Dim db As New CarsDataContext()
        Dim query As CarScheduling = CType(( _
            From carSchedule In db.CarSchedulings _
            Where carSchedule.ID = appt.ID _
            Select carSchedule).SingleOrDefault(), CarScheduling)
        db.CarSchedulings.DeleteOnSubmit(query)
        db.SubmitChanges()
    End Sub
End Class

Public Class CustomAppointmentTemplateContainer
    Inherits AppointmentFormTemplateContainer

    Public Sub New(ByVal scheduler As MVCxScheduler)
        MyBase.New(scheduler)
    End Sub

    Public Shadows ReadOnly Property ResourceDataSource() As IEnumerable
        Get
            Return SchedulerDataHelper.GetResources()
        End Get
    End Property
    Public Shadows ReadOnly Property ReminderDataSource() As IEnumerable
        Get
            Return SchedulerDataHelper.GetReminders(MyBase.ReminderDataSource)
        End Get
    End Property
    Public ReadOnly Property ContactInfo() As String
        Get
            Return Convert.ToString(Appointment.CustomFields("ContactInfo"))
        End Get
    End Property
    Public ReadOnly Property Price() As Decimal?
        Get
            Dim priceRawValue As Object = Appointment.CustomFields("Price")
            Return If(priceRawValue Is DBNull.Value, 0, DirectCast(priceRawValue, Decimal?))
        End Get
    End Property
    Public ReadOnly Property CarId() As Integer?
        Get

            Dim carId_Renamed As Object = Appointment.ResourceId
            Return If(carId_Renamed Is ResourceEmpty.Id, 1, DirectCast(carId_Renamed, Integer?)) ' select first resource if empty
        End Get
    End Property
End Class

Public Class Schedule
    Public Sub New()
    End Sub
    Public Sub New(ByVal carScheduling As CarScheduling)
        If carScheduling IsNot Nothing Then
            ID = carScheduling.ID
            EventType = carScheduling.EventType
            Label = carScheduling.Label
            AllDay = carScheduling.AllDay
            Location = carScheduling.Location
            CarId = carScheduling.CarId
            Status = carScheduling.Status
            RecurrenceInfo = carScheduling.RecurrenceInfo
            ReminderInfo = carScheduling.ReminderInfo
            Subject = carScheduling.Subject
            Price = carScheduling.Price
            StartTime = carScheduling.StartTime.Value
            EndTime = carScheduling.EndTime.Value
            Description = carScheduling.Description
            ContactInfo = carScheduling.ContactInfo
        End If
    End Sub

    Public Property ID() As Integer
    Public Property EventType() As Integer?
    Public Property Label() As Integer?
    Public Property AllDay() As Boolean
    Public Property Location() As String
    Public Property CarId() As Object
    Public Property Status() As Integer?
    Public Property RecurrenceInfo() As String
    Public Property ReminderInfo() As String
    <Required(ErrorMessage := "The Subject must contain at least one character.")> _
    Public Property Subject() As String
    Public Property Price() As Decimal?
    <Required> _
    Public Property StartTime() As Date
    <Required> _
    Public Property EndTime() As Date
    Public Property Description() As String
    Public Property ContactInfo() As String
    Public Property HasReminder() As Boolean
    Public Property Reminder() As Reminder

    Public Overridable Sub Assign(ByVal source As Schedule)
        If source IsNot Nothing Then
            ID = source.ID
            EventType = source.EventType
            Label = source.Label
            AllDay = source.AllDay
            Location = source.Location
            CarId = source.CarId
            Status = source.Status
            RecurrenceInfo = source.RecurrenceInfo
            ReminderInfo = source.ReminderInfo
            Subject = source.Subject
            Price = source.Price
            StartTime = source.StartTime
            EndTime = source.EndTime
            Description = source.Description
            ContactInfo = source.ContactInfo
        End If
    End Sub
End Class