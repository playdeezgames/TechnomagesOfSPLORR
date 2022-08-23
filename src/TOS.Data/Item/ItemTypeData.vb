Public Class ItemTypeData
    Inherits BaseData
    Implements IProvidesAll
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

    Public Function All() As IEnumerable(Of Long) Implements IProvidesAll.All
        Return Store.ReadRecords(Of Long)(TableName, ItemTypeIdColumn)
    End Function
End Class
