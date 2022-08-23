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

    Private Function FetchAll(Of TThingie)(
                                          allProvider As IProvidesAll,
                                          conversion As Func(Of WorldData, Long, TThingie)) As IEnumerable(Of TThingie)
        Return allProvider.All.Select(Function(x) conversion(WorldData, x))
    End Function

    Public ReadOnly Property Verges As IEnumerable(Of Verge)
        Get
            Return FetchAll(WorldData.Verge, AddressOf Verge.FromId)
        End Get
    End Property

    Public ReadOnly Property RouteTypes As IEnumerable(Of RouteType)
        Get
            Return FetchAll(WorldData.RouteType, AddressOf RouteType.FromId)
        End Get
    End Property

    Public ReadOnly Property EquipSlots() As IEnumerable(Of EquipSlot)
        Get
            Return FetchAll(WorldData.EquipSlot, AddressOf EquipSlot.FromId)
        End Get
    End Property

    Public ReadOnly Property CharacterTypes As IEnumerable(Of CharacterType)
        Get
            Return FetchAll(WorldData.CharacterType, AddressOf CharacterType.FromId)
        End Get
    End Property

    Public Function CreateVerge(name As String, vergeType As VergeType) As Verge
        Return Verge.FromId(WorldData, WorldData.Verge.Create(name, vergeType.Id))
    End Function

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
            Return FetchAll(WorldData.StatisticType, AddressOf StatisticType.FromId)
        End Get
    End Property

    Public Function CreateStatisticType(newName As String, newDisplayName As String) As StatisticType
        Return StatisticType.FromId(WorldData, WorldData.StatisticType.Create(newName, newDisplayName))
    End Function

    Public ReadOnly Property LocationTypes As IEnumerable(Of LocationType)
        Get
            Return FetchAll(WorldData.LocationType, AddressOf LocationType.FromId)
        End Get
    End Property

    Public ReadOnly Property VergeTypes As IEnumerable(Of VergeType)
        Get
            Return FetchAll(WorldData.VergeType, AddressOf VergeType.FromId)
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
