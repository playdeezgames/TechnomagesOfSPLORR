Public Class LocationData
    Inherits BaseData
    Implements IProvidesAll
    Friend Const TableName = "Locations"
    Friend Const LocationIdColumn = "LocationId"
    Friend Const LocationNameColumn = "LocationName"
    Friend Const LocationTypeIdColumn = LocationTypeData.LocationTypeIdColumn

    Public Function ReadName(locationId As Long) As String
        Return Store.ReadColumnString(
            TableName,
            LocationNameColumn,
            (LocationIdColumn, locationId))
    End Function

    Public Function ReadLocationType(locationId As Long) As Long?
        Return Store.ReadColumnValue(Of Long, Long)(
            TableName,
            LocationTypeIdColumn,
            (LocationIdColumn, locationId))
    End Function

    Public Sub WriteName(locationId As Long, locationName As String)
        Store.WriteColumnValue(
            TableName,
            (LocationNameColumn, locationName),
            (LocationIdColumn, locationId))
    End Sub

    Public Function CountForLocationType(locationTypeId As Long) As Long
        Return Store.ReadCountForColumnValue(TableName, (LocationTypeIdColumn, locationTypeId))
    End Function

    Public Sub WriteLocationType(locationId As Long, locationTypeId As Long)
        Store.WriteColumnValue(
            TableName,
            (LocationTypeIdColumn, locationTypeId),
            (LocationIdColumn, locationId))
    End Sub

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function All() As IEnumerable(Of Long) Implements IProvidesAll.All
        Return Store.ReadRecords(Of Long)(
            TableName,
            LocationIdColumn)
    End Function
End Class
