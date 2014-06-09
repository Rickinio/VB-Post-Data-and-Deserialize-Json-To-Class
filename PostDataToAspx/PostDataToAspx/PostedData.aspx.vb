Imports System.Web.Script.Serialization

Public Class PostedData
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ProcessJsonRequest()
    End Sub

    Public Function ProcessJsonRequest() As String
        Dim body As System.IO.Stream = Request.InputStream
        Dim encoding As System.Text.Encoding = Request.ContentEncoding
        Dim reader As System.IO.StreamReader = New System.IO.StreamReader(body, encoding)
        Dim json As String = reader.ReadToEnd
        Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
        Dim articledata As ArticleData = serializer.Deserialize(json, GetType(ArticleData))

        Dim myconnect As New SqlClient.SqlConnection
        myconnect.ConnectionString = "Data Source=KOSTAS-HP\SQLEXPRESS;Initial Catalog=ArticlesInfo;Integrated Security=True"


        Dim mycommand As SqlClient.SqlCommand = New SqlClient.SqlCommand()
        mycommand.Connection = myconnect
        mycommand.CommandText = "INSERT INTO Articles (Title, Subtitle, Article) VALUES (@Title, @Subtitle, @Article)"
        myconnect.Open()

        Try
            mycommand.Parameters.Add("@Title", SqlDbType.NVarChar).Value = articledata.title
            mycommand.Parameters.Add("@Subtitle", SqlDbType.NVarChar).Value = articledata.subtitle
            mycommand.Parameters.Add("@Article", SqlDbType.VarChar).Value = articledata.article
            mycommand.ExecuteNonQuery()
        Catch ex As System.Data.SqlClient.SqlException

        Finally
            myconnect.Dispose()
            mycommand.Dispose()
            reader.Dispose()
        End Try

        Return "OK"
    End Function

End Class

