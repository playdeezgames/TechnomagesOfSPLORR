Public Class ItemTypeData
    Inherits BaseData
    Friend Const TableName = "ItemTypes"
    Friend Const ItemTypeIdColumn = "ItemTypeId"
    Friend Const ItemTypeNameColumn = "ItemTypeName"

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function ReadName(itemTypeId As Long) As String
        Return Store.ReadColumnString(
            TableName,
            ItemTypeNameColumn,
            (ItemTypeIdColumn, itemTypeId))
    End Function
End Class
