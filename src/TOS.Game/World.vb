Public Class World
    Private worldData As New WorldData(New SPLORR.Data.Store)
    Sub New(filename As String)
        worldData.Load(filename)
    End Sub
    Sub Save(filename As String)
        worldData.Save(filename)
    End Sub
    ReadOnly Property Locations As IEnumerable(Of Location)
        Get
            Return worldData.Location.All.Select(Function(x) Location.FromId(x))
        End Get
    End Property
End Class
