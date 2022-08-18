Public Class WorldData
    Inherits BaseData
    Public ReadOnly Character As CharacterData
    Public ReadOnly CharacterType As CharacterTypeData
    Public ReadOnly Inventory As InventoryData
    Public ReadOnly InventoryItem As InventoryItemData
    Public ReadOnly InventoryItemType As InventoryItemTypeData
    Public ReadOnly Item As ItemData
    Public ReadOnly ItemType As ItemTypeData
    Public ReadOnly Location As LocationData
    Public ReadOnly LocationType As LocationTypeData
    Public ReadOnly Route As RouteData
    Public ReadOnly Team As TeamData

    Public Sub New(store As Store)
        MyBase.New(store)
        Character = New CharacterData(store)
        CharacterType = New CharacterTypeData(store)
        Inventory = New InventoryData(store)
        InventoryItem = New InventoryItemData(store)
        InventoryItemType = New InventoryItemTypeData(store)
        Item = New ItemData(store)
        ItemType = New ItemTypeData(store)
        Location = New LocationData(store)
        LocationType = New LocationTypeData(store)
        Route = New RouteData(store)
        Team = New TeamData(store)
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
