<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="registrazioneUtente.aspx.cs" Inherits="ProgettoDircol_ASP.registrazioneUtente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- FORM DI REGISTRAZIONE UTENTE --%>

    <%--<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="usersignup.aspx.cs" Inherits="WebApplication3.usersignup" %>--%>

    <%--<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server"></asp:Content>--%>
    <%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">--%>


    <asp:Panel ID="pnRegistrazioneUtente" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-md-8 mx-auto">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col">
                                    <center>
                                        <img width="100px" src="Images/accesso.png" />
                                    </center>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <center>
                                        <h4>Registrazione utente</h4>
                                    </center>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <hr>
                                </div>
                            </div>
                            <div class="row">
                                <%-- NOME --%>
                                <div class="col-md-6">
                                    <label>Nome</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control"
                                            ID="txtNomeUtente" runat="server"
                                            placeholder="Nome"></asp:TextBox>
                                    </div>
                                </div>
                                <%-- COGNOME --%>
                                <div class="col-md-6">
                                    <label>Cognome</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control"
                                            ID="txtCognomeUtente" runat="server"
                                            placeholder="Cognome"></asp:TextBox>
                                    </div>
                                </div>
                                <%-- DATA DI NASCITA --%>
                                <div class="col-md-6">
                                    <label>Data di nascita</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control"
                                            ID="txtDataNascitaUtente" runat="server"
                                            placeholder="Data di nascita" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <%-- TELEFONO --%>
                                <div class="col-md-6">
                                    <label>Telefono</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control"
                                            ID="txtTelefonoUtente" runat="server"
                                            placeholder="Numero di telefono"
                                            TextMode="Number"></asp:TextBox>
                                    </div>
                                </div>
                                <%-- EMAIL --%>
                                <div class="col-md-6">
                                    <label>Email</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control"
                                            ID="txtEmailUtente" runat="server"
                                            placeholder="Indirizzo email"
                                            TextMode="Email"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <%-- STATO (drop down list) --%>
                                <div class="col-md-4">
                                    <label>Stato</label>
                                    <div class="form-group">
                                        <asp:DropDownList
                                            class="form-control"
                                            ID="ddlStatoUtente" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%-- CITTA' --%>
                                <div class="col-md-4">
                                    <label>Città</label>
                                    <div class="form-group">
                                        <asp:TextBox class="form-control"
                                            ID="txtCittaUtente" runat="server"
                                            placeholder="Città"></asp:TextBox>
                                    </div>
                                </div>
                                <%-- CAP --%>
                                <div class="col-md-4">
                                    <label>CAP</label>
                                    <div class="form-group">
                                        <asp:TextBox class="form-control"
                                            ID="txtCAPUtente" runat="server"
                                            placeholder="CAP"
                                            TextMode="Number"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <%-- INDIRIZZO  --%>
                                <div class="col">
                                    <label>Indirizzo</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control"
                                            ID="txtIndirizzoUtente" runat="server"
                                            placeholder="Indirizzo"
                                            TextMode="MultiLine" Rows="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <center>
                                        <span class="badge badge-pill badge-info">Credenziali di accesso</span>
                                    </center>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <%-- USERNAME --%>
                                    <label>Username</label>
                                    <div class="form-group">
                                        <asp:TextBox class="form-control"
                                            ID="txtUsernameUtente" runat="server"
                                            placeholder="Username"></asp:TextBox>
                                    </div>
                                </div>
                                <%-- PASSWORD --%>
                                <div class="col-md-6">
                                    <label>Password</label>
                                    <div class="form-group">
                                        <asp:TextBox class="form-control"
                                            ID="txtPasswordUtente" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <%-- BOTTONE REGISTRAZIONE --%>
                                <div class="col">
                                    <div class="form-group">
                                        <asp:Button class="btn btn-success btn-block btn-lg"
                                            ID="btnRegistrati" runat="server"
                                            Text="Registrati"
                                            OnClick="btnRegistrati_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <center>
                       <%-- <asp:Label runat="server" ID="lblTornaHome">Torna alla home</asp:Label>--%>
                        <br />
                        <asp:LinkButton runat="server" ID="btnTornaHome" Text="" CssClass="btn btn-primary" OnClick="btnTornaHome_Click">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-house" viewBox="0 0 16 16">
                                <path fill-rule="evenodd" d="M2 13.5V7h1v6.5a.5.5 0 0 0 .5.5h9a.5.5 0 0 0 .5-.5V7h1v6.5a1.5 1.5 0 0 1-1.5 1.5h-9A1.5 1.5 0 0 1 2 13.5zm11-11V6l-2-2V2.5a.5.5 0 0 1 .5-.5h1a.5.5 0 0 1 .5.5z" />
                                <path fill-rule="evenodd" d="M7.293 1.5a1 1 0 0 1 1.414 0l6.647 6.646a.5.5 0 0 1-.708.708L8 2.207 1.354 8.854a.5.5 0 1 1-.708-.708L7.293 1.5z" />
                            </svg>
                            &nbsp; Torna alla Home
                        </asp:LinkButton>
                    </center>
                    <br>
                </div>
            </div>
        </div>
    </asp:Panel>



















    <%-- 
    <asp:Panel runat="server" ID="pnRegistrazioneUtente"
        HorizontalAlign="Center">
       
        <div class="form-row">
            <div class="form-group col-md-6">
                <asp:Label ID="lblEmail" runat="server">Email</asp:Label>
                <asp:TextBox TextMode="Email"  runat="server"
                    ID="txtEmail"
                    CssClass="form-control" id="inputEmail4" placeholder="Email"/>
            </div>
            <div class="form-group col-md-6">
                <label for="inputPassword4">Password</label>
                <input type="password" class="form-control" id="inputPassword4" placeholder="Password">
            </div>
        </div>
        <div class="form-group">
            <label for="inputAddress">Address</label>
            <input type="text" class="form-control" id="inputAddress" placeholder="1234 Main St">
        </div>
        <div class="form-group">
            <label for="inputAddress2">Address 2</label>
            <input type="text" class="form-control" id="inputAddress2" placeholder="Apartment, studio, or floor">
        </div>
        <div class="form-row">
            <div class="form-group col-md-6">
                <label for="inputCity">City</label>
                <input type="text" class="form-control" id="inputCity">
            </div>
            <div class="form-group col-md-4">
                <label for="inputState">State</label>
                <select id="inputState" class="form-control">
                    <option selected>Choose...</option>
                    <option>...</option>
                </select>
            </div>
            <div class="form-group col-md-2">
                <label for="inputZip">Zip</label>
                <input type="text" class="form-control" id="inputZip">
            </div>
        </div>
        <div class="form-group">
            <div class="form-check">
                <input class="form-check-input" type="checkbox" id="gridCheck">
                <label class="form-check-label" for="gridCheck">
                    Check me out
                </label>
            </div>
        </div>
        <button type="submit" class="btn btn-primary">Sign in</button>
    </asp:Panel>
    --%>
</asp:Content>
