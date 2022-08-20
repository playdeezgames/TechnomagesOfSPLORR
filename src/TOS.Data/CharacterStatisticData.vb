Public Class CharacterStatisticData
    Inherits BaseData
    Friend Const ViewName = "CharacterStatistics"
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn
    Friend Const StatisticTypeIdColumn = StatisticTypeData.StatisticTypeIdColumn
    Friend Const DeltaColumn = "Delta"

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
End Class
