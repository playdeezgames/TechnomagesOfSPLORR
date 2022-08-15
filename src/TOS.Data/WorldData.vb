Public Class WorldData
    Inherits BaseData
    Public ReadOnly Location As LocationData

    Public Sub New(store As Store)
        MyBase.New(store)
        Location = New LocationData(store)
    End Sub

    Sub Load(filename As String)
        Store.Load(filename)
    End Sub
    Sub Save(filename As String)
        Store.Save(filename)
    End Sub

    Public Sub Reset()
        Store.Reset()
    End Sub
End Class
