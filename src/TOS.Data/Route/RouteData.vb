﻿Public Class RouteData
    Inherits BaseData
    Friend Const TableName = "Routes"
    Friend Const RouteIdColumn = "RouteId"
    Friend Const RouteNameColumn = "RouteName"
    Friend Const FromLocationIdColumn = "FromLocationId"
    Friend Const ToLocationIdColumn = "ToLocationId"
    Friend Const RouteTypeIdColumn = RouteTypeData.RouteTypeIdColumn
    Friend Const VergeIdColumn = VergeData.VergeIdColumn

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function ReadName(routeId As Long) As String
        Return Store.ReadColumnString(
            TableName,
            RouteNameColumn,
            (RouteIdColumn, routeId))
    End Function

    Public Function ReadForFromLocation(fromLocationId As Long) As IEnumerable(Of Long)
        Return Store.ReadRecordsWithColumnValue(Of Long, Long)(
            TableName,
            RouteIdColumn,
            (FromLocationIdColumn, fromLocationId))
    End Function

    Public Function ReadForToLocation(fromLocationId As Long) As IEnumerable(Of Long)
        Return Store.ReadRecordsWithColumnValue(Of Long, Long)(
            TableName,
            RouteIdColumn,
            (ToLocationIdColumn, fromLocationId))
    End Function

    Public Function CountForVerge(vergeId As Long) As Long
        Return Store.ReadCountForColumnValue(TableName, (VergeIdColumn, vergeId))
    End Function

    Public Function CountForToLocation(locationId As Long) As Long
        Return Store.ReadCountForColumnValue(TableName, (ToLocationIdColumn, locationId))
    End Function

    Public Function CountForFromLocation(locationId As Long) As Long
        Return Store.ReadCountForColumnValue(TableName, (FromLocationIdColumn, locationId))
    End Function

    Public Function CountForRouteType(routeTypeId As Long) As Long
        Return Store.ReadCountForColumnValue(TableName, (RouteTypeIdColumn, routeTypeId))
    End Function

    Public Function ReadToLocationId(routeId As Long) As Long?
        Return Store.ReadColumnValue(Of Long, Long)(TableName, ToLocationIdColumn, (RouteIdColumn, routeId))
    End Function

    Public Function ReadFromLocationId(routeId As Long) As Long?
        Return Store.ReadColumnValue(Of Long, Long)(TableName, FromLocationIdColumn, (RouteIdColumn, routeId))
    End Function

    Public Function ReadVergeId(routeId As Long) As Long?
        Return Store.ReadColumnValue(Of Long, Long)(TableName, VergeIdColumn, (RouteIdColumn, routeId))
    End Function
End Class
