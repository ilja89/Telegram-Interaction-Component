Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

''' <summary>
''' Class to create instance of Telegram bot
''' </summary>
Public Class CTelegramBot
    Private telegramBotName As String = ""
    Private telegramBotToken As String = "5207145300:AAGgsp2QZy7L6Ee-63zzLLHtyEMwIe84nhM" 'DEBUG!
    Private telegramChatID As String = "698249543" 'DEBUG!
    Private telegramBotSet As Boolean = True 'DEBUG!
    Private telMessageOffset As Integer = 0

    ''' <summary>
    ''' Name of bot
    ''' </summary>
    ''' <returns></returns>
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

    ''' <summary>
    ''' Bot token
    ''' </summary>
    ''' <returns></returns>
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

    ''' <summary>
    ''' Id of chat into what bot will be sending messages
    ''' </summary>
    ''' <returns></returns>
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

    ''' <summary>
    ''' Create new bot
    ''' </summary>
    ''' <param name="botName"></param>
    ''' <param name="botToken"></param>
    ''' <param name="chatID"></param>
    Public Sub New(botName As String, botToken As String, chatID As String)
        If (botName <> "" And botToken <> "" And chatID <> "") Then
            telegramBotName = botName
            telegramBotToken = botToken
            telegramChatID = chatID
            telegramBotSet = True
        End If
    End Sub
    Public Sub New()

    End Sub
    Public Sub SetBot(botName As String, botToken As String, chatID As String)
        If (botName <> "" And botToken <> "" And chatID <> "") Then
            telegramBotName = botName
            telegramBotToken = botToken
            telegramChatID = chatID
            telegramBotSet = True
        End If
    End Sub


    ''' <summary>
    ''' Function to send messages to Telegram<br/>
    ''' Returns json response with information about message sent<br/>
    '''<br/>
    ''' Error codes:<br/>
    ''' "noMessage" - empty input string<br/>
    ''' "botNotSet" - bot is not set (checks "Private telegramBotSet As Boolean")<br/>
    ''' </summary>
    ''' <param name="message"></param>
    ''' <returns>Instance of &lt;<see cref="CTelegramResponse"/>&gt;</returns>
    Function SendTelegramMessage(ByVal message As String) As CTelegramResponse
        Dim response As New CTelegramResponse
        If (telegramBotSet = True) Then
            If (message.Length = 0) Then
                response.SetError = "noMessage"
                Return response
            End If

            Dim xmlhttp As New MSXML2.XMLHTTP60, url As String
            message = SpecialFormat(message)
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


    ''' <summary>
    ''' This is function to send inline keyboards in Telegram<br/>
    ''' It request:<br/>
    ''' - Instance of &lt;<see cref="CInlineKeyboardButtonBuilder"/>&gt; to build up a keyboard json<br/>
    ''' - Optionally a <paramref name="message"></paramref> what will be sent together with keyboard<br/>
    '''<br/>
    ''' Example:<br/><c>
    ''' Dim keyboard As New TelegramBotLib.CInlineKeyboardButtonBuilder({<br/>
    ''' ___New TelegramBotLib.CInlineKeyboardButton("Help", 1, "/help"),<br/>
    ''' ___New TelegramBotLib.CInlineKeyboardButton("Help short", 1, "/help short"),<br/>
    ''' ___New TelegramBotLib.CInlineKeyboardButton("Help full", 1, "/help full"),<br/>
    ''' ___New TelegramBotLib.CInlineKeyboardButton("btn5", 2, "1234"),<br/>
    ''' ___New TelegramBotLib.CInlineKeyboardButton("btn6", 2, "1234")})<br/>
    ''' RichTextBox1.Text = TelegramBotLib.CTelegramBot.sendTelegramInlineKeyboard
    ''' (InputBox.Text.Replace("%newline", "".newline), keyboard).Text</c>
    ''' </summary>
    ''' <param name="keyboard"></param>
    ''' <param name="message"></param>
    ''' <returns>Instance of &lt;<see cref="CTelegramResponse"/>&gt;</returns>
    Function sendTelegramInlineKeyboard(keyboard As CInlineKeyboardButtonBuilder, Optional message As String = "Keyboard:") As CTelegramResponse
        Dim response As New CTelegramResponse
        If (telegramBotSet = True) Then
            If (message = "") Then
                message = "Keyboard:"
            End If

            Dim xmlhttp As New MSXML2.XMLHTTP60, url As String
            message = SpecialFormat(message)
            url = "https://api.telegram.org/bot" & BotToken & "/sendMessage?parse_mode=HTML&chat_id=" & ChatID & "&text=" & message &
                "&reply_markup=" + keyboard.Build
            xmlhttp.open("POST", url, False)
            xmlhttp.send()
            response.Text = xmlhttp.responseText
            Return response
        Else
            response.SetError = "botNotSet"
            Return response
        End If

    End Function


    ''' <summary>
    ''' Function to receive messages from Telegram<br/>
    ''' Returns text of first message from array of messages received after message with 
    ''' id = <paramref name="optionalOffset"/> (if set) (telMessageOffset)<br/>
    ''' <br/>
    ''' Error codes:<br/>
    ''' "empty" - message request is empty, no messages received since last message<br/>
    ''' "botNotSet" - bot is not set (checks "Private telegramBotSet As Boolean")<br/>
    ''' </summary>
    ''' <param name="optionalOffset"></param>
    ''' <returns>Instance of &lt;<see cref="CTelegramResponse"/>&gt;</returns>
    Function GetUpdate(ByVal Optional optionalOffset As Integer = -1) As CTelegramResponse
        Dim response As CTelegramResponse
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
            ' Check for standart text message or keyboard pressed
            response = New CTelegramResponse(json)
            If (json.Exists("result", 0).Exists("update_id") IsNot Nothing) Then
                telMessageOffset = CInt(json.Exists("result", 0).Exists("update_id")) + 1
            End If
            Return response
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Get raw update JSON
    ''' </summary>
    ''' <param name="optionalOffset"></param>
    ''' <returns>JSON string</returns>
    Function GetRawUpdate(ByVal Optional optionalOffset As Integer = -1)
        Dim response As CTelegramResponse
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
            response = New CTelegramResponse(json)
            Return json.ToString
        End If
        Return False
    End Function

    ''' <summary>
    ''' Turns some tags in raw text to HTML format
    ''' </summary>
    ''' <param name="message"></param>
    ''' <returns></returns>
    Private Function SpecialFormat(message As String)
        Return message.
            Replace("%b", "<b>").                       ' bold
            Replace("%/b", "</b>").
            Replace("%i", "<i>").                       ' italic
            Replace("%/i", "</i>").
            Replace("%u", "<u>").                       ' underlined
            Replace("%/u", "</u>").
            Replace("%sp", "<span class='tg-spoiler'>").' spoiler
            Replace("%/sp", "</span>").
            Replace("%s", "<s>").                       ' strikethrough
            Replace("%/s", "</s>").
            Replace("%c", "<code>").                    ' code inline
            Replace("%/c", "</code>").
            Replace("%p", "<pre>").                     ' code
            Replace("%/p", "</pre>").
            Replace("%newline", "%0A")                  ' new line

    End Function
End Class