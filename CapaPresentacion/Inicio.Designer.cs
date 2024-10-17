namespace CapaPresentacion
{
    partial class Inicio
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtTitulo = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.contenedor = new System.Windows.Forms.Panel();
            this.iconMenuItem10 = new FontAwesome.Sharp.IconMenuItem();
            this.menuUsuarios = new FontAwesome.Sharp.IconMenuItem();
            this.menuClientes = new FontAwesome.Sharp.IconMenuItem();
            this.menuProveedores = new FontAwesome.Sharp.IconMenuItem();
            this.menuProducto = new FontAwesome.Sharp.IconMenuItem();
            this.iconMenuItem3 = new FontAwesome.Sharp.IconMenuItem();
            this.menuCompras = new FontAwesome.Sharp.IconMenuItem();
            this.menuDetalleCompra = new FontAwesome.Sharp.IconMenuItem();
            this.menuReporteCompras = new FontAwesome.Sharp.IconMenuItem();
            this.iconMenuItem5 = new FontAwesome.Sharp.IconMenuItem();
            this.menuVentas = new FontAwesome.Sharp.IconMenuItem();
            this.menuDetalleVenta = new FontAwesome.Sharp.IconMenuItem();
            this.menuReporteVentas = new FontAwesome.Sharp.IconMenuItem();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkSlateGray;
            this.panel2.Controls.Add(this.txtTitulo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 88);
            this.panel2.TabIndex = 3;
            // 
            // txtTitulo
            // 
            this.txtTitulo.AutoSize = true;
            this.txtTitulo.BackColor = System.Drawing.Color.Transparent;
            this.txtTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitulo.ForeColor = System.Drawing.Color.White;
            this.txtTitulo.Location = new System.Drawing.Point(83, 31);
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new System.Drawing.Size(101, 29);
            this.txtTitulo.TabIndex = 5;
            this.txtTitulo.Text = "txtTitulo";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.panel1.Controls.Add(this.lblUsuario);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1442, 88);
            this.panel1.TabIndex = 2;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.BackColor = System.Drawing.Color.Transparent;
            this.lblUsuario.ForeColor = System.Drawing.Color.White;
            this.lblUsuario.Location = new System.Drawing.Point(309, 31);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(68, 16);
            this.lblUsuario.TabIndex = 4;
            this.lblUsuario.Text = "lblUsuario";
            this.lblUsuario.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(1230, 28);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 38);
            this.panel3.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(55, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 29);
            this.label3.TabIndex = 6;
            this.label3.Text = "MENU";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(226, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "USUARIO";
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.Color.DarkSlateGray;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iconMenuItem10,
            this.menuUsuarios,
            this.menuClientes,
            this.menuProveedores,
            this.menuProducto,
            this.iconMenuItem3,
            this.menuCompras,
            this.menuDetalleCompra,
            this.menuReporteCompras,
            this.iconMenuItem5,
            this.menuVentas,
            this.menuDetalleVenta,
            this.menuReporteVentas});
            this.menuStrip1.Location = new System.Drawing.Point(0, 88);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(200, 790);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // contenedor
            // 
            this.contenedor.BackColor = System.Drawing.Color.DarkSlateGray;
            this.contenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contenedor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.contenedor.Location = new System.Drawing.Point(200, 88);
            this.contenedor.Name = "contenedor";
            this.contenedor.Size = new System.Drawing.Size(1242, 790);
            this.contenedor.TabIndex = 5;
            // 
            // iconMenuItem10
            // 
            this.iconMenuItem10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.iconMenuItem10.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.iconMenuItem10.ForeColor = System.Drawing.Color.White;
            this.iconMenuItem10.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconMenuItem10.IconColor = System.Drawing.Color.Black;
            this.iconMenuItem10.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItem10.IconSize = 50;
            this.iconMenuItem10.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.iconMenuItem10.Name = "iconMenuItem10";
            this.iconMenuItem10.Size = new System.Drawing.Size(191, 54);
            this.iconMenuItem10.Text = "MENU";
            this.iconMenuItem10.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // menuUsuarios
            // 
            this.menuUsuarios.ForeColor = System.Drawing.Color.White;
            this.menuUsuarios.IconChar = FontAwesome.Sharp.IconChar.UserGroup;
            this.menuUsuarios.IconColor = System.Drawing.Color.Black;
            this.menuUsuarios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuUsuarios.IconSize = 50;
            this.menuUsuarios.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuUsuarios.Name = "menuUsuarios";
            this.menuUsuarios.Size = new System.Drawing.Size(191, 54);
            this.menuUsuarios.Text = "Usuarios";
            this.menuUsuarios.Click += new System.EventHandler(this.menuUsuarios_Click);
            // 
            // menuClientes
            // 
            this.menuClientes.ForeColor = System.Drawing.Color.White;
            this.menuClientes.IconChar = FontAwesome.Sharp.IconChar.Users;
            this.menuClientes.IconColor = System.Drawing.Color.Black;
            this.menuClientes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuClientes.IconSize = 50;
            this.menuClientes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuClientes.Name = "menuClientes";
            this.menuClientes.Size = new System.Drawing.Size(191, 54);
            this.menuClientes.Text = "Clientes";
            this.menuClientes.Click += new System.EventHandler(this.menuClientes_Click);
            // 
            // menuProveedores
            // 
            this.menuProveedores.ForeColor = System.Drawing.Color.White;
            this.menuProveedores.IconChar = FontAwesome.Sharp.IconChar.HandshakeSimple;
            this.menuProveedores.IconColor = System.Drawing.Color.Black;
            this.menuProveedores.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuProveedores.IconSize = 50;
            this.menuProveedores.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuProveedores.Name = "menuProveedores";
            this.menuProveedores.Size = new System.Drawing.Size(191, 54);
            this.menuProveedores.Text = "Proveedores";
            this.menuProveedores.Click += new System.EventHandler(this.menuProveedores_Click);
            // 
            // menuProducto
            // 
            this.menuProducto.ForeColor = System.Drawing.Color.White;
            this.menuProducto.IconChar = FontAwesome.Sharp.IconChar.BoxesStacked;
            this.menuProducto.IconColor = System.Drawing.Color.Black;
            this.menuProducto.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuProducto.IconSize = 50;
            this.menuProducto.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuProducto.Name = "menuProducto";
            this.menuProducto.Size = new System.Drawing.Size(191, 54);
            this.menuProducto.Text = "Producto";
            this.menuProducto.Click += new System.EventHandler(this.menuProducto_Click);
            // 
            // iconMenuItem3
            // 
            this.iconMenuItem3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.iconMenuItem3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.iconMenuItem3.ForeColor = System.Drawing.Color.White;
            this.iconMenuItem3.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconMenuItem3.IconColor = System.Drawing.Color.Black;
            this.iconMenuItem3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItem3.IconSize = 50;
            this.iconMenuItem3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.iconMenuItem3.Name = "iconMenuItem3";
            this.iconMenuItem3.Size = new System.Drawing.Size(191, 54);
            this.iconMenuItem3.Text = "COMPRAS";
            this.iconMenuItem3.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // menuCompras
            // 
            this.menuCompras.ForeColor = System.Drawing.Color.White;
            this.menuCompras.IconChar = FontAwesome.Sharp.IconChar.CartShopping;
            this.menuCompras.IconColor = System.Drawing.Color.Black;
            this.menuCompras.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuCompras.IconSize = 50;
            this.menuCompras.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuCompras.Name = "menuCompras";
            this.menuCompras.Size = new System.Drawing.Size(191, 54);
            this.menuCompras.Text = "Compras";
            this.menuCompras.Click += new System.EventHandler(this.menuCompras_Click);
            // 
            // menuDetalleCompra
            // 
            this.menuDetalleCompra.ForeColor = System.Drawing.Color.White;
            this.menuDetalleCompra.IconChar = FontAwesome.Sharp.IconChar.Paste;
            this.menuDetalleCompra.IconColor = System.Drawing.Color.Black;
            this.menuDetalleCompra.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuDetalleCompra.IconSize = 50;
            this.menuDetalleCompra.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuDetalleCompra.Name = "menuDetalleCompra";
            this.menuDetalleCompra.Size = new System.Drawing.Size(191, 54);
            this.menuDetalleCompra.Text = "Detalle Compra";
            this.menuDetalleCompra.Click += new System.EventHandler(this.menuDetalleCompra_Click);
            // 
            // menuReporteCompras
            // 
            this.menuReporteCompras.ForeColor = System.Drawing.Color.White;
            this.menuReporteCompras.IconChar = FontAwesome.Sharp.IconChar.ChartColumn;
            this.menuReporteCompras.IconColor = System.Drawing.Color.Black;
            this.menuReporteCompras.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuReporteCompras.IconSize = 50;
            this.menuReporteCompras.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuReporteCompras.Name = "menuReporteCompras";
            this.menuReporteCompras.Size = new System.Drawing.Size(191, 54);
            this.menuReporteCompras.Text = "Reporte Compras";
            this.menuReporteCompras.Click += new System.EventHandler(this.menuReporteCompras_Click);
            // 
            // iconMenuItem5
            // 
            this.iconMenuItem5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.iconMenuItem5.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.iconMenuItem5.ForeColor = System.Drawing.Color.White;
            this.iconMenuItem5.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconMenuItem5.IconColor = System.Drawing.Color.Black;
            this.iconMenuItem5.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItem5.IconSize = 50;
            this.iconMenuItem5.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.iconMenuItem5.Name = "iconMenuItem5";
            this.iconMenuItem5.Size = new System.Drawing.Size(191, 54);
            this.iconMenuItem5.Text = "VENTAS";
            this.iconMenuItem5.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // menuVentas
            // 
            this.menuVentas.ForeColor = System.Drawing.Color.White;
            this.menuVentas.IconChar = FontAwesome.Sharp.IconChar.Tags;
            this.menuVentas.IconColor = System.Drawing.Color.Black;
            this.menuVentas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuVentas.IconSize = 50;
            this.menuVentas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuVentas.Name = "menuVentas";
            this.menuVentas.Size = new System.Drawing.Size(191, 54);
            this.menuVentas.Text = "Venta";
            this.menuVentas.Click += new System.EventHandler(this.menuVentas_Click);
            // 
            // menuDetalleVenta
            // 
            this.menuDetalleVenta.ForeColor = System.Drawing.Color.White;
            this.menuDetalleVenta.IconChar = FontAwesome.Sharp.IconChar.ThList;
            this.menuDetalleVenta.IconColor = System.Drawing.Color.Black;
            this.menuDetalleVenta.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuDetalleVenta.IconSize = 50;
            this.menuDetalleVenta.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuDetalleVenta.Name = "menuDetalleVenta";
            this.menuDetalleVenta.Size = new System.Drawing.Size(191, 54);
            this.menuDetalleVenta.Text = "Detalle Venta";
            this.menuDetalleVenta.Click += new System.EventHandler(this.menuDetalleVenta_Click);
            // 
            // menuReporteVentas
            // 
            this.menuReporteVentas.ForeColor = System.Drawing.Color.White;
            this.menuReporteVentas.IconChar = FontAwesome.Sharp.IconChar.ChartColumn;
            this.menuReporteVentas.IconColor = System.Drawing.Color.Black;
            this.menuReporteVentas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuReporteVentas.IconSize = 50;
            this.menuReporteVentas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuReporteVentas.Name = "menuReporteVentas";
            this.menuReporteVentas.Size = new System.Drawing.Size(191, 54);
            this.menuReporteVentas.Text = "Reporte Ventas";
            this.menuReporteVentas.Click += new System.EventHandler(this.menuReporteVentas_Click);
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1442, 878);
            this.Controls.Add(this.contenedor);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "Inicio";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Inicio_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label txtTitulo;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private FontAwesome.Sharp.IconMenuItem menuUsuarios;
        private FontAwesome.Sharp.IconMenuItem menuClientes;
        private FontAwesome.Sharp.IconMenuItem menuProveedores;
        private FontAwesome.Sharp.IconMenuItem menuProducto;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem3;
        private FontAwesome.Sharp.IconMenuItem menuCompras;
        private FontAwesome.Sharp.IconMenuItem menuDetalleCompra;
        private FontAwesome.Sharp.IconMenuItem menuReporteCompras;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem5;
        private FontAwesome.Sharp.IconMenuItem menuVentas;
        private FontAwesome.Sharp.IconMenuItem menuDetalleVenta;
        private FontAwesome.Sharp.IconMenuItem menuReporteVentas;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem10;
        private System.Windows.Forms.Panel contenedor;
    }
}

