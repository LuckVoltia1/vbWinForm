Imports System.Windows.Forms
Imports System
Imports MySql.Data.MySqlClient

Public Class Form1
    Inherits Form

    ' Define controls
    Dim LblUser As New Label()
    Dim LblPass As New Label()
    Dim txtUser As New TextBox()
    Dim txtPass As New TextBox()
    Dim btnLogin As New Button()
    Dim gridView As New DataGridView()
    Dim btnUpdate As New Button()
        Dim btnDelete As New Button()
        Dim btnSearch As New Button()
        Dim txtSearch As New TextBox()

    


    ' Constructor
    Sub New()
        InitializeComponent()
        InitializeDataGridView()
        ShowData()
    End Sub


Public Sub InitializeDataGridView()
gridView.AutoSize = True
gridView.Location = New Point(10,200)
Me.Controls.Add(gridView)
End Sub
    ' Method to initialize the components of the form
    Public Sub InitializeComponent()
        ' Set properties for Username label
        LblUser.Text = "Username"
        LblUser.Location = New Point(10, 10)
        LblUser.AutoSize = True
        Me.Controls.Add(LblUser)

        txtSearch.Location = New Point(400, 10)
        txtSearch.AutoSize = True
        Me.Controls.Add(txtSearch)


        btnSearch.Text = "Search"
        btnSearch.Location = New Point(600, 10)
        btnSearch.AutoSize = True
        AddHandler btnSearch.Click, AddressOf btnSearch_Click
        Me.Controls.Add(btnSearch)


        ' Set properties for Password label
        LblPass.Text = "Password"
        LblPass.Location = New Point(10, 40)
        LblPass.AutoSize = True
        Me.Controls.Add(LblPass)

        ' Set properties for Username textbox
        txtUser.Size = New Size(150, 20)
        txtUser.Location = New Point(150, 10)
        Me.Controls.Add(txtUser)

        ' Set properties for Password textbox
        txtPass.Size = New Size(150, 20)
        txtPass.Location = New Point(150, 40)
        Me.Controls.Add(txtPass)

        ' Set properties for Login button
        btnLogin.Text = "Login"
        btnLogin.Location = New Point(100, 100)
        btnLogin.AutoSize = True
        AddHandler btnLogin.Click, AddressOf btnLogin_Click
        Me.Controls.Add(btnLogin)


        btnUpdate.Text = "Update"
        btnUpdate.Location = New Point(200, 100)
        btnUpdate.AutoSize = True
        AddHandler btnUpdate.Click, AddressOf UpdateData
        Me.Controls.Add(btnUpdate)

        btnDelete.Text = "Delete"
        btnDelete.Location = New Point(300, 100)
        btnDelete.AutoSize = True
        AddHandler btnDelete.Click, AddressOf DeleteData
        Me.Controls.Add(btnDelete)

    End Sub

    ' Event handler for Login button click
    Public Sub btnLogin_Click(obj As Object, args As EventArgs)
    Try
    Dim ab As String = "server=localhost;Database=loggin;User name=root;pass=;"
    Dim con As New MySqlConnection(ab)
    con.Open()
    Dim query As String = "Insert into loggin(user,pass) values(@name,@pass)"
    Dim command As New MySqlCommand(query,con)
        ' If txtUser.Text = "admin" AndAlso txtPass.Text = "password" Then
        '     MsgBox("Login Successful")
        ' Else
        '     MsgBox("Invalid Credentials")
        ' End If

        command.Parameters.AddWithValue("@name",txtUser.Text)
        command.Parameters.AddWithValue("@pass",txtPass.Text)

        command.ExecuteNonQuery()
        ShowData()

        con.Close()
        MsgBox("Success login")
    Catch ex As Exception
    MsgBox("Error: " & ex.Message)

    End Try
    End Sub


    Public Sub ShowData()
    Dim ab As String = "server=localhost;Database=loggin;User name=root;pass="
    Dim con As New MySqlConnection(ab)
    con.Open()
    Dim query As String = "select * from loggin"
    Dim command As New MySqlCommand(query,con)
    Dim DataTable As New DataTable()
    Dim DataAdapter As New MySqlDataAdapter(command)
    DataAdapter.Fill(DataTable)
    gridView.DataSource = DataTable
    End Sub

    Public Sub UpdateData(Obj As Object,Evg As EventArgs)
     Dim ab As String = "server=localhost;Database=loggin;User name=root;pass="
     Dim id As Integer = Convert.ToInt32(gridView.SelectedRows(0).Cells("id").Value)
    Dim con As New MySqlConnection(ab)
    con.Open()
    Dim query As String = "Update loggin Set user = @name,pass = @pass where id=@id"
    Dim command As New MySqlCommand(query,con)
    command.Parameters.AddWithValue("@id",id)

    command.Parameters.AddWithValue("@name",txtUser.Text)
    command.Parameters.AddWithValue("@pass",txtPass.Text)

    command.ExecuteNonQuery()
    ShowData()
    End Sub 


    Public Sub DeleteData(Obj As Object,Args As EventArgs)
    Dim ab As String = "server=localhost;Database=loggin;User name=root;pass="
    Dim id As Integer = Convert.ToInt32(gridView.SelectedRows(0).Cells("id").Value)
    Dim con As New MySqlConnection(ab)
    Dim query As String = "Delete from loggin where id=@id"
    con.Open()
        Dim command As New MySqlCommand(query,con)
        command.Parameters.AddWithValue("@id",id)
        command.ExecuteNonQuery()
        ShowData()
        End Sub

Public Sub btnSearch_Click(obj As Object,Args As EventArgs)
Dim ab As String = "server=localhost;Database=loggin;User name=root;pass="
Dim con As New MySqlConnection(ab)
con.Open()
Dim query As String = "select * from loggin where user like @name"
Dim command As New MySqlCommand(query,con)
command.Parameters.AddWithValue("@name",txtSearch.Text)
Dim DataTable As New DataTable()
Dim DataAdapter As New MySqlDataAdapter(command)
DataAdapter.Fill(DataTable)
gridView.DataSource = DataTable
con.Close()
End Sub






End Class