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

    Public ReadOnly Property RouteTypes As IEnumerable(Of RouteType)
        Get
            Return WorldData.RouteType.All.Select(Function(x) RouteType.FromId(WorldData, x))
        End Get
    End Property

    Public ReadOnly Property EquipSlots() As IEnumerable(Of EquipSlot)
        Get
            Return WorldData.EquipSlot.All.Select(Function(x) EquipSlot.FromId(WorldData, x))
        End Get
    End Property

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

    Public ReadOnly Property LocationTypes As IEnumerable(Of LocationType)
        Get
            Return WorldData.LocationType.All.Select(Function(x) LocationType.FromId(WorldData, x))
        End Get
    End Property

    Public ReadOnly Property VergeTypes As IEnumerable(Of VergeType)
        Get
            Return WorldData.VergeType.All.Select(Function(x) VergeType.FromId(WorldData, x))
        End Get
    End Property

    Public Function CreateRouteType(newName As String) As RouteType
        Return RouteType.FromId(WorldData, WorldData.RouteType.Create(newName))
    End Function

    Public Function CreateLocationType(newName As String) As LocationType
        Return LocationType.FromId(WorldData, WorldData.LocationType.Create(newName))
    End Function

    Public Function CreateVergeType(newName As String) As VergeType
        Return VergeType.FromId(WorldData, WorldData.VergeType.Create(newName))
    End Function

    Public Function CreateEquipSlot(newName As String, newDisplayName As String) As EquipSlot
        Return EquipSlot.FromId(WorldData, WorldData.EquipSlot.Create(newName, newDisplayName))
    End Function
End Class
