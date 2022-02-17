Imports System.Runtime.CompilerServices
' Extension module for Telegram HTML parse mode
' Adds some text style modifiers
' Example of use:
' Dim message As String = "text"
' Dim link As String = "www.anysite.com/something.html"
' message.bold
' link.inlineURL("This is inline URL")
Public Module CMarkup
    <Extension()>
    Function bold(ByVal input As String) As String
        input.HTMLReplace
        Return "<b>" + input + "</b>"
    End Function
    <Extension()>
    Function italic(ByVal input As String) As String
        input.HTMLReplace
        Return "<i>" + input + "</i>"
    End Function
    <Extension()>
    Function underlined(ByVal input As String) As String
        input.HTMLReplace
        Return "<u>" + input + "</u>"
    End Function
    <Extension()>
    Function strikethrough(ByVal input As String) As String
        input.HTMLReplace
        Return "<s>" + input + "</s>"
    End Function
    ' Create spoiler
    <Extension()>
    Function spoiler(ByVal input As String) As String
        input.HTMLReplace
        Return "<span class='tg-spoiler'>" + input + "</span>"
    End Function
    ' Make hyperlink with visible text "text" and link "link"
    ' Example:
    ' link.inlineURL("This is inline URL")
    <Extension()>
    Function inlineURL(ByVal link As String, ByVal text As String) As String
        link.HTMLReplace
        text.HTMLReplace
        Return "<a href='" + link + "'>" + text + "</a>"
    End Function
    ' Fixed width character text
    <Extension()>
    Function inlineFixedWidth(ByVal input As String) As String
        input.HTMLReplace
        Return "<code>" + input + "</code>"
    End Function
    ' Fixed width character text
    <Extension()>
    Function fixedWidth(ByVal input As String) As String
        input.HTMLReplace
        Return "<pre>" + input + "</pre>"
    End Function
    ' Adds new line into Telegram message
    ' Example:
    ' Dim message1 As String = "This is line 1"
    ' Dim message2 As String = "This is line 2"
    ' message1.newline + message2
    '
    ' Will result into:
    ' This is line 1
    ' This is line 2
    <Extension()>
    Function newline(ByVal input As String) As String
        input.HTMLReplace
        Return input + "%0A"
    End Function
    ' If there are any symbols in message like "<",">","&", they must be replaced with special HTML codes
    ' This is requred because this case Telegram message is sent in HTML encoding
    <Extension()>
    Function HTMLReplace(ByVal input As String) As String
        Return input.Replace("<", "&lt").Replace(">", "&gt").Replace("&", "&amp")
    End Function
End Module
