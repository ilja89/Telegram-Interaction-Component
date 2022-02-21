Imports TelegramBotLib
Imports TelegramBotLib.CMarkup
Imports TelegramBotLib.CCommandObject
Imports TelegramBotLib.CCommandObjectCollection
Imports cbBtn = TelegramBotLib.CInlineKeyboardButtonBuilder
Imports cBtn = TelegramBotLib.CInlineKeyboardButton
Public Class Form1
    Private telecom As New TelegramBotLib.CTelegramBot
    Private objCol As New CCommandObjectCollection
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Send.Click
        RichTextBox1.Text = telecom.SendTelegramMessage(RichTextBoxInput.Text).Text
    End Sub

    Private Sub GetUpdates_Click(sender As Object, e As EventArgs) Handles GetUpdates.Click
        Dim response As CTelegramResponse = telecom.GetUpdate()
        If (response.IsCommand) Then
            objCol.Item(response.Command).CallCommand({response.CommandText})
        End If
        RichTextBox1.Text = response.Text
    End Sub

    Sub help(Optional descriptionType As String = "full")
        Dim send As String = "List of commands:".bold.italic
        Dim i As Integer = 1
        If (descriptionType = "full" Or descriptionType = "") Then
            While (i <= objCol.Count)
                send = send.newline + objCol.Item(i).FullDescription
                i = i + 1
            End While
        ElseIf (descriptionType = "short") Then
            While (i <= objCol.Count)
                send = send.newline + objCol.Item(i).ShortDescription
                i = i + 1
            End While
        ElseIf (descriptionType = "list") Then
            While (i <= objCol.Count)
                send = send.newline + "/" + objCol.Item(i).Command
                i = i + 1
            End While
        End If
        telecom.SendTelegramMessage(send)
    End Sub

    Sub Loaded() Handles Me.Load
        objCol.Add(New CCommandObject("help", Me, "help", vbMethod,
                                      "/help - used to receive list of commands",
                                      "/help - used to receive list of commands.".
                                      newline + "Syntax:".
                                      newline.tab + "/help or /help full - full list of commands".
                                      newline.tab + "/help short - short list of commands"))
    End Sub

    Private Sub GetRawUpdates_Click(sender As Object, e As EventArgs) Handles GetRawUpdates.Click
        RichTextBox1.Text = telecom.GetRawUpdate()
    End Sub

    Private Sub SendKeyboardButton_Click(sender As Object, e As EventArgs) Handles SendKeyboardButton.Click
        Dim keyboard As New cbBtn({
                                    New cBtn("Help list", 1, "/help list"),
                                    New cBtn("Help short", 2, "/help short"), New cBtn("Help full", 2, "/help full"),
                                    New cBtn("Example Button", 3, "/help")})
        RichTextBox1.Text = telecom.sendTelegramInlineKeyboard(keyboard, RichTextBox1.Text.Replace("%newline", "".newline)).Text
    End Sub
End Class
