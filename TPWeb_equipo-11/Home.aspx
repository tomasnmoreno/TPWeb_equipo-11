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
        <% foreach (dominio.Articulo item in listaArticulos) { %>
            <div class="col-md-4 mb-4">
                <div class="card" style="width: 18rem;">
                    <img src="<%=item.imagenUrl %>" class="card-img-top" alt="...">
                    <div class="card-body">
                        <h5 class="card-title"><%=item.nombre %></h5>
                        <p class="card-text"><%=item.descripcion %></p>
                        <a href="#" class="btn btn-secondary">Ver detalle</a>
                        <button class="btn btn-primary">Agregar al carrito</button>
                    </div>
                </div>
            </div>
        <% } %>
    </div>

    </div>

</asp:Content>
