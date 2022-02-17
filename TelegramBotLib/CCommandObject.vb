' This class is used to link together any custom Telegram command and function, what handles it
' Using this object function with name "commandObjectFunctionName" what exists in environment
' "commandObjectObjectRef" (Class, for example) can be called with ease using this object function
' "CallCommand", what gets array of optional parameters, and returns result of called function with
' "commandObjectFunctionName" name.
' Constructor reques 4 properties:
' 1. ByVal constructorCommand As String             - command from Telegram.
' 2. ByRef constructorObjectRef As Object           - environment, from what this function will be called.
' 3. ByVal constructorFunctionName As String        - name of function in environment "constructorObjectRef"
' 4. ByVal constructorFunctionCallType As CallType  - type of function. There are few types of call types in VB:
'   a) A property value is being retrieved          - vbGet. 
'   b) An Object property value is being determined - vbLet.
'   c) A method is being invoked                    - vbMethod.
'   d) A property value is being determined         - vbSet.
'
' Example of use:
' Dim cObject As New TelegramBotLib.CCommandObject("help", Me, "help", vbMethod)
' Dim Args As Object()
' Args(0) = "param1"
' Args(1) = "param2"
' return cObject.CallCommand(Args)
Public Class CCommandObject
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

