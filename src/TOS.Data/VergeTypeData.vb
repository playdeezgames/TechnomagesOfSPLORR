Public Class VergeTypeData
    Inherits BaseData
    Friend Const TableName = "VergeTypes"
    Friend Const VergeTypeIdColumn = "VergeTypeId"
    Friend Const VergeTypeNameColumn = "VergeTypeName"

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function All() As IEnumerable(Of Long)
        Return Store.ReadRecords(Of Long)(
            TableName,
            VergeTypeIdColumn)
    End Function

    Public Function ReadName(vergeTypeId As Long) As String
        Return Store.ReadColumnString(TableName, VergeTypeNameColumn, (VergeTypeIdColumn, vergeTypeId))
    End Function
End Class
