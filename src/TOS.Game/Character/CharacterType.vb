Public Class CharacterType
    Inherits BaseThingie

    Public Sub New(world As World, id As Long)
        MyBase.New(world, id)
    End Sub
    Public Shared Function FromId(world As World, characterTypeId As Long) As CharacterType
        Return New CharacterType(world, characterTypeId)
    End Function
    Property Name As String
        Get
            Return WorldData.CharacterType.ReadName(Id)
        End Get
        Set(value As String)
            WorldData.CharacterType.WriteName(Id, value)
        End Set
    End Property

    Public ReadOnly Property Statistics As IEnumerable(Of (StatisticType, Long))
        Get
            Return WorldData.CharacterTypeStatistic.ReadForCharacterType(Id).
                Select(Function(x) (StatisticType.FromId(World, x.Item1), x.Item2))
        End Get
    End Property

    Public ReadOnly Property CanDelete As Boolean
        Get
            Return WorldData.Character.CountForCharacterType(Id) = 0 AndAlso
                WorldData.CharacterTypeStatistic.CountForCharacterType(Id) = 0 AndAlso
                WorldData.CharacterTypeCondition.CountForCharacterType(Id) = 0
        End Get
    End Property

    Public ReadOnly Property HasStatistics As Boolean
        Get
            Return WorldData.CharacterTypeStatistic.CountForCharacterType(Id) > 0
        End Get
    End Property

    Public ReadOnly Property UniqueName As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property

    Public Sub Destroy()
        WorldData.CharacterType.Clear(Id)
    End Sub

    Public ReadOnly Property EquipSlots As IEnumerable(Of EquipSlot)
        Get
            Return WorldData.CharacterTypeEquipSlot.ReadForCharacterType(Id).Select(Function(x) EquipSlot.FromId(World, x))
        End Get
    End Property

    Public Property Statistic(statisticType As StatisticType) As Long?
        Get
            Return WorldData.CharacterTypeStatistic.Read(Id, statisticType.Id)
        End Get
        Set(value As Long?)
            If value.HasValue Then
                WorldData.CharacterTypeStatistic.Write(Id, statisticType.Id, value.Value)
            Else
                WorldData.CharacterTypeStatistic.Clear(Id, statisticType.Id)
            End If
        End Set
    End Property

    Public ReadOnly Property HasEquipSlots As Boolean
        Get
            Return WorldData.CharacterTypeEquipSlot.CountForCharacterType(Id) > 0
        End Get
    End Property

    Public Sub RemoveEquipSlot(equipSlot As EquipSlot)
        WorldData.CharacterTypeEquipSlot.Clear(Id, equipSlot.Id)
    End Sub

    Public Sub AddEquipSlot(equipSlot As EquipSlot)
        WorldData.CharacterTypeEquipSlot.Write(Id, equipSlot.Id)
    End Sub
End Class
