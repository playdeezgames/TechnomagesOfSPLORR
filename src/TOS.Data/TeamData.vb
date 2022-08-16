Public Class TeamData
    Inherits BaseData
    Friend Const TableName = "Team"
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn
    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function ReadCharacterIds() As IEnumerable(Of Long)
        Return Store.ReadRecords(Of Long)(TableName, CharacterIdColumn)
    End Function

    Public Sub ClearForCharacterId(characterId As Long)
        Store.ClearForColumnValue(TableName, (CharacterIdColumn, characterId))
    End Sub
End Class
