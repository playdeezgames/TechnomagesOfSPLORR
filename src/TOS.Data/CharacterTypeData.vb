Public Class CharacterTypeData
    Inherits BaseData
    Friend Const TableName = "CharacterTypes"
    Friend Const CharacterTypeIdColumn = "CharacterTypeId"
    Friend Const CharacterTypeNameColumn = "CharacterTypeName"

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function ReadName(characterTypeId As Long) As String
        Return Store.ReadColumnString(
            TableName,
            CharacterTypeNameColumn,
            (CharacterTypeIdColumn, characterTypeId))
    End Function
End Class
