Imports System.Runtime.CompilerServices

Public Enum IncentiveType
    None
    StartingFood
    StartingPotion
    StartingDagger
End Enum
Public Module IncentiveTypeExtensions
    ReadOnly Property AllIncentives As IEnumerable(Of IncentiveType)
        Get
            Return IncentiveTypeDescriptors.Keys
        End Get
    End Property
    <Extension>
    Function Name(incentiveType As IncentiveType) As String
        Return IncentiveTypeDescriptors(incentiveType).Name
    End Function
    <Extension>
    Function IncentivePrice(incentiveType As IncentiveType, level As Long) As Long
        Return IncentiveTypeDescriptors(incentiveType).IncentivePrice(level)
    End Function
    <Extension>
    Sub ApplyTo(incentiveType As IncentiveType, character As Character, level As Long)
        IncentiveTypeDescriptors(incentiveType).ApplyTo(character, level)
    End Sub
End Module