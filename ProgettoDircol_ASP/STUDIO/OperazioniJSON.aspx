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
                <table class="table table-dark">
                    <thead class="thead-dark">
                        <th>Username</th>
                        <th>Lista ID capi</th>
                    </thead>
                    <tbody class="tbody-dark">
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Eval("Username")%>
                    </td>
                    <td>
                        <%-- POSSO SCEGLIERE DI VEDERE SIA CON LIST BOX SIA CON DDL --%>
                        <%-- <asp:DropDownList ID="ddlCarrello_Iesimo" runat="server"
                            DataSource='<%# Eval("ListaIDCapi") %>' CssClass="form-control">
                        </asp:DropDownList>--%>
                        <asp:ListBox ID="lbCarrello_Iesimo" runat="server"
                            DataSource='<%# Eval("ListaIDCapi") %>' CssClass="form-control"></asp:ListBox>
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



<%-- ItemType="ProgettoDircol_ASP/Dati/Carrello"
                            SelectMethod="GetListaID"
                            DataSource='<%# Eval("ListaIDCapi") %>' --%>


