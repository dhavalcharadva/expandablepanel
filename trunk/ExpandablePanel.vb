Imports System
Imports System.Drawing
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

<Assembly: CLSCompliant(True)> 

<DefaultProperty("Text")> _
<ToolboxData("<{0}:ExpandablePanel runat=server></{0}:ExpandablePanel>")> _
<ToolboxBitmap(GetType(ExpandablePanel), "ExpandablePanel.ExpandablePanel.ico")> _
Public Class ExpandablePanel
    Inherits Panel
    Implements IPostBackDataHandler

#Region " Properties..."

    Private _Expanded As Boolean = True
    <Category("Custom")> _
    <Browsable(True)> _
    <Description("")> _
    Public Property Expanded() As Boolean
        Get
            Return _Expanded
        End Get

        Set(ByVal Value As Boolean)
            _Expanded = Value
        End Set
    End Property

    Private _HeaderText As String
    <Category("Custom")> _
    <Browsable(True)> _
    <Description("")> _
    Property HeaderText() As String
        Get
            Return _HeaderText
        End Get

        Set(ByVal Value As String)
            _HeaderText = Value
        End Set
    End Property

    Private _HeaderHelpText As String
    <Category("Custom")> _
    <Browsable(True)> _
    <Description("")> _
    Property HeaderHelpText() As String
        Get
            Return _HeaderHelpText
        End Get

        Set(ByVal Value As String)
            _HeaderHelpText = Value
        End Set
    End Property

    Private _HeaderBackColor As Color
    <Category("Custom")> _
    <Browsable(True)> _
    <Description("")> _
    Public Property HeaderBackColor() As Color
        Get
            Return _HeaderBackColor
        End Get
        Set(ByVal Value As Color)
            _HeaderBackColor = Value
        End Set
    End Property

    Private _HeaderForeColor As Color
    <Category("Custom")> _
    <Browsable(True)> _
    <Description("")> _
    Public Property HeaderForeColor() As Color
        Get
            Return _HeaderForeColor
        End Get
        Set(ByVal Value As Color)
            _HeaderForeColor = Value
        End Set
    End Property

    Private _HeaderHelpForeColor As Color
    <Category("Custom")> _
    <Browsable(True)> _
    <Description("")> _
    Public Property HeaderHelpForeColor() As Color
        Get
            Return _HeaderHelpForeColor
        End Get
        Set(ByVal Value As Color)
            _HeaderHelpForeColor = Value
        End Set
    End Property


#End Region

    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        MyBase.OnInit(e)
        Page.RegisterRequiresPostBack(Me)
    End Sub

    Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
        JavaScriptRegistrator.RegisterEmbeddedScript(Page, Me.GetType, "ExpandablePanel")
        MyBase.OnPreRender(e)
    End Sub

    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)

        If Not String.IsNullOrEmpty(_HeaderText) Then

            writer.AddAttribute(HtmlTextWriterAttribute.Id, Me.ClientID & "_hidden")
            writer.AddAttribute(HtmlTextWriterAttribute.Name, Me.ClientID & "_hidden")
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "hidden")
            writer.AddAttribute(HtmlTextWriterAttribute.Value, IIf(_Expanded, String.Empty, "none"))
            writer.RenderBeginTag(HtmlTextWriterTag.Input)
            writer.RenderEndTag()

            writer.AddAttribute(HtmlTextWriterAttribute.Id, Me.ClientID & "_header")
            writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, MyBase.BorderWidth.ToString)
            writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, MyBase.BorderStyle.ToString)
            writer.AddStyleAttribute(HtmlTextWriterStyle.BorderColor, ColorTranslator.ToHtml(MyBase.BorderColor))
            writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, ColorTranslator.ToHtml(_HeaderBackColor))
            writer.AddStyleAttribute(HtmlTextWriterStyle.Color, ColorTranslator.ToHtml(_HeaderForeColor))
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, MyBase.Width.ToString)
            writer.AddStyleAttribute(HtmlTextWriterStyle.MarginTop, "2px")

            writer.RenderBeginTag(HtmlTextWriterTag.Div)

            writer.AddStyleAttribute(HtmlTextWriterStyle.FontFamily, "verdana")
            writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "11px")
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "3")
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0")
            writer.AddAttribute(HtmlTextWriterAttribute.Width, "100%")

            writer.RenderBeginTag(HtmlTextWriterTag.Table) '<table>

            ''''''''''''''''''''''

            writer.RenderBeginTag(HtmlTextWriterTag.Tr) '<tr>
            writer.AddStyleAttribute(HtmlTextWriterStyle.FontWeight, "bold")
            writer.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, TextAlign.Left.ToString)
            writer.RenderBeginTag(HtmlTextWriterTag.Td) '<td>
            writer.Write(_HeaderText)
            writer.RenderEndTag() '</td>

            writer.AddStyleAttribute("cursor", "pointer")
            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "fnTogglePanel('" & Me.ClientID & "')")
            writer.AddAttribute(HtmlTextWriterAttribute.Id, Me.ClientID & "_UpDownButton")

            If _Expanded Then
                writer.AddAttribute(HtmlTextWriterAttribute.Title, "Click to hide this panel")
            Else
                writer.AddAttribute(HtmlTextWriterAttribute.Title, "Click to show this panel")
            End If

            writer.AddAttribute(HtmlTextWriterAttribute.Align, WebControls.HorizontalAlign.Right.ToString)
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "5%")

            writer.RenderBeginTag(HtmlTextWriterTag.Td) '<td>
            If _Expanded Then writer.Write("Hide") Else writer.Write("Show") 'down arrow
            writer.RenderEndTag() '</td>

            writer.RenderEndTag() '</tr>

            ''''''''''''''''''''''

            If Not String.IsNullOrEmpty(_HeaderHelpText) Then
                writer.RenderBeginTag(HtmlTextWriterTag.Tr) '<tr>
                writer.AddStyleAttribute(HtmlTextWriterStyle.FontStyle, [Enum].GetName(GetType(FontStyle), 2))
                writer.AddStyleAttribute(HtmlTextWriterStyle.Color, ColorTranslator.ToHtml(_HeaderHelpForeColor))
                writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "11px")
                writer.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, TextAlign.Left.ToString)

                writer.RenderBeginTag(HtmlTextWriterTag.Td) '<td>
                writer.Write(_HeaderHelpText)
                writer.RenderEndTag() '</td>

                writer.RenderEndTag() '</tr>
            End If

            writer.RenderEndTag() '</table>

            writer.RenderEndTag() '</div>

            Dim vis As String
            If _Expanded Then vis = "" Else vis = "none"
            writer.AddStyleAttribute("display", vis)
            MyBase.Render(writer)
        Else
            MyBase.Render(writer)
        End If

    End Sub

    Public Function LoadPostData(ByVal postDataKey As String, ByVal postCollection As System.Collections.Specialized.NameValueCollection) As Boolean Implements System.Web.UI.IPostBackDataHandler.LoadPostData
        Dim vis As String = postCollection(Me.ClientID & "_hidden")
        If vis IsNot Nothing Then
            If vis = "none" AndAlso _Expanded <> False Then
                _Expanded = False
            End If
            If vis = "" AndAlso _Expanded <> True Then
                _Expanded = True
            End If
        End If
    End Function

    Public Sub RaisePostDataChangedEvent() Implements System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent

    End Sub
End Class
