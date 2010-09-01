Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Web.UI

Public Class JavaScriptRegistrator
    Public Shared Sub RegisterEmbeddedScript(ByVal page As Page, ByVal type As Type, ByVal scriptName As String)
        Dim scriptLocation As String = page.ClientScript.GetWebResourceUrl(type, "ExpandablePanel." + scriptName + ".js")
        If Not page.ClientScript.IsClientScriptBlockRegistered(type, scriptName) Then
            page.ClientScript.RegisterClientScriptInclude(type, scriptName, scriptLocation)
        End If
    End Sub
End Class
