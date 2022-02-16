Imports TelegramBotLib
Public Class Form1
    Private telecom As New TelegramBotLib.CTBot
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Send.Click
        RichTextBox1.Text = telecom.SendTelegramMessage(RichTextBoxInput.Text).Text
    End Sub

    Private Sub GetUpdates_Click(sender As Object, e As EventArgs) Handles GetUpdates.Click
        Dim a As New telResponse
        a.SetError = "error"
        If (RichTextBoxInput.Text = "") Then
            RichTextBox1.Text = telecom.GetUpdate().Text
        Else
            RichTextBox1.Text = telecom.GetUpdate(CInt(RichTextBoxInput.Text)).Text
        End If
    End Sub
End Class
