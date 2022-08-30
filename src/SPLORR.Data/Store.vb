Imports Microsoft.Data.Sqlite

Public Class Store
    Private connection As SqliteConnection
    Public Sub Reset()
        ShutDown()
        connection = New SqliteConnection("Data Source=:memory:")
        connection.Open()
    End Sub
    Public Function Renew() As SqliteConnection
        Dim result = connection
        connection = Nothing
        Reset()
        Return result
    End Function
    Public Sub Restore(oldConnection As SqliteConnection)
        ShutDown()
        connection = oldConnection
    End Sub
    Public Sub ShutDown()
        If connection IsNot Nothing Then
            connection.Close()
            connection = Nothing
        End If
    End Sub
    Public Sub Save(filename As String)
        Using saveConnection As New SqliteConnection($"Data Source={filename}")
            connection.BackupDatabase(saveConnection)
        End Using
    End Sub
    Public Sub Load(filename As String)
        Reset()
        Using loadConnection As New SqliteConnection($"Data Source={filename}")
            loadConnection.Open()
            loadConnection.BackupDatabase(connection)
        End Using
    End Sub
    Private Function CreateCommand(query As String, ParamArray parameters() As SqliteParameter) As SqliteCommand
        Dim command = connection.CreateCommand()
        command.CommandText = query
        For Each parameter In parameters
            command.Parameters.Add(parameter)
        Next
        Return command
    End Function
    Private Function MakeParameter(Of TParameter)(name As String, value As TParameter) As SqliteParameter
        Return New SqliteParameter(name, value)
    End Function
    Public Sub ExecuteNonQuery(sql As String, ParamArray parameters() As SqliteParameter)
        Using command = CreateCommand(sql, parameters)
            command.ExecuteNonQuery()
        End Using
    End Sub
    Private Function ExecuteScalar(Of TResult As Structure)(query As String, ParamArray parameters() As SqliteParameter) As TResult?
        Using command = CreateCommand(query, parameters)
            Return ExecuteScalar(Of TResult)(command)
        End Using
    End Function
    Private Function ExecuteScalar(Of TResult As Class)(transform As Func(Of Object, TResult), query As String, ParamArray parameters() As SqliteParameter) As TResult
        Using command = CreateCommand(query, parameters)
            Return transform(command.ExecuteScalar)
        End Using
    End Function
    Private Function ExecuteReader(Of TResult)(transform As Func(Of SqliteDataReader, TResult), query As String, ParamArray parameters() As SqliteParameter) As IEnumerable(Of TResult)
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
    Private ReadOnly Property LastInsertRowId() As Long
        Get
            Using command = connection.CreateCommand()
                command.CommandText = "SELECT last_insert_rowid();"
                Return CLng(command.ExecuteScalar())
            End Using
        End Get
    End Property
    Private Function ExecuteScalar(Of TResult As Structure)(command As SqliteCommand) As TResult?
        Dim result = command.ExecuteScalar
        If result IsNot Nothing AndAlso result IsNot DBNull.Value Then
            Return CType(result, TResult?)
        End If
        Return Nothing
    End Function
    Public Function ReadColumnValues(Of TOutputColumn)(initializer As Action, tableName As String, outputColumnName As String) As IEnumerable(Of TOutputColumn)
        initializer()
        Return ExecuteReader(
            Function(reader) CType(reader(outputColumnName), TOutputColumn),
            $"SELECT [{outputColumnName}] FROM [{tableName}];")
    End Function
    Public Function ReadColumnValue(Of TInputColumn, TOutputColumn As Structure)(initializer As Action, tableName As String, outputColumnName As String, inputColumnValue As (String, TInputColumn)) As TOutputColumn?
        initializer()
        Return ExecuteScalar(Of TOutputColumn)(
            $"SELECT [{outputColumnName}] FROM [{tableName}] WHERE [{inputColumnValue.Item1}]=@{inputColumnValue.Item1};",
            MakeParameter($"@{inputColumnValue.Item1}", inputColumnValue.Item2))
    End Function
    Public Function ReadColumnValue(Of TInputColumn, TOutputColumn As Structure)(tableName As String, outputColumnName As String, inputColumnValue As (String, TInputColumn)) As TOutputColumn?
        Return ReadColumnValue(Of TInputColumn, TOutputColumn)(
            AddressOf NoInitializer,
            tableName,
            outputColumnName,
            inputColumnValue)
    End Function
    Public Function ReadColumnValue(Of TFirstInputColumn, TSecondInputColumn, TOutputColumn As Structure)(initializer As Action, tableName As String, outputColumnName As String, firstColumnValue As (String, TFirstInputColumn), secondColumnValue As (String, TSecondInputColumn)) As TOutputColumn?
        initializer()
        Return ExecuteScalar(Of TOutputColumn)(
            $"SELECT [{outputColumnName}] FROM [{tableName}] WHERE [{firstColumnValue.Item1}]=@{firstColumnValue.Item1} AND  [{secondColumnValue.Item1}]=@{secondColumnValue.Item1};",
            MakeParameter($"@{firstColumnValue.Item1}", firstColumnValue.Item2),
            MakeParameter($"@{secondColumnValue.Item1}", secondColumnValue.Item2))
    End Function
    Public Function ReadColumnValue(Of TFirstInputColumn, TSecondInputColumn, TOutputColumn As Structure)(tableName As String, outputColumnName As String, firstColumnValue As (String, TFirstInputColumn), secondColumnValue As (String, TSecondInputColumn)) As TOutputColumn?
        Return ReadColumnValue(Of TFirstInputColumn, TSecondInputColumn, TOutputColumn)(AddressOf NoInitializer, tableName, outputColumnName, firstColumnValue, secondColumnValue)
    End Function
    Public Function ReadColumnValue(Of TFirstInputColumn, TSecondInputColumn, TThirdInputColumn, TOutputColumn As Structure)(initializer As Action, tableName As String, outputColumnName As String, firstColumnValue As (String, TFirstInputColumn), secondColumnValue As (String, TSecondInputColumn), thirdColumnValue As (String, TThirdInputColumn)) As TOutputColumn?
        initializer()
        Return ExecuteScalar(Of TOutputColumn)(
            $"SELECT [{outputColumnName}] FROM [{tableName}] WHERE [{firstColumnValue.Item1}]=@{firstColumnValue.Item1} AND [{secondColumnValue.Item1}]=@{secondColumnValue.Item1} AND [{thirdColumnValue.Item1}]=@{thirdColumnValue.Item1};",
            MakeParameter($"@{firstColumnValue.Item1}", firstColumnValue.Item2),
            MakeParameter($"@{secondColumnValue.Item1}", secondColumnValue.Item2),
            MakeParameter($"@{thirdColumnValue.Item1}", thirdColumnValue.Item2))
    End Function
    Public Function ReadColumnString(initializer As Action, tableName As String, outputColumnName As String, inputColumnValue As (String, Long)) As String
        initializer()
        Return ExecuteScalar(
            Function(o) If(o Is Nothing OrElse TypeOf o Is DBNull, Nothing, CStr(o)),
            $"SELECT [{outputColumnName}] FROM [{tableName}] WHERE [{inputColumnValue.Item1}]=@{inputColumnValue.Item1};",
            MakeParameter($"@{inputColumnValue.Item1}", inputColumnValue.Item2))
    End Function
    Public Function ReadColumnString(tableName As String, outputColumnName As String, inputColumnValue As (String, Long)) As String
        Return ReadColumnString(AddressOf NoInitializer, tableName, outputColumnName, inputColumnValue)
    End Function
    Public Sub WriteColumnValue(Of TWhereColumn, TSetColumn)(initializer As Action, tableName As String, setColumn As (String, TSetColumn), whereColumn As (String, TWhereColumn))
        initializer()
        ExecuteNonQuery(
            $"UPDATE 
                [{tableName}] 
            SET 
                [{setColumn.Item1}]=@{setColumn.Item1} 
            WHERE 
                [{whereColumn.Item1}]=@{whereColumn.Item1};",
            MakeParameter($"@{whereColumn.Item1}", whereColumn.Item2),
            MakeParameter($"@{setColumn.Item1}", setColumn.Item2))
    End Sub
    Public Sub WriteColumnValue(Of TWhereColumn, TSetColumn)(tableName As String, setColumn As (String, TSetColumn), whereColumn As (String, TWhereColumn))
        WriteColumnValue(
            AddressOf NoInitializer,
            tableName,
            setColumn,
            whereColumn)
    End Sub
    Public Function ReadRecords(Of TOutputColumn)(initializer As Action, tableName As String, outputColumnName As String) As IEnumerable(Of TOutputColumn)
        initializer()
        Return ExecuteReader(
            Function(reader) CType(reader(outputColumnName), TOutputColumn),
            $"SELECT [{outputColumnName}] FROM [{tableName}];")
    End Function
    Private Sub NoInitializer()

    End Sub
    Public Function ReadRecords(Of TOutputColumn)(tableName As String, outputColumnName As String) As IEnumerable(Of TOutputColumn)
        Return ReadRecords(Of TOutputColumn)(
            AddressOf NoInitializer,
            tableName,
            outputColumnName)
    End Function
    Public Function ReadRecordsWithColumnValue(Of TInputColumn, TOutputColumn)(initializer As Action, tableName As String, outputColumnName As String, forColumnValue As (String, TInputColumn)) As IEnumerable(Of TOutputColumn)
        initializer()
        Return ExecuteReader(
            Function(reader) CType(reader(outputColumnName), TOutputColumn),
            $"SELECT [{outputColumnName}] FROM [{tableName}] WHERE [{forColumnValue.Item1}]=@{forColumnValue.Item1};",
            MakeParameter($"@{forColumnValue.Item1}", forColumnValue.Item2))
    End Function
    Public Function ReadRecordsWithColumnValue(Of TInputColumn, TOutputColumn)(tableName As String, outputColumnName As String, forColumnValue As (String, TInputColumn)) As IEnumerable(Of TOutputColumn)
        Return ReadRecordsWithColumnValue(Of TInputColumn, TOutputColumn)(
            AddressOf NoInitializer,
            tableName,
            outputColumnName,
            forColumnValue)
    End Function
    Public Function ReadRecordsWithColumnValues(Of TFirstInputColumn, TSecondInputColumn, TOutputColumn)(tableName As String, outputColumnName As String, firstColumnValue As (String, TFirstInputColumn), secondColumnValue As (String, TSecondInputColumn)) As IEnumerable(Of TOutputColumn)
        Return ReadRecordsWithColumnValues(Of TFirstInputColumn, TSecondInputColumn, TOutputColumn)(AddressOf NoInitializer, tableName, outputColumnName, firstColumnValue, secondColumnValue)
    End Function
    Public Function ReadRecordsWithColumnValues(Of TFirstInputColumn, TSecondInputColumn, TOutputColumn)(initializer As Action, tableName As String, outputColumnName As String, firstColumnValue As (String, TFirstInputColumn), secondColumnValue As (String, TSecondInputColumn)) As IEnumerable(Of TOutputColumn)
        initializer()
        Return ExecuteReader(
            Function(reader) CType(reader(outputColumnName), TOutputColumn),
            $"SELECT 
                [{outputColumnName}] 
            FROM [{tableName}] 
            WHERE 
                [{firstColumnValue.Item1}]=@{firstColumnValue.Item1} 
                AND [{secondColumnValue.Item1}]=@{secondColumnValue.Item1};",
            MakeParameter($"@{firstColumnValue.Item1}", firstColumnValue.Item2),
            MakeParameter($"@{secondColumnValue.Item1}", secondColumnValue.Item2))
    End Function
    Public Function ReadRecordsWithColumnValues(Of TFirstInputColumn, TSecondInputColumn, TFirstOutputColumn, TSecondOutputColumn)(tableName As String, outputColumnNames As (String, String), firstColumnValue As (String, TFirstInputColumn), secondColumnValue As (String, TSecondInputColumn)) As IEnumerable(Of Tuple(Of TFirstOutputColumn, TSecondOutputColumn))
        Return ReadRecordsWithColumnValues(Of TFirstInputColumn, TSecondInputColumn, TFirstOutputColumn, TSecondOutputColumn)(AddressOf NoInitializer, tableName, outputColumnNames, firstColumnValue, secondColumnValue)
    End Function
    Public Function ReadRecordsWithColumnValues(Of TFirstInputColumn, TSecondInputColumn, TFirstOutputColumn, TSecondOutputColumn)(initializer As Action, tableName As String, outputColumnNames As (String, String), firstColumnValue As (String, TFirstInputColumn), secondColumnValue As (String, TSecondInputColumn)) As IEnumerable(Of Tuple(Of TFirstOutputColumn, TSecondOutputColumn))
        initializer()
        Return ExecuteReader(
            Function(reader) New Tuple(Of TFirstOutputColumn, TSecondOutputColumn)(CType(reader(outputColumnNames.Item1), TFirstOutputColumn), CType(reader(outputColumnNames.Item2), TSecondOutputColumn)),
            $"SELECT 
                [{outputColumnNames.Item1}] ,
                [{outputColumnNames.Item2}] 
            FROM [{tableName}] 
            WHERE 
                [{firstColumnValue.Item1}]=@{firstColumnValue.Item1} 
                AND [{secondColumnValue.Item1}]=@{secondColumnValue.Item1};",
            MakeParameter($"@{firstColumnValue.Item1}", firstColumnValue.Item2),
            MakeParameter($"@{secondColumnValue.Item1}", secondColumnValue.Item2))
    End Function
    Public Function ReadRecordsWithColumnValue(
            Of TInputColumn,
                TFirstOutputColumn,
                TSecondOutputColumn)(
                    initializer As Action,
                    tableName As String,
                    outputColumnNames As (String, String),
                    forColumnValue As (String, TInputColumn)) As IEnumerable(Of Tuple(Of TFirstOutputColumn, TSecondOutputColumn))
        initializer()
        Return ExecuteReader(
            Function(reader) New Tuple(Of TFirstOutputColumn, TSecondOutputColumn)(CType(reader(outputColumnNames.Item1), TFirstOutputColumn), CType(reader(outputColumnNames.Item2), TSecondOutputColumn)),
            $"SELECT [{outputColumnNames.Item1}],[{outputColumnNames.Item2}] FROM [{tableName}] WHERE [{forColumnValue.Item1}]=@{forColumnValue.Item1};",
            MakeParameter($"@{forColumnValue.Item1}", forColumnValue.Item2))
    End Function
    Public Function ReadRecordsWithColumnValue(
            Of TInputColumn,
                TFirstOutputColumn,
                TSecondOutputColumn,
                TThirdOutputColumn)(
                    initializer As Action,
                    tableName As String,
                    outputColumnNames As (String, String, String),
                    forColumnValue As (String, TInputColumn)) As IEnumerable(Of Tuple(Of TFirstOutputColumn, TSecondOutputColumn, TThirdOutputColumn))
        initializer()
        Return ExecuteReader(
            Function(reader) New Tuple(Of TFirstOutputColumn, TSecondOutputColumn, TThirdOutputColumn)(CType(reader(outputColumnNames.Item1), TFirstOutputColumn), CType(reader(outputColumnNames.Item2), TSecondOutputColumn), CType(reader(outputColumnNames.Item3), TThirdOutputColumn)),
            $"SELECT [{outputColumnNames.Item1}],[{outputColumnNames.Item2}],[{outputColumnNames.Item3}] FROM [{tableName}] WHERE [{forColumnValue.Item1}]=@{forColumnValue.Item1};",
            MakeParameter($"@{forColumnValue.Item1}", forColumnValue.Item2))
    End Function
    Public Function ReadRecordsWithColumnValue(
            Of TInputColumn,
                TFirstOutputColumn,
                TSecondOutputColumn,
                TThirdOutputColumn)(
                    tableName As String,
                    outputColumnNames As (String, String, String),
                    forColumnValue As (String, TInputColumn)) As IEnumerable(Of Tuple(Of TFirstOutputColumn, TSecondOutputColumn, TThirdOutputColumn))
        Return ReadRecordsWithColumnValue(Of TInputColumn, TFirstOutputColumn, TSecondOutputColumn, TThirdOutputColumn)(
            AddressOf NoInitializer,
            tableName,
            outputColumnNames,
            forColumnValue)
    End Function
    Public Function ReadRecordsWithColumnValue(
            Of TInputColumn,
                TFirstOutputColumn,
                TSecondOutputColumn)(
                    tableName As String,
                    outputColumnNames As (String, String),
                    forColumnValue As (String, TInputColumn)) As IEnumerable(Of Tuple(Of TFirstOutputColumn, TSecondOutputColumn))
        Return ReadRecordsWithColumnValue(
            Of TInputColumn,
                TFirstOutputColumn,
                TSecondOutputColumn)(
                AddressOf NoInitializer,
                tableName,
                outputColumnNames,
                forColumnValue)
    End Function
    Public Sub ClearForColumnValue(Of TColumn)(initializer As Action, tableName As String, columnValue As (String, TColumn))
        initializer()
        ExecuteNonQuery($"DELETE FROM [{tableName}] WHERE [{columnValue.Item1}]=@{columnValue.Item1};", MakeParameter($"@{columnValue.Item1}", columnValue.Item2))
    End Sub
    Public Sub ClearForColumnValue(Of TColumn)(tableName As String, columnValue As (String, TColumn))
        ClearForColumnValue(Of TColumn)(AddressOf NoInitializer, tableName, columnValue)
    End Sub
    Public Sub ClearForColumnValues(Of TFirstColumn, TSecondColumn)(
                                                                   initializer As Action,
                                                                   tableName As String,
                                                                   firstColumnValue As (String, TFirstColumn),
                                                                   secondColumnValue As (String, TSecondColumn))
        initializer()
        ExecuteNonQuery(
            $"DELETE FROM [{tableName}] WHERE [{firstColumnValue.Item1}]=@{firstColumnValue.Item1} AND [{secondColumnValue.Item1}]=@{secondColumnValue.Item1};",
            MakeParameter($"@{firstColumnValue.Item1}", firstColumnValue.Item2),
            MakeParameter($"@{secondColumnValue.Item1}", secondColumnValue.Item2))
    End Sub
    Public Sub ClearForColumnValues(Of TFirstColumn, TSecondColumn)(
                                                                   tableName As String,
                                                                   firstColumnValue As (String, TFirstColumn),
                                                                   secondColumnValue As (String, TSecondColumn))
        ClearForColumnValues(AddressOf NoInitializer, tableName, firstColumnValue, secondColumnValue)
    End Sub
    Public Sub ClearForColumnValues(Of TFirstColumn, TSecondColumn, TThirdColumn)(
                                                                   initializer As Action,
                                                                   tableName As String,
                                                                   firstColumnValue As (String, TFirstColumn),
                                                                   secondColumnValue As (String, TSecondColumn),
                                                                   thirdColumnValue As (String, TThirdColumn))
        initializer()
        ExecuteNonQuery(
            $"DELETE FROM [{tableName}] WHERE [{firstColumnValue.Item1}]=@{firstColumnValue.Item1} AND [{secondColumnValue.Item1}]=@{secondColumnValue.Item1} AND [{thirdColumnValue.Item1}]=@{thirdColumnValue.Item1};",
            MakeParameter($"@{firstColumnValue.Item1}", firstColumnValue.Item2),
            MakeParameter($"@{secondColumnValue.Item1}", secondColumnValue.Item2),
            MakeParameter($"@{thirdColumnValue.Item1}", thirdColumnValue.Item2))
    End Sub
    Public Sub ClearForColumnValues(Of TFirstColumn, TSecondColumn, TThirdColumn)(
                                                                   tableName As String,
                                                                   firstColumnValue As (String, TFirstColumn),
                                                                   secondColumnValue As (String, TSecondColumn),
                                                                   thirdColumnValue As (String, TThirdColumn))
        ClearForColumnValues(AddressOf NoInitializer, tableName, firstColumnValue, secondColumnValue, thirdColumnValue)
    End Sub
    Public Sub ReplaceRecord(Of TColumn)(tableName As String, columnValue As (String, TColumn))
        ReplaceRecord(AddressOf NoInitializer, tableName, columnValue)
    End Sub
    Public Sub ReplaceRecord(Of TColumn)(initializer As Action, tableName As String, columnValue As (String, TColumn))
        initializer()
        ExecuteNonQuery(
            $"REPLACE INTO [{tableName}]
            (
                [{columnValue.Item1}]
            ) 
            VALUES
            (
                @{columnValue.Item1}
            );",
            MakeParameter($"@{columnValue.Item1}", columnValue.Item2))
    End Sub
    Public Sub ReplaceRecord(Of TFirstColumn, TSecondColumn)(tableName As String, firstColumnValue As (String, TFirstColumn), secondColumnValue As (String, TSecondColumn))
        ReplaceRecord(AddressOf NoInitializer, tableName, firstColumnValue, secondColumnValue)
    End Sub
    Public Sub ReplaceRecord(Of TFirstColumn, TSecondColumn)(initializer As Action, tableName As String, firstColumnValue As (String, TFirstColumn), secondColumnValue As (String, TSecondColumn))
        initializer()
        ExecuteNonQuery(
            $"REPLACE INTO [{tableName}]
            (
                [{firstColumnValue.Item1}],
                [{secondColumnValue.Item1}]
            ) 
            VALUES
            (
                @{firstColumnValue.Item1},
                @{secondColumnValue.Item1}
            );",
            MakeParameter($"@{firstColumnValue.Item1}", firstColumnValue.Item2),
            MakeParameter($"@{secondColumnValue.Item1}", secondColumnValue.Item2))
    End Sub
    Public Sub ReplaceRecord(
                            Of TFirstColumn,
                                TSecondColumn,
                                TThirdColumn)(
                                                tableName As String,
                                                firstColumnValue As (String, TFirstColumn),
                                                secondColumnValue As (String, TSecondColumn),
                                                thirdColumnValue As (String, TThirdColumn))
        ReplaceRecord(
            AddressOf NoInitializer,
            tableName,
            firstColumnValue,
            secondColumnValue,
            thirdColumnValue)
    End Sub
    Public Sub ReplaceRecord(Of TFirstColumn, TSecondColumn, TThirdColumn)(initializer As Action, tableName As String, firstColumnValue As (String, TFirstColumn), secondColumnValue As (String, TSecondColumn), thirdColumnValue As (String, TThirdColumn))
        initializer()
        ExecuteNonQuery(
            $"REPLACE INTO [{tableName}]
            (
                [{firstColumnValue.Item1}],
                [{secondColumnValue.Item1}],
                [{thirdColumnValue.Item1}]
            ) 
            VALUES
            (
                @{firstColumnValue.Item1},
                @{secondColumnValue.Item1},
                @{thirdColumnValue.Item1}
            );",
            MakeParameter($"@{firstColumnValue.Item1}", firstColumnValue.Item2),
            MakeParameter($"@{secondColumnValue.Item1}", secondColumnValue.Item2),
            MakeParameter($"@{thirdColumnValue.Item1}", thirdColumnValue.Item2))
    End Sub
    Public Sub ReplaceRecord(Of
                                 TFirstColumn,
                                 TSecondColumn,
                                 TThirdColumn,
                                 TFourthColumn)(
                                               tableName As String,
                                               firstColumnValue As (String, TFirstColumn),
                                               secondColumnValue As (String, TSecondColumn),
                                               thirdColumnValue As (String, TThirdColumn),
                                               fourthColumnValue As (String, TFourthColumn))
        ReplaceRecord(
            AddressOf NoInitializer,
            tableName,
            firstColumnValue,
            secondColumnValue,
            thirdColumnValue,
            fourthColumnValue)
    End Sub
    Public Sub ReplaceRecord(Of
                                 TFirstColumn,
                                 TSecondColumn,
                                 TThirdColumn,
                                 TFourthColumn)(
                                               initializer As Action,
                                               tableName As String,
                                               firstColumnValue As (String, TFirstColumn),
                                               secondColumnValue As (String, TSecondColumn),
                                               thirdColumnValue As (String, TThirdColumn),
                                               fourthColumnValue As (String, TFourthColumn))
        initializer()
        ExecuteNonQuery(
            $"REPLACE INTO [{tableName}]
            (
                [{firstColumnValue.Item1}],
                [{secondColumnValue.Item1}],
                [{thirdColumnValue.Item1}],
                [{fourthColumnValue.Item1}]
            ) 
            VALUES
            (
                @{firstColumnValue.Item1},
                @{secondColumnValue.Item1},
                @{thirdColumnValue.Item1},
                @{fourthColumnValue.Item1}
            );",
            MakeParameter($"@{firstColumnValue.Item1}", firstColumnValue.Item2),
            MakeParameter($"@{secondColumnValue.Item1}", secondColumnValue.Item2),
            MakeParameter($"@{thirdColumnValue.Item1}", thirdColumnValue.Item2),
            MakeParameter($"@{fourthColumnValue.Item1}", fourthColumnValue.Item2))
    End Sub
    Public Sub ReplaceRecord(
                     Of TFirstColumn,
                         TSecondColumn,
                         TThirdColumn,
                         TFourthColumn,
                         TFifthColumn)(
                                      initializer As Action,
                                      tableName As String,
                                      firstColumnValue As (String, TFirstColumn),
                                      secondColumnValue As (String, TSecondColumn),
                                      thirdColumnValue As (String, TThirdColumn),
                                      fourthColumnValue As (String, TFourthColumn),
                                      fifthColumnValue As (String, TFifthColumn))
        initializer()
        ExecuteNonQuery(
            $"REPLACE INTO [{tableName}]
            (
                [{firstColumnValue.Item1}],
                [{secondColumnValue.Item1}],
                [{thirdColumnValue.Item1}],
                [{fourthColumnValue.Item1}],
                [{fifthColumnValue.Item1}]
            ) 
            VALUES
            (
                @{firstColumnValue.Item1},
                @{secondColumnValue.Item1},
                @{thirdColumnValue.Item1},
                @{fourthColumnValue.Item1},
                @{fifthColumnValue.Item1}
            );",
            MakeParameter($"@{firstColumnValue.Item1}", firstColumnValue.Item2),
            MakeParameter($"@{secondColumnValue.Item1}", secondColumnValue.Item2),
            MakeParameter($"@{thirdColumnValue.Item1}", thirdColumnValue.Item2),
            MakeParameter($"@{fourthColumnValue.Item1}", fourthColumnValue.Item2),
            MakeParameter($"@{fifthColumnValue.Item1}", fifthColumnValue.Item2))
    End Sub
    Public Sub ReplaceRecord(
                     Of TFirstColumn,
                         TSecondColumn,
                         TThirdColumn,
                         TFourthColumn,
                         TFifthColumn,
                         TSixthColumn)(
                                      initializer As Action,
                                      tableName As String,
                                      firstColumnValue As (String, TFirstColumn),
                                      secondColumnValue As (String, TSecondColumn),
                                      thirdColumnValue As (String, TThirdColumn),
                                      fourthColumnValue As (String, TFourthColumn),
                                      fifthColumnValue As (String, TFifthColumn),
                                      sixthColumnValue As (String, TSixthColumn))
        initializer()
        ExecuteNonQuery(
            $"REPLACE INTO [{tableName}]
            (
                [{firstColumnValue.Item1}],
                [{secondColumnValue.Item1}],
                [{thirdColumnValue.Item1}],
                [{fourthColumnValue.Item1}],
                [{fifthColumnValue.Item1}],
                [{sixthColumnValue.Item1}]
            ) 
            VALUES
            (
                @{firstColumnValue.Item1},
                @{secondColumnValue.Item1},
                @{thirdColumnValue.Item1},
                @{fourthColumnValue.Item1},
                @{fifthColumnValue.Item1},
                @{sixthColumnValue.Item1}
            );",
            MakeParameter($"@{firstColumnValue.Item1}", firstColumnValue.Item2),
            MakeParameter($"@{secondColumnValue.Item1}", secondColumnValue.Item2),
            MakeParameter($"@{thirdColumnValue.Item1}", thirdColumnValue.Item2),
            MakeParameter($"@{fourthColumnValue.Item1}", fourthColumnValue.Item2),
            MakeParameter($"@{fifthColumnValue.Item1}", fifthColumnValue.Item2),
            MakeParameter($"@{sixthColumnValue.Item1}", sixthColumnValue.Item2))
    End Sub
    Public Sub ReplaceRecord(
                     Of TFirstColumn,
                         TSecondColumn,
                         TThirdColumn,
                         TFourthColumn,
                         TFifthColumn,
                         TSixthColumn,
                         TSeventhColumn)(
                                      initializer As Action,
                                      tableName As String,
                                      firstColumnValue As (String, TFirstColumn),
                                      secondColumnValue As (String, TSecondColumn),
                                      thirdColumnValue As (String, TThirdColumn),
                                      fourthColumnValue As (String, TFourthColumn),
                                      fifthColumnValue As (String, TFifthColumn),
                                      sixthColumnValue As (String, TSixthColumn),
                                      seventhColumnValue As (String, TSeventhColumn))
        initializer()
        ExecuteNonQuery(
            $"REPLACE INTO [{tableName}]
            (
                [{firstColumnValue.Item1}],
                [{secondColumnValue.Item1}],
                [{thirdColumnValue.Item1}],
                [{fourthColumnValue.Item1}],
                [{fifthColumnValue.Item1}],
                [{sixthColumnValue.Item1}],
                [{seventhColumnValue.Item1}]
            ) 
            VALUES
            (
                @{firstColumnValue.Item1},
                @{secondColumnValue.Item1},
                @{thirdColumnValue.Item1},
                @{fourthColumnValue.Item1},
                @{fifthColumnValue.Item1},
                @{sixthColumnValue.Item1},
                @{seventhColumnValue.Item1}
            );",
            MakeParameter($"@{firstColumnValue.Item1}", firstColumnValue.Item2),
            MakeParameter($"@{secondColumnValue.Item1}", secondColumnValue.Item2),
            MakeParameter($"@{thirdColumnValue.Item1}", thirdColumnValue.Item2),
            MakeParameter($"@{fourthColumnValue.Item1}", fourthColumnValue.Item2),
            MakeParameter($"@{fifthColumnValue.Item1}", fifthColumnValue.Item2),
            MakeParameter($"@{sixthColumnValue.Item1}", sixthColumnValue.Item2),
            MakeParameter($"@{seventhColumnValue.Item1}", seventhColumnValue.Item2))
    End Sub
    Public Function CreateRecord(initializer As Action, tableName As String) As Long
        initializer()
        ExecuteNonQuery($"INSERT INTO [{tableName}] DEFAULT VALUES;")
        Return LastInsertRowId
    End Function
    Public Function CreateRecord(Of TColumn)(initializer As Action, tableName As String, columnValue As (String, TColumn)) As Long
        initializer()
        ExecuteNonQuery(
            $"INSERT INTO [{tableName}] ([{columnValue.Item1}]) VALUES(@{columnValue.Item1});",
            MakeParameter($"@{columnValue.Item1}", columnValue.Item2))
        Return LastInsertRowId
    End Function
    Public Function CreateRecord(Of TColumn)(tableName As String, columnValue As (String, TColumn)) As Long
        Return CreateRecord(
            AddressOf NoInitializer,
            tableName,
            columnValue)
    End Function
    Public Function CreateRecord(Of TFirstColumn, TSecondColumn)(initializer As Action, tableName As String, firstColumnValue As (String, TFirstColumn), secondColumnValue As (String, TSecondColumn)) As Long
        initializer()
        ExecuteNonQuery(
            $"INSERT INTO [{tableName}] ([{firstColumnValue.Item1}],[{secondColumnValue.Item1}]) VALUES(@{firstColumnValue.Item1},@{secondColumnValue.Item1});",
            MakeParameter($"@{firstColumnValue.Item1}", firstColumnValue.Item2),
            MakeParameter($"@{secondColumnValue.Item1}", secondColumnValue.Item2))
        Return LastInsertRowId
    End Function
    Public Function CreateRecord(Of TFirstColumn, TSecondColumn)(tableName As String, firstColumnValue As (String, TFirstColumn), secondColumnValue As (String, TSecondColumn)) As Long
        Return CreateRecord(AddressOf NoInitializer, tableName, firstColumnValue, secondColumnValue)
    End Function
    Public Function CreateRecord(Of TFirstColumn, TSecondColumn, TThirdColumn)(initializer As Action, tableName As String, firstColumnValue As (String, TFirstColumn), secondColumnValue As (String, TSecondColumn), thirdColumnValue As (String, TThirdColumn)) As Long
        initializer()
        ExecuteNonQuery(
            $"INSERT INTO [{tableName}] ([{firstColumnValue.Item1}],[{secondColumnValue.Item1}],[{thirdColumnValue.Item1}]) VALUES(@{firstColumnValue.Item1},@{secondColumnValue.Item1},@{thirdColumnValue.Item1});",
            MakeParameter($"@{firstColumnValue.Item1}", firstColumnValue.Item2),
            MakeParameter($"@{secondColumnValue.Item1}", secondColumnValue.Item2),
            MakeParameter($"@{thirdColumnValue.Item1}", thirdColumnValue.Item2))
        Return LastInsertRowId
    End Function
    Public Function CreateRecord(Of TFirstColumn, TSecondColumn, TThirdColumn)(tableName As String, firstColumnValue As (String, TFirstColumn), secondColumnValue As (String, TSecondColumn), thirdColumnValue As (String, TThirdColumn)) As Long
        Return CreateRecord(AddressOf NoInitializer, tableName, firstColumnValue, secondColumnValue, thirdColumnValue)
    End Function
    Public Function CreateRecord(Of TFirstColumn, TSecondColumn, TThirdColumn, TFourthColumn)(tableName As String, firstColumnValue As (String, TFirstColumn), secondColumnValue As (String, TSecondColumn), thirdColumnValue As (String, TThirdColumn), fourthColumnValue As (String, TFourthColumn)) As Long
        Return CreateRecord(
            AddressOf NoInitializer,
            tableName,
            firstColumnValue,
            secondColumnValue,
            thirdColumnValue,
            fourthColumnValue)
    End Function

    Public Function CreateRecord(Of TFirstColumn, TSecondColumn, TThirdColumn, TFourthColumn)(initializer As Action, tableName As String, firstColumnValue As (String, TFirstColumn), secondColumnValue As (String, TSecondColumn), thirdColumnValue As (String, TThirdColumn), fourthColumnValue As (String, TFourthColumn)) As Long
        initializer()
        ExecuteNonQuery(
            $"INSERT INTO [{tableName}] ([{firstColumnValue.Item1}],[{secondColumnValue.Item1}],[{thirdColumnValue.Item1}],[{fourthColumnValue.Item1}]) VALUES(@{firstColumnValue.Item1},@{secondColumnValue.Item1},@{thirdColumnValue.Item1}, @{fourthColumnValue.Item1});",
            MakeParameter($"@{firstColumnValue.Item1}", firstColumnValue.Item2),
            MakeParameter($"@{secondColumnValue.Item1}", secondColumnValue.Item2),
            MakeParameter($"@{thirdColumnValue.Item1}", thirdColumnValue.Item2),
            MakeParameter($"@{fourthColumnValue.Item1}", fourthColumnValue.Item2))
        Return LastInsertRowId
    End Function
    Public Function CreateRecord(Of TFirstColumn, TSecondColumn, TThirdColumn, TFourthColumn, TFifthColumn)(tableName As String, firstColumnValue As (String, TFirstColumn), secondColumnValue As (String, TSecondColumn), thirdColumnValue As (String, TThirdColumn), fourthColumnValue As (String, TFourthColumn), fifthColumnValue As (String, TFifthColumn)) As Long
        Return CreateRecord(AddressOf NoInitializer, tableName, firstColumnValue, secondColumnValue, thirdColumnValue, fourthColumnValue, fifthColumnValue)
    End Function
    Public Function CreateRecord(Of TFirstColumn, TSecondColumn, TThirdColumn, TFourthColumn, TFifthColumn)(initializer As Action, tableName As String, firstColumnValue As (String, TFirstColumn), secondColumnValue As (String, TSecondColumn), thirdColumnValue As (String, TThirdColumn), fourthColumnValue As (String, TFourthColumn), fifthColumnValue As (String, TFifthColumn)) As Long
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
    Public Function ReadCountForColumnValue(Of TInputColumn)(initializer As Action, tableName As String, inputColumnValue As (String, TInputColumn)) As Long
        initializer()
        Return ExecuteScalar(Of Long)(
            $"SELECT 
                COUNT(1) 
            FROM [{tableName}] 
            WHERE 
                [{inputColumnValue.Item1}]=@{inputColumnValue.Item1};",
            MakeParameter($"@{inputColumnValue.Item1}", inputColumnValue.Item2)).Value
    End Function
    Public Function ReadCountForColumnValue(Of TInputColumn)(
                                                            tableName As String,
                                                            inputColumnValue As (String, TInputColumn)) As Long
        Return ReadCountForColumnValue(AddressOf NoInitializer, tableName, inputColumnValue)
    End Function
    Public Function ReadCount(initializer As Action, tableName As String) As Long
        initializer()
        Return ExecuteScalar(Of Long)(
            $"SELECT 
                COUNT(1) 
            FROM [{tableName}];").Value
    End Function
    Public Function ReadCount(tableName As String) As Long
        Return ReadCount(AddressOf NoInitializer, tableName)
    End Function
    Public Function ReadCountForColumnValues(Of TFirstInputColumn, TSecondInputColumn)(initializer As Action, tableName As String, firstColumnValue As (String, TFirstInputColumn), secondColumnValue As (String, TSecondInputColumn)) As Long
        initializer()
        Return ExecuteScalar(Of Long)(
            $"SELECT 
                COUNT(1) 
            FROM [{tableName}] 
            WHERE 
                [{firstColumnValue.Item1}]=@{firstColumnValue.Item1} 
                AND [{secondColumnValue.Item1}]=@{secondColumnValue.Item1};",
            MakeParameter($"@{firstColumnValue.Item1}", firstColumnValue.Item2),
            MakeParameter($"@{secondColumnValue.Item1}", secondColumnValue.Item2)).Value
    End Function
    Public Function ReadCountForColumnValues(Of TFirstInputColumn, TSecondInputColumn)(tableName As String, firstColumnValue As (String, TFirstInputColumn), secondColumnValue As (String, TSecondInputColumn)) As Long
        Return ReadCountForColumnValues(AddressOf NoInitializer, tableName, firstColumnValue, secondColumnValue)
    End Function
End Class
