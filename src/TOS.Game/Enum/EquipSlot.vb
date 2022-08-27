Public Class EquipSlot
    Inherits BaseThingie

    Public Sub New(world As World, id As Long)
        MyBase.New(world, id)
    End Sub
    Public Shared Function FromId(world As World, id As Long) As EquipSlot
        Return New EquipSlot(world, id)
    End Function
    Public Property DisplayName As String
        Get
            Return WorldData.EquipSlot.ReadDisplayName(Id)
        End Get
        Set(value As String)
            WorldData.EquipSlot.WriteDisplayName(Id, value)
        End Set
    End Property
    Public Property Name As String
        Get
            Return WorldData.EquipSlot.ReadName(Id)
        End Get
        Set(value As String)
            WorldData.EquipSlot.WriteName(Id, value)
        End Set
    End Property

    Public ReadOnly Property UniqueName As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property

    Public ReadOnly Property CanDelete As Boolean
        Get
            Return WorldData.CharacterEquipSlot.CountForEquipSlot(Id) = 0 AndAlso
                WorldData.CharacterTypeEquipSlot.CountForEquipSlot(Id) = 0 AndAlso
                WorldData.ItemTypeEquipSlot.CountForEquipSlot(Id) = 0
        End Get
    End Property

    Public Sub Destroy()
        WorldData.EquipSlot.Clear(Id)
    End Sub
End Class
