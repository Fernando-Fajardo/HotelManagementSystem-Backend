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
    public partial class HabitacionesForm : Form
    {
        HabitacionesNegocio habitacionNegocio = new HabitacionesNegocio();
        
        public HabitacionesForm()
        {
            InitializeComponent();
        }
        /*  -----   CONSULTAR   -----   */

        // Cambia el titulo a las columnas 
        private void CambiarTitulo(string nombreColumna, string titulo)
        {
            if (dgvHabitaciones.Columns.Contains(nombreColumna))
            {
                dgvHabitaciones.Columns[nombreColumna].HeaderText = titulo;
            }
        }
        private void MtdRenombrarNombreColumna()
        {
            CambiarTitulo("CodigoHabitacion", "Código");
            CambiarTitulo("CantidadHuesped", "Cantidad Huesped");
            CambiarTitulo("PorcentajeAnticipo", "Porcentaje Anticipo");
        }
        // Contar cantidad de lineas del DataGridView  
        private void MtdContarTotalRegistros()
        {
            int totalRegistros = dgvHabitaciones.Rows.Count;
            lblTotalRegistros.Text = "Total de registros: " + totalRegistros.ToString();
        }
        // Consultar datos de tabla Habitaciones y mostrar en DataGridView
        private void MtdConsultarHabitaciones()
        {
            try
            {
                dgvHabitaciones.DataSource = habitacionNegocio.MtdConsultar();
                dgvHabitaciones.ClearSelection();
                dgvHabitaciones.CurrentCell = null;

                filaActiva = null;

                MtdContarTotalRegistros();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al consultar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        // Convertir valores de las columnas Genero y Estado 
        private void dgvHabitaciones_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvHabitaciones.Columns[e.ColumnIndex].Name == "Genero" && e.Value != null)
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
            nudNumero.Enabled = true;
            cboxUbicacion.Enabled = true;
            cboxTipo.Enabled = true;
            nudCantidadHuespedes.Enabled = true;
            nudPrecio.Enabled = true;
            nudAnticipo.Enabled = true;
            /*--Estado: Activo o Inactivo--*/
            rdbActivo.Enabled = true;
            rdbInactivo.Enabled = true;
            /*--Pet Friendly--*/
            rdbSi.Enabled = true;
            rdbNo.Enabled = true;
        }
        private void MtdEstadoControles(bool editable)
        {

            btnGuardar.Enabled = editable;
            btnCancelar.Enabled = editable;

            btnNuevo.Enabled = !editable;
            btnEditar.Enabled = !editable;
            btnEliminar.Enabled = !editable;

            nudNumero.Enabled = editable;
            cboxUbicacion.Enabled = editable;
            cboxTipo.Enabled = editable;
            nudCantidadHuespedes.Enabled = editable;
            nudPrecio.Enabled = editable;
            nudAnticipo.Enabled = editable;
            /*--Estado: Activo o Inactivo--*/
            rdbActivo.Enabled = editable;
            rdbInactivo.Enabled = editable;
            /*--Pet Friendly--*/
            rdbSi.Enabled = editable;
            rdbNo.Enabled = editable;

            txtCodigo.Enabled = false;
        }
        // Limpia Formulario
        private void MtdLimpiarControlesForm()
        {
            txtCodigo.Clear();
            nudNumero.Value = 0;
            cboxUbicacion.SelectedIndex = -1;
            cboxTipo.SelectedIndex = -1;
            nudCantidadHuespedes.Value = 0;
            nudPrecio.Value = 0;
            nudAnticipo.Value = 0;

            // Limpiar RadioButtons de Pet Friendly
            rdbSi.Checked = false;
            rdbNo.Checked = false;

            // Limpiar RadioButtons de Estado
            rdbActivo.Checked = false;
            rdbInactivo.Checked = false;
            
            dgvHabitaciones.ClearSelection();
            dgvHabitaciones.CurrentCell = null;

            foreach (DataGridViewRow row in dgvHabitaciones.Rows)
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
            nudNumero.Enabled = Estado;
            cboxUbicacion.Enabled = Estado;
            cboxTipo.Enabled = Estado;
            nudCantidadHuespedes.Enabled = Estado;
            nudPrecio.Enabled = Estado;
            nudAnticipo.Enabled = Estado;

            rdbActivo.Enabled = Estado;
            rdbInactivo.Enabled = Estado;
            rdbSi.Enabled = Estado;
            rdbNo.Enabled = Estado;
        }
        //Cargar datos de la fila selecciohnada en los controles del Form
        private void MtdCargarDatosFilaEnControlesForm(int filaSeleccionada)
        {
            if (filaSeleccionada >= 0 && filaSeleccionada < dgvHabitaciones.Rows.Count)
            {
                DataGridViewRow fila = dgvHabitaciones.Rows[filaSeleccionada];
                txtCodigo.Text = fila.Cells["CodigoHabitacion"].Value?.ToString();
                nudNumero.Value = Convert.ToDecimal(fila.Cells["Numero"].Value);
                cboxUbicacion.Text = fila.Cells["Ubicacion"].Value?.ToString();
                cboxTipo.Text = fila.Cells["Tipo"].Value?.ToString();
                nudCantidadHuespedes.Value = Convert.ToDecimal(fila.Cells["CantidadHuesped"].Value);
                nudPrecio.Value = Convert.ToDecimal(fila.Cells["Precio"].Value);
                nudAnticipo.Value = Convert.ToDecimal(fila.Cells["PorcentajeAnticipo"].Value);
                
                bool estadoHabitacion = Convert.ToBoolean(fila.Cells["Estado"].Value);
                rdbActivo.Checked = estadoHabitacion;
                rdbInactivo.Checked = !estadoHabitacion;
                bool estadoPet = Convert.ToBoolean(fila.Cells["PetFriendly"].Value);
                rdbSi.Checked = estadoPet;
                rdbNo.Checked = !estadoPet;
                MtdEstadoFilaSelecionada(true);
            }
        }
        //Activar fila seleccionada
        private int? filaActiva = null;
        private void MtdActivarFilaSeleccionada(int filaSeleccionada)
        {
            try
            {
                if (filaActiva.HasValue && filaActiva.Value < dgvHabitaciones.Rows.Count)
                {
                    dgvHabitaciones.Rows[filaActiva.Value].Cells["Seleccionar"].Value = false;
                    dgvHabitaciones.Rows[filaActiva.Value].DefaultCellStyle.BackColor = Color.White;
                }
                if (filaSeleccionada >= 0 && filaSeleccionada < dgvHabitaciones.Rows.Count)
                {
                    filaActiva = filaSeleccionada;
                    dgvHabitaciones.Rows[filaSeleccionada].Cells["Seleccionar"].Value = true;
                    dgvHabitaciones.Rows[filaSeleccionada].DefaultCellStyle.BackColor = Color.FromArgb(220, 235, 255);

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
                dgvHabitaciones.Rows[filaActiva.Value].Cells["Seleccionar"].Value = false;
                dgvHabitaciones.Rows[filaActiva.Value].DefaultCellStyle.BackColor = Color.White;
            }

            filaActiva = null;

            MtdLimpiarControlesForm();
            MtdEstadoFilaSelecionada(false);
        }
        //Metodo validar datos
        private bool MtdValidarDatos()
        {
            if (
                nudNumero.Value <= 0 ||
                string.IsNullOrWhiteSpace(cboxUbicacion.Text) ||
                string.IsNullOrWhiteSpace(cboxTipo.Text) ||
                nudCantidadHuespedes.Value <= 0 ||
                nudPrecio.Value <= 0 ||
                nudAnticipo.Value < 0)
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void HabitacionesForm_Load(object sender, EventArgs e)
        {
            MtdConsultarHabitaciones(); // Carga los datos de la DB
            MtdRenombrarNombreColumna(); // Pone nombres bonitos a las columnas
            MtdEstadoBotonNuevo();
        }

        private void dgvHabitaciones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvHabitaciones.EndEdit();
            if (e.RowIndex < 0)
                return;

            if (dgvHabitaciones.Columns[e.ColumnIndex].Name != "Seleccionar")
                return;

            if (!chkSeleccionar.Checked)
            {
                dgvHabitaciones.Rows[e.RowIndex].Cells["Seleccionar"].Value = false;
                return;
            }

            bool seleccionActual = Convert.ToBoolean(
                dgvHabitaciones.Rows[e.RowIndex].Cells["Seleccionar"].Value ?? false);

            if (seleccionActual)
                MtdDesactivaFilaSeleccionada();
            else
                MtdActivarFilaSeleccionada(e.RowIndex);
            if (e.RowIndex >= 0 && dgvHabitaciones.Rows.Count > 0)
            {
                MtdActivarFilaSeleccionada(e.RowIndex);
                MtdEstadoFilaSelecionada(true);
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void rdbSi_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdbNo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string numero = txtBuscarNumero.Text.Trim();

                dgvHabitaciones.DataSource = habitacionNegocio.MtdBuscarHabitacion(numero);

                dgvHabitaciones.ClearSelection();
                dgvHabitaciones.CurrentCell = null;

                filaActiva = null;

                MtdContarTotalRegistros();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscarNumero.Clear();
            MtdConsultarHabitaciones();

            MtdLimpiarControlesForm();
            MtdEstadoFilaSelecionada(false);
            MtdContarTotalRegistros();
        }

        private void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            dgvHabitaciones.Columns["Seleccionar"].ReadOnly = !chkSeleccionar.Checked;
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
            MtdEstadoControles(false);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (MtdValidarDatos() == false)
                return;

            try
            {
                HabitacionesEntidad habitaciones = new HabitacionesEntidad
                {
                    Numero = nudNumero.Value.ToString(),
                    Ubicacion = cboxUbicacion.Text.Trim(),
                    Tipo = cboxTipo.Text.Trim(),
                    CantidadHuesped = Convert.ToInt32(nudCantidadHuespedes.Value),
                    Precio = nudPrecio.Value,
                    PorcentajeAnticipo = nudAnticipo.Value,
                    PetFriendly = rdbSi.Checked,
                    Estado = rdbActivo.Checked
                };

                string mensaje = habitacionNegocio.MtdAgregarHabitacion(habitaciones);

                MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MtdConsultarHabitaciones();
                MtdLimpiarControlesForm();
                MtdEstadoFilaSelecionada(false); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("Por favor, seleccione una habitación para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MtdValidarDatos() == false)
                return;

            try
            {
                HabitacionesEntidad habitacion = new HabitacionesEntidad
                {
                    CodigoHabitacion = Convert.ToInt32(txtCodigo.Text),
                    Numero = nudNumero.Value.ToString(),
                    Ubicacion = cboxUbicacion.Text.Trim(),
                    Tipo = cboxTipo.Text.Trim(),
                    CantidadHuesped = Convert.ToInt32(nudCantidadHuespedes.Value),
                    Precio = nudPrecio.Value,
                    PorcentajeAnticipo = nudAnticipo.Value,
                    PetFriendly = rdbSi.Checked,
                    Estado = rdbActivo.Checked
                };

                string mensaje = habitacionNegocio.MtdEditarHabitacion(habitacion);

                MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MtdConsultarHabitaciones();
                MtdLimpiarControlesForm();
                MtdDesactivaFilaSeleccionada();

                btnGuardar.Enabled = true;
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
                btnNuevo.Enabled = false;

                if (typeof(Form).GetMethod("MtdEstadoControles") != null) MtdEstadoControles(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /* -----   ELIMINAR HABITACIÓN   -----   */
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCodigo.Text))
                    throw new Exception("Debe seleccionar una habitación");

                int codigoHabitacion = Convert.ToInt32(txtCodigo.Text);

                DialogResult confirmacion = MessageBox.Show("¿Está seguro de eliminar esta habitación?", "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacion == DialogResult.No)
                    return;

                string mensaje = habitacionNegocio.MtdEliminarHabitacion(codigoHabitacion);

                MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                filaActiva = null; 

                MtdLimpiarControlesForm();
                MtdConsultarHabitaciones();
                MtdDesactivaFilaSeleccionada();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
