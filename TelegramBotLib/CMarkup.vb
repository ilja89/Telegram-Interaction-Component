' FILENAME: CMarkup.vb
' AUTHOR: El Plan : Ilja Kuznetsov.
' CREATED: 16.02.2022
' CHANGED: 22.02.2022
'
' DESCRIPTION: See below↓↓↓

' Related components: CTelegramBot

Imports System.Runtime.CompilerServices

''' <summary>
''' Extension module for Telegram HTML parse mode<br/>
''' Adds some text style modifiers<br/>
''' <br/>
''' Example of use:<br/><c>
''' Dim message As String = "text"<br/>
''' Dim link As String = "www.anysite.com/something.html"<br/>
''' message.bold<br/>
''' link.inlineURL("This is inline URL")<br/></c>
''' </summary>
Public Module CMarkup
    ''' <summary>
    ''' Makes text bold
    ''' </summary>
    ''' <param name="input"></param>
    ''' <returns></returns>
    <Extension()>
    Function bold(ByVal input As String) As String
        input.HTMLReplace
        Return "<b>" + input + "</b>"
    End Function

    ''' <summary>
    ''' Makes text italic
    ''' </summary>
    ''' <param name="input"></param>
    ''' <returns></returns>
    <Extension()>
    Function italic(ByVal input As String) As String
        input.HTMLReplace
        Return "<i>" + input + "</i>"
    End Function

    ''' <summary>
    ''' Makes text underlined
    ''' </summary>
    ''' <param name="input"></param>
    ''' <returns></returns>
    <Extension()>
    Function underlined(ByVal input As String) As String
        input.HTMLReplace
        Return "<u>" + input + "</u>"
    End Function

    ''' <summary>
    ''' Makes text strikethrough
    ''' </summary>
    ''' <param name="input"></param>
    ''' <returns></returns>
    <Extension()>
    Function strikethrough(ByVal input As String) As String
        input.HTMLReplace
        Return "<s>" + input + "</s>"
    End Function

    ''' <summary>
    ''' Creates spoiler
    ''' </summary>
    ''' <param name="input"></param>
    ''' <returns></returns>
    <Extension()>
    Function spoiler(ByVal input As String) As String
        input.HTMLReplace
        Return "<span class='tg-spoiler'>" + input + "</span>"
    End Function

    ''' <summary>
    ''' Makes hyperlink with visible text <paramref name="text"/> and link <paramref name="link"/><br/>
    ''' Example:<br/><c>
    ''' link.inlineURL("This is inline URL")</c>
    ''' </summary>
    ''' <param name="link"></param>
    ''' <param name="text"></param>
    ''' <returns></returns>
    <Extension()>
    Function inlineURL(ByVal link As String, ByVal text As String) As String
        link.HTMLReplace
        text.HTMLReplace
        Return "<a href='" + link + "'>" + text + "</a>"
    End Function

    ''' <summary>
    ''' Fixed width character text
    ''' </summary>
    ''' <param name="input"></param>
    ''' <returns></returns>
    <Extension()>
    Function inlineFixedWidth(ByVal input As String) As String
        input.HTMLReplace
        Return "<code>" + input + "</code>"
    End Function

    ''' <summary>
    ''' Fixed width character text
    ''' </summary>
    ''' <param name="input"></param>
    ''' <returns></returns>
    <Extension()>
    Function fixedWidth(ByVal input As String) As String
        input.HTMLReplace
        Return "<pre>" + input + "</pre>"
    End Function

    ''' <summary>
    ''' Adds new line into Telegram message<br/>
    ''' Example:<br/><c>
    ''' Dim message1 As String = "This is line 1"<br/>
    ''' Dim message2 As String = "This is line 2"<br/>
    ''' message1.newline + message2<br/></c>
    '''<br/>
    ''' Will result into:<br/>
    ''' This is line 1<br/>
    ''' This is line 2<br/>
    ''' </summary>
    ''' <param name="input"></param>
    ''' <returns></returns>
    <Extension()>
    Function newline(ByVal input As String) As String
        input.HTMLReplace
        Return input + "%0A"
    End Function


    ''' <summary>
    ''' If there are any symbols in message like &quot;&lt;&quot;,&quot;&gt;&quot;,&quot;&amp;&quot;,
    ''' they must be replaced with special HTML codes<br/>
    ''' This is requred because this case Telegram message is sent in HTML encoding
    ''' </summary>
    ''' <param name="input"></param>
    ''' <returns></returns>
    <Extension()>
    Function HTMLReplace(ByVal input As String) As String
        Return input.Replace("<", "&lt").Replace(">", "&gt").Replace("&", "&amp")
    End Function

    ''' <summary>
    ''' Pseudo-tabulation
    ''' </summary>
    ''' <param name="input"></param>
    ''' <returns></returns>
    <Extension()>
    Function tab(input As String) As String
        Return input + "   "
    End Function
End Module
