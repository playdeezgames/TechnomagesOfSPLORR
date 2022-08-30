Public Class ConditionTypeStatisticRangeData
    Inherits BaseData
    Friend Const TableName = "ConditionTypeStatisticRanges"
    Friend Const ConditionTypeIdColumn = ConditionTypeData.ConditionTypeIdColumn
    Friend Const StatisticTypeIdColumn = StatisticTypeData.StatisticTypeIdColumn
    Friend Const MinimumColumn = "Minimum"
    Friend Const MaximumColumn = "Maximum"

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function CountForConditionType(conditionTypeId As Long) As Long
        Return Store.ReadCountForColumnValue(TableName, (ConditionTypeIdColumn, conditionTypeId))
    End Function

    Public Function ReadForConditionType(conditionTypeId As Long) As IEnumerable(Of Tuple(Of Long, Long, Long))
        Return Store.ReadRecordsWithColumnValue(Of Long, Long, Long, Long)(
            TableName,
            (StatisticTypeIdColumn, MinimumColumn, MaximumColumn),
            (ConditionTypeIdColumn, conditionTypeId))
    End Function

    Public Function Read(conditionTypeId As Long, statisticTypeId As Long) As (Long, Long)?
        Dim result = Store.ReadRecordsWithColumnValues(Of Long, Long, Long, Long)(TableName, (MinimumColumn, MaximumColumn), (ConditionTypeIdColumn, conditionTypeId), (StatisticTypeIdColumn, statisticTypeId))
        If Not result.Any Then
            Return Nothing
        End If
        Return (result.First.Item1, result.First.Item2)
    End Function

    Public Sub Write(conditionTypeId As Long, statisticTypeId As Long, minimum As Long, maximum As Long)
        Store.ReplaceRecord(
            TableName,
            (ConditionTypeIdColumn, conditionTypeId),
            (StatisticTypeIdColumn, statisticTypeId),
            (MinimumColumn, minimum),
            (MaximumColumn, maximum))
    End Sub

    Public Sub Clear(conditionTypeId As Long, statisticTypeId As Long)
        Store.ClearForColumnValues(
            TableName,
            (ConditionTypeIdColumn, conditionTypeId),
            (StatisticTypeIdColumn, statisticTypeId))
    End Sub
End Class
