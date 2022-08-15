Public Class LocationData
    Inherits BaseData
    Friend Const TableName = "Locations"
    Friend Const LocationIdColumn = "LocationId"
    Friend Const LocationNameColumn = "LocationName"

    Public Function Create(name As String) As Long
        Return Store.CreateRecord(AddressOf Initialize, TableName, (LocationNameColumn, name))
    End Function

    Friend Sub Initialize()
        Store.ExecuteNonQuery(
            $"CREATE TABLE IF NOT EXISTS [{TableName}]
            (
                [{LocationIdColumn}] INTEGER PRIMARY KEY,
                [{LocationNameColumn}] TEXT NOT NULL
            );")
    End Sub

    Public Function ReadName(locationId As Long) As String
        Return Store.ReadColumnString(
            AddressOf Initialize,
            TableName,
            LocationNameColumn,
            (LocationIdColumn, locationId))
    End Function

    Public Sub WriteName(locationId As Long, locationName As String)
        Store.WriteColumnValue(
            AddressOf Initialize,
            TableName,
            (LocationNameColumn, locationName),
            (LocationIdColumn, locationId))
    End Sub

    Public Sub Clear(locationId As Long)
        Store.ClearForColumnValue(AddressOf Initialize, TableName, (LocationIdColumn, locationId))
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
