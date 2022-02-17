Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class CTBot
    Private telegramBotName As String = ""
    Private telegramBotToken As String = "5207145300:AAGgsp2QZy7L6Ee-63zzLLHtyEMwIe84nhM" 'DEBUG!
    Private telegramChatID As String = "698249543" 'DEBUG!
    Private telegramBotSet As Boolean = True 'DEBUG!
    Private telMessageOffset As Integer = 0
    Property BotName As String
        Get
            If (telegramBotName = "") Then
                Return "error"
            Else
                Return telegramBotName
            End If
        End Get
        Set(value As String)
            telegramBotName = value
        End Set
    End Property
    Property BotToken As String
        Get
            If (telegramBotToken = "") Then
                Return "error"
            Else
                Return telegramBotToken
            End If
        End Get
        Set(value As String)
            telegramBotToken = value
        End Set
    End Property
    Property ChatID As String
        Get
            If (telegramChatID = "") Then
                Return "error"
            Else
                Return telegramChatID
            End If
        End Get
        Set(value As String)
            telegramChatID = value
        End Set
    End Property
    Sub SetBot(botName As String, botToken As String, chatID As String)
        If (botName <> "" And botToken <> "" And chatID <> "") Then
            telegramBotName = botName
            telegramBotToken = botToken
            telegramChatID = chatID
            telegramBotSet = True
        End If
    End Sub

    ' Function to send messages to Telegram
    ' Returns json response with information about message sent
    ' Input = String
    ' Output = String (JSON)
    '
    ' Error codes:
    ' "noMessage" - empty input string
    ' "botNotSet" - bot is not set (checks "Private telegramBotSet As Boolean")
    Function SendTelegramMessage(ByVal message As String)
        Dim response As New telResponse
        If (telegramBotSet = True) Then
            If (message.Length = 0) Then
                response.SetError = "noMessage"
                Return response
            End If

            Dim xmlhttp As New MSXML2.XMLHTTP60, url As String

            url = "https://api.telegram.org/bot" & BotToken & "/sendMessage?parse_mode=HTML&chat_id=" & ChatID & "&text=" & message
            xmlhttp.open("POST", url, False)
            xmlhttp.send()
            response.Text = xmlhttp.responseText
            Return response
        Else
            response.SetError = "botNotSet"
            Return response
        End If
    End Function

    ' Function to receive messages from Telegram
    ' Returns text of first message from array of messages received after message with id = optionalOffset (if set) or telMessageOffset
    ' Input = Optional Integer
    ' Output = String
    ' 
    ' Error codes:
    ' "empty" - message request is empty, no messages received since last message
    ' "botNotSet" - bot is not set (checks "Private telegramBotSet As Boolean")
    Function GetUpdate(ByVal Optional optionalOffset As Integer = -1)
        Dim response As New telResponse
        If (telegramBotSet = True) Then
            Dim xmlhttp As New MSXML2.XMLHTTP60, url As String

            If (optionalOffset > -1) Then
                url = "https://api.telegram.org/bot" & BotToken & "/getUpdates?offset=" + optionalOffset.ToString
            Else
                url = "https://api.telegram.org/bot" & BotToken & "/getUpdates?offset=" + telMessageOffset.ToString
            End If
            xmlhttp.open("POST", url, False)
            xmlhttp.send()

            Dim json As JObject = JObject.Parse(xmlhttp.responseText)
            If (json.SelectToken("result").Count > 0) Then
                If (json.SelectToken("result")(0).SelectToken("update_id")) Then
                    telMessageOffset = CInt(json.SelectToken("result")(0).SelectToken("update_id")) + 1
                End If
                response.Text = json.SelectToken("result")(0).SelectToken("message").SelectToken("text")
                Return response
            Else
                response.SetError = "empty"
                Return response
            End If
        Else
            response.SetError = "botNotSet"
            Return response
        End If
    End Function
End Class

' This class is used to link together any custom Telegram command and function, what handles it
' Using this object function with name "commandObjectFunctionName" what exists in environment
' "commandObjectObjectRef" (Class, for example) can be called with ease using this object function
' "CallCommand", what gets array of optional parameters, and returns result of called function with
' "commandObjectFunctionName" name. 
Public Class commandObject
    Private commandObjectObjectRef As Object = Nothing          ' Environment inside what aim function will be searched
    Private commandObjectFunctionName As String = Nothing       ' Aim function name
    Private commandObjectFunctionCallType As CallType = Nothing ' Aim function call type
    Private commandObjectCommand As String = Nothing            ' A Telegram command, to what this function is related
    Private commandObjectCreated As Boolean = False             ' Shows if object is correctly created
    Public ReadOnly Property Command As String
        Get
            If (commandObjectCreated) Then
                Return commandObjectCommand
            Else
                Return Nothing
            End If
        End Get
    End Property
    Public ReadOnly Property IsCreated As Boolean
        Get
            If (commandObjectCreated) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property
    Public ReadOnly Property ObjectRef As Object
        Get
            If (commandObjectCreated) Then
                Return commandObjectObjectRef
            Else
                Return Nothing
            End If
        End Get
    End Property
    Public ReadOnly Property FunctionName As String
        Get
            If (commandObjectCreated) Then
                Return commandObjectFunctionName
            Else
                Return Nothing
            End If
        End Get
    End Property
    Public ReadOnly Property FunctionCallType As CallType
        Get
            If (commandObjectCreated) Then
                Return commandObjectFunctionCallType
            Else
                Return Nothing
            End If
        End Get
    End Property
    Public Sub New(
                  ByVal constructorCommand As String,
                  ByRef constructorObjectRef As Object,
                  ByVal constructorFunctionName As String,
                  ByVal constructorFunctionCallType As CallType)
        If (
        constructorCommand IsNot Nothing And
        constructorObjectRef IsNot Nothing And
        constructorFunctionName IsNot Nothing And
        constructorFunctionCallType <> Nothing
        ) Then
            commandObjectCommand = constructorCommand
            commandObjectObjectRef = constructorObjectRef
            commandObjectFunctionCallType = constructorFunctionCallType
            commandObjectFunctionName = constructorFunctionName
            commandObjectCreated = True
        Else
            Exit Sub
        End If
    End Sub
    Public Function CallCommand(ParamArray Args As Object())
        If (commandObjectCreated) Then
            Return CallByName(ObjectRef, FunctionName, FunctionCallType, Args)
        Else
            Return Nothing
        End If
    End Function
End Class

Public Class telResponse
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
                responseText = value
                If (value = "") Then
                    responseIsCommand = False
                    responseCommand = ""
                    responseCommandText = ""
                Else
                    If ((value.Chars(1) = "/" Or value.Chars(1) = "\") And responseIsError = False) Then
                        Dim parts As Array = value.Replace("/", "").Replace("\", "").Split(" ")
                        responseIsCommand = True
                        responseCommand = parts(0)
                        responseCommandText = parts(1)
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