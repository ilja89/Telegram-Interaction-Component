' This class is used for inline buttons builder
' With this you can add buttons to your messages
' To do this, and get ready-to-send JSON for keyboard, you have to create new instance of CInlineKeyboardButtonBuilder,
' sending array of buttons inside as parameter. Then you can use CInlineKeyboardButtonBuilder.Build to get JSON.
' Example:
' Dim buttons As New CInlineKeyboardButtonBuilder({
'                           New CInlineKeyboardButton("btn1", 1, "btn2"), New CInlineKeyboardButton("btn2", 1, "btn2"),
'                           New CInlineKeyboardButton("btn3", 1, "btn2"),New CInlineKeyboardButton("btn4", 2, "btn2"),
'                           New CInlineKeyboardButton("btn5", 2, "btn2"), New CInlineKeyboardButton("btn6", 2, "btn2")})
' Dim builtJson As String = buttons.Build
' Then you can add this builtJson after "&reply_markup="
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

' This class is used to describe instances of Telegram inline buttons
Public Class CInlineKeyboardButton
    Private buttonText As String = Nothing
    Private buttonLayer As Integer = Nothing
    Private buttonURL As String = Nothing
    Private buttonCallBackData As String = Nothing
    ' Number of layer, on what this button is placed
    Public ReadOnly Property Layer As String
        Get
            Return buttonLayer
        End Get
    End Property
    ' Button text
    Public ReadOnly Property Text As String
        Get
            Return """text"": """ + buttonText + """"
        End Get
    End Property
    ' Function to build a Json string out of button
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