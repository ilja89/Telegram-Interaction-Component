Imports System.Runtime.CompilerServices
Module CMarkup
    ' Extension module for Telegram HTML parse mode
    ' Adds some text style modifiers
    <Extension()>
    Function bold(ByVal input As String)
        Return "<b>" + input + "</b>"
    End Function
    <Extension()>
    Function cursive(ByVal input As String)
        Return "<i>" + input + "</i>"
    End Function
    <Extension()>
    Function underlined(ByVal input As String)
        Return "<u>" + input + "</u>"
    End Function
    <Extension()>
    Function strikethrough(ByVal input As String)
        Return "<s>" + input + "</s>"
    End Function
    <Extension()>
    Function spoiler(ByVal input As String)
        Return "<span class='tg-spoiler'>" + input + "</span>"
    End Function
    <Extension()>
    Function inlineURL(ByVal link As String, ByVal text As String)
        Return "<a href='" + link + "'>" + text + "</a>"
    End Function
    <Extension()>
    Function inlineFixedWidth(ByVal input As String)
        Return "<code>" + input + "</code>"
    End Function
    <Extension()>
    Function fixedWidth(ByVal input As String)
        Return "<pre>" + input + "</pre>"
    End Function
    <Extension()>
    Function newline(ByVal input As String)
        Return input + "%0A"
    End Function

    Function HTMLReplace(ByVal input As String)
        Return input.Replace("<", "&lt").Replace(">", "&gt").Replace("&", "&amp")
    End Function
End Module
