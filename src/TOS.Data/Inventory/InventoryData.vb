Public Class InventoryData
    Inherits BaseData
    Friend Const TableName = "Inventories"
    Friend Const InventoryIdColumn = "InventoryId"
    Friend Const LocationIdColumn = LocationData.LocationIdColumn
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function ReadCountForLocation(locationId As Long) As Long
        Return Store.ReadCountForColumnValue(
            TableName,
            (LocationIdColumn, locationId))
    End Function

    Public Function ReadForLocation(locationId As Long) As Long?
        Return Store.ReadColumnValue(Of Long, Long)(
            TableName,
            InventoryIdColumn,
            (LocationIdColumn, locationId))
    End Function

    Public Function CreateForLocation(locationId As Long) As Long
        Return Store.CreateRecord(TableName, (LocationIdColumn, locationId))
    End Function

    Public Function CreateForCharacter(characterId As Long) As Long
        Return Store.CreateRecord(TableName, (CharacterIdColumn, characterId))
    End Function

    Public Function ReadLocation(inventoryId As Long) As Long?
        Return Store.ReadColumnValue(Of Long, Long)(TableName, LocationIdColumn, (InventoryIdColumn, inventoryId))
    End Function

    Public Function ReadCharacter(inventoryId As Long) As Long?
        Return Store.ReadColumnValue(Of Long, Long)(TableName, CharacterIdColumn, (InventoryIdColumn, inventoryId))
    End Function

    Public Function ReadCountForCharacter(characterId As Long) As Long
        Return Store.ReadCountForColumnValue(
            TableName,
            (CharacterIdColumn, characterId))
    End Function

    Public Sub Clear(inventoryId As Long)
        Store.ClearForColumnValue(TableName, (InventoryIdColumn, inventoryId))
    End Sub

    Public Function ReadForCharacter(characterId As Long) As Long?
        Return Store.ReadColumnValue(Of Long, Long)(
            TableName,
            InventoryIdColumn,
            (CharacterIdColumn, characterId))
    End Function
End Class
