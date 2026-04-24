using MODELO;
using NEGOCIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRESENTACION
{
    public partial class ServiciosForm : Form
    {
        ServiciosNegocio serviciosNegocio = new ServiciosNegocio();
        public ServiciosForm()
        {
            InitializeComponent();
        }
        /*  -----   CONSULTAR   -----   */
        // Cambia el titulo a las columnas
        private void CambiarTitulo(string nombreColumna, string titulo)
        {
            if (dgvServicios.Columns.Contains(nombreColumna))
            {
                dgvServicios.Columns[nombreColumna].HeaderText = titulo;
            }
        }
        private void MtdRenombrarNombreColumna()
        {
            CambiarTitulo("CodigoServicio", "Código");
            CambiarTitulo("UnidadMedida", "Unidad Medida");
            CambiarTitulo("PrecioUnitario", "Precio Unitario");
            CambiarTitulo("TiempoPreparacion", "Tiempo Preparacion");
        }
        // Contar cantidad de lineas del DataGridView
        private void MtdContarTotalRegistros()
        {
            int totalRegistros = dgvServicios.Rows.Count;
            lblTotalRegistros.Text = "Total de registros: " + totalRegistros.ToString();
        }
        // Consultar datos de tabla Servicios y mostrar en DataGridView
        private void MtdConsultarServicios()
        {
            try
            {
                dgvServicios.DataSource = serviciosNegocio.MtdConsultar();
                dgvServicios.ClearSelection();
                dgvServicios.CurrentCell = null;

                filaActiva = null;

                MtdContarTotalRegistros();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al consultar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Convertir valores de las columnas Genero y Estado 
        private void dgvServicios_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvServicios.Columns[e.ColumnIndex].Name == "Genero" && e.Value != null)
            {
                switch ((char)e.Value)
                {
                    case 'M': e.Value = "Masculino"; break;
                    case 'F': e.Value = "Femenino"; break;
                    case 'O': e.Value = "Otro"; break;
                    default: e.Value = ""; break;
                }
                e.FormattingApplied = true;
            }
        }
        // Controla el estados para el boton Nuevo
        private void MtdEstadoBotonNuevo()
        {
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = false;
            btnNuevo.Enabled = true;
            btnGuardar.Enabled = false;
            txtCodigo.Enabled = false;
            txtNombre.Enabled = true;
            cboxTipo.Enabled = true;
            cboxCategoria.Enabled = true;
            cboxUnidadMedida.Enabled = true;
            nudPrecio.Enabled = true;
            txtTiempo.Enabled = true;
            /*--Estado: Activo o Inactivo--*/
            rdbActivo.Enabled = true;
            rdbInactivo.Enabled = true;
        }
        private void MtdEstadoControles(bool editable)
        {

            btnGuardar.Enabled = editable;
            btnCancelar.Enabled = editable;
            btnNuevo.Enabled = !editable;
            btnEditar.Enabled = !editable;
            btnEliminar.Enabled = !editable;
            txtNombre.Enabled = editable;
            cboxTipo.Enabled = editable;
            cboxCategoria.Enabled = editable;
            cboxUnidadMedida.Enabled = editable;
            nudPrecio.Enabled = editable;
            txtTiempo.Enabled = editable;
            /*--Estado: Activo o Inactivo--*/
            rdbActivo.Enabled = editable;
            rdbInactivo.Enabled = editable;

            txtCodigo.Enabled = false;
        }
        // Limpia Formulario
        private void MtdLimpiarControlesForm()
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            cboxTipo.SelectedIndex = -1;
            cboxCategoria.SelectedIndex = -1;
            cboxUnidadMedida.SelectedIndex = -1;
            nudPrecio.Value = 0;
            txtTiempo.Clear();
            /*--Estado: Activo o Inactivo--*/
            rdbActivo.Checked = false;
            rdbInactivo.Checked = false;

            dgvServicios.ClearSelection();
            dgvServicios.CurrentCell = null;

            foreach (DataGridViewRow row in dgvServicios.Rows)
            {
                row.Cells["Seleccionar"].Value = false;
                row.DefaultCellStyle.BackColor = Color.White;
            }
        }
        private void MtdEstadoFilaSelecionada(bool Estado)
        {
            btnEditar.Enabled = Estado;
            btnEliminar.Enabled = Estado;
            btnCancelar.Enabled = Estado;
            btnGuardar.Enabled = false;
            btnNuevo.Enabled = !Estado;
            txtCodigo.Enabled = false;
            txtNombre.Enabled = true;
            cboxTipo.Enabled = true;
            cboxCategoria.Enabled = true;
            cboxUnidadMedida.Enabled = true;
            nudPrecio.Enabled = true;
            txtTiempo.Enabled = true;
            /*--Estado: Activo o Inactivo--*/
            rdbActivo.Enabled = true;
            rdbInactivo.Enabled = true;
        }
        //Cargar datos de la fila selecciohnada en los controles del Form
        private void MtdCargarDatosFilaEnControlesForm(int filaSeleccionada)
        {
            if (filaSeleccionada >= 0 && filaSeleccionada < dgvServicios.Rows.Count)
            {
                DataGridViewRow fila = dgvServicios.Rows[filaSeleccionada];
                txtCodigo.Text = fila.Cells["CodigoServicio"].Value?.ToString();
                txtNombre.Text = fila.Cells["Nombre"].Value?.ToString();
                cboxTipo.SelectedItem = fila.Cells["Tipo"].Value?.ToString();
                cboxCategoria.SelectedItem = fila.Cells["Categoria"].Value?.ToString();
                cboxUnidadMedida.SelectedItem = fila.Cells["UnidadMedida"].Value?.ToString();
                nudPrecio.Value = Convert.ToDecimal(fila.Cells["PrecioUnitario"].Value);
                txtTiempo.Text = fila.Cells["TiempoPreparacion"].Value?.ToString();
                bool estadoServicio = Convert.ToBoolean(fila.Cells["Estado"].Value);
                rdbActivo.Checked = estadoServicio;
                rdbInactivo.Checked = !estadoServicio;
                MtdEstadoFilaSelecionada(true);
            }
        }
        //Activar fila seleccionada
        private int? filaActiva = null;
        private void MtdActivarFilaSeleccionada(int filaSeleccionada)
        {
            try
            {
                if (filaActiva.HasValue && filaActiva.Value < dgvServicios.Rows.Count)
                {
                    dgvServicios.Rows[filaActiva.Value].Cells["Seleccionar"].Value = false;
                    dgvServicios.Rows[filaActiva.Value].DefaultCellStyle.BackColor = Color.White;
                }
                if (filaSeleccionada >= 0 && filaSeleccionada < dgvServicios.Rows.Count)
                {
                    filaActiva = filaSeleccionada;
                    dgvServicios.Rows[filaSeleccionada].Cells["Seleccionar"].Value = true;
                    dgvServicios.Rows[filaSeleccionada].DefaultCellStyle.BackColor = Color.FromArgb(220, 235, 255);

                    MtdCargarDatosFilaEnControlesForm(filaSeleccionada);
                }
            }
            catch (Exception)
            {
                filaActiva = null;
            }
        }
        //Desactivar fila seleccionada
        private void MtdDesactivaFilaSeleccionada()
        {
            if (filaActiva.HasValue)
            {
                dgvServicios.Rows[filaActiva.Value].Cells["Seleccionar"].Value = false;
                dgvServicios.Rows[filaActiva.Value].DefaultCellStyle.BackColor = Color.White;
            }

            filaActiva = null;

            MtdLimpiarControlesForm();
            MtdEstadoFilaSelecionada(false);
        }
        //Metodo validar datos
        private bool MtdValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(cboxTipo.Text) ||
                string.IsNullOrWhiteSpace(cboxCategoria.Text) ||
                string.IsNullOrWhiteSpace(cboxUnidadMedida.Text) ||
                nudPrecio.Value <= 0 ||
                string.IsNullOrWhiteSpace(txtTiempo.Text) ||
                (!rdbActivo.Checked && !rdbInactivo.Checked))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void ServiciosForm_Load(object sender, EventArgs e)
        {
            MtdConsultarServicios(); // Carga los datos de la DB
            MtdRenombrarNombreColumna(); // Pone nombres bonitos a las columnas
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvServicios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvServicios.EndEdit();
            if (e.RowIndex < 0)
                return;

            if (dgvServicios.Columns[e.ColumnIndex].Name != "Seleccionar")
                return;

            if (!chkSeleccionar.Checked)
            {
                dgvServicios.Rows[e.RowIndex].Cells["Seleccionar"].Value = false;
                return;
            }

            bool seleccionActual = Convert.ToBoolean(
                dgvServicios.Rows[e.RowIndex].Cells["Seleccionar"].Value ?? false);

            if (seleccionActual)
                MtdDesactivaFilaSeleccionada();
            else
                MtdActivarFilaSeleccionada(e.RowIndex);
            if (e.RowIndex >= 0 && dgvServicios.Rows.Count > 0)
            {
                MtdActivarFilaSeleccionada(e.RowIndex);
                MtdEstadoFilaSelecionada(true);
            }
        }

        /* ---- BUSCAR SERVICIO ---- */
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string criterio = txtBuscarNombre.Text.Trim();

                dgvServicios.DataSource = serviciosNegocio.MtdBuscarServicio(criterio);

                dgvServicios.ClearSelection();
                dgvServicios.CurrentCell = null;

                filaActiva = null; 

                MtdContarTotalRegistros();

                MtdEstadoFilaSelecionada(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscarNombre.Clear();
            MtdConsultarServicios();

            MtdLimpiarControlesForm();
            MtdEstadoFilaSelecionada(false);
            MtdContarTotalRegistros();
        }

        private void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            dgvServicios.Columns["Seleccionar"].ReadOnly = !chkSeleccionar.Checked;
            btnImprimir.Enabled = chkSeleccionar.Checked;

            MtdDesactivaFilaSeleccionada();
            MtdEstadoControles(true);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            MtdLimpiarControlesForm();
            MtdEstadoFilaSelecionada(false);
            MtdEstadoControles(true);
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            btnNuevo.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            MtdLimpiarControlesForm();
            MtdEstadoFilaSelecionada(false);
        }

        // Boton Guardar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (MtdValidarDatos() == false)
                return;

            try
            {
                ServiciosEntidad servicio = new ServiciosEntidad
                {
                    Nombre = txtNombre.Text.Trim(),
                    Tipo = cboxTipo.Text.Trim(),
                    Categoria = cboxCategoria.Text.Trim(),
                    UnidadMedida = cboxUnidadMedida.Text.Trim(),
                    PrecioUnitario = nudPrecio.Value,
                    TiempoPreparacion = Convert.ToDecimal(txtTiempo.Text),
                    Estado = rdbActivo.Checked
                };

                string mensaje = serviciosNegocio.MtdAgregarServicio(servicio);

                MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MtdLimpiarControlesForm();
                MtdConsultarServicios();

                MtdEstadoFilaSelecionada(false);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Boton Editar
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("Por favor, seleccione un servicio para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MtdValidarDatos() == false)
                return;

            try
            {
                ServiciosEntidad servicio = new ServiciosEntidad
                {
                    CodigoServicio = Convert.ToInt32(txtCodigo.Text),
                    Nombre = txtNombre.Text.Trim(),
                    Tipo = cboxTipo.Text.Trim(),
                    Categoria = cboxCategoria.Text.Trim(),
                    UnidadMedida = cboxUnidadMedida.Text.Trim(),
                    PrecioUnitario = nudPrecio.Value,
                    TiempoPreparacion = Convert.ToDecimal(txtTiempo.Text),
                    Estado = rdbActivo.Checked
                };
                string mensaje = serviciosNegocio.MtdEditarServicio(servicio);

                MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MtdLimpiarControlesForm();
                MtdConsultarServicios();

                MtdEstadoFilaSelecionada(false);

                btnGuardar.Enabled = true;
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
                btnNuevo.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /* -----   ELIMINAR SERVICIO   -----   */
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCodigo.Text))
                    throw new Exception("Debe seleccionar un servicio");

                int codigoServicio = Convert.ToInt32(txtCodigo.Text);

                DialogResult confirmacion = MessageBox.Show("¿Está seguro de eliminar este servicio?", "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacion == DialogResult.No)
                    return;

                string mensaje = serviciosNegocio.MtdEliminarServicio(codigoServicio);

                MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                filaActiva = null;

                MtdLimpiarControlesForm();
                MtdConsultarServicios();
                MtdEstadoFilaSelecionada(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
