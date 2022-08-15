Public Class LocationData
    Inherits BaseData
    Friend Const TableName = "Locations"
    Friend Const LocationIdColumn = "LocationId"
    Friend Sub Initialize()
        Store.ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{LocationIdColumn}] INTEGER PRIMARY KEY
            );")
    End Sub

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function All() As IEnumerable(Of Long)
        Return Store.ReadRecords(Of Long)(
            AddressOf Initialize,
            TableName,
            LocationIdColumn)
    End Function
End Class
