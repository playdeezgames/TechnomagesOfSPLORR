Public Class Character
    Inherits BaseThingie

    Public Sub New(worldData As WorldData, characterId As Long)
        MyBase.New(worldData, characterId)
    End Sub
    Shared Function FromId(worldData As WorldData, characterId As Long) As Character
        Return New Character(worldData, characterId)
    End Function

    Friend Sub LeaveTeam()
        WorldData.Team.ClearForCharacterId(Id)
    End Sub
    ReadOnly Property Name As String
        Get
            Return WorldData.Character.ReadName(Id)
        End Get
    End Property
    ReadOnly Property FullName As String
        Get
            Return $"{Name} the {CharacterType.Name}"
        End Get
    End Property
    ReadOnly Property CharacterType As CharacterType
        Get
            Return CharacterType.FromId(WorldData, WorldData.Character.ReadCharacterType(Id).Value)
        End Get
    End Property
    Property Location As Location
        Get
            Return Location.FromId(WorldData, WorldData.Character.ReadLocation(Id).Value)
        End Get
        Set(value As Location)
            WorldData.Character.WriteLocation(Id, value.Id)
        End Set
    End Property
End Class
