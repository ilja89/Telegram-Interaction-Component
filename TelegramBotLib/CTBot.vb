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
        Dim response As New CTelegramResponse
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
        Dim response As New CTelegramResponse
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
            Else
                response.SetError = "empty"
            End If
        Else
            response.SetError = "botNotSet"
        End If
        Return response
    End Function
End Class