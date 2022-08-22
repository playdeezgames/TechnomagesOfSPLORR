Public Class CharacterEquipSlotData
    Inherits BaseData
    Friend Const TableName = "CharacterEquipSlots"
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn
    Friend Const EquipSlotIdColumn = EquipSlotData.EquipSlotIdColumn
    Friend Const ItemIdColumn = ItemData.ItemIdColumn

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Sub Write(characterId As Long, equipSlotId As Long, itemId As Long)
        Store.ReplaceRecord(TableName, (CharacterIdColumn, characterId), (EquipSlotIdColumn, equipSlotId), (ItemIdColumn, itemId))
    End Sub

    Public Function ReadCount(characterId As Long) As Long
        Return Store.ReadCountForColumnValue(TableName, (CharacterIdColumn, characterId))
    End Function

    Public Function ReadForCharacter(characterId As Long) As IEnumerable(Of Tuple(Of Long, Long))
        Return Store.ReadRecordsWithColumnValue(
            Of Long, Long, Long)(
            TableName,
            (EquipSlotIdColumn, ItemIdColumn),
            (CharacterIdColumn, characterId))
    End Function

    Public Function CountForEquipSlot(equipSlotId As Long) As Long
        Return Store.ReadCountForColumnValue(TableName, (EquipSlotIdColumn, equipSlotId))
    End Function

    Public Function Read(characterId As Long, equipSlotId As Long) As Long?
        Return Store.ReadColumnValue(Of Long, Long, Long)(
            TableName,
            ItemIdColumn,
            (CharacterIdColumn, characterId),
            (EquipSlotIdColumn, equipSlotId))
    End Function

    Public Sub ClearForItem(itemId As Long)
        Store.ClearForColumnValue(TableName, (ItemIdColumn, itemId))
    End Sub
End Class
