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

    Public ReadOnly Property CharacterTypes As IEnumerable(Of CharacterType)
        Get
            Return WorldData.CharacterType.All.Select(Function(x) CharacterType.FromId(WorldData, x))
        End Get
    End Property

    Sub Save(filename As String)
        WorldData.Save(filename)
    End Sub
    ReadOnly Property Team As Team
        Get
            Return New Team(WorldData)
        End Get
    End Property

    Public Function CreateCharacterType(newName As String) As CharacterType
        Return CharacterType.FromId(WorldData, WorldData.CharacterType.Create(newName))
    End Function

    Public ReadOnly Property StatisticTypes As IEnumerable(Of StatisticType)
        Get
            Return WorldData.StatisticType.All.Select(Function(x) StatisticType.FromId(WorldData, x))
        End Get
    End Property

    Public Function CreateStatisticType(newName As String, newDisplayName As String) As StatisticType
        Return StatisticType.FromId(WorldData, WorldData.StatisticType.Create(newName, newDisplayName))
    End Function
End Class
