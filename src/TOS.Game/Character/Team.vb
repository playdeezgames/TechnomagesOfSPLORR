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
    ReadOnly Property CharacterCount As Long
        Get
            Return WorldData.Team.ReadCount()
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

    Public ReadOnly Property HasItems() As Boolean
        Get
            Return WorldData.TeamItem.ReadCount() > 0
        End Get
    End Property

    Public ReadOnly Property HasEquipment() As Boolean
        Get
            Return WorldData.TeamEquipSlot.ReadCount() > 0
        End Get
    End Property
End Class
