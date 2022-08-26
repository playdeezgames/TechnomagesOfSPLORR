Public Class Character
    Inherits BaseThingie

    Public Sub New(worldData As WorldData, characterId As Long)
        MyBase.New(worldData, characterId)
    End Sub
    Shared Function FromId(worldData As WorldData, characterId As Long) As Character
        Return New Character(worldData, characterId)
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
                    Function(x) (Game.EquipSlot.FromId(WorldData, x.Item1), Item.FromId(WorldData, x.Item2)))
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
            Return Item.FromId(WorldData, WorldData.CharacterEquipSlot.Read(Id, slot.Id))
        End Get
    End Property

    Public ReadOnly Property CanDelete As Boolean
        Get
            Return Not OnTheTeam AndAlso Not CanJoin AndAlso Not HasInventory() AndAlso Not HasEquipment
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
        WorldData.Character.Clear(Id)
    End Sub

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
            Return WorldData.CharacterAvailableEquipSlot.ReadForCharacter(Id).Select(Function(x) Game.EquipSlot.FromId(WorldData, x))
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
            Return CharacterType.FromId(WorldData, WorldData.Character.ReadCharacterType(Id).Value)
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
            Return Location.FromId(WorldData, WorldData.Character.ReadLocation(Id).Value)
        End Get
        Set(value As Location)
            WorldData.Character.WriteLocation(Id, value.Id)
        End Set
    End Property
    ReadOnly Property Team As Team
        Get
            Return If(WorldData.Team.Exists(Id), New Team(WorldData), Nothing)
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

    Public ReadOnly Property Inventory As Inventory
        Get
            Dim inventoryId = WorldData.Inventory.ReadForCharacter(Id)
            If inventoryId.HasValue Then
                Return Inventory.FromId(WorldData, inventoryId.Value)
            End If
            inventoryId = WorldData.Inventory.CreateForCharacter(Id)
            Return Inventory.FromId(WorldData, inventoryId.Value)
        End Get
    End Property

    Public ReadOnly Property Statistics As IEnumerable(Of StatisticType)
        Get
            Return WorldData.CharacterStatistic.ForCharacter(Id).Select(Function(x) StatisticType.FromId(WorldData, x))
        End Get
    End Property
    Public ReadOnly Property EquippedItems As IEnumerable(Of Item)
        Get
            Return WorldData.CharacterEquippedItem.ForCharacter(Id).Select(Function(x) Item.FromId(WorldData, x))
        End Get
    End Property

    Public ReadOnly Property Statistic(statisticType As StatisticType) As Long?
        Get
            Dim result = WorldData.CharacterStatistic.Read(Id, statisticType.Id)
            result += If(Location.Statistic(statisticType), 0)
            Dim items = EquippedItems
            For Each item In items
                result += If(item.Statistic(statisticType), 0)
            Next
            Return result
        End Get
    End Property
End Class
