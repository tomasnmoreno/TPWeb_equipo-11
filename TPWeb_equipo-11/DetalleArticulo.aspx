<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DetalleArticulo.aspx.cs" Inherits="TPWeb_equipo_11.DetalleArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            <p>ACA VA EL CAROUSEL</p>
        </div>

        <div class="col-6">

            <div class="row mb-3">
                <label for="txtCodigo" class="col-sm-2 col-form-label">Codigo</label>
                <div class="col-sm-10">
                    <asp:TextBox CssClass="col-sm-2" ID="txtCodigo" runat="server" ReadOnly="true"/>
                    <%--<input type="email" class="form-control" id="colFormLabel" placeholder="col-form-label">--%>
                </div>
            </div>
            <div class="row mb-3">
                <label for="txtNombre" class="col-sm-2 col-form-label">Nombre</label>
                <div class="col-sm-10">
                    <asp:TextBox CssClass="col-sm-2 " ID="txtNombre" runat="server" ReadOnly="true"/>
                </div>
            </div>

            <div class="row mb-3">
                <label for="txtDescripcion" class="col-sm-2 col-form-label">Descripcion</label>
                <div class="col-sm-10">
                    <asp:TextBox CssClass="col-sm-2 " ID="txtDescripcion" runat="server" ReadOnly="true"/>
                </div>
            </div>

            <div class="row mb-3">
                <label for="txtMarca" class="col-sm-2 col-form-label">Marca</label>
                <div class="col-sm-10">
                    <asp:TextBox CssClass="col-sm-2 " ID="txtMarca" runat="server" ReadOnly="true"/>
                </div>
            </div>

            <div class="row mb-3">
                <label for="txtCategoria" class="col-sm-2 col-form-label">Categoria</label>
                <div class="col-sm-10">
                    <asp:TextBox CssClass="col-sm-2 " ID="txtCategoria" runat="server" ReadOnly="true"/>
                </div>
            </div>

            <div class="row mb-3">
                <label for="txtPrecio" class="col-sm-2 col-form-label">Precio</label>
                <div class="col-sm-10">
                    <asp:TextBox CssClass="col-sm-2 " ID="txtPrecio" runat="server" ReadOnly="true"/>
                </div>
            </div>

        </div>

    </div>

</asp:Content>
