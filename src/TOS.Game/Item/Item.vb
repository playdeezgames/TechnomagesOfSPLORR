Public Class Item
    Inherits BaseThingie

    Public Sub New(worldData As WorldData, id As Long)
        MyBase.New(worldData, id)
    End Sub
    Public Shared Function FromId(worldData As WorldData, id As Long?) As Item
        Return If(id.HasValue, New Item(worldData, id.Value), Nothing)
    End Function

    Public ReadOnly Property Name As String
        Get
            Return ItemType.Name
        End Get
    End Property
    Public ReadOnly Property UniqueName As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property
    Public ReadOnly Property ItemType As ItemType
        Get
            Return ItemType.FromId(WorldData, WorldData.Item.ReadItemType(Id).Value)
        End Get
    End Property

    Friend Function FindEquipConfiguration(availableEquipSlots As IEnumerable(Of EquipSlot)) As IEnumerable(Of EquipSlot)
        Dim available = New HashSet(Of Long)(availableEquipSlots.Select(Function(x) x.Id))
        Return NeededEquipSlots.Select(
            Function(x) New HashSet(Of Long)(x.Select(Function(y) y.Id))).
                First(Function(x) x.IsSubsetOf(available)).
                Select(Function(x) EquipSlot.FromId(WorldData, x))
    End Function

    Public ReadOnly Property NeededEquipSlots As IEnumerable(Of IEnumerable(Of EquipSlot))
        Get
            Return WorldData.ItemEquipSlot.
                ReadForItem(Id).
                GroupBy(Function(x) x.Item1).
                Select(Function(x) x.Select(
                    Function(y) EquipSlot.FromId(WorldData, y.Item2)))
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
        WorldData.InventoryItem.ClearForItem(Id)
        WorldData.Item.Clear(Id)
    End Sub
End Class
