' FILENAME: CInlineKeyboardButtonBuilder.vb
' AUTHOR: El Plan : Ilja Kuznetsov.
' CREATED: 17.02.2022
' CHANGED: 22.02.2022
'
' DESCRIPTION: See below↓↓↓

' Related components: CInlineKeyboardButtonBuilder

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
    ''' <paramref name="URL"/> - Optionally, URL of button. User will be sent to this URL when button is pressed
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