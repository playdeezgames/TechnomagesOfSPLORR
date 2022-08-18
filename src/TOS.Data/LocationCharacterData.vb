Public Class LocationCharacterData
    Inherits BaseData
    Friend Const ViewName = "LocationCharacters"
    Friend Const LocationIdColumn = LocationData.LocationIdColumn
    Friend Const CharacterIdColumn = CharacterData.CharacterIdColumn
    Friend Const IsOnTeamColumn = "IsOnTeam"

    Public Sub New(store As Store)
        MyBase.New(store)
    End Sub

    Public Function ReadCharacterCount(locationId As Long, isOnTeam As Boolean) As Long
        Return Store.ReadCountForColumnValues(ViewName, (LocationIdColumn, locationId), (IsOnTeamColumn, If(isOnTeam, 1L, 0L)))
    End Function

    Public Function ReadCharacters(locationId As Long, isOnTeam As Boolean) As IEnumerable(Of Long)
        Return Store.ReadRecordsWithColumnValues(Of Long, Long, Long)(ViewName, CharacterIdColumn, (LocationIdColumn, locationId), (IsOnTeamColumn, If(isOnTeam, 1L, 0L)))
    End Function
End Class
