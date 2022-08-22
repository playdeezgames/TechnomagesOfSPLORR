﻿Public Class StatisticType
    Inherits BaseThingie

    Public Sub New(worldData As WorldData, id As Long)
        MyBase.New(worldData, id)
    End Sub
    Friend Shared Function FromId(worldData As WorldData, id As Long) As StatisticType
        Return New StatisticType(worldData, id)
    End Function
    Public ReadOnly Property Name As String
        Get
            Return WorldData.StatisticType.ReadName(Id)
        End Get
    End Property
    Public ReadOnly Property DisplayName As String
        Get
            Return WorldData.StatisticType.ReadDisplayName(Id)
        End Get
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
End Class
