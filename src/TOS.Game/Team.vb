Public Class Team
    Private WorldData As WorldData
    Sub New(worldData As WorldData)
        Me.WorldData = worldData
    End Sub
    ReadOnly Property Characters As IEnumerable(Of Character)
        Get
            Return WorldData.Team.ReadCharacterIds().Select(Function(x) Character.FromId(WorldData, x))
        End Get
    End Property

    Public Sub Disband()
        For Each character In Characters
            character.LeaveTeam()
        Next
    End Sub
End Class
