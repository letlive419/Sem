'Name: Elvis Cruz
'Date: 02/19/2021
'I affirm that this program was created by Me. It Is solely my work And does Not include any work done by anyone Else.
Public Class CCategory
        Private _strCatName As String
        Private _dblTotalValue As Double
    Private _intTotalCount As Integer
    Private _inttotalNbrRegistered As Integer
    Private _intTotalRoomCount As Integer
    Private _strRoomChange As String

    Public Sub New(strName As String, dblValue As Double, nbrRegistered As Integer, roomChng As String)
        _strCatName = strName
        _dblTotalValue = dblValue
        _strRoomChange = roomChng
        _inttotalNbrRegistered = nbrRegistered
        _intTotalCount = 1
        _intTotalRoomCount = 0

    End Sub

    Public ReadOnly Property CatName() As String
            Get
                Return _strCatName
            End Get

        End Property
        Public Property TotalValue() As Double
            Get
                Return _dblTotalValue

            End Get
            Set(dblVal As Double)
                _dblTotalValue = dblVal

            End Set
        End Property

    Public Property TotalCount() As Integer
        Get
            Return _intTotalCount
        End Get

        Set(intVal As Integer)
            _intTotalCount = intVal
        End Set
    End Property


    Public Property TotalNbrCount() As Integer
        Get
            Return _inttotalNbrRegistered
        End Get

        Set(nbrRegistered As Integer)
            _inttotalNbrRegistered = nbrRegistered
        End Set
    End Property

    Public Property strRoom() As String
        Get
            Return _strRoomChange
        End Get
        Set(value As String)
            _strRoomChange = value
        End Set


    End Property

    Public Property TotalRoomCount() As Integer
        Get
            Return _intTotalRoomCount
        End Get

        Set(intTotal As Integer)
            _intTotalRoomCount = intTotal
        End Set
    End Property




End Class


