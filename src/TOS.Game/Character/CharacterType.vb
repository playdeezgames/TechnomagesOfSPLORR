Public Class CharacterType
    Inherits BaseThingie

    Public Sub New(worldData As WorldData, id As Long)
        MyBase.New(worldData, id)
    End Sub
    Public Shared Function FromId(worldData As WorldData, characterTypeId As Long) As CharacterType
        Return New CharacterType(worldData, characterTypeId)
    End Function
    Property Name As String
        Get
            Return WorldData.CharacterType.ReadName(Id)
        End Get
        Set(value As String)
            WorldData.CharacterType.WriteName(Id, value)
        End Set
    End Property

    Public ReadOnly Property UniqueName As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property
End Class
