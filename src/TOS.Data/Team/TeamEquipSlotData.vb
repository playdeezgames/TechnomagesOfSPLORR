Public Class TeamEquipSlotData
    Inherits BaseData
    Friend Const ViewName = "TeamEquipSlots"

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function ReadCount() As Long
        Return Store.ReadCount(ViewName)
    End Function
End Class
