Public MustInherit Class BaseThingie
    Protected ReadOnly WorldData As WorldData
    Sub New(worldData As WorldData)
        Me.WorldData = worldData
    End Sub
End Class
