Public Class CharacterData
    Inherits BaseData
    Friend Const TableName = "Characters"
    Friend Const CharacterIdColumn = "CharacterId"

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub
End Class
