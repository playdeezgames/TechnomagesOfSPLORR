Public Class WorldData
    Inherits BaseData
    Public ReadOnly Character As CharacterData
    Public ReadOnly CharacterType As CharacterTypeData
    Public ReadOnly Location As LocationData
    Public ReadOnly LocationType As LocationTypeData
    Public ReadOnly Team As TeamData

    Public Sub New(store As Store)
        MyBase.New(store)
        Character = New CharacterData(store)
        CharacterType = New CharacterTypeData(store)
        Location = New LocationData(store)
        LocationType = New LocationTypeData(store)
        Team = New TeamData(store)
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
