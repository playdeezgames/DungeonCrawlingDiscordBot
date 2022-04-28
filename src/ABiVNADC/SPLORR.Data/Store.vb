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
    Function ReadAllIds(initializer As Action, tableName As String, idColumnName As String) As List(Of Long)
        initializer()
        Return ExecuteReader(
            Function(reader) CLng(reader($"{idColumnName}")),
            $"SELECT [{idColumnName}] FROM [{tableName}]")
    End Function
    Function ReadIdsWithColumnValue(Of TColumn)(initializer As Action, tableName As String, idColumnName As String, forColumnName As String, forColumnValue As TColumn) As List(Of Long)
        initializer()
        Return ExecuteReader(
            Function(reader) CLng(reader(idColumnName)),
            $"SELECT [{idColumnName}] FROM [{tableName}] WHERE [{forColumnName}]=@{forColumnName};",
            MakeParameter($"@{forColumnName}", forColumnValue))
    End Function

    Sub ClearForColumnValue(Of TColumn)(initializer As Action, tableName As String, columnName As String, columnValue As TColumn)
        initializer()
        ExecuteNonQuery($"DELETE FROM [{tableName}] WHERE [{columnName}]=@{columnName};", MakeParameter($"@{columnName}", columnValue))
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
End Module