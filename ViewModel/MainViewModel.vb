

Imports System.Collections.ObjectModel
Imports System.Net.Http
Imports Newtonsoft.Json

Namespace wpfVbCrud.ViewModel

    Public Class MainViewModel
        Inherits ViewModelBase

        Private _isPreviousButtonEnabled As Boolean = False
        Private _isNextButtonEnabled As Boolean = True

        Private _customers As New ObservableCollection(Of CustomerModel)()
        Public ReadOnly Property NextCommand As ICommand
        Public Property PreviousCommand As ICommand

        Public Property AscCommand As ICommand
        Public Property DescCommand As ICommand
        Public Property AgeclickCommand As ICommand
        Public Property SortCommand As ICommand
        Public Property AgeFilterCommand As ICommand
        Public Property FirstNameFillCommand As ICommand
        Public Property RemoveSortCommand As ICommand
        Public Property ClearSearchCommand As ICommand
        Public Property SearchCommand As ICommand

        Private _currentPage As Integer = 1

        Private _sortingCol As String = ""

        Private _sortType As String = ""

        Private _filterCol As String = Nothing

        Private _filterValue As String = Nothing

        Private _totalpage As Integer
        Public Property Customers As ObservableCollection(Of CustomerModel)
            Get
                Return _customers
            End Get
            Set(value As ObservableCollection(Of CustomerModel))
                _customers = value
                OnPropertyChanged(NameOf(Customers))
            End Set
        End Property

        Public Property CurrentPage As Integer
            Get
                Return _currentPage
            End Get
            Set(value As Integer)
                _currentPage = value
                OnPropertyChanged(NameOf(CurrentPage))
                IsPreviousButtonEnabled = CurrentPage > 1
                IsNextButtonEnabled = CurrentPage < Totalpage

            End Set
        End Property


        Public Property IsPreviousButtonEnabled As Boolean
            Get
                Return _isPreviousButtonEnabled
            End Get
            Set(value As Boolean)
                _isPreviousButtonEnabled = value
                OnPropertyChanged(NameOf(IsPreviousButtonEnabled))
            End Set
        End Property

        Public Property IsNextButtonEnabled As Boolean
            Get
                Return _isNextButtonEnabled
            End Get
            Set(value As Boolean)
                _isNextButtonEnabled = value
                OnPropertyChanged(IsNextButtonEnabled)
            End Set
        End Property

        Public Property Totalpage As Integer
            Get
                Return _totalpage
            End Get
            Set(value As Integer)
                _totalpage = value
                OnPropertyChanged(NameOf(Totalpage))
            End Set
        End Property

        Public Property SortingCol As String
            Get
                Return _sortingCol
            End Get
            Set(value As String)
                _sortingCol = value
                OnPropertyChanged(NameOf(SortingCol))
            End Set
        End Property

        Public Property SortType As String
            Get
                Return _sortType
            End Get
            Set(value As String)
                _sortType = value
                OnPropertyChanged(NameOf(SortType))
            End Set
        End Property

        Public Property FilterCol As String
            Get
                Return _filterCol
            End Get
            Set(value As String)
                _filterCol = value
                OnPropertyChanged(NameOf(FilterCol))
            End Set
        End Property

        Public Property FilterValue As String
            Get
                Return _filterValue
            End Get
            Set(value As String)
                _filterValue = value
                OnPropertyChanged(FilterValue)
            End Set
        End Property

        Public Sub New()
            NextCommand = New ViewModelCommand(AddressOf ExcutenextCommand)
            PreviousCommand = New ViewModelCommand(AddressOf ExecutePreviousCommand)
            AgeclickCommand = New ViewModelCommand(AddressOf ExecuteAgeCommand)
            AscCommand = New ViewModelCommand(AddressOf ExecuteAscCommand)
            DescCommand = New ViewModelCommand(AddressOf ExecuteDescCommand)
            SortCommand = New ViewModelCommand(AddressOf ExecuteSortCommand)
            RemoveSortCommand = New ViewModelCommand(AddressOf ExecuteRemoveSortCommand)
            AgeFilterCommand = New ViewModelCommand(AddressOf ExecuteAfCommand)
            FirstNameFillCommand = New ViewModelCommand(AddressOf ExecuteFnFCommand)
            SearchCommand = New ViewModelCommand(AddressOf ExecuteSearchCommand)
            ClearSearchCommand = New ViewModelCommand(AddressOf ExecuteClearCommand)
            LoadDataAsync()

        End Sub

        Private Async Sub ExecuteClearCommand(obj As Object)
            FilterCol = Nothing
            FilterValue = Nothing
            Await LoadDataAsync()
        End Sub

        Private Async Sub ExecuteSearchCommand(obj As Object)
            Await LoadDataAsync()
        End Sub

        Private Sub ExecuteFnFCommand(obj As Object)
            FilterCol = "FirstName"
        End Sub

        Private Sub ExecuteAfCommand(obj As Object)
            FilterCol = "Age"
        End Sub

        Private Async Sub ExecuteRemoveSortCommand(obj As Object)
            SortType = ""
            SortingCol = ""
            Await LoadDataAsync()
        End Sub

        Private Async Sub ExecuteSortCommand(obj As Object)
            Await LoadDataAsync()
        End Sub

        Private Sub ExecuteDescCommand(obj As Object)
            SortType = "DESC"
        End Sub

        Private Sub ExecuteAscCommand(obj As Object)
            SortType = "ASC"
        End Sub

        Private Sub ExecuteAgeCommand(obj As Object)
            SortingCol = "Age"
        End Sub

        Private Async Function LoadDataAsync() As Task
            Try
                Dim apiUrl As String = $"https://localhost:7257/Pgenination?pageno={CurrentPage}&sortingCol={SortingCol}&sortType={SortType}&filterCol={FilterCol}&filterValue={FilterValue}"

                Using httpClient As New HttpClient()
                    Dim response = Await httpClient.GetAsync(apiUrl)

                    If response.IsSuccessStatusCode Then
                        Dim jsonData As String = Await response.Content.ReadAsStringAsync()
                        'retriving the data in the form of dto
                        Dim customerDto As CustomerDto = JsonConvert.DeserializeObject(Of CustomerDto)(jsonData)
                        'sending the data to dto
                        Dim customerss As List(Of CustomerModel) = customerDto.Customers
                        'retriving the data to customer collection
                        Customers = New ObservableCollection(Of CustomerModel)(customerss)
                        Totalpage = customerDto.Pages
                        CurrentPage = customerDto.CurrentPage
                    Else
                        ' Handle API error
                        MessageBox.Show("Error: Unable to fetch data from the API.")
                    End If
                End Using
            Catch ex As Exception
                ' Handle any exceptions that may occur during the API request
                MessageBox.Show($"An error occurred: {ex.Message}")
            End Try
        End Function

        Private Async Sub ExecutePreviousCommand(obj As Object)
            If CurrentPage > 1 Then
                CurrentPage -= 1
                Await LoadDataAsync()
            End If

        End Sub


        Private Async Sub ExcutenextCommand(obj As Object)
            If CurrentPage < Totalpage Then
                CurrentPage += 1
                Await LoadDataAsync()
            End If
        End Sub
    End Class
End Namespace

