using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

public partial class _boke : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataListDataBind();
    }
    public string zhaiyao(string str)
    {
        if (str.Length > 150)
            return str.Substring(0, 150) + "....";
        else
            return str;
    }
    protected void DataListDataBind()
    {
        Sql_StringChar sql_String = new Sql_StringChar();//动态数据源   
        sql_String.ConnectionString = sql_String.sql_char;//连接字符串
        sql_String.SelectCommand = "SELECT * FROM [blogMessages]  ORDER BY [blog_date] DESC ";
        DataView dv = (DataView)sql_String.Select(DataSourceSelectArguments.Empty);
        PagedDataSource objPage = new PagedDataSource();
        objPage.DataSource = dv;
        objPage.AllowPaging = true;
        objPage.PageSize = 10;
        int TolPage;
        TolPage = objPage.PageCount;
        int CurPage;
        if (Request.QueryString["Page"] != null)
        {
            CurPage = Convert.ToInt32(Request.QueryString["Page"]);
        }
        else
        {
            CurPage = 1;
        }
        objPage.CurrentPageIndex = CurPage - 1;
        pagenum.Text = CurPage.ToString();
        pagecount.Text = objPage.PageCount + "";
        lnkFirst.NavigateUrl = Request.CurrentExecutionFilePath + "?Page=1" + "&PageSize=" + Convert.ToString(objPage.PageSize);
        lnkLast.NavigateUrl = Request.CurrentExecutionFilePath + "?Page=" + TolPage.ToString() + "&PageSize=" + Convert.ToString(objPage.PageSize);
        if (!objPage.IsFirstPage)
        {
            lnkPrev.NavigateUrl = Request.CurrentExecutionFilePath + "?Page=" + Convert.ToString(CurPage - 1) + "&PageSize=" + Convert.ToString(objPage.PageSize);
        }
        else
        {
            lnkPrev.Visible = false;
            lnkFirst.Visible = false;
        }
        if (!objPage.IsLastPage)
        {
            lnkNext.NavigateUrl = Request.CurrentExecutionFilePath + "?Page=" + Convert.ToString(CurPage + 1) + "&PageSize=" + Convert.ToString(objPage.PageSize);
        }
        else
        {
            lnkNext.Visible = false;
            lnkLast.Visible = false;
        }
        sql_String.Dispose();
        DataList2.DataSource = objPage;
        DataList2.DataBind();
        #region/******************千万不能删************************/
        //get评论数
        //string j = string.Empty;
        //for (int i = 0; i < DataList2.Items.Count; i++)
        //{
        //    j = ((Label)DataList2.Items[i].FindControl("Label12")).Text;
        //    string sql_char = "server=.;Initial Catalog=Blog_wwww;User ID=sa;Password=1";//连接字符串
        //    string sql = "SELECT  count(blog_id)  FROM [blogComments] WHERE BLOG_ID='" + j + "'";
        //    using (SqlConnection conn = new SqlConnection(sql_char))
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {
        //            string k = conn.State.ToString();
        //            cmd.CommandText = sql;

        //            //if (cmd.ExecuteReader() > 0)
        //            //{
        //            //}
        //            int n = cmd.ExecuteNonQuery();
        //            using (SqlDataReader dr = cmd.ExecuteReader())
        //            {
        //                //string k=dr[""]
        //            }
        //        }
        //    }
        //}
        #endregion /******************千万不能删************************/
    }

    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        String year, month, day;
        year = Convert.ToString(e.Day.Date.Year);
        month = Convert.ToString(e.Day.Date.Month);
        day = Convert.ToString(e.Day.Date.Day);
        Sql_StringChar sql_String = new Sql_StringChar();//动态数据源   
        sql_String.ConnectionString = sql_String.sql_char;//连接字符串
        //AccessDataSource ads = new AccessDataSource();
        //ads.DataFile = "~/date/blog_Data.mdb";
        sql_String.SelectCommand = "select * from blogMessages where day(cast(blog_date as datetime))=" + day + " and month(cast(blog_date as datetime))=" + month + " and year(cast(blog_date as datetime))=" + year;

        //ads.SelectCommand = "select * from blogMessages where day(cdate(blog_date))=" + day + " and month(cdate(blog_date))=" + month + " and year(cdate(blog_date))=" + year;


        DataView dv = (DataView)sql_String.Select(DataSourceSelectArguments.Empty);

        if (dv.Count != 0)
        {
            e.Cell.BackColor = Color.LightPink;
            e.Cell.Controls.Clear();
            HyperLink hl = new HyperLink();
            hl.NavigateUrl = "blogSearch.aspx?year=" + year + "&month=" + month + "&day=" + day;
            hl.NavigateUrl = "blogSearch.aspx?year=" + year + "&month=" + month + "&day=" + day;
            hl.Text = e.Day.Date.Day.ToString();
            e.Cell.Controls.Add(hl);
        }
        else
        {
            e.Cell.Controls.Clear();
            e.Cell.Text = e.Day.Date.Day.ToString();
        }

    }

    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {

    }
    protected void Calendar1_DayRender1(object sender, DayRenderEventArgs e)
    {

    }
    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        if (Session["userName"] != null)
        {
            Response.Redirect("Personal_base.aspx");
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        if (Session["username"] != null)
        {
            Session.RemoveAll();
            Response.Redirect("_boke.aspx");
        }
        else
        { Response.Write("@<script>alert('还没登录？哪来的登出')</srcipt>");
        Response.Redirect("_boke.aspx");
            return; }
    }
}