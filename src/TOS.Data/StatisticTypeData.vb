Public Class StatisticTypeData
    Inherits BaseData
    Friend Const TableName = "StatisticTypes"
    Friend Const StatisticTypeIdColumn = "StatisticTypeId"
    Friend Const StatisticTypeDisplayNameColumn = "StatisticTypeDisplayName"
    Friend Const StatisticTypeNameColumn = "StatisticTypeName"

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function ReadDisplayName(statisticTypeId As Long) As String
        Return Store.ReadColumnString(TableName, StatisticTypeDisplayNameColumn, (StatisticTypeIdColumn, statisticTypeId))
    End Function

    Public Sub WriteName(statisticTypeId As Long, name As String)
        Store.WriteColumnValue(TableName, (StatisticTypeNameColumn, name), (StatisticTypeIdColumn, statisticTypeId))
    End Sub

    Public Function ReadName(statisticTypeId As Long) As String
        Return Store.ReadColumnString(TableName, StatisticTypeNameColumn, (StatisticTypeIdColumn, statisticTypeId))
    End Function

    Public Sub WriteDisplayName(statisticTypeId As Long, displayName As String)
        Store.WriteColumnValue(TableName, (StatisticTypeDisplayNameColumn, displayName), (StatisticTypeIdColumn, statisticTypeId))
    End Sub

    Public Function All() As IEnumerable(Of Long)
        Return Store.ReadRecords(Of Long)(TableName, StatisticTypeIdColumn)
    End Function

    Public Sub Clear(statisticTypeId As Long)
        Store.ClearForColumnValue(TableName, (StatisticTypeIdColumn, statisticTypeId))
    End Sub
End Class
