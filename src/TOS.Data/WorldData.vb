Public Class WorldData
    Inherits BaseData
    Public ReadOnly Character As CharacterData
    Public ReadOnly CharacterAvailableEquipSlot As CharacterAvailableEquipSlotData
    Public ReadOnly CharacterBaseStatistic As CharacterBaseStatisticData
    Public ReadOnly CharacterEquippedItem As CharacterEquippedItemData
    Public ReadOnly CharacterEquipSlot As CharacterEquipSlotData
    Public ReadOnly CharacterStatistic As CharacterStatisticData
    Public ReadOnly CharacterType As CharacterTypeData
    Public ReadOnly CharacterTypeEquipSlot As CharacterTypeEquipSlotData
    Public ReadOnly CharacterTypeStatistic As CharacterTypeStatisticData
    Public ReadOnly ConditionType As ConditionTypeData
    Public ReadOnly EquipSlot As EquipSlotData
    Public ReadOnly Inventory As InventoryData
    Public ReadOnly InventoryItem As InventoryItemData
    Public ReadOnly InventoryItemType As InventoryItemTypeData
    Public ReadOnly Item As ItemData
    Public ReadOnly ItemBaseStatistic As ItemBaseStatisticData
    Public ReadOnly ItemEquipSlot As ItemEquipSlotData
    Public ReadOnly ItemStatistic As ItemStatisticData
    Public ReadOnly ItemType As ItemTypeData
    Public ReadOnly ItemTypeEquipSlot As ItemTypeEquipSlotData
    Public ReadOnly ItemTypeStatistic As ItemTypeStatisticData
    Public ReadOnly Location As LocationData
    Public ReadOnly LocationBaseStatistic As LocationBaseStatisticData
    Public ReadOnly LocationCharacter As LocationCharacterData
    Public ReadOnly LocationStatistic As LocationStatisticData
    Public ReadOnly LocationType As LocationTypeData
    Public ReadOnly LocationTypeStatistic As LocationTypeStatisticData
    Public ReadOnly Route As RouteData
    Public ReadOnly RouteType As RouteTypeData
    Public ReadOnly StatisticType As StatisticTypeData
    Public ReadOnly Team As TeamData
    Public ReadOnly TeamCharacter As TeamCharacterData
    Public ReadOnly TeamEquipSlot As TeamEquipSlotData
    Public ReadOnly TeamItem As TeamItemData
    Public ReadOnly Verge As VergeData
    Public ReadOnly VergeType As VergeTypeData

    Public Sub New(store As Store)
        MyBase.New(store)
        Character = New CharacterData(store)
        CharacterAvailableEquipSlot = New CharacterAvailableEquipSlotData(store)
        CharacterBaseStatistic = New CharacterBaseStatisticData(store)
        CharacterEquippedItem = New CharacterEquippedItemData(store)
        CharacterEquipSlot = New CharacterEquipSlotData(store)
        CharacterStatistic = New CharacterStatisticData(store)
        CharacterType = New CharacterTypeData(store)
        CharacterTypeEquipSlot = New CharacterTypeEquipSlotData(store)
        CharacterTypeStatistic = New CharacterTypeStatisticData(store)
        ConditionType = New ConditionTypeData(store)
        EquipSlot = New EquipSlotData(store)
        Inventory = New InventoryData(store)
        InventoryItem = New InventoryItemData(store)
        InventoryItemType = New InventoryItemTypeData(store)
        Item = New ItemData(store)
        ItemBaseStatistic = New ItemBaseStatisticData(store)
        ItemEquipSlot = New ItemEquipSlotData(store)
        ItemStatistic = New ItemStatisticData(store)
        ItemType = New ItemTypeData(store)
        ItemTypeEquipSlot = New ItemTypeEquipSlotData(store)
        ItemTypeStatistic = New ItemTypeStatisticData(store)
        Location = New LocationData(store)
        LocationBaseStatistic = New LocationBaseStatisticData(store)
        LocationCharacter = New LocationCharacterData(store)
        LocationStatistic = New LocationStatisticData(store)
        LocationType = New LocationTypeData(store)
        LocationTypeStatistic = New LocationTypeStatisticData(store)
        Route = New RouteData(store)
        RouteType = New RouteTypeData(store)
        StatisticType = New StatisticTypeData(store)
        Team = New TeamData(store)
        TeamCharacter = New TeamCharacterData(store)
        TeamEquipSlot = New TeamEquipSlotData(store)
        TeamItem = New TeamItemData(store)
        Verge = New VergeData(store)
        VergeType = New VergeTypeData(store)
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
