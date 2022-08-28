Public Class Item
    Inherits BaseThingie

    Public Sub New(world As World, id As Long)
        MyBase.New(world, id)
    End Sub
    Public Shared Function FromId(world As World, id As Long?) As Item
        Return If(id.HasValue, New Item(world, id.Value), Nothing)
    End Function

    Public ReadOnly Property Name As String
        Get
            Return ItemType.Name
        End Get
    End Property

    Public ReadOnly Property EquippedOn As IEnumerable(Of (Character, EquipSlot))
        Get
            Return WorldData.CharacterEquipSlot.ReadForItem(Id).Select(Function(x) (Character.FromId(World, x.Item1), EquipSlot.FromId(World, x.Item2)))
        End Get
    End Property

    Public ReadOnly Property Location As Location
        Get
            Dim inventoryId = WorldData.InventoryItem.ReadForItem(Id)
            If Not inventoryId.HasValue Then
                Return Nothing
            End If
            Return Location.FromId(World, WorldData.Inventory.ReadLocation(inventoryId.Value))
        End Get
    End Property

    Public ReadOnly Property Character As Character
        Get
            Dim inventoryId = WorldData.InventoryItem.ReadForItem(Id)
            If Not inventoryId.HasValue Then
                Return Nothing
            End If
            Return Character.FromId(World, WorldData.Inventory.ReadCharacter(inventoryId.Value))
        End Get
    End Property

    Public ReadOnly Property CanDelete As Boolean
        Get
            Return Not IsEquipped AndAlso Not HasLocation AndAlso Not HasCharacter
        End Get
    End Property

    Public ReadOnly Property HasLocation As Boolean
        Get
            Dim inventoryId = WorldData.InventoryItem.ReadForItem(Id)
            If Not inventoryId.HasValue Then
                Return False
            End If
            Return WorldData.Inventory.ReadLocation(inventoryId.Value).HasValue
        End Get
    End Property

    Public ReadOnly Property HasCharacter As Boolean
        Get
            Dim inventoryId = WorldData.InventoryItem.ReadForItem(Id)
            If Not inventoryId.HasValue Then
                Return False
            End If
            Return WorldData.Inventory.ReadCharacter(inventoryId.Value).HasValue
        End Get
    End Property

    Public ReadOnly Property UniqueName As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property

    Public ReadOnly Property IsEquipped As Boolean
        Get
            Return WorldData.CharacterEquipSlot.CountForItem(Id) > 0
        End Get
    End Property

    Public Property ItemType As ItemType
        Get
            Return ItemType.FromId(World, WorldData.Item.ReadItemType(Id).Value)
        End Get
        Set(value As ItemType)
            WorldData.Item.WriteItemType(Id, value.Id)
        End Set
    End Property

    Public Sub Unequip()
        WorldData.CharacterEquipSlot.ClearForItem(Id)
    End Sub

    Friend Function FindEquipConfiguration(availableEquipSlots As IEnumerable(Of EquipSlot)) As IEnumerable(Of EquipSlot)
        Dim available = New HashSet(Of Long)(availableEquipSlots.Select(Function(x) x.Id))
        Return NeededEquipSlots.Select(
            Function(x) New HashSet(Of Long)(x.Select(Function(y) y.Id))).
                First(Function(x) x.IsSubsetOf(available)).
                Select(Function(x) EquipSlot.FromId(World, x))
    End Function

    Public ReadOnly Property NeededEquipSlots As IEnumerable(Of IEnumerable(Of EquipSlot))
        Get
            Return WorldData.ItemEquipSlot.
                ReadForItem(Id).
                GroupBy(Function(x) x.Item1).
                Select(Function(x) x.Select(
                    Function(y) EquipSlot.FromId(World, y.Item2)))
        End Get
    End Property

    Friend Function CanBeEquipped(availableEquipSlots As IEnumerable(Of EquipSlot)) As Boolean
        Dim available = New HashSet(Of Long)(availableEquipSlots.Select(Function(x) x.Id))
        Return NeededEquipSlots.Select(
            Function(x) New HashSet(Of Long)(x.Select(Function(y) y.Id))).
                Any(Function(x) x.IsSubsetOf(available))
    End Function
    Public ReadOnly Property Statistic(statisticType As StatisticType) As Long?
        Get
            Return WorldData.ItemBaseStatistic.Read(Id, statisticType.Id)
        End Get
    End Property

    Public Sub Destroy()
        WorldData.Item.Clear(Id)
    End Sub
End Class
