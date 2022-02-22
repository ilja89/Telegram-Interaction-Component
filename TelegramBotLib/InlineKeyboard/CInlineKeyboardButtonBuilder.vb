' FILENAME: CInlineKeyboardButtonBuilder.vb
' AUTHOR: El Plan : Ilja Kuznetsov.
' CREATED: 17.02.2022
' CHANGED: 22.02.2022
'
' DESCRIPTION: See below↓↓↓

' Related components: CInlineKeyboardButton, CTelegramBot

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
            If (i + 1 <= buttonCollection.Count) Then
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