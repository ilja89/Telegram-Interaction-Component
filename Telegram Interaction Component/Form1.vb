Imports TelegramBotLib
Imports TelegramBotLib.CMarkup
Imports TelegramBotLib.CCommandObject
Imports TelegramBotLib.CCommandObjectCollection
Public Class Form1
    Private telecom As New TelegramBotLib.CTBot
    Private objCol As New CCommandObjectCollection
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Send.Click
        RichTextBox1.Text = telecom.SendTelegramMessage(RichTextBoxInput.Text).Text
    End Sub

    Private Sub GetUpdates_Click(sender As Object, e As EventArgs) Handles GetUpdates.Click
        Dim response As CTelegramResponse = telecom.GetUpdate()
        If (objCol.Contains(response.Command)) Then
            objCol.Item(response.Command).CallCommand()
        End If
        RichTextBox1.Text = response.Text
    End Sub

    Function help()
        telecom.SendTelegramMessage("List of commands:".bold.italic.newline + "/help")
    End Function

    Sub Loaded() Handles Me.Load
        Dim obj1 As New CCommandObject("help", Me, "help", vbMethod)
        objCol.Add(obj1)
    End Sub
End Class
