Public Class VergeData
    Inherits BaseData
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
End Class
