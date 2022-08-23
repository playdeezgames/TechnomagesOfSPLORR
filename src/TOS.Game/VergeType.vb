Public Class VergeType
    Inherits BaseThingie

    Public Sub New(worldData As WorldData, id As Long)
        MyBase.New(worldData, id)
    End Sub
    Public Shared Function FromId(worldData As WorldData, id As Long) As VergeType
        Return New VergeType(worldData, id)
    End Function

    Public ReadOnly Property Name As String
        Get
            Return WorldData.VergeType.ReadName(Id)
        End Get
    End Property

    Public ReadOnly Property UniqueName() As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property
End Class
