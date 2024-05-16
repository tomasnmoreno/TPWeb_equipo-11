<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TPWeb_equipo_11.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" cssclass="col-md-6">

    <div class="row">
        <div>
            <h1>Listado de artículos</h1>
            <hr />
        </div>

        <div class="row">
            <asp:Repeater runat="server" ID="repetidor">
                <ItemTemplate>
                    <div class="col-md-4 mb-4">
                        <div class="card" style="width: 18rem;">

                            <div class="card-body">
                                
                                <img src="<%# Eval("imagenUrl") %>" class="card-img-top" alt="...">
                                <h5 class="card-title"><%# Eval("nombre") %></h5>
                                <p class="card-text"><%#  Eval("descripcion") %></p>
                                <p class="card-text">Precio: $ <%# string.Format("{0:0.00}", Eval("precio")) %></p>
                                <a href="DetalleArticulo.aspx?id=<%# Eval("id") %>" class="btn btn-secondary">Ver detalle</a>
                                <asp:Button ID="btnAgregarAlCarrito" runat="server" Text="Agregar al carrito" Onclick="btnAgregarAlCarrito_Click" CssClass="btn btn-primary" CommandArgument='<%# Eval("Id") %>' CommandName="ArticuloId"/>
                            
                            </div>
                        
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

        </div>

    </div>

</asp:Content>
