<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="TPWeb_equipo_11.Carrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Carrito</h1>
    <hr />
    <div class="row">
        <asp:Repeater runat="server" ID="repCarrito">
            <ItemTemplate>
                <div class="col-md-3 mb-4">
                    <div class="card border border-dark font-weight-bold mx-auto h-100" style="width: 18rem;">
                        <div class="card-body">
                            <img src="<%# Eval("imagenUrl") %>" class="card-img-top" alt="...">
                            <h5 class="card-title"><%# Eval("Nombre") %></h5>
                            <p class="card-text"><%# Eval("Descripcion") %></p>
                            <p class="card-text">Precio: $ <%# string.Format("{0:0.00}", Eval("Precio")) %></p>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>
    <div>
        <asp:Label runat="server" ID="lblTotalCarrito" Text="Total del Carrito: $0.00" />
        <hr />
    </div>

</asp:Content>
