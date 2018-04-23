Imports Microsoft.VisualBasic
Imports System.Collections
Imports System.Linq
Imports DevExpressMvcApplication1
Imports DevExpress.Web.Mvc
Imports System.ComponentModel.DataAnnotations
Imports System
Imports DevExpressMvcApplication1.Models
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.XtraScheduler

Public Class SchedulerDataObject
    Private privateAppointments As IEnumerable
    Public Property Appointments() As IEnumerable
        Get
            Return privateAppointments
        End Get
        Set(ByVal value As IEnumerable)
            privateAppointments = value
        End Set
    End Property
    Private privateResources As IEnumerable
    Public Property Resources() As IEnumerable
        Get
            Return privateResources
        End Get
        Set(ByVal value As IEnumerable)
            privateResources = value
        End Set
    End Property
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
        Dim query As CarScheduling = CType((From carSchedule In db.CarSchedulings _
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
        Dim query As CarScheduling = CType((From carSchedule In db.CarSchedulings _
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
    Public ReadOnly Property Price() As Nullable(Of Decimal)
        Get
            Dim priceRawValue As Object = Appointment.CustomFields("Price")
            Return If(priceRawValue Is DBNull.Value, 0, CType(priceRawValue, Nullable(Of Decimal)))
        End Get
    End Property
    Public ReadOnly Property CarId() As Nullable(Of Integer)
        Get
            Dim carIdRawValue As Object = Appointment.ResourceId
            Return If(carIdRawValue Is Resource.Empty, 1, CType(carIdRawValue, Nullable(Of Integer))) ' select first resource if empty
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

    Private privateID As Integer
    Public Property ID() As Integer
        Get
            Return privateID
        End Get
        Set(ByVal value As Integer)
            privateID = value
        End Set
    End Property
    Private privateEventType As Nullable(Of Integer)
    Public Property EventType() As Nullable(Of Integer)
        Get
            Return privateEventType
        End Get
        Set(ByVal value As Nullable(Of Integer))
            privateEventType = value
        End Set
    End Property
    Private privateLabel As Nullable(Of Integer)
    Public Property Label() As Nullable(Of Integer)
        Get
            Return privateLabel
        End Get
        Set(ByVal value As Nullable(Of Integer))
            privateLabel = value
        End Set
    End Property
    Private privateAllDay As Boolean
    Public Property AllDay() As Boolean
        Get
            Return privateAllDay
        End Get
        Set(ByVal value As Boolean)
            privateAllDay = value
        End Set
    End Property
    Private privateLocation As String
    Public Property Location() As String
        Get
            Return privateLocation
        End Get
        Set(ByVal value As String)
            privateLocation = value
        End Set
    End Property
    Private privateCarId As Object
    Public Property CarId() As Object
        Get
            Return privateCarId
        End Get
        Set(ByVal value As Object)
            privateCarId = value
        End Set
    End Property
    Private privateStatus As Nullable(Of Integer)
    Public Property Status() As Nullable(Of Integer)
        Get
            Return privateStatus
        End Get
        Set(ByVal value As Nullable(Of Integer))
            privateStatus = value
        End Set
    End Property
    Private privateRecurrenceInfo As String
    Public Property RecurrenceInfo() As String
        Get
            Return privateRecurrenceInfo
        End Get
        Set(ByVal value As String)
            privateRecurrenceInfo = value
        End Set
    End Property
    Private privateReminderInfo As String
    Public Property ReminderInfo() As String
        Get
            Return privateReminderInfo
        End Get
        Set(ByVal value As String)
            privateReminderInfo = value
        End Set
    End Property
    Private privateSubject As String
    <Required(ErrorMessage:="The Subject must contain at least one character.")> _
    Public Property Subject() As String
        Get
            Return privateSubject
        End Get
        Set(ByVal value As String)
            privateSubject = value
        End Set
    End Property
    Private privatePrice As Nullable(Of Decimal)
    Public Property Price() As Nullable(Of Decimal)
        Get
            Return privatePrice
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            privatePrice = value
        End Set
    End Property
    Private privateStartTime As DateTime
    <Required> _
    Public Property StartTime() As DateTime
        Get
            Return privateStartTime
        End Get
        Set(ByVal value As DateTime)
            privateStartTime = value
        End Set
    End Property
    Private privateEndTime As DateTime
    <Required> _
    Public Property EndTime() As DateTime
        Get
            Return privateEndTime
        End Get
        Set(ByVal value As DateTime)
            privateEndTime = value
        End Set
    End Property
    Private privateDescription As String
    Public Property Description() As String
        Get
            Return privateDescription
        End Get
        Set(ByVal value As String)
            privateDescription = value
        End Set
    End Property
    Private privateContactInfo As String
    Public Property ContactInfo() As String
        Get
            Return privateContactInfo
        End Get
        Set(ByVal value As String)
            privateContactInfo = value
        End Set
    End Property
    Private privateHasReminder As Boolean
    Public Property HasReminder() As Boolean
        Get
            Return privateHasReminder
        End Get
        Set(ByVal value As Boolean)
            privateHasReminder = value
        End Set
    End Property
    Private privateReminder As Reminder
    Public Property Reminder() As Reminder
        Get
            Return privateReminder
        End Get
        Set(ByVal value As Reminder)
            privateReminder = value
        End Set
    End Property

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