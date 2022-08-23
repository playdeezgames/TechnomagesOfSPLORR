Public Class VergeData
    Inherits BaseData
    Implements IProvidesAll
    Friend Const TableName = "Verges"
    Friend Const VergeIdColumn = "VergeId"
    Friend Const VergeNameColumn = "VergeName"
    Friend Const VergeTypeIdColumn = VergeTypeData.VergeTypeIdColumn

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function CountForVergeType(vergeTypeId As Long) As Long
        Return Store.ReadCountForColumnValue(TableName, (VergeTypeIdColumn, vergeTypeId))
    End Function

    Public Sub WriteName(vergeId As Long, name As String)
        Store.WriteColumnValue(TableName, (VergeNameColumn, name), (VergeIdColumn, vergeId))
    End Sub

    Public Function ReadName(vergeId As Long) As String
        Return Store.ReadColumnString(TableName, VergeNameColumn, (VergeIdColumn, vergeId))
    End Function

    Public Function All() As IEnumerable(Of Long) Implements IProvidesAll.All
        Return Store.ReadRecords(Of Long)(TableName, VergeIdColumn)
    End Function

    Public Sub WriteVergeType(vergeId As Long, vergeTypeId As Long)
        Store.WriteColumnValue(
            TableName,
            (VergeTypeIdColumn, vergeTypeId),
            (VergeIdColumn, vergeId))
    End Sub

    Public Function ReadVergeType(vergeId As Long) As Long?
        Return Store.ReadColumnValue(Of Long, Long)(TableName, VergeTypeIdColumn, (VergeIdColumn, vergeId))
    End Function

    Public Function Create(name As String, vergeTypeId As Long) As Long
        Return Store.CreateRecord(TableName, (VergeNameColumn, name), (VergeTypeIdColumn, vergeTypeId))
    End Function

    Public Sub Clear(vergeId As Long)
        Store.ClearForColumnValue(TableName, (VergeIdColumn, vergeId))
    End Sub
End Class
