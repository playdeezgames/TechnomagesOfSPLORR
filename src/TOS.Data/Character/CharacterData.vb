﻿Public Class CharacterData
    Inherits BaseData
    Friend Const TableName = "Characters"
    Friend Const CharacterIdColumn = "CharacterId"
    Friend Const CharacterNameColumn = "CharacterName"
    Friend Const CharacterTypeIdColumn = CharacterTypeData.CharacterTypeIdColumn
    Friend Const LocationIdColumn = LocationData.LocationIdColumn

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function ReadName(characterId As Long) As String
        Return Store.ReadColumnString(
            TableName,
            CharacterNameColumn,
            (CharacterIdColumn, characterId))
    End Function

    Public Function ReadCharacterType(characterId As Long) As Long?
        Return Store.ReadColumnValue(Of Long, Long)(TableName, CharacterTypeIdColumn, (CharacterIdColumn, characterId))
    End Function

    Public Function CountForCharacterType(characterTypeId As Long) As Long
        Return Store.ReadCountForColumnValue(TableName, (CharacterTypeIdColumn, characterTypeId))
    End Function

    Public Function ReadLocation(characterId As Long) As Long?
        Return Store.ReadColumnValue(Of Long, Long)(TableName, LocationIdColumn, (CharacterIdColumn, characterId))
    End Function

    Public Sub WriteLocation(characterId As Long, locationId As Long)
        Store.WriteColumnValue(
            TableName,
            (LocationIdColumn, locationId),
            (CharacterIdColumn, characterId))
    End Sub
End Class
