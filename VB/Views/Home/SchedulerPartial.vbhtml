@Functions 
    Private Function CreateAppointmentRecurrenceFormSettings(container As CustomAppointmentTemplateContainer) As AppointmentRecurrenceFormSettings
        Dim settings As AppointmentRecurrenceFormSettings = New AppointmentRecurrenceFormSettings()
        settings.Name = "appointmentRecurrenceForm"
        settings.Width = System.Web.UI.WebControls.Unit.Percentage(100)
        settings.IsRecurring = container.Appointment.IsRecurring
        settings.DayNumber = container.RecurrenceDayNumber
        settings.End = container.RecurrenceEnd
        settings.Month = container.RecurrenceMonth
        settings.OccurrenceCount = container.RecurrenceOccurrenceCount
        settings.Periodicity = container.RecurrencePeriodicity
        settings.RecurrenceRange = container.RecurrenceRange
        settings.Start = container.Start
        settings.WeekDays = container.RecurrenceWeekDays
        settings.WeekOfMonth = container.RecurrenceWeekOfMonth
        settings.RecurrenceType = container.RecurrenceType
        settings.IsFormRecreated = container.IsFormRecreated
        Return settings
    End Function
End Functions

@Html.DevExpress().Scheduler(
    Sub(settings)
            settings.Name = "scheduler"
            settings.CallbackRouteValues = New With {.Controller = "Home", .Action = "SchedulerPartial"}
            settings.EditAppointmentRouteValues = New With {.Controller = "Home", .Action = "EditAppointment"}
            settings.Storage.Appointments.Assign(SchedulerDataHelper.DefaultAppointmentStorage)
            settings.Storage.Resources.Assign(SchedulerDataHelper.DefaultResourceStorage)
            settings.Storage.EnableReminders = True
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100)
            settings.Views.DayView.Styles.ScrollAreaHeight = 600
            settings.Start = New DateTime(2010, 7, 1)
            settings.OptionsForms.RecurrenceFormName = "appointmentRecurrenceForm"
            settings.AppointmentFormShowing =
                Sub(sender, e)
                        Dim scheduler As MVCxScheduler = TryCast(sender, MVCxScheduler)
                        If scheduler IsNot Nothing Then
                            e.Container = New CustomAppointmentTemplateContainer(scheduler)
                        End If
                End Sub
            settings.OptionsForms.SetAppointmentFormTemplateContent(
                Sub(c)
                        Dim container As CustomAppointmentTemplateContainer = DirectCast(c, CustomAppointmentTemplateContainer)
                        Dim schedule = If(ViewData("EditableSchedule") IsNot Nothing, DirectCast(ViewData("EditableSchedule"), Schedule), New Schedule() With { _
                            .ID = If(container.Appointment.Id Is Nothing, -1, CInt(container.Appointment.Id)), _
                            .Subject = container.Appointment.Subject, _
                            .Location = container.Appointment.Location, _
                            .StartTime = container.Appointment.Start, _
                            .EndTime = container.Appointment.[End], _
                            .AllDay = container.Appointment.AllDay, _
                            .Description = container.Appointment.Description, _
                            .EventType = CType(container.Appointment.Type, Nullable(Of Integer)), _
                            .Status = container.Appointment.StatusId, _
                            .Label = container.Appointment.LabelId, _
                            .CarId = container.CarId, _
                            .Price = container.Price, _
                            .ContactInfo = container.ContactInfo, _
                            .HasReminder = container.Appointment.HasReminder, _
                            .Reminder = container.Appointment.Reminder _
                        })
                        ViewBag.DeleteButtonEnabled = container.CanDeleteAppointment
                        ViewBag.IsRecurring = container.Appointment.IsRecurring
                        ViewBag.AppointmentRecurrenceFormSettings = CreateAppointmentRecurrenceFormSettings(container)
                        ViewBag.ResourceDataSource = container.ResourceDataSource
                        ViewBag.StatusDataSource = container.StatusDataSource
                        ViewBag.LabelDataSource = container.LabelDataSource
                        ViewBag.ReminderDataSource = container.ReminderDataSource
                        Html.RenderPartial("CustomAppointmentFormPartial", schedule)
                End Sub)
    End Sub).Bind(Model.Appointments, Model.Resources).GetHtml()