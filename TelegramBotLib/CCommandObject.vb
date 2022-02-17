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
'   Function help(param1 As String, param2 As String) As String
'   return param1 + param2
' End Function 
' 
' Function Execute()
'   Dim cObject As New TelegramBotLib.CCommandObject("help", Me, "help", vbMethod)
'   Dim Args = {"param1Arg","param2Arg"}
'   return cObject.CallCommand(Args)
' End Function
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
' This class is used to assemble multiple CCommandObject instances into one object, working with what is more convenient
'
' Example of use:
'   Function help(param1 As String, param2 As String) As String
'   return param1 + param2
' End Function 
' 
' Function Execute()
'   Dim objCol As New CCommandObjectCollection
'   objCol.Add(New CCommandObject("help", Me, "help", vbMethod))
'   Dim Args = {"param1Arg","param2Arg"}
'   return objCol.Item("help").CallCommand(Args)
' End Function
Public Class CCommandObjectCollection
    Private objCol As New Collection
    Private objColIsEmpty As New Boolean
    Public ReadOnly Property IsEmpty As Boolean
        Get
            Return objColIsEmpty
        End Get
    End Property
    ' Return number of objects in collection
    Public ReadOnly Property Count As Integer
        Get
            Return objCol.Count
        End Get
    End Property
    ' Get item from collection by key
    Public Function Item(ByVal key As String) As CCommandObject
        If (objCol.Contains(key)) Then
            Dim foundItem = objCol.Item(key)
            Return foundItem
        Else
            Return Nothing
        End If
    End Function
    ' Get item from collection by index
    Public Function Item(ByVal index As Integer) As CCommandObject
        If (objCol.Count >= index) Then
            Dim foundItem = objCol.Item(index)
            Return foundItem
        Else
            Return Nothing
        End If
    End Function
    Public Function Add(ByRef commandObject As CCommandObject) As CCommandObjectCollection
        objCol.Add(commandObject, commandObject.Command)
        If (objCol.Count >= 1) Then
            objColIsEmpty = False
        End If
        Return Me
    End Function
    Public Function Remove(ByVal key As String) As CCommandObjectCollection
        objCol.Remove(key)
        If (objCol.Count < 1) Then
            objColIsEmpty = True
        End If
        Return Me
    End Function
    Public Function Remove(ByVal index As Integer) As CCommandObjectCollection
        objCol.Remove(index)
        If (objCol.Count < 1) Then
            objColIsEmpty = True
        End If
        Return Me
    End Function
    ' Check if object with key (command) is inside collection
    Public Function Contains(ByVal key As String) As Boolean
        Return objCol.Contains(key)
    End Function
    ' Check if object with any of this keys (command) is inside collection
    Public Function Contains(ByVal key() As String) As Boolean
        Dim i As Integer = 0
        While (i < key.Length)
            If (objCol.Contains(key(i))) Then
                Return True
            End If
            i = i + 1
        End While
        Return False
    End Function
    ' Return "command" strings of all objects in collection as array of strings
    Public Function List() As String()
        Dim i As Integer = 1
        Dim returnable(objCol.Count) As String
        While (i <= objCol.Count)
            returnable(i) = DirectCast(objCol(i), CCommandObject).Command
            i = i + 1
        End While
        Return returnable
    End Function
    ' Return "command" strings of all objects in collection as string with "command" texts divided with delimiter
    Public Function AsString(Optional ByVal delimiter As String = ",") As String
        Dim i As Integer = 1
        Dim returnable As String = ""
        While (i <= objCol.Count)
            returnable = returnable + delimiter + DirectCast(objCol(i), CCommandObject).Command
            i = i + 1
        End While
        Return Right(returnable, returnable.Length - delimiter.Length)
    End Function
End Class