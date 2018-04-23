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


Imports Microsoft.VisualBasic
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

			routes.MapRoute("Default", "{controller}/{action}/{id}", New With {Key .controller = "Home", Key .action = "Index", Key .id = UrlParameter.Optional})

		End Sub

		Protected Sub Application_Start()
			AreaRegistration.RegisterAllAreas()

			ModelBinders.Binders.DefaultBinder = New DevExpress.Web.Mvc.DevExpressEditorsBinder()


			RegisterGlobalFilters(GlobalFilters.Filters)
			RegisterRoutes(RouteTable.Routes)
		End Sub
	End Class
End Namespace