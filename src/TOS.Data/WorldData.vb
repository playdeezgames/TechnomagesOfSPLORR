Public Class WorldData
    Inherits BaseData
    Public ReadOnly Character As CharacterData
    Public ReadOnly CharacterAvailableEquipSlot As CharacterAvailableEquipSlotData
    Public ReadOnly CharacterEquipSlot As CharacterEquipSlotData
    Public ReadOnly CharacterType As CharacterTypeData
    Public ReadOnly EquipSlot As EquipSlotData
    Public ReadOnly Inventory As InventoryData
    Public ReadOnly InventoryItem As InventoryItemData
    Public ReadOnly InventoryItemType As InventoryItemTypeData
    Public ReadOnly Item As ItemData
    Public ReadOnly ItemEquipSlot As ItemEquipSlotData
    Public ReadOnly ItemType As ItemTypeData
    Public ReadOnly Location As LocationData
    Public ReadOnly LocationCharacter As LocationCharacterData
    Public ReadOnly LocationType As LocationTypeData
    Public ReadOnly Route As RouteData
    Public ReadOnly Team As TeamData
    Public ReadOnly TeamCharacter As TeamCharacterData
    Public ReadOnly TeamItem As TeamItemData

    Public Sub New(store As Store)
        MyBase.New(store)
        Character = New CharacterData(store)
        CharacterAvailableEquipSlot = New CharacterAvailableEquipSlotData(store)
        CharacterEquipSlot = New CharacterEquipSlotData(store)
        CharacterType = New CharacterTypeData(store)
        EquipSlot = New EquipSlotData(store)
        Inventory = New InventoryData(store)
        InventoryItem = New InventoryItemData(store)
        InventoryItemType = New InventoryItemTypeData(store)
        Item = New ItemData(store)
        ItemEquipSlot = New ItemEquipSlotData(store)
        ItemType = New ItemTypeData(store)
        Location = New LocationData(store)
        LocationCharacter = New LocationCharacterData(store)
        LocationType = New LocationTypeData(store)
        Route = New RouteData(store)
        Team = New TeamData(store)
        TeamCharacter = New TeamCharacterData(store)
        TeamItem = New TeamItemData(store)
    End Sub

    Sub Load(filename As String)
        Store.Load(filename)
    End Sub
    Sub Save(filename As String)
        Store.Save(filename)
    End Sub

    Public Sub Reset()
        Store.Reset()
    End Sub
End Class
