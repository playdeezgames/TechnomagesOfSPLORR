Public Class CharacterStatisticData
    Inherits BaseData
    Friend Const ViewName = "CharacterStatistics"
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn
    Friend Const StatisticTypeIdColumn = StatisticTypeData.StatisticTypeIdColumn
    Friend Const StatisticDeltaColumn = "StatisticDelta"

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function ForCharacter(characterId As Long) As IEnumerable(Of Long)
        Return Store.ReadRecordsWithColumnValue(
            Of Long, Long)(
            ViewName,
            StatisticTypeIdColumn,
            (CharacterIdColumn, characterId))
    End Function

    Public Function Read(characterId As Long, statisticTypeId As Long) As Long?
        Return Store.ReadColumnValue(
            Of Long, Long, Long)(
            ViewName,
            StatisticDeltaColumn,
            (CharacterIdColumn, characterId),
            (StatisticTypeIdColumn, statisticTypeId))
    End Function
End Class
