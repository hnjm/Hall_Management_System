﻿using HallManagementSystem.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Hall_HallList : System.Web.UI.Page
{
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Check Valid Admin
        if (Session["AdminID"] == null)
            Response.Redirect("~/AdminPanel/Authorization/AdminLogin.aspx");
        #endregion

        if (!Page.IsPostBack)
        {
            FillHallGridView();
        }
    }
    #endregion

    #region Fill Hall Grid View
    private void FillHallGridView()
    {
        HallBAL balHall = new HallBAL();
        DataTable dtHall = new DataTable();

        dtHall = balHall.SelectAll();

        if (dtHall != null && dtHall.Rows.Count > 0)
        {
            gvHall.DataSource = dtHall;
            gvHall.DataBind();
        }
        else
        {
            gvHall.DataSource = null;
            gvHall.DataBind();
        }
    }
    #endregion

    #region Button Add Hall
    protected void btnAddHall_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AdminPanel/Hall/HallAddEdit.aspx");
    }
    #endregion

    #region Button Delete or Edit Click
    protected void gvHall_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            if (e.CommandArgument != null)
            {
                HallBAL balHall = new HallBAL();
                HallPhotosBAL balHallPhotos = new HallPhotosBAL();

                if (balHallPhotos.Delete(Convert.ToInt32(e.CommandArgument.ToString())) && balHall.Delete(Convert.ToInt32(e.CommandArgument.ToString())))
                {
                    FillHallGridView();
                }
                else
                {
                    lblErrorMessage.Text = balHallPhotos.Message;
                    lblErrorMessage.Text = balHall.Message;
                }
            }
        }
        if (e.CommandName == "EditRecord")
        {
            if (e.CommandArgument != null)
            {
                Response.Redirect(e.CommandArgument.ToString().Trim());
            }
        }
    }
    #endregion

    #region Hall Row DataBound
    protected void gvHall_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            e.Row.TableSection = TableRowSection.TableHeader;
    }
    #endregion
}