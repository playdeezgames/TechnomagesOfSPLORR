Public Class VergeTypeData
    Inherits BaseData
    Implements IProvidesAll
    Friend Const TableName = "VergeTypes"
    Friend Const VergeTypeIdColumn = "VergeTypeId"
    Friend Const VergeTypeNameColumn = "VergeTypeName"

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function All() As IEnumerable(Of Long) Implements IProvidesAll.All
        Return Store.ReadRecords(Of Long)(
            TableName,
            VergeTypeIdColumn)
    End Function

    Public Function ReadName(vergeTypeId As Long) As String
        Return Store.ReadColumnString(TableName, VergeTypeNameColumn, (VergeTypeIdColumn, vergeTypeId))
    End Function

    Public Sub WriteName(vergeTypeId As Long, value As String)
        Store.WriteColumnValue(TableName, (VergeTypeNameColumn, value), (VergeTypeIdColumn, vergeTypeId))
    End Sub

    Public Sub Clear(vergeTypeId As Long)
        Store.ClearForColumnValue(TableName, (VergeTypeIdColumn, vergeTypeId))
    End Sub

    Public Function Create(newName As String) As Long
        Return Store.CreateRecord(TableName, (VergeTypeNameColumn, newName))
    End Function
End Class
