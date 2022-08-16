Public Class LocationTypeData
    Inherits BaseData
    Friend Const TableName = "LocationTypes"
    Friend Const LocationTypeIdColumn = "LocationTypeId"
    Friend Const LocationTypeNameColumn = "LocationTypeName"
    Friend Sub Initialize()

    End Sub

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

End Class
