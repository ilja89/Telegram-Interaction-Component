Imports TelegramBotLib
Public Class Form1
    Private telecom As New TelegramBotLib.CTBot
    Private cObjCollection As New Collection
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Send.Click
        RichTextBox1.Text = telecom.SendTelegramMessage(RichTextBoxInput.Text).Text
    End Sub

    Private Sub GetUpdates_Click(sender As Object, e As EventArgs) Handles GetUpdates.Click
        RichTextBox1.Text = telecom.GetUpdate().Text
    End Sub

    Function test(cObject As Object)
        RichTextBox1.Text = "Delegated Function" + vbCrLf +
        cObject.Command.ToString + vbCrLf +
        cObject.FunctionCallType.ToString + vbCrLf +
        cObject.FunctionName.ToString + vbCrLf +
        cObject.IsCreated.ToString + vbCrLf +
        cObject.ObjectRef.ToString()
        Return 111
    End Function

    Sub Loaded() Handles Me.Load
        Dim cObject As New TelegramBotLib.commandObject("/help", Me, "test", vbMethod)
        cObjCollection.Add(cObject, cObject.Command)
        cObjCollection.Item("/help").CallCommand(cObjCollection.Item("/help"))
    End Sub
End Class
