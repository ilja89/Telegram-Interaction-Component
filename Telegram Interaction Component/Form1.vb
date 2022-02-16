Imports TelegramBotLib
Public Class Form1
    Private telecom As New TelegramBotLib.CTBot
    Private cObjects As New Collection
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Send.Click
        RichTextBox1.Text = telecom.SendTelegramMessage(RichTextBoxInput.Text).Text
    End Sub

    Private Sub GetUpdates_Click(sender As Object, e As EventArgs) Handles GetUpdates.Click
        RichTextBox1.Text = telecom.GetUpdate().Text
    End Sub

    Function test()

    End Function

    Sub Loaded() Handles Me.Load
        Dim cObject As New TelegramBotLib.CommandObject(AddressOf test, "/help")
        cObjects.Add()
    End Sub
End Class
