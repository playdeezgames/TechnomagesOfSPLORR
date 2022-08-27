Public Class StatisticType
    Inherits BaseThingie

    Public Sub New(world as World, id As Long)
        MyBase.New(world, id)
    End Sub
    Friend Shared Function FromId(world As World, id As Long) As StatisticType
        Return New StatisticType(world, id)
    End Function
    Public Property Name As String
        Get
            Return WorldData.StatisticType.ReadName(Id)
        End Get
        Set(value As String)
            WorldData.StatisticType.WriteName(Id, value)
        End Set
    End Property
    Public Property DisplayName As String
        Get
            Return WorldData.StatisticType.ReadDisplayName(Id)
        End Get
        Set(value As String)
            WorldData.StatisticType.WriteDisplayName(Id, value)
        End Set
    End Property

    Public ReadOnly Property UniqueName As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property

    Public Function CanDelete() As Boolean
        Return WorldData.CharacterTypeStatistic.CountForStatisticType(Id) = 0 AndAlso
            WorldData.LocationTypeStatistic.CountForStatisticType(Id) = 0 AndAlso
            WorldData.ItemTypeStatistic.CountForStatisticType(Id) = 0
    End Function

    Public Sub Destroy()
        WorldData.StatisticType.Clear(Id)
    End Sub
End Class
