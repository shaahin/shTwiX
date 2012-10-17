<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="Callback.aspx.cs" Inherits="shTwiX.Callback" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Voila!</h2>
    
    
  <p> Your API Proxy URL is: <pre><asp:Label ID="txt_APIURL" runat="server" Text="Label"></asp:Label></pre></p>
  <p>Please write down the URL somewhere safe, and follow the instructions below to setup your Twitter App.</p>
  <ol>
  <li>Open Twitter on your iPhone/iPad.</li>
  <li>Tap Sign In.</li>
  <li>Tap on the gear icon.</li>
  <li>Enter your API Proxy URL.</li>
  <li>Go Back and Sign In!</li>
  <li>Enjoy Twitter!</li>
  </ol>
  <div class="alert alert-info">
  <strong>iOS 5 and above users:</strong> If you don't see the Sign In and Gear button in your Twitter App, go to Settings>Twitter and set the switch in the front of Twitter to OFF.
  </div>
  <p>

  </p>
</asp:Content>

