Public Class ConditionType
    Inherits BaseThingie

    Public Sub New(world As World, id As Long)
        MyBase.New(world, id)
    End Sub
    Public Shared Function FromId(world As World, id As Long) As ConditionType
        Return New ConditionType(world, id)
    End Function
    Public Property Name As String
        Get
            Return WorldData.ConditionType.ReadName(Id)
        End Get
        Set(value As String)
            WorldData.ConditionType.WriteName(Id, value)
        End Set
    End Property
    Public Property DisplayName As String
        Get
            Return WorldData.ConditionType.ReadDisplayName(Id)
        End Get
        Set(value As String)
            WorldData.ConditionType.WriteDisplayName(Id, value)
        End Set
    End Property
    Public ReadOnly Property UniqueName As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property

    Public ReadOnly Property CanDelete() As Boolean
        Get
            Return WorldData.CharacterTypeCondition.CountForConditionType(Id) = 0
        End Get
    End Property

    Public Sub Destroy()
        WorldData.ConditionType.Clear(Id)
    End Sub

    Public ReadOnly Property HasStatisticRange As Boolean
        Get
            Return WorldData.ConditionTypeStatisticRange.CountForConditionType(Id) > 0
        End Get
    End Property

    Public ReadOnly Property StatisticRanges As IEnumerable(Of (StatisticType, (Long, Long)))
        Get
            Return WorldData.ConditionTypeStatisticRange.ReadForConditionType(Id).
                Select(Function(x) (StatisticType.FromId(World, x.Item1), (x.Item2, x.Item3)))
        End Get
    End Property

    Public Property StatisticRange(statisticType As StatisticType) As (Long, Long)?
        Get
            Return WorldData.ConditionTypeStatisticRange.Read(Id, statisticType.Id)
        End Get
        Set(value As (Long, Long)?)
            If value.HasValue Then
                WorldData.ConditionTypeStatisticRange.Write(Id, statisticType.Id, value.Value.Item1, value.Value.Item2)
            Else
                WorldData.ConditionTypeStatisticRange.Clear(Id, statisticType.Id)
            End If
        End Set
    End Property
End Class
