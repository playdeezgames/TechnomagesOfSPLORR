Public Class CharacterEquippedItemData
    Inherits BaseData
    Friend Const ViewName = "CharacterEquippedItems"
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn
    Friend Const ItemIdColumn = ItemData.ItemIdColumn

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function ForCharacter(characterId As Long) As IEnumerable(Of Long)
        Return Store.ReadRecordsWithColumnValue(Of Long, Long)(ViewName, ItemIdColumn, (CharacterIdColumn, characterId))
    End Function
End Class
