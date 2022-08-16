Public Class World
    Inherits BaseThingie
    Sub New()
        MyBase.New(New WorldData(New SPLORR.Data.Store), 1)
    End Sub
    Sub New(worldData As WorldData)
        MyBase.New(worldData, 1)
    End Sub

    Public Sub AddLocation(name As String)
        WorldData.Location.Create(name)
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

    Public Sub Reset()
        WorldData.Reset()
    End Sub
End Class
