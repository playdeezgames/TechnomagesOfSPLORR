Public Class TeamCharacterData
    Inherits BaseData
    Friend Const TableName = "TeamCharacters"
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn
    Friend Const CanLeaveColumn = "CanLeave"

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function ReadCanLeave(characterId As Long) As Long?
        Return Store.ReadColumnValue(Of Long, Long)(
            TableName,
            CanLeaveColumn,
            (CharacterIdColumn, characterId))
    End Function

    Public Function Exists(characterId As Long) As Boolean
        Return Store.ReadCountForColumnValue(
            TableName,
            (CharacterIdColumn, characterId)) > 0
    End Function

    Public Sub Write(characterId As Long, canLeave As Boolean)
        Store.ReplaceRecord(
            TableName,
            (CharacterIdColumn, characterId),
            (CanLeaveColumn, If(canLeave, 1L, 0L)))
    End Sub

    Public Sub Clear(characterId As Long)
        Store.ClearForColumnValue(
            TableName,
            (CharacterIdColumn, characterId))
    End Sub
End Class
