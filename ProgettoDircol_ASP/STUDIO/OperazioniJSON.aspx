<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OperazioniJSON.aspx.cs" Inherits="ProgettoDircol_ASP.STUDIO.OperazioniJSON" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel ID="pnOperazioniJSON" runat="server">


        <%-- PROVA PER UN SINGOLO CARRELLO --%>
        <h3>PROVA PER UN SINGOLO CARRELLO </h3>
        <br />
        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:DropDownList ID="ddlListaID" runat="server" CssClass="form-control"></asp:DropDownList>

        <br />
        <br />

        <%-- REPEATER PER PROVARE LISTA CARRELLI --%>
        <h3>REPEATER PER PROVARE LISTA CARRELLI </h3>
        <br />
        <asp:Repeater ID="rptCarrello" runat="server" OnItemDataBound="rptCarrello_ItemDataBound">
            <HeaderTemplate>
                <table width="100%">
                    <thead>
                        <th>Username</th>
                        <th>Lista ID capi</th>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td valign="top" align="left" width="100%">
                        <font size="2" color="white">
                            <%# Eval("Username")%>
                            </font>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlCarrello_Iesimo" runat="server" 
                            ItemType="ProgettoDircol_ASP/Dati/Carrello"
                            SelectMethod="GetListaID"
                            DataSource='<%# Eval("ListaIDCapi") %>'>
                        </asp:DropDownList>
                    </td>
                    </td>
                </tr>
            </ItemTemplate>
            <SeparatorTemplate>
                <tr>
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
            </SeparatorTemplate>
            <FooterTemplate>
                </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </asp:Panel>

</asp:Content>


