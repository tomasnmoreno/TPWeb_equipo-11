<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DetalleArticulo.aspx.cs" Inherits="TPWeb_equipo_11.DetalleArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- <link rel="stylesheet" href="resize_img.css"/>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div>
            <h1>Detalle de artículo</h1>
            <hr />
        </div>
    </div>

    <%-- CAROUSEL --%>

    <div class="row">
        <div class="col-6">
            <div id="carouselExample" class="carousel slide">
                <div class="carousel-inner">
                    <% int i = 0; foreach (dominio.Imagen item in listaImagenes)
                        { %>
                    <div class="carousel-item <%= i == 0 ? "active" : "" %>">
                        <img src="<%=item.url %>" class="card-img-top" style="object-fit: cover; object-position: center; width: 500px; height: 500px;" alt="...">
                    </div>
                    <% i++;
                        } %>
                </div>
                <button class="carousel-control-prev" type="button" data-bs-target="#carouselExample" data-bs-slide="prev">
                    <span class="carousel-control-prev-icon bg-dark" aria-hidden="true"></span>
                    <span class="visually-hidden">Previous</span>
                </button>
                <button class="carousel-control-next " type="button" data-bs-target="#carouselExample" data-bs-slide="next">
                    <span class="carousel-control-next-icon bg-dark" aria-hidden="true"></span>
                    <span class="visually-hidden">Next</span>
                </button>
            </div>
        </div>

        <div class="col-6">

            <div class="row mb-3">
                <label for="txtCodigo" class="col-sm-2 col-form-label">Codigo</label>
                <div class="col-sm-10">
                    <asp:TextBox CssClass="col-sm-2" ID="txtCodigo" runat="server" ReadOnly="true" Width="400px" />
                </div>
            </div>
            <div class="row mb-3">
                <label for="txtNombre" class="col-sm-2 col-form-label">Nombre</label>
                <div class="col-sm-10">
                    <asp:TextBox CssClass="col-sm-2 " ID="txtNombre" runat="server" ReadOnly="true" Width="400px" />
                </div>
            </div>

            <div class="row mb-3">
                <label for="txtDescripcion" class="col-sm-2 col-form-label">Descripcion</label>
                <div class="col-sm-10">
                    <asp:TextBox CssClass="col-sm-2 form-control" TextMode="MultiLine" ID="txtDescripcion" runat="server" ReadOnly="true" />
                </div>
            </div>

            <div class="row mb-3">
                <label for="txtMarca" class="col-sm-2 col-form-label">Marca</label>
                <div class="col-sm-10">
                    <asp:TextBox CssClass="col-sm-2 " ID="txtMarca" runat="server" ReadOnly="true" Width="400px" />
                </div>
            </div>

            <div class="row mb-3">
                <label for="txtCategoria" class="col-sm-2 col-form-label">Categoria</label>
                <div class="col-sm-10">
                    <asp:TextBox CssClass="col-sm-2 " ID="txtCategoria" runat="server" ReadOnly="true" Width="400px" />
                </div>
            </div>

            <div class="row mb-3">
                <label for="txtPrecio" class="col-sm-2 col-form-label">Precio</label>
                <div class="col-sm-10">
                    <asp:TextBox CssClass="col-sm-2 " ID="txtPrecio" runat="server" ReadOnly="true" Width="400px" />
                </div>
            </div>


            <div class="col-md-3">
                <label for="validCantidad" class="col sm-2">Cantidad</label>
                <asp:DropDownList runat="server" ID="validCantidad" AutoPostBack="True" OnSelectedIndexChanged="validCantidad_SelectedIndexChanged">
                    <%--OnSelectedIndexChanged=""--%>
                    <asp:ListItem Text="0" />
                    <asp:ListItem Text="1" />
                    <asp:ListItem Text="2" />
                    <asp:ListItem Text="3" />
                    <asp:ListItem Text="4" />
                    <asp:ListItem Text="5" />
                </asp:DropDownList>
            </div>
            
            <hr />

            <asp:Button cssclass="btn btn-primary" Text="Confirmar" ID="btnConfirmar" runat="server" OnClick="btnConfirmar_Click"/>

        </div>
    </div>

</asp:Content>
