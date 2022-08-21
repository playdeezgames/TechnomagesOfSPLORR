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

    Public Sub WriteName(characterTypeId As Long, name As String)
        Store.WriteColumnValue(
            TableName,
            (CharacterTypeNameColumn, name),
            (CharacterTypeIdColumn, characterTypeId))
    End Sub

    Public Function All() As IEnumerable(Of Long)
        Return Store.ReadRecords(Of Long)(
            TableName,
            CharacterTypeIdColumn)
    End Function

    Public Sub Clear(characterTypeId As Long)
        Store.ClearForColumnValue(TableName, (CharacterTypeIdColumn, characterTypeId))
    End Sub

    Public Function Create(name As String) As Long
        Return Store.CreateRecord(TableName, (CharacterTypeNameColumn, name))
    End Function
End Class
