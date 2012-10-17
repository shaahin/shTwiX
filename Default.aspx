<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="shTwiX.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Tweet Freely!</h2>
  <p>Twittr.me is a Twitter API Proxy powered by <a href="http://github.com/shaahin/shtwix" target="_blank">shTwiX open-source Project</a>.</p>
  <p>Click on the button below to get your API Proxy URL. Then use it on Twitter App on your iOS Device!</p>
  <div class="row-fluid">
    <div class="span3">
    <a class="btn btn-primary btn-large" href="/Authorize.aspx">
      Sign In to Twitter
    </a>
  </div>
  <div class="span3 offset6">

  </div>
  </div>

</asp:Content>
