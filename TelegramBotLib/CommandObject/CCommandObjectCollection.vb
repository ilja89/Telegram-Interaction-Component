' FILENAME: CCommandObjectCollection.vb
' AUTHOR: El Plan : Ilja Kuznetsov.
' CREATED: 17.02.2022
' CHANGED: 22.02.2022
'
' DESCRIPTION: See below↓↓↓

' Related components: CCommandObject.vb


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