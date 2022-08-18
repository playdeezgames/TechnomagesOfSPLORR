﻿Public Class Team
    Private WorldData As WorldData
    Sub New(worldData As WorldData)
        Me.WorldData = worldData
    End Sub
    ReadOnly Property Characters As IEnumerable(Of Character)
        Get
            Return WorldData.Team.ReadCharacterIds().Select(Function(x) Character.FromId(WorldData, x))
        End Get
    End Property
    ReadOnly Property Leader As Character
        Get
            Return Characters.First
        End Get
    End Property
    ReadOnly Property Location As Location
        Get
            Return Leader.Location
        End Get
    End Property
    ReadOnly Property CharacterNames As String
        Get
            Return String.Join(", ", Characters.Select(Function(x) x.FullName))
        End Get
    End Property

    Public Sub Move(route As Route)
        For Each character In Characters
            character.Location = route.ToLocation
        Next
    End Sub

    Public Sub Disband()
        For Each character In Characters
            character.LeaveTeam()
        Next
    End Sub

    Public Function HasItems() As Boolean
        Return WorldData.Team.ReadItemCount() > 0
    End Function
End Class
