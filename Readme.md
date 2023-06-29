<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128553630/15.2.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4520)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# Scheduler for ASP.NET MVC - How to implement a custom edit appointment form

This example demonstrates how to use the [SetAppointmentFormTemplateContent](https://docs.devexpress.com/AspNetMvc/DevExpress.Web.Mvc.MVCxSchedulerOptionsForms.SetAppointmentFormTemplateContent.overloads) method to create a custom appointment form.

## Overview

Call the control's [MVCxSchedulerOptionsForms.SetAppointmentFormTemplateContent](https://docs.devexpress.com/AspNetMvc/DevExpress.Web.Mvc.MVCxSchedulerOptionsForms.SetAppointmentFormTemplateContent.overloads) method to add a custom appointment form to the *SchedulerPartial* view.

```cshtml
settings.OptionsForms.SetAppointmentFormTemplateContent(c => {
    var container = (CustomAppointmentTemplateContainer)c;
    var schedule = ViewData["EditableSchedule"] != null
        ? (Schedule)ViewData["EditableSchedule"]
        : new Schedule() {
            ID = container.Appointment.Id == null ? -1 : (int)container.Appointment.Id,
            <!-- ... -->
        };
    
    ViewBag.DeleteButtonEnabled = container.CanDeleteAppointment;
    ViewBag.IsRecurring = container.Appointment.IsRecurring;
    ViewBag.AppointmentRecurrenceFormSettings = CreateAppointmentRecurrenceFormSettings(container);

    ViewBag.ResourceDataSource = container.ResourceDataSource;
    ViewBag.StatusDataSource = container.StatusDataSource;
    ViewBag.LabelDataSource = container.LabelDataSource;
    ViewBag.ReminderDataSource = container.ReminderDataSource;

    Html.RenderPartial("CustomAppointmentFormPartial", schedule);
});
```

Create an [AppointmentFormTemplateContainer](https://docs.devexpress.com/AspNet/DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer) to add custom fields to your appointment form and handle the control's server-side [ASPxScheduler.AppointmentFormShowing](https://docs.devexpress.com/AspNet/DevExpress.Web.ASPxScheduler.ASPxScheduler.AppointmentFormShowing) event to replace the default container with the custom container.

```cshtml
settings.AppointmentFormShowing = (sender, e) => {
    var scheduler = sender as MVCxScheduler;
    if (scheduler != null)
        e.Container = new CustomAppointmentTemplateContainer(scheduler);
};
```

## Files to Review

* [SchedulerPartial.cshtml](./CS/Views/Home/SchedulerPartial.cshtml)
* [CustomAppointmentFormPartial.cstml](./CS/Views/Home/CustomAppointmentFormPartial.cshtml)
* [Index.cshtml](./CS/Views/Home/Index.cshtml)
* [Scheduling.cs](./CS/Models/Scheduling.cs) (VB: [Scheduling.vb](./VB/Models/Scheduling.vb))

## Documentation

* [MVCxSxheduler](https://docs.devexpress.com/AspNetMvc/DevExpress.Web.Mvc.MVCxScheduler)
* [Scheduler Templates](https://docs.devexpress.com/AspNet/4550/components/scheduler/concepts/templates)

## More Examples

* [Scheduler for ASP.NET Web Forms - How to construct an appointment edit form with custom fields](https://github.com/DevExpress-Examples/how-to-construct-an-appointment-editing-form-with-custom-fields-e2924)
* [Scheduler for ASP.NET MVC - Insert-Update-Delete appointment feature](https://github.com/DevExpress-Examples/scheduler-lesson-2-insert-update-delete-appointment-feature-e3984)


