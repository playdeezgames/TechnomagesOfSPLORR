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
End Class
