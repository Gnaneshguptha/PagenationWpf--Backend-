

Public Class CustomerModel
    Private _firstName As String
    Public Property FirstName As String
        Get
            Return _firstName
        End Get
        Set(value As String)
            _firstName = value
        End Set
    End Property

    Private _lastName As String
    Public Property LastName As String
        Get
            Return _lastName
        End Get
        Set(value As String)
            _lastName = value
        End Set
    End Property

    Private _id As Integer
    Public Property Id As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            _id = value
        End Set
    End Property

    Private _phonenumber As String
    Public Property Phonenumber As String
        Get
            Return _phonenumber
        End Get
        Set(value As String)
            _phonenumber = value
        End Set
    End Property

    Private _age As Integer
    Public Property Age As Integer
        Get
            Return _age
        End Get
        Set(value As Integer)
            _age = value
        End Set
    End Property



    Private _address As String
    Public Property Address As String
        Get
            Return _address
        End Get
        Set(value As String)
            _address = value
        End Set
    End Property




End Class
