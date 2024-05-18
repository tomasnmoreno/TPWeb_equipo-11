<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="TPWeb_equipo_11.Carrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%  if (listaCarrito.Count() != 0)
        //if(Session["listaCarrito"] != null)
        {%>
    <h1>Carrito</h1>
    <%}
        else
        {%>
    <h2>El carrito está vacío.</h2>
    <a href="/Home.aspx" class="btn btn-info">Ir a comprar</a>
    <% }%>
    <hr />
    <div class="row">
        <asp:Repeater runat="server" ID="repCarrito">
            <ItemTemplate>
                <div class="col-md-3 mb-4">
                    <div class="card border border-dark font-weight-bold mx-auto h-100" style="width: 18rem;">
                        <div class="card-body">
                            <img src="<%#Eval("agregado.imagenUrl") %>" class="card-img-top" style="object-fit: cover; object-position: center; width: 200px; height: 200px;" alt="...">
                            <h5 class="card-title"><%#Eval("agregado.Nombre") %></h5>
                            <p class="card-text"><%#Eval("agregado.Descripcion") %></p>
                            <p class="card-text">Precio: $ <%# string.Format("{0:0.00}", Eval("agregado.Precio")) %></p>

                            <div class="col-md-3">
                                <label for="validCantidad" class="col sm-2">Cantidad</label>
                                <asp:DropDownList runat="server" ID="validCantidad" AutoPostBack="True" OnSelectedIndexChanged="validCantidad_SelectedIndexChanged">
                                    <asp:ListItem Text="1" />
                                    <asp:ListItem Text="2" />
                                    <asp:ListItem Text="3" />
                                    <asp:ListItem Text="4" />
                                    <asp:ListItem Text="5" />
                                    <asp:ListItem Text="6" />
                                    <asp:ListItem Text="7" />
                                </asp:DropDownList>
                            </div>

                            <%--//// OTRA OPCION: Select de Bootstrap ////--%>
                            <%--<div class="col-md-3">
                                <select id="dpdCantidad" class="form-select" aria-label="Default select example">
                                    <option selected>Open this select menu</option>
                                    <option value="1">One</option>
                                    <option value="2">Two</option>
                                    <option value="3">Three</option>
                                </select>
                            </div>--%>





                            <asp:Button ID="btnEliminarDelCarrito" CssClass="btn btn-danger" Text="Eliminar" runat="server" OnClick="btnEliminarDelCarrito_Click" CommandArgument='<%# Eval("agregado.Id") %>' CommandName="ArticuloId" />
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
