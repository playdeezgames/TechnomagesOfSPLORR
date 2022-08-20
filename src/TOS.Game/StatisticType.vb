Public Class StatisticType
    Inherits BaseThingie

    Public Sub New(worldData As WorldData, id As Long)
        MyBase.New(worldData, id)
    End Sub
    Friend Shared Function FromId(worldData As WorldData, id As Long) As StatisticType
        Return New StatisticType(worldData, id)
    End Function
    Public ReadOnly Property DisplayName As String
        Get
            Return WorldData.StatisticType.ReadDisplayName(Id)
        End Get
    End Property
End Class
