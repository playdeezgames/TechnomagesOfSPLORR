Public Class CharacterAvailableEquipSlotData
    Inherits BaseData
    Friend Const ViewName = "CharacterAvailableEquipSlots"
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn
    Friend Const EquipSlotIdColumn = EquipSlotData.EquipSlotIdColumn

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function ReadForCharacter(characterId As Long) As IEnumerable(Of Long)
        Return Store.ReadRecordsWithColumnValue(Of Long, Long)(
            ViewName,
            EquipSlotIdColumn,
            (CharacterIdColumn, characterId))
    End Function
End Class
