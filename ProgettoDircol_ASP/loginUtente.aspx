<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="loginUtente.aspx.cs" Inherits="ProgettoDircol_ASP.loginUtente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel ID="pnLoginUtente" runat="server">

        <div class="container">
            <div class="row">
                <div class="col-md-6 mx-auto">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col">
                                    <center>
                                        <img width="100px" src="Images/LoginUtente.png" />
                                    </center>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <center>
                                        <h3>Login Utente</h3>
                                    </center>
                                </div>
                            </div>
                            <%-- USERNAME --%>
                            <div class="row">
                                <div class="col">
                                    <hr>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <asp:Label Text="Username:" ID="lblUsername" runat="server"></asp:Label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control"
                                            ID="txtUsernameUtente" runat="server"
                                            aria-describedby="usernameHelp" placeholder="Inserisci lo username" />
                                        <small id="usernameHelp" class="form-text text-muted">Non mostreremo mai il tuo account a nessuno</small>
                                    </div>


                                    <%-- PASSWORD --%>

                                    <asp:Label Text="Password:" ID="lblPasswordUtente" runat="server" />
                                    <div class="form-group">

                                        <asp:TextBox TextMode="Password" CssClass="form-control"
                                            ID="txtPasswordUtente" runat="server" placeholder="Password" />
                                    </div>
                                    <br />
                                    <br />
                                    <div class="form-group">
                                        <asp:Button ID="btnAccedi" runat="server"
                                            CssClass="btn btn-success btn-block btn-lg"
                                            Text="Accedi"
                                            OnClick="btnAccedi_Click" />
                                    </div>
                                    <br />
                                    <br />
                                    <div class="form-group">
                                        <asp:Button ID="btnRegistrati" runat="server"
                                            CssClass="btn btn-primary btn-block btn-lg"
                                            Text="Registrati"
                                            OnClick="btnRegistrati_Click" />

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%-- Torna indietro --%>
                </div>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
