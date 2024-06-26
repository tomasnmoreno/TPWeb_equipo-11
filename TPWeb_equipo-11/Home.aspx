﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TPWeb_equipo_11.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" href="resize_img.css"/>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" cssclass="col-md-6">

    <div class="row">
        <div>
            <h1>Listado de artículos</h1>
            <asp:TextBox ID="txtBusqueda" runat="server" BorderStyle="Ridge"></asp:TextBox>
            <%--<input style="margin-left: 20px" class="form-control me-2 " type="search" placeholder="Búsqueda" aria-label="Search">--%>
            <asp:Button Text="Buscar" id="btnBuscar" onclick="btnBuscar_Click" cssclass="btn btn-primary" runat="server" style="margin-left: 20px" class="form-control me-2 " type="search" placeholder="Búsqueda" aria-label="Search"/>
            <%--<button class="btn btn-primary" type="submit">Buscar</button>--%>
            <hr />
        </div>
        <div class="row" style="margin-top: 20px">
            <asp:Repeater runat="server" ID="repetidor">
                <ItemTemplate>
                    <div class="col-md-4 mb-4">
                        <div class="card" style="width: 18rem;">
                            <div class="card-body">
                                <%--<img src="<%#Eval("imagenUrl") %>" class="card-img-top" style="object-fit: cover; width:auto; height:auto; max-width:200px; max-height:200px;" alt="...">--%>
                                <img src="<%#Eval("imagenUrl") %>" class="card-img-top" style="object-fit: cover; object-position: center; width:200px; height:200px;" alt="...">
                                <h5 class="card-title"><%#Eval("nombre") %></h5>
                                <p class="card-text"><%#Eval("descripcion") %></p>
                                <p class="card-text">Precio: $ <%# string.Format("{0:0.00}",Eval("precio")) %></p>
                                <a href="DetalleArticulo.aspx?id=<%#Eval("id") %>" class="btn btn-secondary">Ver detalle</a>
                                <asp:Button ID="btnAgregarAlCarrito" runat="server" Text="Agregar al carrito" Onclick="btnAgregarAlCarrito_Click" CssClass="btn btn-primary" CommandArgument='<%# Eval("Id") %>' CommandName="ArticuloId"/>                            
                           </div>                       
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

        </div>

    </div>

</asp:Content>
