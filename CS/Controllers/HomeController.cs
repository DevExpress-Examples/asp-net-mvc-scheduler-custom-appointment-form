// Developer Express Code Central Example:
// Scheduler - How to implement a custom Edit Appointment Form with custom fields
// 
// This example illustrates how to implement a custom Appointment Form and display
// it instead of the default one.
// 
// To include a custom Appointment Form to the
// SchedulerPartial view, the
// MVCxSchedulerOptionsForms.SetAppointmentFormTemplateContent Method
// (ms-help://DevExpress.NETv12.2/DevExpress.AspNet/DevExpressWebMvcMVCxSchedulerOptionsForms_SetAppointmentFormTemplateContenttopic.htm)
// should be handled.
// To add custom fields to the Appointment Form, implement a
// custom AppointmentFormTemplateContainer
// (ms-help://DevExpress.NETv12.2/DevExpress.AspNet/clsDevExpressWebASPxSchedulerAppointmentFormTemplateContainertopic.htm)
// and substitute the default container with your custom one by handling the
// ASPxScheduler.AppointmentFormShowing Event
// (ms-help://DevExpress.NETv12.2/DevExpress.AspNet/DevExpressWebASPxSchedulerASPxScheduler_AppointmentFormShowingtopic.htm).
// See
// Also:
// http://www.devexpress.com/scid=E2924
// http://www.devexpress.com/scid=E3984
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E4520

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.Mvc;
using DevExpress.XtraScheduler;
using DevExpressMvcApplication1;
using DevExpressMvcApplication1.Models;
using System.Collections;

namespace DevExpressMvcApplication1.Views {
    public class HomeController : Controller {
        //
        // GET: /Home/

        public ActionResult Index() {
            return View(SchedulerDataHelper.DataObject);
        }

        public ActionResult SchedulerPartial() {
            return PartialView("SchedulerPartial", SchedulerDataHelper.DataObject);
        }

        public ActionResult EditAppointment() {
            UpdateAppointment();
            return PartialView("SchedulerPartial", SchedulerDataHelper.DataObject);
        }

        void UpdateAppointment() {
            CarScheduling insertedAppt = SchedulerExtension.GetAppointmentToInsert<CarScheduling>("scheduler", SchedulerDataHelper.GetAppointments(),
                SchedulerDataHelper.GetResources(), SchedulerDataHelper.DefaultAppointmentStorage, SchedulerDataHelper.DefaultResourceStorage);
            SchedulerDataHelper.InsertAppointment(insertedAppt);

            ViewData["EditableSchedule"] = insertedAppt;

            CarScheduling[] updatedAppt = SchedulerExtension.GetAppointmentsToUpdate<CarScheduling>("scheduler", SchedulerDataHelper.GetAppointments(),
                SchedulerDataHelper.GetResources(), SchedulerDataHelper.DefaultAppointmentStorage, SchedulerDataHelper.DefaultResourceStorage);
            foreach (var appt in updatedAppt) {
                SchedulerDataHelper.UpdateAppointment(appt);
            }

            CarScheduling[] removedAppt = SchedulerExtension.GetAppointmentsToRemove<CarScheduling>("scheduler", SchedulerDataHelper.GetAppointments(),
                SchedulerDataHelper.GetResources(), SchedulerDataHelper.DefaultAppointmentStorage, SchedulerDataHelper.DefaultResourceStorage);
            foreach (var appt in removedAppt) {
                SchedulerDataHelper.RemoveAppointment(appt);
            }
        }
    }
}
