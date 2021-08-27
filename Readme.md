<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128553630/14.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4520)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# Scheduler - How to implement a custom Edit Appointment Form with custom fields


<p>This example illustrates how to implement a custom Appointment Form and display it instead of the default one.</p><p>To include a custom Appointment Form to the <strong>SchedulerPartial</strong> view, the <a href="http://documentation.devexpress.com/#AspNet/DevExpressWebMvcMVCxSchedulerOptionsForms_SetAppointmentFormTemplateContenttopic"><u>MVCxSchedulerOptionsForms.SetAppointmentFormTemplateContent Method</u></a>  should be handled.<br />
To add custom fields to the Appointment Form, implement a custom <a href="http://documentation.devexpress.com/#AspNet/clsDevExpressWebASPxSchedulerAppointmentFormTemplateContainertopic"><u>AppointmentFormTemplateContainer</u></a>  and substitute the default container with your custom one by handling the <a href="http://documentation.devexpress.com/#AspNet/DevExpressWebASPxSchedulerASPxScheduler_AppointmentFormShowingtopic"><u>ASPxScheduler.AppointmentFormShowing Event</u></a><u>.</u> </p><p><strong>See Also:</strong><strong><br />
</strong><a href="https://www.devexpress.com/Support/Center/p/E2924">E2924: How to construct an appointment editing form with custom fields</a><strong><u><br />
</u></strong><a href="https://www.devexpress.com/Support/Center/p/E3984">E3984: Scheduler - Lesson 2 - Insert-Update-Delete appointment feature</a></p>

<br/>


