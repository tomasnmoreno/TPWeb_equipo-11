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
            <% int j = 0; foreach (dominio.Articulo item in listaArticulos)
                {
                    listaimagenes = imaNegocio.listar(item);
            %>
            <div class="col-md-4 mb-4">
                <div class="card" style="width: 18rem;">
                    <div>
                        <div id="carouselExample<%=j%>" class="carousel slide">
                            <div class="carousel-inner">
                                <% int i = 0; foreach (dominio.Imagen itemima in listaimagenes)
                                    { %>
                                <div class="carousel-item <%= i == 0 ? "active" : "" %>">
                                    <img src="<%=itemima.url %>" class="d-block w-100" alt="...">
                                </div>
                                <% i++;
                                    } %>
                            </div>
                            <button class="carousel-control-prev" type="button" data-bs-target="#carouselExample<%=j%>" data-bs-slide="prev">
                                <span class="carousel-control-prev-icon bg-dark" aria-hidden="true"></span>
                                <span class="visually-hidden ">Previous</span>
                            </button>
                            <button class="carousel-control-next " type="button" data-bs-target="#carouselExample<%=j%>" data-bs-slide="next">
                                <span class="carousel-control-next-icon bg-dark" aria-hidden="true"></span>
                                <span class="visually-hidden">Next</span>
                            </button>
                        </div>
                    </div>
                    <div class="card-body">
                        <h5 class="card-title"><%=item.nombre %></h5>
                        <p class="card-text"><%=item.descripcion %></p>
                        <a href="DetalleArticulo.aspx?id=<%=item.id %>" class="btn btn-secondary">Ver detalle</a>
                        <button class="btn btn-primary" id="btnAgregar">Agregar al carrito</button>
                    </div>
                </div>
            </div>
            <% j++;
            } %>
        </div>

    </div>

</asp:Content>
