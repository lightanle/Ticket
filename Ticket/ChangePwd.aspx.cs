﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ticket
{
    public partial class ChangePwd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Session["ValidateNum"].ToString() == TextBox3.Text)
            {
                try
                {
                    SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TicketConnectionString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("select count(*) from HuiYuan where HuiYuanHao = '" + TextBox1.Text + "' and HuiYuanPassword = '" + TextBox2.Text + "'", cnn);
                    cnn.Open();
                    int num = (int)cmd.ExecuteScalar();
                    cnn.Close();
                    if (num == 1)
                    {
                        Session["UserInfo"] = TextBox1.Text;
                        Response.Redirect("./InputNewPwd.aspx");
                    }
                    else
                    {
                        Response.Write(createDlgStr("登录失败"));
                    }
                }
                catch (SqlException ex)
                {
                    Response.Write(createDlgStr("数据库查询失败" + ex.Message));
                }
            }
            else
            {
                Response.Write(createDlgStr("验证码输入错误!"));
            }

        }
        public String createDlgStr(string tmp)
        {
            return ("<script>alert(\"" + tmp + "\");</script>");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("./forgetPwd.aspx");
        }
    }
}