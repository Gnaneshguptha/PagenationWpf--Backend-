Imports System.Windows.Input

Public Class ViewModelCommand
    Implements ICommand

    Private ReadOnly _executeAction As Action(Of Object)
    Private ReadOnly _canExecuteAction As Predicate(Of Object)

    Public Sub New(executeAction As Action(Of Object), canExecuteAction As Predicate(Of Object))
        _executeAction = executeAction
        _canExecuteAction = canExecuteAction
    End Sub

    Public Sub New(executeAction As Action(Of Object))
        _executeAction = executeAction
        _canExecuteAction = Nothing
    End Sub

    Public Custom Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged
        AddHandler(ByVal value As EventHandler)
            AddHandler CommandManager.RequerySuggested, value
        End AddHandler
        RemoveHandler(ByVal value As EventHandler)
            RemoveHandler CommandManager.RequerySuggested, value
        End RemoveHandler
        RaiseEvent(sender As Object, e As EventArgs)
            ' No need to raise this event manually in VB.NET
        End RaiseEvent
    End Event

    Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
        Return If(_canExecuteAction Is Nothing, True, _canExecuteAction(parameter))
    End Function

    Public Sub Execute(parameter As Object) Implements ICommand.Execute
        _executeAction(parameter)
    End Sub
End Class
