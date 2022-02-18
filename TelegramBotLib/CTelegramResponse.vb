
''' <summary>
''' Public class, purpose of what is to store received Telegram message in form of regular message or command<br/>
''' After once set, any object properties can't be changed, only read<br/>
'''<br/>
''' Example of use:<br/><c>
''' Dim response As New CTelegramResponse<br/>
''' Dim anyReceivedTelegramMessage As String = "/help 123"<br/></c>
''' Then:<br/>
''' 1. To set regular message or command (auto detected, if message starts from "/" or "\"), use:<br/><c>
''' response.Text = anyReceivedTelegramMessage<br/></c>
''' This case Text will receive "/help 123". If this is command, additionally Command will receive "help",
''' CommandText will receive "123", IsCommand will be set on "True" and changeLocked will be set on "True"<br/>
''' <br/>
''' 2. To set error message, use:<br/><c>
''' response.SetError = anyReceivedTelegramMessage<br/></c>
''' This case IsError will be set on "True", Text will be set on "/help 123" and
''' changeLocked will be set on "True"<br/>
''' </summary>
Public Class CTelegramResponse
    Private responseText As String = ""             ' Response text
    Private responseCommand As String = ""          ' Response command
    Private responseCommandText As String = ""      ' Response command parameter
    Private responseIsCommand As Boolean = False    ' Shows if response is command or not
    Private responseIsError As Boolean = False      ' Shows if response is error or not
    Private changeIsLocked As Boolean = False       ' Shows if response change is locked or not
    Public Property Text As String
        Get
            Return responseText
        End Get
        Set(ByVal value As String)
            If (changeIsLocked = False) Then
                changeIsLocked = True
                responseText = value
                If (value = "") Then
                    responseIsCommand = False
                    responseCommand = ""
                    responseCommandText = ""
                Else
                    If ((value.Chars(0) = "/" Or value.Chars(0) = "\") And responseIsError = False) Then
                        Dim parts As Array = value.Replace("/", "").Replace("\", "").Split(" ")
                        responseIsCommand = True
                        responseCommand = parts(0)
                        If (parts.Length > 1) Then
                            responseCommandText = parts(1)
                        End If
                    End If
                End If
            End If
        End Set
    End Property
    Public ReadOnly Property IsCommand As Boolean
        Get
            Return responseIsCommand
        End Get
    End Property
    Public ReadOnly Property IsError As Boolean
        Get
            Return responseIsError
        End Get
    End Property
    Public ReadOnly Property Command As String
        Get
            If (responseIsCommand) Then
                Return responseCommand
            Else
                Return Text
            End If
        End Get
    End Property
    Public ReadOnly Property CommandText As String
        Get
            If (responseIsCommand) Then
                Return responseCommandText
            Else
                Return Text
            End If
        End Get
    End Property
    Public ReadOnly Property IsLocked As String
        Get
            Return changeIsLocked
        End Get
    End Property
    Public WriteOnly Property SetError As String
        Set(ByVal errorCode As String)
            changeIsLocked = True
            responseIsError = True
            responseIsCommand = False
            responseText = errorCode
        End Set
    End Property
    Public Sub New(json As Newtonsoft.Json.Linq.JObject)
        If (json.Exists("result", 0).Exists("update_id") IsNot Nothing) Then
            If (json.Exists("result", 0).Exists("message").Exists("text") IsNot Nothing) Then
                Text = json.Exists("result", 0).Exists("message").Exists("text")
            ElseIf (json.Exists("result", 0).Exists("callback_query").Exists("data") IsNot Nothing) Then
                Text = json.Exists("result", 0).Exists("callback_query").Exists("data")
            End If
        Else
            SetError = "empty"
        End If
        changeIsLocked = True
    End Sub
    Public Sub New()

    End Sub

End Class