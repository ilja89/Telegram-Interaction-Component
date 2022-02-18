
''' <summary>
'''  This class is used to link together any custom Telegram command and function, what handles it. <br/>
''' Using this object function with name &lt;<see cref="commandObjectFunctionName"/>&gt; what exists in environment<br/>
''' &lt;<see cref="commandObjectObjectRef"/>&gt; (class instance, for example) can be called with ease using this object function
''' &lt;<see cref="CallCommand"/>&gt;, what gets array of optional parameters, and returns result of called function with
''' &lt;<see cref="commandObjectFunctionName"/>&gt; name.<br/>
'''<br/>
''' Example of use:<br/><code>
'''___Function help(param1 As String, param2 As String) As String<br/>
'''___return param1 + param2<br/>
''' End Function <br/>
''' <br/>
''' Function Execute()<br/>
'''___Dim cObject As New TelegramBotLib.CCommandObject("help", Me, "help", vbMethod)<br/>
'''___Dim Args = {"param1Arg","param2Arg"}<br/>
'''___return cObject.CallCommand(Args)<br/>
''' End Function<br/></code>
''' </summary>
Public Class CCommandObject
    Private commandObjectObjectRef As Object = Nothing          ' Environment inside what aim function will be searched
    Private commandObjectFunctionName As String = Nothing       ' Aim function name
    Private commandObjectFunctionCallType As CallType = Nothing ' Aim function call type
    Private commandObjectCommand As String = Nothing            ' A Telegram command, to what this function is related
    Private commandObjectCreated As Boolean = False             ' Shows if object is correctly created
    Private commandObjectShortDescription As String = Nothing   ' Short description of this command
    Private commandObjectFullDescription As String = Nothing    ' Full description of this command

    ''' <summary>
    ''' A Telegram command, to what this command object is related<br/>
    ''' Must be without /
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Command As String
        Get
            If (commandObjectCreated) Then
                Return commandObjectCommand
            Else
                Return Nothing
            End If
        End Get
    End Property

    ''' <summary>
    ''' Shows if object is created
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IsCreated As Boolean
        Get
            If (commandObjectCreated) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    ''' <summary>
    ''' Environment inside what aim function will be searched
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ObjectRef As Object
        Get
            If (commandObjectCreated) Then
                Return commandObjectObjectRef
            Else
                Return Nothing
            End If
        End Get
    End Property

    ''' <summary>
    ''' Returns name of function stored in instance of &lt;<see cref="CCommandObject"/>&gt;
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property FunctionName As String
        Get
            If (commandObjectCreated) Then
                Return commandObjectFunctionName
            Else
                Return Nothing
            End If
        End Get
    End Property

    ''' <summary>
    '''a) A property value is being retrieved          - vbGet. <br/>
    '''b) An Object property value is being determined - vbLet.<br/>
    '''c) A method is being invoked                    - vbMethod.<br/>
    '''d) A property value is being determined         - vbSet.<br/>
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property FunctionCallType As CallType
        Get
            If (commandObjectCreated) Then
                Return commandObjectFunctionCallType
            Else
                Return Nothing
            End If
        End Get
    End Property

    ''' <summary>
    ''' Short description of function stored in instance of &lt;<see cref="CCommandObject"/>&gt;
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ShortDescription As String
        Get
            If (commandObjectCreated) Then
                Return commandObjectShortDescription
            Else
                Return Nothing
            End If
        End Get
    End Property
    ''' <summary>
    ''' Full description of function stored in instance of &lt;<see cref="CCommandObject"/>&gt;
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property FullDescription As String
        Get
            If (commandObjectCreated) Then
                Return commandObjectFullDescription
            Else
                Return Nothing
            End If
        End Get
    End Property

    ''' <summary>
    '''  Constructor reques 6,3 of them are optional properties:<br/>
    ''' 1. constructorCommand As String             - command from Telegram without /.<br/>
    ''' 2. constructorObjectRef As Object           - environment, from what this function will be called.<br/>
    ''' 3. constructorFunctionName As String        - name of function in environment "constructorObjectRef"<br/>
    ''' 4. constructorFunctionCallType As CallType  - type of call. There are few types of calls in VB:<br/>
    '''___a) A property value is being retrieved          - vbGet. <br/>
    '''___b) An Object property value is being determined - vbLet.<br/>
    '''___c) A method is being invoked                    - vbMethod.<br/>
    '''___d) A property value is being determined         - vbSet.<br/>
    '''<br/>
    ''' 5. constructorCommandShortDescription       - short description of function<br/>
    ''' 6. constructorCommandFullDescription        - full description of function<br/>
    ''' </summary>
    ''' <param name="constructorCommand"></param>
    ''' <param name="constructorObjectRef"></param>
    ''' <param name="constructorFunctionName"></param>
    ''' <param name="constructorFunctionCallType"></param>
    ''' <param name="constructorCommandShortDescription"></param>
    ''' <param name="constructorCommandFullDescription"></param>
    Public Sub New(
                  ByVal constructorCommand As String,
                  ByRef constructorObjectRef As Object,
                  ByVal constructorFunctionName As String,
                  Optional constructorFunctionCallType As CallType = vbMethod,
                  Optional constructorCommandShortDescription As String = Nothing,
                  Optional constructorCommandFullDescription As String = Nothing)
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
            commandObjectShortDescription = constructorCommandShortDescription
            commandObjectFullDescription = constructorCommandFullDescription
            commandObjectCreated = True
        Else
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' Calls command from instance of &lt;<see cref="CCommandObject"/>&gt;
    ''' </summary>
    ''' <param name="Args"></param>
    ''' <returns>Result of funciton, if it exists</returns>
    Public Function CallCommand(ParamArray Args As Object())
        If (commandObjectCreated) Then
            Return CallByName(ObjectRef, FunctionName, FunctionCallType, Args)
        Else
            Return Nothing
        End If
    End Function
End Class


''' <summary>
''' This class is used to assemble multiple &lt;<see cref="CCommandObject"/>&gt; instances into one object collection, working with what is more convenient<br/>
'''<br/>
''' Example of use:<br/><c>
'''___Function help(param1 As String, param2 As String) As String<br/>
'''___return param1 + param2<br/>
''' End Function <br/>
''' <br/>
''' Function Execute()<br/>
'''___Dim objCol As New CCommandObjectCollection<br/>
'''___objCol.Add(New CCommandObject("help", Me, "help", vbMethod))<br/>
'''___Dim Args = {"param1Arg","param2Arg"}<br/>
'''___return objCol.Item("help").CallCommand(Args)<br/>
''' End Function<br/></c>
''' </summary>
Public Class CCommandObjectCollection
    Private objCol As New Collection
    Private objColIsEmpty As New Boolean

    ''' <summary>
    ''' Shows if collection is empty
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IsEmpty As Boolean
        Get
            Return objColIsEmpty
        End Get
    End Property

    ''' <summary>
    ''' Return number of objects in collection
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Count As Integer
        Get
            Return objCol.Count
        End Get
    End Property

    ''' <summary>
    ''' Get item from collection by key
    ''' </summary>
    ''' <param name="key"></param>
    ''' <returns>Instance of&lt;<see cref="CCommandObject"/>&gt; if it was inside collection<br/>
    ''' Otherwise, returns <c>Nothing</c></returns>
    Public Function Item(ByVal key As String) As CCommandObject
        If (objCol.Contains(key)) Then
            Dim foundItem = objCol.Item(key)
            Return foundItem
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Get item from collection by index
    ''' </summary>
    ''' <param name="index"></param>
    ''' <returns>Instance of&lt;<see cref="CCommandObject"/>&gt; if it was inside collection<br/>
    ''' Otherwise, returns <c>Nothing</c></returns>
    Public Function Item(ByVal index As Integer) As CCommandObject
        If (objCol.Count >= index) Then
            Dim foundItem = objCol.Item(index)
            Return foundItem
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Adds instance of&lt;<see cref="CCommandObject"/>&gt; into collection
    ''' </summary>
    ''' <param name="commandObject"></param>
    ''' <returns>Updated collection</returns>
    Public Function Add(ByRef commandObject As CCommandObject) As CCommandObjectCollection
        objCol.Add(commandObject, commandObject.Command)
        If (objCol.Count >= 1) Then
            objColIsEmpty = False
        End If
        Return Me
    End Function

    ''' <summary>
    ''' Removes instance of&lt;<see cref="CCommandObject"/>&gt; from the collection using key
    ''' </summary>
    ''' <param name="key"></param>
    ''' <returns>Updated collection</returns>
    Public Function Remove(ByVal key As String) As CCommandObjectCollection
        objCol.Remove(key)
        If (objCol.Count < 1) Then
            objColIsEmpty = True
        End If
        Return Me
    End Function
    ''' <summary>
    ''' Removes instance of&lt;<see cref="CCommandObject"/>&gt; from the collection using index
    ''' </summary>
    ''' <param name="index"></param>
    ''' <returns>Updated collection</returns>
    Public Function Remove(ByVal index As Integer) As CCommandObjectCollection
        objCol.Remove(index)
        If (objCol.Count < 1) Then
            objColIsEmpty = True
        End If
        Return Me
    End Function

    ''' <summary>
    ''' Checks if object with this key <c>(Telegram command)</c> is inside collection
    ''' </summary>
    ''' <param name="key"></param>
    ''' <returns>Boolean</returns>
    Public Function Contains(ByVal key As String) As Boolean
        Return objCol.Contains(key)
    End Function
    ''' <summary>
    ''' Checks if object with any these keys <c>(Telegram command)</c> is inside collection
    ''' </summary>
    ''' <param name="key"></param>
    ''' <returns>Boolean</returns>
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

    ''' <summary>
    ''' Returns &lt;<see cref="CCommandObject.Command"/>&gt; strings of all objects in collection as array of strings
    ''' </summary>
    ''' <returns></returns>
    Public Function List() As String()
        Dim i As Integer = 1
        Dim returnable(objCol.Count) As String
        While (i <= objCol.Count)
            returnable(i) = DirectCast(objCol(i), CCommandObject).Command
            i = i + 1
        End While
        Return returnable
    End Function
    ''' <summary>
    ''' Return &lt;<see cref="CCommandObject.Command"/>&gt; strings of all objects in collection as string with
    ''' &lt;<see cref="CCommandObject.Command"/>&gt; texts divided with delimiter <paramref name="delimiter"/>  
    ''' </summary>
    ''' <returns></returns>
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