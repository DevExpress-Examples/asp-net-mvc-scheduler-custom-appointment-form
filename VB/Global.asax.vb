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

' Developer Express Code Central Example:
' Scheduler - Getting Started - Lesson 2 - Implement Insert-Update-Delete appointment functionality
' 
' This example demonstrates a simple application that enables end-users to add new
' appointments, modify existing appointments and delete them if necessary.
' This
' project is created by following the step-by-step guide of the Lesson 2 -
' Implement Insert-Update-Delete Appointment Functionality
' (ms-help://DevExpress.NETv12.1/DevExpress.AspNet/CustomDocument11567.htm).
' 
' See
' also the http://www.devexpress.com/scid=E3997.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E3984

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Routing

Namespace DevExpressMvcApplication1
    ' Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    ' visit http://go.microsoft.com/?LinkId=9394801

    Public Class MvcApplication
        Inherits System.Web.HttpApplication

        Public Shared Sub RegisterGlobalFilters(ByVal filters As GlobalFilterCollection)
            filters.Add(New HandleErrorAttribute())
        End Sub

        Public Shared Sub RegisterRoutes(ByVal routes As RouteCollection)
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}")
            routes.IgnoreRoute("{resource}.ashx/{*pathInfo}")

            routes.MapRoute("Default", "{controller}/{action}/{id}", New With {Key .controller = "Home", Key .action = "Index", Key .id = UrlParameter.Optional}) ' Parameter defaults -  URL with parameters -  Route name

        End Sub

        Protected Sub Application_Start()
            AreaRegistration.RegisterAllAreas()

            ModelBinders.Binders.DefaultBinder = New DevExpress.Web.Mvc.DevExpressEditorsBinder()


            RegisterGlobalFilters(GlobalFilters.Filters)
            RegisterRoutes(RouteTable.Routes)
        End Sub
    End Class
End Namespace