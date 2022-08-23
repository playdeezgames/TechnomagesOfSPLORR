Public Class RouteTypeData
    Inherits BaseData
    Implements IProvidesAll
    Friend Const TableName = "RouteTypes"
    Friend Const RouteTypeIdColumn = "RouteTypeId"
    Friend Const RouteTypeNameColumn = "RouteTypeName"

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function All() As IEnumerable(Of Long) Implements IProvidesAll.All
        Return Store.ReadRecords(Of Long)(TableName, RouteTypeIdColumn)
    End Function

    Public Function ReadName(routeTypeId As Long) As String
        Return Store.ReadColumnString(TableName, RouteTypeNameColumn, (RouteTypeIdColumn, routeTypeId))
    End Function

    Public Sub WriteName(routeTypeId As Long, name As String)
        Store.WriteColumnValue(TableName, (RouteTypeNameColumn, name), (RouteTypeIdColumn, routeTypeId))
    End Sub

    Public Sub Clear(routeTypeId As Long)
        Store.ClearForColumnValue(TableName, (RouteTypeIdColumn, routeTypeId))
    End Sub

    Public Function Create(newName As String) As Long
        Return Store.CreateRecord(TableName, (RouteTypeNameColumn, newName))
    End Function
End Class
