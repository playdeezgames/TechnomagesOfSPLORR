Public Class Location
    Inherits BaseThingie
    Sub New(worldData As WorldData, locationId As Long)
        MyBase.New(worldData, locationId)
    End Sub
    Shared Function FromId(data As WorldData, locationId As Long) As Location
        Return New Location(data, locationId)
    End Function

    Public ReadOnly Property UniqueName As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property

    Public Property Name As String
        Get
            Return WorldData.Location.ReadName(Id)
        End Get
        Set(value As String)
            WorldData.Location.WriteName(Id, value)
        End Set
    End Property
    Public ReadOnly Property Exits As IEnumerable(Of Route)
        Get
            Return WorldData.Route.ReadForFromLocation(Id).Select(Function(x) Route.FromId(WorldData, x))
        End Get
    End Property
    Public ReadOnly Property Entrances As IEnumerable(Of Route)
        Get
            Return WorldData.Route.ReadForToLocation(Id).Select(Function(x) Route.FromId(WorldData, x))
        End Get
    End Property

    Public ReadOnly Property CanDelete As Boolean
        Get
            Return Not HasExits AndAlso
                Not HasEntrances AndAlso
                Not HasItems()
        End Get
    End Property

    Public ReadOnly Property HasCharacters As Boolean
        Get
            Return WorldData.Character.CountForLocation(Id) > 0
        End Get
    End Property

    Public Sub Destroy()
        If HasInventory() Then
            Inventory.Destroy()
        End If
        WorldData.Location.Clear(Id)
    End Sub

    Public ReadOnly Property RouteNames As String
        Get
            Return String.Join(", ", Exits.Select(Function(x) x.Name))
        End Get
    End Property
    Public ReadOnly Property ItemStackNames As String
        Get
            Return String.Join(", ", ItemStacks.Select(Function(x) $"{x.Item1.Name}(x{x.Item2.Count})"))
        End Get
    End Property
    Public ReadOnly Property ItemStacks As IEnumerable(Of (ItemType, IEnumerable(Of Item)))
        Get
            Return Inventory.ItemStacks
        End Get
    End Property

    Public ReadOnly Property HasStatisticDeltas As Boolean
        Get
            Return WorldData.LocationStatistic.CountForLocation(Id) > 0
        End Get
    End Property

    Public ReadOnly Property StatisticDeltas() As IEnumerable(Of (StatisticType, Long))
        Get
            Return WorldData.LocationStatistic.
                ReadForLocation(Id).Select(Function(x) (StatisticType.FromId(WorldData, x.Item1), x.Item2))
        End Get
    End Property

    Public Property StatisticDelta(statisticType As StatisticType) As Long?
        Get
            Return WorldData.LocationStatistic.Read(Id, statisticType.Id)
        End Get
        Set(value As Long?)
            If value.HasValue Then
                WorldData.LocationStatistic.Write(Id, statisticType.Id, value.Value)
            Else
                WorldData.LocationStatistic.Clear(Id, statisticType.Id)
            End If
        End Set
    End Property

    Public ReadOnly Property ItemNames As String
        Get
            Return String.Join(", ", Items.Select(Function(x) x.Name))
        End Get
    End Property

    Public Function HasOtherCharacters() As Boolean
        Return WorldData.LocationCharacter.ReadCharacterCount(Id, False) > 0
    End Function

    Public ReadOnly Property OtherCharacters As IEnumerable(Of Character)
        Get
            Return WorldData.LocationCharacter.ReadCharacters(Id, False).Select(Function(x) Character.FromId(WorldData, x))
        End Get
    End Property

    Public ReadOnly Property Characters As IEnumerable(Of Character)
        Get
            Return WorldData.LocationCharacter.ReadCharacters(Id).Select(Function(x) Character.FromId(WorldData, x))
        End Get
    End Property

    Public Function OtherCharacterNames() As String
        Return String.Join(", ", OtherCharacters.Select(Function(x) x.FullName))
    End Function

    Public ReadOnly Property Items As IEnumerable(Of Item)
        Get
            Return If(HasItems(), Inventory.Items, Array.Empty(Of Item))
        End Get
    End Property
    Public Property LocationType As LocationType
        Get
            Return LocationType.FromId(WorldData, WorldData.Location.ReadLocationType(Id).Value)
        End Get
        Set(value As LocationType)
            WorldData.Location.WriteLocationType(Id, value.Id)
        End Set
    End Property

    Public ReadOnly Property HasExits As Boolean
        Get
            Return WorldData.Route.CountForFromLocation(Id) > 0
        End Get
    End Property

    Public ReadOnly Property HasEntrances As Boolean
        Get
            Return WorldData.Route.CountForToLocation(Id) > 0
        End Get
    End Property

    Public Function HasInventory() As Boolean
        Return WorldData.Inventory.ReadCountForLocation(Id) > 0
    End Function

    Public ReadOnly Property Inventory As Inventory
        Get
            Dim inventoryId = WorldData.Inventory.ReadForLocation(Id)
            If inventoryId.HasValue Then
                Return Inventory.FromId(WorldData, inventoryId.Value)
            End If
            inventoryId = WorldData.Inventory.CreateForLocation(Id)
            Return Inventory.FromId(WorldData, inventoryId.Value)
        End Get
    End Property

    Public ReadOnly Property HasItems As Boolean
        Get
            Return HasInventory() AndAlso Inventory.HasItems
        End Get
    End Property
    Public ReadOnly Property Statistic(statisticType As StatisticType) As Long?
        Get
            Return WorldData.LocationBaseStatistic.Read(Id, statisticType.Id)
        End Get
    End Property
End Class
