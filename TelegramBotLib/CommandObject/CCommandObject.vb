' FILENAME: CCommandObject.vb
' AUTHOR: El Plan : Ilja Kuznetsov.
' CREATED: 17.02.2022
' CHANGED: 22.02.2022
'
' DESCRIPTION: See below↓↓↓

' Related components: CCommandObjectCollection.vb

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