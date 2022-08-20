Public Class StatisticTypeData
    Inherits BaseData
    Friend Const TableName = "StatisticTypes"
    Friend Const StatisticTypeIdColumn = "StatisticTypeId"
    Friend Const StatisticTypeDisplayNameColumn = "StatisticTypeDisplayName"

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function ReadDisplayName(statisticTypeId As Long) As String
        Return Store.ReadColumnString(TableName, StatisticTypeDisplayNameColumn, (StatisticTypeIdColumn, statisticTypeId))
    End Function
End Class
