Public Class CharacterStatisticData
    Inherits BaseData
    Friend Const TableName = "CharacterStatistics"
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn
    Friend Const StatisticTypeIdColumn = StatisticTypeData.StatisticTypeIdColumn
    Friend Const StatisticDeltaColumn = "StatisticDelta"

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function CountForCharacter(characterId As Long) As Long
        Return Store.ReadCountForColumnValue(
            TableName,
            (CharacterIdColumn, characterId))
    End Function

    Public Function ReadForCharacter(characterId As Long) As IEnumerable(Of Tuple(Of Long, Long))
        Return Store.ReadRecordsWithColumnValue(Of Long, Long, Long)(
            TableName,
            (StatisticTypeIdColumn, StatisticDeltaColumn),
            (CharacterIdColumn, characterId))
    End Function

    Public Function Read(characterId As Long, statisticTypeId As Long) As Long?
        Return Store.ReadColumnValue(Of Long, Long, Long)(
            TableName,
            StatisticDeltaColumn,
            (StatisticTypeIdColumn, statisticTypeId),
            (CharacterIdColumn, characterId))
    End Function

    Public Sub Write(characterId As Long, statisticTypeId As Long, delta As Long)
        Store.ReplaceRecord(
            TableName,
            (CharacterIdColumn, characterId),
            (StatisticTypeIdColumn, statisticTypeId),
            (StatisticDeltaColumn, delta))
    End Sub

    Public Sub Clear(characterId As Long, statisticTypeId As Long)
        Store.ClearForColumnValues(
            TableName,
            (CharacterIdColumn, characterId),
            (StatisticTypeIdColumn, statisticTypeId))
    End Sub
End Class
