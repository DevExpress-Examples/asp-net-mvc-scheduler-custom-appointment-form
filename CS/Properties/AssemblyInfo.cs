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

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("DevExpressMvcApplication1")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("DevExpressMvcApplication1")]
[assembly: AssemblyCopyright("Copyright ? 2012")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("9fe9ebbc-b32e-4e40-b81a-a3e219e9e91f")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]