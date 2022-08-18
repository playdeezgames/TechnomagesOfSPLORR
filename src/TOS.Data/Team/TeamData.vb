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

    Public Function ReadCount() As Long
        Return Store.ReadCount(TableName)
    End Function

    Public Function Exists(characterId As Long) As Boolean
        Return Store.ReadColumnValue(Of Long, Long)(
            TableName,
            CharacterIdColumn,
            (CharacterIdColumn, characterId)).HasValue
    End Function
End Class
