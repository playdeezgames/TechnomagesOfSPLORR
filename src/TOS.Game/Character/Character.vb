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
    ReadOnly Property Name As String
        Get
            Return WorldData.Character.ReadName(Id)
        End Get
    End Property
    ReadOnly Property FullName As String
        Get
            Return $"{Name} the {CharacterType.Name}"
        End Get
    End Property
    ReadOnly Property CharacterType As CharacterType
        Get
            Return CharacterType.FromId(WorldData, WorldData.Character.ReadCharacterType(Id).Value)
        End Get
    End Property
    Property Location As Location
        Get
            Return Location.FromId(WorldData, WorldData.Character.ReadLocation(Id).Value)
        End Get
        Set(value As Location)
            WorldData.Character.WriteLocation(Id, value.Id)
        End Set
    End Property
    ReadOnly Property Team As Team
        Get
            Return If(WorldData.Team.Exists(Id), New Team(WorldData), Nothing)
        End Get
    End Property

    Public Function CanGive() As Boolean
        Return If(Team?.CharacterCount, 0) > 0
    End Function

    Public Function HasInventory() As Boolean
        Return WorldData.Inventory.ReadCountForCharacter(Id) > 0
    End Function

    Public ReadOnly Property Inventory As Inventory
        Get
            Dim inventoryId = WorldData.Inventory.ReadForCharacter(Id)
            If inventoryId.HasValue Then
                Return Inventory.FromId(WorldData, inventoryId.Value)
            End If
            inventoryId = WorldData.Inventory.CreateForCharacter(Id)
            Return Inventory.FromId(WorldData, inventoryId.Value)
        End Get
    End Property
End Class
