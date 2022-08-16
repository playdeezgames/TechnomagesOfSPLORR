﻿Public Class Character
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
    ReadOnly Property CharacterType As CharacterType
        Get
            Return CharacterType.FromId(WorldData, WorldData.Character.ReadCharacterType(Id).Value)
        End Get
    End Property
End Class
