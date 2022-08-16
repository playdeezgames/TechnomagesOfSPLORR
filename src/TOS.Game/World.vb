Public Class World
    Private ReadOnly WorldData As WorldData
    Sub New(worldData As WorldData)
        Me.WorldData = worldData
    End Sub

    Public Function CanContinue() As Boolean
        Return Team.Characters.Any
    End Function

    Sub New(filename As String)
        WorldData = New WorldData(New SPLORR.Data.Store)
        WorldData.Load(filename)
    End Sub
    Sub Save(filename As String)
        worldData.Save(filename)
    End Sub
    ReadOnly Property Team As Team
        Get
            Return New Team(WorldData)
        End Get
    End Property
End Class
