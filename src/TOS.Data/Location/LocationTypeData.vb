Public Class LocationTypeData
    Inherits BaseData
    Friend Const TableName = "LocationTypes"
    Friend Const LocationTypeIdColumn = "LocationTypeId"
    Friend Const LocationTypeNameColumn = "LocationTypeName"

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function All() As IEnumerable(Of Long)
        Return Store.ReadRecords(Of Long)(TableName, LocationTypeIdColumn)
    End Function

    Public Function ReadName(locationTypeId As Long) As String
        Return Store.ReadColumnString(TableName, LocationTypeNameColumn, (LocationTypeIdColumn, locationTypeId))
    End Function

    Public Function Create(newName As String) As Long
        Return Store.CreateRecord(TableName, (LocationTypeNameColumn, newName))
    End Function

    Public Sub Clear(locationTypeId As Long)
        Store.ClearForColumnValue(TableName, (LocationTypeIdColumn, locationTypeId))
    End Sub
End Class
