' Public class, purpose of what is to store received Telegram message in form of regular message or command
' IMPORTANT - after once set, any object properties can't be changed, only read
'
' Example of use
' Dim response As New CTelegramResponse
' Dim anyReceivedTelegramMessage As String = "/help 123"
'!Then:
'!1. To set regular message or command (auto detected, if message starts from "/" or "\"), use:
' response.Text = anyReceivedTelegramMessage
'!This case responseText will receive "/help 123". If this is command, additionally responseCommand will receive "help",
'!responseCommandText will receive "123", responseIsCommand will be set on "True" and changeLocked will be set on "True"
'
'!2. To set error message, use:
' response.SetError = anyReceivedTelegramMessage
'!This case responseIsError will be set on "True", responseText will be set on "/help 123" and
'!changeLocked will be set on "True"
Public Class CTelegramResponse
    Private responseText As String = ""             ' Response text
    Private responseCommand As String = ""          ' Response command
    Private responseCommandText As String = ""      ' Response command parameter
    Private responseIsCommand As Boolean = False    ' Shows if response is command or not
    Private responseIsError As Boolean = False      ' Shows if response is error or not
    Private changeLocked As Boolean = False         ' Shows if response change is locked or not
    Public Property Text As String
        Get
            Return responseText
        End Get
        Set(ByVal value As String)
            If (changeLocked = False) Then
                changeLocked = True
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
    Public WriteOnly Property SetError As String
        Set(ByVal errorCode As String)
            changeLocked = True
            responseIsError = True
            responseIsCommand = False
            responseText = errorCode
        End Set
    End Property


End Class