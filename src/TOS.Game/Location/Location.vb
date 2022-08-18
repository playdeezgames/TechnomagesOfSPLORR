Public Class Location
    Inherits BaseThingie
    Sub New(worldData As WorldData, locationId As Long)
        MyBase.New(worldData, locationId)
    End Sub
    Shared Function FromId(data As WorldData, locationId As Long) As Location
        Return New Location(data, locationId)
    End Function
    Public Property Name As String
        Get
            Return WorldData.Location.ReadName(Id)
        End Get
        Set(value As String)
            WorldData.Location.WriteName(Id, value)
        End Set
    End Property
    Public ReadOnly Property Routes As IEnumerable(Of Route)
        Get
            Return WorldData.Route.ReadForLocationId(Id).Select(Function(x) Route.FromId(WorldData, x))
        End Get
    End Property
    Public ReadOnly Property RouteNames As String
        Get
            Return String.Join(", ", Routes.Select(Function(x) x.Name))
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
    Public ReadOnly Property ItemNames As String
        Get
            Return String.Join(", ", Items.Select(Function(x) x.Name))
        End Get
    End Property

    Public Function HasOtherCharacters() As Boolean
        Return WorldData.LocationCharacter.ReadCharacterCount(Id, False) > 0
    End Function

    Public Function OtherCharacters() As IEnumerable(Of Character)
        Return WorldData.LocationCharacter.ReadCharacters(Id, False).Select(Function(x) Character.FromId(WorldData, x))
    End Function

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

    Public Function HasRoutes() As Boolean
        Return WorldData.Route.ReadCountForLocation(Id) > 0
    End Function

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

    Public Function HasItems() As Boolean
        Return HasInventory AndAlso Inventory.HasItems
    End Function
End Class
