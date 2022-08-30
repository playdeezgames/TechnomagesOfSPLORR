Public Class CharacterTypeConditionData
    Inherits BaseData
    Friend Const TableName = "CharacterTypeConditions"
    Friend Const CharacterTypeIdColumn = CharacterTypeData.CharacterTypeIdColumn
    Friend Const ConditionTypeIdColumn = ConditionTypeData.ConditionTypeIdColumn

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function CountForConditionType(conditionTypeId As Long) As Long
        Return Store.ReadCountForColumnValue(TableName, (ConditionTypeIdColumn, conditionTypeId))
    End Function

    Public Function CountForCharacterType(characterTypeId As Long) As Long
        Return Store.ReadCountForColumnValue(TableName, (CharacterTypeIdColumn, characterTypeId))
    End Function
End Class
