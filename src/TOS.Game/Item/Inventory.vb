Public Class Inventory
    Inherits BaseThingie

    Public Sub New(world As World, id As Long)
        MyBase.New(world, id)
    End Sub
    Shared Function FromId(world As World, id As Long) As Inventory
        Return New Inventory(world, id)
    End Function

    Friend Function HasItems() As Boolean
        Return WorldData.InventoryItem.ReadItemCount(Id) > 0
    End Function

    Friend ReadOnly Property Items As IEnumerable(Of Item)
        Get
            Return WorldData.InventoryItem.ReadForInventory(Id).Select(Function(x) Item.FromId(World, x))
        End Get
    End Property
    Public ReadOnly Property ItemStacks As IEnumerable(Of (ItemType, IEnumerable(Of Item)))
        Get
            Return WorldData.InventoryItemType.ReadItemStacks(Id).Select(Function(x) (ItemType.FromId(World, x.Item1), x.Item2.Select(Function(y) Item.FromId(World, y))))
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
