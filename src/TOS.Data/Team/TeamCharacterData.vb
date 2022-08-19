﻿Public Class TeamCharacterData
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
End Class