Public Class Inventory
    Inherits BaseThingie

    Public Sub New(worldData As WorldData, id As Long)
        MyBase.New(worldData, id)
    End Sub
    Shared Function FromId(worldData As WorldData, id As Long) As Inventory
        Return New Inventory(worldData, id)
    End Function

    Friend Function HasItems() As Boolean
        Return WorldData.InventoryItem.ReadItemCount(Id) > 0
    End Function

    Friend ReadOnly Property Items As IEnumerable(Of Item)
        Get
            Return WorldData.InventoryItem.ReadForInventory(Id).Select(Function(x) Item.FromId(WorldData, x))
        End Get
    End Property
    Public ReadOnly Property ItemStacks As IEnumerable(Of (ItemType, IEnumerable(Of Item)))
        Get
            Return WorldData.InventoryItemType.ReadItemStacks(Id).Select(Function(x) (ItemType.FromId(WorldData, x.Item1), x.Item2.Select(Function(y) Item.FromId(WorldData, y))))
        End Get
    End Property
    Public Sub Add(item As Item)
        WorldData.InventoryItem.Write(item.Id, Id)
    End Sub

    Public Sub Add(items As IEnumerable(Of Item))
        For Each item In items
            Add(item)
        Next
    End Sub

    Friend Sub Destroy()
        WorldData.Inventory.Clear(Id)
    End Sub
End Class
