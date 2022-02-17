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
        If (descriptionType = "full" Or descriptionType = "") Then
            telecom.SendTelegramMessage("List of commands:".bold.italic.newline +
                                        "/help - used to receive list of commands.".newline + "Syntax:".newline +
                                        "/help or /help full - full list of commands".newline +
                                        "/help short - short list of commands")
        ElseIf (descriptionType = "short") Then
            telecom.SendTelegramMessage("List of commands:".bold.italic.newline + "/help")
        End If
    End Sub

    Sub Loaded() Handles Me.Load
        objCol.Add(New CCommandObject("help", Me, "help", vbMethod))
    End Sub

    Private Sub GetRawUpdates_Click(sender As Object, e As EventArgs) Handles GetRawUpdates.Click
        RichTextBox1.Text = telecom.GetRawUpdate()
    End Sub

    Private Sub SendKeyboardButton_Click(sender As Object, e As EventArgs) Handles SendKeyboardButton.Click
        Dim keyboard As New cbBtn({
                                    New cBtn("Help", 1, "/help"), New cBtn("Help short", 1, "/help short"), New cBtn("Help full", 1, "/help full"),
                                    New cBtn("btn5", 2, "1234"), New cBtn("btn6", 2, "1234")})
        RichTextBox1.Text = telecom.sendTelegramInlineKeyboard(keyboard, InputBox.Text.Replace("%newline", "".newline)).Text
    End Sub
End Class
