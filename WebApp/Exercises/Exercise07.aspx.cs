﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DBSystem.BLL;
using DBSystem.ENTITIES;

namespace WebApp.Exercises
{
    public partial class Exercise07 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MessageLabel.Text = "";
            if (!Page.IsPostBack)
            {
                BindList();
            }
        }
        protected void BindList()
        {
            try
            {
                TeamController sysmgr = new TeamController();
                List<Team> info = null;
                info = sysmgr.List();
                info.Sort((x, y) => x.TeamName.CompareTo(y.TeamName));
                List01.DataSource = info;
                List01.DataTextField = nameof(Team.TeamName);
                List01.DataValueField = nameof(Team.TeamID);
                List01.DataBind();
                List01.Items.Insert(0, "select...");
            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }
        }
        protected void Fetch_Click(object sender, EventArgs e)
        {
            if (List01.SelectedIndex == 0)
            {
                MessageLabel.Text = "Select a category to view its products";
            }
            else
            {
                try
                {
                    TeamController sysmgr01 = new TeamController();
                    Team info01 = null;
                    info01 = sysmgr01.FindByPKID(int.Parse(List01.SelectedValue));
                    IDLabel01.Text = "Coach:";
                    IDLabel02.Text = info01.Coach.ToString();
                    NameLabel01.Text = "Assistant Coach:";
                    NameLabel02.Text = info01.AssistantCoach;
                    NameLabel03.Text = "Wins:";
                    NameLabel04.Text = info01.Wins.ToString();
                    NameLabel05.Text = "Losses:";
                    NameLabel06.Text = info01.Losses.ToString();

                    PlayerController sysmgr02 = new PlayerController();
                    List<Player> info02 = null;
                    info02 = sysmgr02.FindByID(int.Parse(List01.SelectedValue));
                    info02.Sort((x, y) => x.PlayerID.CompareTo(y.PlayerID));
                    List02.DataSource = info02;
                    List02.DataBind();
                }
                catch (Exception ex)
                {
                    MessageLabel.Text = ex.Message;
                }
            }
        }
        protected void List02_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            List02.PageIndex = e.NewPageIndex;
            Fetch_Click(sender, new EventArgs());
        }
    }
}