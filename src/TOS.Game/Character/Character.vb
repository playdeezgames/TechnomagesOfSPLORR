Public Class Character
    Inherits BaseThingie

    Public Sub New(world As World, characterId As Long)
        MyBase.New(world, characterId)
    End Sub
    Shared Function FromId(world As World, characterId As Long?) As Character
        Return If(characterId.HasValue, New Character(world, characterId.Value), Nothing)
    End Function
    Public ReadOnly Property UniqueName As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property
    Public ReadOnly Property HasEquipment As Boolean
        Get
            Return WorldData.CharacterEquipSlot.ReadCount(Id) > 0
        End Get
    End Property

    Public ReadOnly Property Equipment As IEnumerable(Of (EquipSlot, Item))
        Get
            Return WorldData.
                CharacterEquipSlot.
                ReadForCharacter(Id).
                Select(
                    Function(x) (Game.EquipSlot.FromId(World, x.Item1), Item.FromId(World, x.Item2)))
        End Get
    End Property


    Public Sub Equip(item As Item)
        If Not CanEquip(item) Then
            Return
        End If
        WorldData.InventoryItem.ClearForItem(item.Id)
        Dim neededEquipSlots = item.FindEquipConfiguration(AvailableEquipSlots)
        UnequipEquipSlots(neededEquipSlots)
        For Each neededEquipSlot In neededEquipSlots
            PlaceItemInEquipSlot(item, neededEquipSlot)
        Next
    End Sub

    Private Sub PlaceItemInEquipSlot(item As Item, equipSlot As EquipSlot)
        WorldData.CharacterEquipSlot.Write(Id, equipSlot.Id, item.Id)
    End Sub

    Private ReadOnly Property EquipSlot(slot As EquipSlot) As Item
        Get
            Return Item.FromId(World, WorldData.CharacterEquipSlot.Read(Id, slot.Id))
        End Get
    End Property

    Public ReadOnly Property CanDelete As Boolean
        Get
            Return Not OnTheTeam AndAlso
                Not CanJoin AndAlso
                Not HasInventory() AndAlso
                Not HasEquipment
        End Get
    End Property

    Private Sub UnequipEquipSlots(equipSlots As IEnumerable(Of EquipSlot))
        For Each slot In equipSlots
            Dim item = EquipSlot(slot)
            If item IsNot Nothing Then
                WorldData.CharacterEquipSlot.ClearForItem(item.Id)
                Inventory.Add(item)
            End If
        Next
    End Sub

    Public Sub Destroy()
        If HasInventory() Then
            Inventory.Destroy()
        End If
        WorldData.Character.Clear(Id)
    End Sub
    Public ReadOnly Property Items As IEnumerable(Of Item)
        Get
            Return If(HasInventory(), Inventory.Items, Array.Empty(Of Item))
        End Get
    End Property

    Public Sub Join()
        If CanJoin AndAlso Not OnTheTeam Then
            WorldData.Team.Write(Id)
        End If
    End Sub

    Public ReadOnly Property OnTheTeam As Boolean
        Get
            Return WorldData.Team.Exists(Id)
        End Get
    End Property

    Friend Sub LeaveTeam()
        WorldData.Team.ClearForCharacterId(Id)
    End Sub

    Public Property CanLeave() As Boolean
        Get
            Return WorldData.TeamCharacter.ReadCanLeave(Id).Value > 0
        End Get
        Set(value As Boolean)
            WorldData.TeamCharacter.Write(Id, value)
        End Set
    End Property

    Public Property Name As String
        Get
            Return WorldData.Character.ReadName(Id)
        End Get
        Set(value As String)
            WorldData.Character.WriteName(Id, value)
        End Set
    End Property
    ReadOnly Property FullName As String
        Get
            Return $"{Name} the {CharacterType.Name}"
        End Get
    End Property

    ReadOnly Property AvailableEquipSlots As IEnumerable(Of EquipSlot)
        Get
            Return WorldData.CharacterAvailableEquipSlot.ReadForCharacter(Id).Select(Function(x) Game.EquipSlot.FromId(World, x))
        End Get
    End Property

    Public Function CanEquip(item As Item) As Boolean
        Return item.CanBeEquipped(AvailableEquipSlots)
    End Function

    Public Property CanJoin As Boolean
        Get
            Return WorldData.TeamCharacter.Exists(Id)
        End Get
        Set(value As Boolean)
            If value Then
                WorldData.TeamCharacter.Write(Id, False)
            Else
                WorldData.TeamCharacter.Clear(Id)
            End If
        End Set
    End Property

    Public Property CharacterType As CharacterType
        Get
            Return CharacterType.FromId(World, WorldData.Character.ReadCharacterType(Id).Value)
        End Get
        Set(value As CharacterType)
            WorldData.Character.WriteCharacterType(Id, value.Id)
        End Set
    End Property

    Public Sub Leave()
        WorldData.Team.ClearForCharacterId(Id)
    End Sub

    Property Location As Location
        Get
            Return Location.FromId(World, WorldData.Character.ReadLocation(Id).Value)
        End Get
        Set(value As Location)
            WorldData.Character.WriteLocation(Id, value.Id)
        End Set
    End Property
    ReadOnly Property Team As Team
        Get
            Return If(WorldData.Team.Exists(Id), New Team(World), Nothing)
        End Get
    End Property

    Public Function CanGive() As Boolean
        Return If(Team?.CharacterCount, 0) > 1
    End Function

    Public ReadOnly Property Teammates As IEnumerable(Of Character)
        Get
            Return If(Team?.Characters, Array.Empty(Of Character)).Where(Function(x) x.Id <> Id)
        End Get
    End Property

    Public Function HasInventory() As Boolean
        Return WorldData.Inventory.ReadCountForCharacter(Id) > 0
    End Function
    Public ReadOnly Property HasItems As Boolean
        Get
            Return HasInventory() AndAlso Inventory.HasItems
        End Get
    End Property

    Public ReadOnly Property Inventory As Inventory
        Get
            Dim inventoryId = WorldData.Inventory.ReadForCharacter(Id)
            If inventoryId.HasValue Then
                Return Inventory.FromId(World, inventoryId.Value)
            End If
            inventoryId = WorldData.Inventory.CreateForCharacter(Id)
            Return Inventory.FromId(World, inventoryId.Value)
        End Get
    End Property

    Public ReadOnly Property Statistics As IEnumerable(Of StatisticType)
        Get
            Return WorldData.CharacterBaseStatistic.ForCharacter(Id).Select(Function(x) StatisticType.FromId(World, x))
        End Get
    End Property
    Public ReadOnly Property EquippedItems As IEnumerable(Of Item)
        Get
            Return WorldData.CharacterEquippedItem.ForCharacter(Id).Select(Function(x) Item.FromId(World, x))
        End Get
    End Property

    Public ReadOnly Property Statistic(statisticType As StatisticType) As Long?
        Get
            Dim result = WorldData.CharacterBaseStatistic.Read(Id, statisticType.Id)
            result += If(Location.Statistic(statisticType), 0)
            Dim items = EquippedItems
            For Each item In items
                result += If(item.Statistic(statisticType), 0)
            Next
            Return result
        End Get
    End Property
    Public ReadOnly Property HasStatisticDeltas As Boolean
        Get
            Return WorldData.CharacterStatistic.CountForCharacter(Id) > 0
        End Get
    End Property

    Public ReadOnly Property StatisticDeltas() As IEnumerable(Of (StatisticType, Long))
        Get
            Return WorldData.CharacterStatistic.
                ReadForCharacter(Id).Select(Function(x) (StatisticType.FromId(World, x.Item1), x.Item2))
        End Get
    End Property

    Public Property StatisticDelta(statisticType As StatisticType) As Long?
        Get
            Return WorldData.CharacterStatistic.Read(Id, statisticType.Id)
        End Get
        Set(value As Long?)
            If value.HasValue Then
                WorldData.CharacterStatistic.Write(Id, statisticType.Id, value.Value)
            Else
                WorldData.CharacterStatistic.Clear(Id, statisticType.Id)
            End If
        End Set
    End Property
End Class
