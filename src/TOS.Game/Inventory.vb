﻿Public Class Inventory
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

    Friend ReadOnly Property Items() As IEnumerable(Of Item)
        Get
            Return WorldData.InventoryItem.ReadForInventory(Id).Select(Function(x) Item.FromId(WorldData, x))
        End Get
    End Property
End Class
