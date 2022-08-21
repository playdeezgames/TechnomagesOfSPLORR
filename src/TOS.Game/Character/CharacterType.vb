Public Class CharacterType
    Inherits BaseThingie

    Public Sub New(worldData As WorldData, id As Long)
        MyBase.New(worldData, id)
    End Sub
    Public Shared Function FromId(worldData As WorldData, characterTypeId As Long) As CharacterType
        Return New CharacterType(worldData, characterTypeId)
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
                Select(Function(x) (StatisticType.FromId(WorldData, x.Item1), x.Item2))
        End Get
    End Property

    Public ReadOnly Property CanDelete As Boolean
        Get
            Return WorldData.Character.CountForCharacterType(Id) = 0 AndAlso WorldData.CharacterTypeStatistic.CountForCharacterType(Id) = 0
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
End Class
