
''' <summary>
''' This class is used for inline buttons builder<br/>
''' With this you can add buttons to your messages<br/>
''' To do this, and get ready-to-send JSON for keyboard, you have to create new instance of &lt;<see cref="CInlineKeyboardButtonBuilder"/>&gt;,
''' sending array of buttons inside as parameter. Then you can use &lt;<see cref="CInlineKeyboardButtonBuilder.Build"/>&gt; to get JSON.<br/><br/>
''' <c>
''' Example:<br/>
''' Dim buttons As New CInlineKeyboardButtonBuilder({<br/>
''' ___New CInlineKeyboardButton("btn1", 1, "btn2"), New CInlineKeyboardButton("btn2", 1, "btn2"),<br/>
''' ___New CInlineKeyboardButton("btn3", 1, "btn2"),New CInlineKeyboardButton("btn4", 2, "btn2"),<br/>
''' ___New CInlineKeyboardButton("btn5", 2, "btn2"), New CInlineKeyboardButton("btn6", 2, "btn2")})<br/>
''' Dim builtJson As String = buttons.Build<br/>
''' Then you can add this builtJson after "&amp;reply_markup="
''' </c>
''' </summary>
Public Class CInlineKeyboardButtonBuilder
    Private Property buttonCollection As Collection ' Collection of buttons of CInlineKeyboardButton class
    Public Sub New(buttonArray As Array)
        buttonCollection = buttonArray.ToCollection
    End Sub
    ' Function to turn button collection into inline keyboard json
    Public Function Build() As String
        Dim i As Integer = 1
        Dim curLayer As New Collection
        Dim result As String = "{""inline_keyboard"":["
        While (i <= buttonCollection.Count)
            Dim item As CInlineKeyboardButton = buttonCollection.Item(i)
            curLayer.Add(buttonCollection.Item(i))

            ' If there is next button and current layer is not last, then build layer
            If (i + 1 < buttonCollection.Count) Then
                Dim nextItem As CInlineKeyboardButton = buttonCollection.Item(i + 1)
                If (item.Layer <> nextItem.Layer) Then
                    result = result + BuildLayer(curLayer) + ","
                    curLayer.Clear()
                End If
                ' If there are no other buttons anymore and this is last, build layer
            ElseIf (i = buttonCollection.Count) Then
                result = result + BuildLayer(curLayer)
                curLayer.Clear()
            End If

            i = i + 1
        End While
        result = result + "]}"
        Return result
    End Function
    ' Function to build one row (layer) of button objects CInlineKeyboardButton into json string
    Private Function BuildLayer(curLayer As Collection) As String
        Dim i As Integer = 1
        Dim result As String = "["
        While (i <= curLayer.Count)
            Dim button As CInlineKeyboardButton = curLayer(i)
            result = result + button.Build
            If (i < curLayer.Count) Then
                result = result + ","
            End If
            i = i + 1
        End While
        result = result + "]"
        Return result
    End Function

End Class


''' <summary>
''' This class is used to describe instances of Telegram inline buttons
''' </summary>
Public Class CInlineKeyboardButton
    Private buttonText As String = Nothing
    Private buttonLayer As Integer = Nothing
    Private buttonURL As String = Nothing
    Private buttonCallBackData As String = Nothing

    ''' <summary>
    ''' Number of layer, on what this button is placed
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Layer As String
        Get
            Return buttonLayer
        End Get
    End Property
    ''' <summary>
    ''' Button text
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Text As String
        Get
            Return """text"": """ + buttonText + """"
        End Get
    End Property

    ''' <summary>
    ''' Function to build a Json string out of button instance &lt;<see cref="CInlineKeyboardButton"/>&gt;
    ''' </summary>
    ''' <returns></returns>
    Public Function Build() As String
        Dim result As String = "{"
        result = result + """text"": """ + buttonText + """"
        If (buttonURL <> Nothing) Then
            result = result + ",""url"": """ + buttonURL + """"
        End If
        If (buttonCallBackData <> Nothing) Then
            result = result + ",""callback_data"": """ + buttonCallBackData + """"
        End If
        result = result + "}"
        Return result
    End Function

    ''' <summary>
    ''' <paramref name="text"/> - text of new button<br/>
    ''' <paramref name="layer"/> - layer of new button<br/>
    ''' <paramref name="callBackData"/> - text what will receive Telegram bot when button is pressed<br/>
    ''' <paramref name="URL"/> - Optionally, URL of button. User will be sent on this URL when button is pressed
    ''' </summary>
    ''' <param name="text"></param>
    ''' <param name="layer"></param>
    ''' <param name="callBackData"></param>
    ''' <param name="URL"></param>
    Public Sub New(text As String, layer As Integer, callBackData As String, Optional URL As String = Nothing)
        buttonText = text
        buttonLayer = layer
        If (URL <> Nothing) Then
            buttonURL = URL
        End If
        If (callBackData <> Nothing) Then
            buttonCallBackData = callBackData
        End If
    End Sub
End Class