Public Class ConditionTypeData
    Inherits BaseData
    Implements IProvidesAll
    Friend Const TableName = "ConditionTypes"
    Friend Const ConditionTypeIdColumn = "ConditionTypeId"
    Friend Const ConditionTypeNameColumn = "ConditionTypeName"
    Friend Const ConditionTypeDisplayNameColumn = "ConditionTypeDisplayName"

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function All() As IEnumerable(Of Long) Implements IProvidesAll.All
        Return Store.ReadRecords(Of Long)(TableName, ConditionTypeIdColumn)
    End Function

    Public Sub WriteName(conditionTypeId As Long, name As String)
        Store.WriteColumnValue(
            TableName,
            (ConditionTypeNameColumn, name),
            (ConditionTypeIdColumn, conditionTypeId))
    End Sub

    Public Function ReadDisplayName(conditionTypeId As Long) As String
        Return Store.ReadColumnString(
            TableName,
            ConditionTypeDisplayNameColumn,
            (ConditionTypeIdColumn, conditionTypeId))
    End Function

    Public Sub WriteDisplayName(conditionTypeId As Long, displayName As String)
        Store.WriteColumnValue(
            TableName,
            (ConditionTypeDisplayNameColumn, displayName),
            (ConditionTypeIdColumn, conditionTypeId))
    End Sub

    Public Function ReadName(conditionTypeId As Long) As String
        Return Store.ReadColumnString(
            TableName,
            ConditionTypeNameColumn,
            (ConditionTypeIdColumn, conditionTypeId))
    End Function

    Public Sub Clear(conditionTypeId As Long)
        Store.ClearForColumnValue(
            TableName,
            (ConditionTypeIdColumn, conditionTypeId))
    End Sub

    Public Function Create(newName As String, newDisplayName As String) As Long
        Return Store.CreateRecord(
            TableName,
            (ConditionTypeNameColumn, newName),
            (ConditionTypeDisplayNameColumn, newDisplayName))
    End Function
End Class
