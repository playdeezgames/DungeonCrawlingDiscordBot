Imports Microsoft.Data.Sqlite
Public Module Store
    Private connection As SqliteConnection
    Sub Reset()
        ShutDown()
        connection = New SqliteConnection("Data Source=:memory:")
        connection.Open()
    End Sub
    Sub ShutDown()
        If connection IsNot Nothing Then
            connection.Close()
            connection = Nothing
        End If
    End Sub
    ReadOnly Property Exists As Boolean
        Get
            Return connection IsNot Nothing
        End Get
    End Property

    Sub Save(filename As String)
        Using saveConnection As New SqliteConnection($"Data Source={filename}")
            connection.BackupDatabase(saveConnection)
        End Using
    End Sub
    Sub Load(filename As String)
        Reset()
        Using loadConnection As New SqliteConnection($"Data Source={filename}")
            loadConnection.Open()
            loadConnection.BackupDatabase(connection)
        End Using
    End Sub
    Function CreateCommand(query As String, ParamArray parameters() As SqliteParameter) As SqliteCommand
        Dim command = connection.CreateCommand()
        command.CommandText = query
        For Each parameter In parameters
            command.Parameters.Add(parameter)
        Next
        Return command
    End Function
    Function MakeParameter(name As String, value As Object) As SqliteParameter
        Return New SqliteParameter(name, value)
    End Function
    Sub ExecuteNonQuery(sql As String, ParamArray parameters() As SqliteParameter)
        Using command = CreateCommand(sql, parameters)
            command.ExecuteNonQuery()
        End Using
    End Sub
    Function ExecuteScalar(Of TResult As Structure)(query As String, ParamArray parameters() As SqliteParameter) As TResult?
        Using command = CreateCommand(query, parameters)
            Return ExecuteScalar(Of TResult)(command)
        End Using
    End Function
    Function ExecuteScalar(Of TResult As Class)(transform As Func(Of Object, TResult), query As String, ParamArray parameters() As SqliteParameter) As TResult
        Using command = CreateCommand(query, parameters)
            Return transform(command.ExecuteScalar)
        End Using
    End Function
    Function ExecuteReader(Of TResult)(transform As Func(Of SqliteDataReader, TResult), query As String, ParamArray parameters() As SqliteParameter) As List(Of TResult)
        Using command = CreateCommand(query, parameters)
            Using reader = command.ExecuteReader
                Dim result As New List(Of TResult)
                While reader.Read
                    result.Add(transform(reader))
                End While
                Return result
            End Using
        End Using
    End Function
    ReadOnly Property LastInsertRowId() As Long
        Get
            Using command = connection.CreateCommand()
                command.CommandText = "SELECT last_insert_rowid();"
                Return CLng(command.ExecuteScalar())
            End Using
        End Get
    End Property
    Function ExecuteScalar(Of TResult As Structure)(command As SqliteCommand) As TResult?
        Dim result = command.ExecuteScalar
        If result IsNot Nothing Then
            Return CType(result, TResult?)
        End If
        Return Nothing
    End Function
    Public Function ReadColumnValue(Of TColumn As Structure)(initializer As Action, tableName As String, idColumnName As String, idColumnValue As Long, columnName As String) As TColumn?
        initializer()
        Return ExecuteScalar(Of TColumn)(
            $"SELECT [{columnName}] FROM [{tableName}] WHERE [{idColumnName}]=@{idColumnName};",
            MakeParameter($"@{idColumnName}", idColumnValue))
    End Function
    Public Function ReadColumnValue(Of TFirstInputColumn, TSecondInputColumn, TOutputColumn As Structure)(initializer As Action, tableName As String, outputColumnName As String, firstColumnValue As (String, TFirstInputColumn), secondColumnValue As (String, TSecondInputColumn)) As TOutputColumn?
        initializer()
        Return ExecuteScalar(Of TOutputColumn)(
            $"SELECT [{outputColumnName}] FROM [{tableName}] WHERE [{firstColumnValue.Item1}]=@{firstColumnValue.Item1} AND  [{secondColumnValue.Item1}]=@{secondColumnValue.Item1};",
            MakeParameter($"@{firstColumnValue.Item1}", firstColumnValue.Item2),
            MakeParameter($"@{secondColumnValue.Item1}", secondColumnValue.Item2))
    End Function
    Public Function ReadColumnString(Of TColumn)(initializer As Action, tableName As String, idColumnName As String, idColumnValue As Long, otherColumnName As String, otherColumnValue As TColumn, outputColumnName As String) As String
        initializer()
        Return ExecuteScalar(
            Function(o) If(o Is Nothing OrElse TypeOf o Is DBNull, Nothing, CStr(o)),
            $"SELECT 
                [{outputColumnName}] 
            FROM 
                [{tableName}] 
            WHERE 
                [{idColumnName}]=@{idColumnName} AND
                [{otherColumnName}]=@{otherColumnName};",
            MakeParameter($"@{idColumnName}", idColumnValue),
            MakeParameter($"@{otherColumnName}", otherColumnValue))
    End Function
    Public Function ReadColumnString(initializer As Action, tableName As String, idColumnName As String, idColumnValue As Long, columnName As String) As String
        initializer()
        Return ExecuteScalar(
            Function(o) If(o Is Nothing OrElse TypeOf o Is DBNull, Nothing, CStr(o)),
            $"SELECT [{columnName}] FROM [{tableName}] WHERE [{idColumnName}]=@{idColumnName};",
            MakeParameter($"@{idColumnName}", idColumnValue))
    End Function
    Public Sub WriteColumnValue(Of TColumn)(initializer As Action, tableName As String, idColumnName As String, idColumnValue As Long, columnName As String, columnValue As TColumn)
        initializer()
        ExecuteNonQuery(
            $"UPDATE 
                [{tableName}] 
            SET 
                [{columnName}]=@{columnName} 
            WHERE 
                [{idColumnName}]=@{idColumnName};",
            MakeParameter($"@{idColumnName}", idColumnValue),
            MakeParameter($"@{columnName}", columnValue))
    End Sub
    Function ReadLongsWithColumnValue(Of TInputColumn, TOutputColumn)(initializer As Action, tableName As String, longColumnName As String, forColumnValue As (String, TInputColumn)) As List(Of TOutputColumn)
        initializer()
        Return ExecuteReader(
            Function(reader) CType(reader(longColumnName), TOutputColumn),
            $"SELECT [{longColumnName}] FROM [{tableName}] WHERE [{forColumnValue.Item1}]=@{forColumnValue.Item1};",
            MakeParameter($"@{forColumnValue.Item1}", forColumnValue.Item2))
    End Function

    Sub ClearForColumnValue(Of TColumn)(initializer As Action, tableName As String, columnName As String, columnValue As TColumn)
        initializer()
        ExecuteNonQuery($"DELETE FROM [{tableName}] WHERE [{columnName}]=@{columnName};", MakeParameter($"@{columnName}", columnValue))
    End Sub

    Sub ClearForColumnValues(Of TFirstColumn, TSecondColumn)(initializer As Action, tableName As String, firstColumnName As String, firstColumnValue As TFirstColumn, secondColumnName As String, secondColumnValue As TSecondColumn)
        initializer()
        ExecuteNonQuery(
            $"DELETE FROM [{tableName}] WHERE [{firstColumnName}]=@{firstColumnName} AND [{secondColumnName}]=@{secondColumnName};",
            MakeParameter($"@{firstColumnName}", firstColumnValue),
            MakeParameter($"@{secondColumnName}", secondColumnValue))
    End Sub

    Sub ReplaceRecord(Of TFirstColumn, TSecondColumn)(initializer As Action, tableName As String, firstColumnName As String, firstColumnValue As TFirstColumn, secondColumnName As String, secondColumnValue As TSecondColumn)
        initializer()
        ExecuteNonQuery(
            $"REPLACE INTO [{tableName}]
            (
                [{firstColumnName}],
                [{secondColumnName}]
            ) 
            VALUES
            (
                @{firstColumnName},
                @{secondColumnName}
            );",
            MakeParameter($"@{firstColumnName}", firstColumnValue),
            MakeParameter($"@{secondColumnName}", secondColumnValue))
    End Sub

    Sub ReplaceRecord(Of TFirstColumn, TSecondColumn, TThirdColumn)(initializer As Action, tableName As String, firstColumnName As String, firstColumnValue As TFirstColumn, secondColumnName As String, secondColumnValue As TSecondColumn, thirdColumnName As String, thirdColumnValue As TThirdColumn)
        initializer()
        ExecuteNonQuery(
            $"REPLACE INTO [{tableName}]
            (
                [{firstColumnName}],
                [{secondColumnName}],
                [{thirdColumnName}]
            ) 
            VALUES
            (
                @{firstColumnName},
                @{secondColumnName},
                @{thirdColumnName}
            );",
            MakeParameter($"@{firstColumnName}", firstColumnValue),
            MakeParameter($"@{secondColumnName}", secondColumnValue),
            MakeParameter($"@{thirdColumnName}", thirdColumnValue))
    End Sub

    Sub ReplaceRecord(Of TFirstColumn, TSecondColumn, TThirdColumn, TFourthColumn)(initializer As Action, tableName As String, firstColumnName As String, firstColumnValue As TFirstColumn, secondColumnName As String, secondColumnValue As TSecondColumn, thirdColumnName As String, thirdColumnValue As TThirdColumn, fourthColumnName As String, fourthColumnValue As TFourthColumn)
        initializer()
        ExecuteNonQuery(
            $"REPLACE INTO [{tableName}]
            (
                [{firstColumnName}],
                [{secondColumnName}],
                [{thirdColumnName}],
                [{fourthColumnName}]
            ) 
            VALUES
            (
                @{firstColumnName},
                @{secondColumnName},
                @{thirdColumnName},
                @{fourthColumnName}
            );",
            MakeParameter($"@{firstColumnName}", firstColumnValue),
            MakeParameter($"@{secondColumnName}", secondColumnValue),
            MakeParameter($"@{thirdColumnName}", thirdColumnValue),
            MakeParameter($"@{fourthColumnName}", fourthColumnValue))
    End Sub
    Function CreateRecord(initializer As Action, tableName As String) As Long
        initializer()
        ExecuteNonQuery($"INSERT INTO [{tableName}] DEFAULT VALUES;")
        Return LastInsertRowId
    End Function
    Function CreateRecord(Of TColumn)(initializer As Action, tableName As String, columnValue As (String, TColumn)) As Long
        initializer()
        ExecuteNonQuery(
            $"INSERT INTO [{tableName}] ([{columnValue.Item1}]) VALUES(@{columnValue.Item1});",
            MakeParameter($"@{columnValue.Item1}", columnValue.Item2))
        Return LastInsertRowId
    End Function
    Function CreateRecord(Of TFirstColumn, TSecondColumn)(initializer As Action, tableName As String, firstColumnValue As (String, TFirstColumn), secondColumnValue As (String, TSecondColumn)) As Long
        initializer()
        ExecuteNonQuery(
            $"INSERT INTO [{tableName}] ([{firstColumnValue.Item1}],[{secondColumnValue.Item1}]) VALUES(@{firstColumnValue.Item1},@{secondColumnValue.Item1});",
            MakeParameter($"@{firstColumnValue.Item1}", firstColumnValue.Item2),
            MakeParameter($"@{secondColumnValue.Item1}", secondColumnValue.Item2))
        Return LastInsertRowId
    End Function
    Function CreateRecord(Of TFirstColumn, TSecondColumn, TThirdColumn)(initializer As Action, tableName As String, firstColumnValue As (String, TFirstColumn), secondColumnValue As (String, TSecondColumn), thirdColumnValue As (String, TThirdColumn)) As Long
        initializer()
        ExecuteNonQuery(
            $"INSERT INTO [{tableName}] ([{firstColumnValue.Item1}],[{secondColumnValue.Item1}],[{thirdColumnValue.Item1}]) VALUES(@{firstColumnValue.Item1},@{secondColumnValue.Item1},@{thirdColumnValue.Item1});",
            MakeParameter($"@{firstColumnValue.Item1}", firstColumnValue.Item2),
            MakeParameter($"@{secondColumnValue.Item1}", secondColumnValue.Item2),
            MakeParameter($"@{thirdColumnValue.Item1}", thirdColumnValue.Item2))
        Return LastInsertRowId
    End Function
    Function CreateRecord(Of TFirstColumn, TSecondColumn, TThirdColumn, TFourthColumn)(initializer As Action, tableName As String, firstColumnValue As (String, TFirstColumn), secondColumnValue As (String, TSecondColumn), thirdColumnValue As (String, TThirdColumn), fourthColumnValue As (String, TFourthColumn)) As Long
        initializer()
        ExecuteNonQuery(
            $"INSERT INTO [{tableName}] ([{firstColumnValue.Item1}],[{secondColumnValue.Item1}],[{thirdColumnValue.Item1}],[{fourthColumnValue.Item1}]) VALUES(@{firstColumnValue.Item1},@{secondColumnValue.Item1},@{thirdColumnValue.Item1}, @{fourthColumnValue.Item1});",
            MakeParameter($"@{firstColumnValue.Item1}", firstColumnValue.Item2),
            MakeParameter($"@{secondColumnValue.Item1}", secondColumnValue.Item2),
            MakeParameter($"@{thirdColumnValue.Item1}", thirdColumnValue.Item2),
            MakeParameter($"@{fourthColumnValue.Item1}", fourthColumnValue.Item2))
        Return LastInsertRowId
    End Function
    Function CreateRecord(Of TFirstColumn, TSecondColumn, TThirdColumn, TFourthColumn, TFifthColumn)(initializer As Action, tableName As String, firstColumnValue As (String, TFirstColumn), secondColumnValue As (String, TSecondColumn), thirdColumnValue As (String, TThirdColumn), fourthColumnValue As (String, TFourthColumn), fifthColumnValue As (String, TFourthColumn)) As Long
        initializer()
        ExecuteNonQuery(
            $"INSERT INTO [{tableName}] ([{firstColumnValue.Item1}],[{secondColumnValue.Item1}],[{thirdColumnValue.Item1}],[{fourthColumnValue.Item1}],[{fifthColumnValue.Item1}]) VALUES(@{firstColumnValue.Item1},@{secondColumnValue.Item1},@{thirdColumnValue.Item1}, @{fourthColumnValue.Item1}, @{fifthColumnValue.Item1});",
            MakeParameter($"@{firstColumnValue.Item1}", firstColumnValue.Item2),
            MakeParameter($"@{secondColumnValue.Item1}", secondColumnValue.Item2),
            MakeParameter($"@{thirdColumnValue.Item1}", thirdColumnValue.Item2),
            MakeParameter($"@{fourthColumnValue.Item1}", fourthColumnValue.Item2),
            MakeParameter($"@{fifthColumnValue.Item1}", fifthColumnValue.Item2))
        Return LastInsertRowId
    End Function
End Module