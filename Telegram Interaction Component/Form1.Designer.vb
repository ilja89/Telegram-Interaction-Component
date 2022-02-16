<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.Send = New System.Windows.Forms.Button()
        Me.GetUpdates = New System.Windows.Forms.Button()
        Me.RichTextBoxInput = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Location = New System.Drawing.Point(348, 59)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(440, 379)
        Me.RichTextBox1.TabIndex = 0
        Me.RichTextBox1.Text = ""
        '
        'Send
        '
        Me.Send.Location = New System.Drawing.Point(12, 12)
        Me.Send.Name = "Send"
        Me.Send.Size = New System.Drawing.Size(106, 41)
        Me.Send.TabIndex = 1
        Me.Send.Text = "Send"
        Me.Send.UseVisualStyleBackColor = True
        '
        'GetUpdates
        '
        Me.GetUpdates.Location = New System.Drawing.Point(124, 12)
        Me.GetUpdates.Name = "GetUpdates"
        Me.GetUpdates.Size = New System.Drawing.Size(106, 41)
        Me.GetUpdates.TabIndex = 3
        Me.GetUpdates.Text = "GetUpdates"
        Me.GetUpdates.UseVisualStyleBackColor = True
        '
        'RichTextBoxInput
        '
        Me.RichTextBoxInput.Location = New System.Drawing.Point(12, 59)
        Me.RichTextBoxInput.Name = "RichTextBoxInput"
        Me.RichTextBoxInput.Size = New System.Drawing.Size(330, 379)
        Me.RichTextBoxInput.TabIndex = 4
        Me.RichTextBoxInput.Text = ""
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.RichTextBoxInput)
        Me.Controls.Add(Me.GetUpdates)
        Me.Controls.Add(Me.Send)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents Send As Button
    Friend WithEvents GetUpdates As Button
    Friend WithEvents RichTextBoxInput As RichTextBox
End Class
