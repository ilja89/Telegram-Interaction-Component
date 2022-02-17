Imports System.Runtime.CompilerServices
Public Module CFunctionalExtensions
    <Extension()>
    Public Function AsString(ByVal arr() As String, Optional ByVal delimiter As String = ",") As String
        Dim i As Integer = 0
        Dim returnable As String = ""
        While (i < arr.Length)
            returnable = returnable + delimiter + arr(i)
            i = i + 1
        End While
        Return Right(returnable, returnable.Length - delimiter.Length)
    End Function
    <Extension()>
    Public Function ToCollection(arr As Array) As Collection
        Dim col As New Collection
        For Each objectInArray In arr
            col.Add(objectInArray)
        Next
        Return col
    End Function
    <Extension()>
    Public Function Exists(
                          jsonObject As Newtonsoft.Json.Linq.JObject,
                          token As String,
                          Optional itemIndex As Integer = -1
                          ) As Newtonsoft.Json.Linq.JToken
        If (jsonObject Is Nothing) Then
            Return Nothing
        Else
            If (itemIndex <> -1) Then
                If (jsonObject.SelectToken(token).Count > itemIndex) Then
                    Return jsonObject.SelectToken(token)(itemIndex)
                Else
                    Return Nothing
                End If
            Else
                If (jsonObject.SelectToken(token) IsNot Nothing) Then
                    Return jsonObject.SelectToken(token)
                Else
                    Return Nothing
                End If
            End If
        End If
    End Function

    <Extension()>
    Public Function Exists(
                          jsonToken As Newtonsoft.Json.Linq.JToken,
                          token As String,
                          Optional itemIndex As Integer = -1
                          ) As Newtonsoft.Json.Linq.JToken
        If (jsonToken Is Nothing) Then
            Return Nothing
        Else
            If (itemIndex <> -1) Then
                If (jsonToken.SelectToken(token).Count > itemIndex) Then
                    Return jsonToken.SelectToken(token)(itemIndex)
                Else
                    Return Nothing
                End If
            Else
                If (jsonToken.SelectToken(token) IsNot Nothing) Then
                    Return jsonToken.SelectToken(token)
                Else
                    Return Nothing
                End If
            End If
        End If
        'If (jsonToken Is Nothing) Then
        'Return Nothing
        'Else
        'If (jsonToken.Count > 0) Then
        'Return jsonToken.SelectToken(token)
        'Else
        'Return Nothing
        'End If
        'End If
    End Function
End Module
