Public Class TeamItemData
    Inherits BaseData
    Friend Const ViewName = "TeamItems"
    Friend Const ItemIdColumn = ItemData.ItemIdColumn

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function ReadCount() As Long
        Return Store.ReadCount(ViewName)
    End Function
End Class
