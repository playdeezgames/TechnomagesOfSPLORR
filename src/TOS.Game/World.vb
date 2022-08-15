Public Class World
    Inherits BaseThingie
    Sub New()
        MyBase.New(New WorldData(New SPLORR.Data.Store))
    End Sub
    Sub New(worldData As WorldData)
        MyBase.New(worldData)
    End Sub

    Public Sub AddLocation()
        worldData.Location.Create()
    End Sub

    Sub New(filename As String)
        Me.New
        WorldData.Load(filename)
    End Sub
    Sub Save(filename As String)
        worldData.Save(filename)
    End Sub
    ReadOnly Property Locations As IEnumerable(Of Location)
        Get
            Return worldData.Location.All.Select(Function(x) Location.FromId(worldData, x))
        End Get
    End Property
End Class
