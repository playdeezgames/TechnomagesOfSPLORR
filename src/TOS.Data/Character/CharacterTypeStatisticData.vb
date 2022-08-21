Public Class CharacterTypeStatisticData
    Inherits BaseData
    Friend Const TableName = "CharacterTypeStatistics"
    Friend Const CharacterTypeIdColumn = CharacterTypeData.CharacterTypeIdColumn
    Friend Const StatisticTypeIdColumn = StatisticTypeData.StatisticTypeIdColumn
    Friend Const StatisticDeltaColumn = "StatisticDelta"

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function ReadForCharacterType(characterTypeId As Long) As IEnumerable(Of Tuple(Of Long, Long))
        Return Store.ReadRecordsWithColumnValue(Of Long, Long, Long)(
            TableName,
            (StatisticTypeIdColumn, StatisticDeltaColumn),
            (CharacterTypeIdColumn, characterTypeId))
    End Function

    Public Function CountForCharacterType(characterTypeId As Long) As Long
        Return Store.ReadCountForColumnValue(TableName, (CharacterTypeIdColumn, characterTypeId))
    End Function

    Public Sub Clear(characterTypeId As Long, statisticTypeId As Long)
        Store.ClearForColumnValues(
            TableName,
            (CharacterTypeIdColumn, characterTypeId),
            (StatisticTypeIdColumn, statisticTypeId))
    End Sub

    Public Function Read(characterTypeId As Long, statisticTypeId As Long) As Long?
        Return Store.ReadColumnValue(Of Long, Long, Long)(
            TableName,
            StatisticDeltaColumn,
            (CharacterTypeIdColumn, characterTypeId),
            (StatisticTypeIdColumn, statisticTypeId))
    End Function

    Public Sub Write(characterTypeId As Long, statisticTypeId As Long, value As Long)
        Store.ReplaceRecord(
            TableName,
            (CharacterTypeIdColumn, characterTypeId),
            (StatisticTypeIdColumn, statisticTypeId),
            (StatisticDeltaColumn, value))
    End Sub
End Class
