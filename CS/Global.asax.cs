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

// Developer Express Code Central Example:
// Scheduler - Getting Started - Lesson 2 - Implement Insert-Update-Delete appointment functionality
// 
// This example demonstrates a simple application that enables end-users to add new
// appointments, modify existing appointments and delete them if necessary.
// This
// project is created by following the step-by-step guide of the Lesson 2 -
// Implement Insert-Update-Delete Appointment Functionality
// (ms-help://DevExpress.NETv12.1/DevExpress.AspNet/CustomDocument11567.htm).
// 
// See
// also the http://www.devexpress.com/scid=E3997.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E3984

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DevExpressMvcApplication1
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            ModelBinders.Binders.DefaultBinder = new DevExpress.Web.Mvc.DevExpressEditorsBinder();


            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}