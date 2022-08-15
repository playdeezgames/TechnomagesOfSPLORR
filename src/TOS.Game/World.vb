Public Class World
    Private store As New SPLORR.Data.Store
    Sub New(filename As String)
        store.Load(filename)
    End Sub
    Sub Save(filename As String)
        store.Save(filename)
    End Sub
End Class
