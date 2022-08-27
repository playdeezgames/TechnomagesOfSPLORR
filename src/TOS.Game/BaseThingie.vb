Public MustInherit Class BaseThingie
    Public ReadOnly World As World
    Protected ReadOnly Property WorldData As WorldData
        Get
            Return World.WorldData
        End Get
    End Property
    Public ReadOnly Id As Long
    Sub New(world As World, id As Long)
        Me.World = world
        Me.Id = id
    End Sub
End Class
