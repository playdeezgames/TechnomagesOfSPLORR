Public Class CharacterTypeEquipSlotData
    Inherits BaseData
    Friend Const TableName = "CharacterTypeEquipSlots"
    Friend Const EquipSlotIdColumn = EquipSlotData.EquipSlotIdColumn
    Friend Const CharacterTypeIdColumn = CharacterTypeData.CharacterTypeIdColumn

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function CountForEquipSlot(equipSlotId As Long) As Long
        Return Store.ReadCountForColumnValue(TableName, (EquipSlotIdColumn, equipSlotId))
    End Function

    Public Function ReadForCharacterType(characterTypeId As Long) As IEnumerable(Of Long)
        Return Store.ReadRecordsWithColumnValue(Of Long, Long)(TableName, EquipSlotIdColumn, (characterTypeidcolumn, characterTypeId))
    End Function
End Class
