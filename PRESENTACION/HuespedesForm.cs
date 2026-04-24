using NEGOCIO;
using MODELO;
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
    public partial class HuespedesForm : Form
    {
        HuespedesNegocio huespedesNegocio = new HuespedesNegocio();
        public HuespedesForm()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        /*  -----   CONSULTAR   -----   */

        // Cambia el titulo a las columnas 
        private void MtdRenombrarNombreColumna()
        {
            CambiarTitulo("CodigoHuesped", "Código");
            CambiarTitulo("TipoIdentificacion", "Código Identificación");
            CambiarTitulo("NumeroIdentificacion", "Número Identificación");
            CambiarTitulo("FechaNacimiento", "Fecha Nacimiento");
        }
        private void CambiarTitulo(string nombreColumna, string titulo)
        {
            if (dgvHuespedes.Columns.Contains(nombreColumna))
            {
                dgvHuespedes.Columns[nombreColumna].HeaderText = titulo;
            }
        }
        // Contar cantidad de lineas del DataGridView 
        private void MtdContarTotalRegistros()
        {
            int totalRegistros = dgvHuespedes.Rows.Count;
            lblTotalRegistros.Text = "Total de registros: " + totalRegistros.ToString();
        }
        // Consultar datos de tabla Huespedes y mostrar en DataGridView 
        private void MtdConsultarHuespedes()
        {
            try
            {
                dgvHuespedes.DataSource = huespedesNegocio.MtdConsultar();
                dgvHuespedes.ClearSelection();
                dgvHuespedes.CurrentCell = null;

                filaActiva = null;

                MtdContarTotalRegistros();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al consultar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Convertir valores de las columnas Genero y Estado 
        private void dgvHuespedes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvHuespedes.Columns[e.ColumnIndex].Name == "Genero" && e.Value != null)
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
        private void HuespedesForm_Load(object sender, EventArgs e)
        {
            MtdConsultarHuespedes(); // Carga los datos de la DB
            MtdRenombrarNombreColumna(); // Pone nombres bonitos a las columnas
            MtdEstadoBotonNuevo();
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
            cboxTipoIdentificacion.Enabled = true;
            nudNumID.Enabled = true;
            dtpFechaNacimiento.Enabled = true;
            cboxGenero.Enabled = true;
            nudTelefono.Enabled = true;
            txtDirección.Enabled = true;
            nudPuntuacion.Enabled = true;
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
            cboxTipoIdentificacion.Enabled = editable;
            nudNumID.Enabled = editable;
            dtpFechaNacimiento.Enabled = editable;
            txtDirección.Enabled = editable;
            cboxGenero.Enabled = editable;
            nudTelefono.Enabled = editable;
            nudPuntuacion.Enabled = editable;
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
            cboxTipoIdentificacion.SelectedIndex = -1;
            nudNumID.Value = 0;
            dtpFechaNacimiento.Value = DateTime.Now;
            txtDirección.Clear();
            cboxGenero.SelectedIndex = -1;
            nudTelefono.Value = 0;
            nudPuntuacion.Value = 0;

            /*--Estado: Activo o Inactivo--*/
            rdbActivo.Checked = false;
            rdbInactivo.Checked = false;

            dgvHuespedes.ClearSelection();
            dgvHuespedes.CurrentCell = null;

            foreach (DataGridViewRow row in dgvHuespedes.Rows)
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
            cboxTipoIdentificacion.Enabled = true;
            nudNumID.Enabled = true;
            dtpFechaNacimiento.Enabled = true;
            txtDirección.Enabled = true;
            cboxGenero.Enabled = true;
            nudTelefono.Enabled = true;
            nudPuntuacion.Enabled = true;
            /*--Estado: Activo o Inactivo--*/
            rdbActivo.Enabled = true;
            rdbInactivo.Enabled = true;
        }
        //Cargar datos de la fila selecciohnada en los controles del Form
        private void MtdCargarDatosFilaEnControlesForm(int filaSeleccionada)
        {
            if (filaSeleccionada >= 0 && filaSeleccionada < dgvHuespedes.Rows.Count)
            {
                DataGridViewRow fila = dgvHuespedes.Rows[filaSeleccionada];
                txtCodigo.Text = fila.Cells["CodigoHuesped"].Value?.ToString();
                txtNombre.Text = fila.Cells["Nombre"].Value?.ToString();
                cboxTipoIdentificacion.SelectedItem = fila.Cells["TipoIdentificacion"].Value?.ToString();
                nudNumID.Value = Convert.ToDecimal(fila.Cells["NumeroIdentificacion"].Value);
                txtDirección.Text = fila.Cells["Direccion"].Value?.ToString();
                cboxGenero.Text = fila.Cells["Genero"].Value?.ToString();
                nudTelefono.Value = Convert.ToDecimal(fila.Cells["Telefono"].Value);
                nudPuntuacion.Value = Convert.ToDecimal(fila.Cells["Puntuacion"].Value);
                
                if (DateTime.TryParse(fila.Cells["FechaNacimiento"].Value?.ToString(), out DateTime FechaNacimiento))
                {
                    dtpFechaNacimiento.Value = FechaNacimiento;
                }
                else
                {
                    dtpFechaNacimiento.Value = DateTime.Now;
                }
                bool estadoHuesped = Convert.ToBoolean(fila.Cells["Estado"].Value);
                rdbActivo.Checked = estadoHuesped;
                rdbInactivo.Checked = !estadoHuesped;
                MtdEstadoFilaSelecionada(true);
            }
        }
        //Activar fila seleccionada
        private int? filaActiva = null;
        private void MtdActivarFilaSeleccionada(int filaSeleccionada)
        {
            try
            {
                if (filaActiva.HasValue && filaActiva.Value < dgvHuespedes.Rows.Count)
                {
                    dgvHuespedes.Rows[filaActiva.Value].Cells["Seleccionar"].Value = false;
                    dgvHuespedes.Rows[filaActiva.Value].DefaultCellStyle.BackColor = Color.White;
                }
                if (filaSeleccionada >= 0 && filaSeleccionada < dgvHuespedes.Rows.Count)
                {
                    filaActiva = filaSeleccionada;
                    dgvHuespedes.Rows[filaSeleccionada].Cells["Seleccionar"].Value = true;
                    dgvHuespedes.Rows[filaSeleccionada].DefaultCellStyle.BackColor = Color.FromArgb(220, 235, 255);

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
                dgvHuespedes.Rows[filaActiva.Value].Cells["Seleccionar"].Value = false;
                dgvHuespedes.Rows[filaActiva.Value].DefaultCellStyle.BackColor = Color.White;
            }

            filaActiva = null;

            MtdLimpiarControlesForm();
            MtdEstadoFilaSelecionada(false);
        }
        //Metodo validar datos
        private bool MtdValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(cboxTipoIdentificacion.Text) ||
                nudNumID.Value <= 0 ||
                dtpFechaNacimiento.Value >= DateTime.Now ||
                string.IsNullOrWhiteSpace(txtDirección.Text) ||
                string.IsNullOrWhiteSpace(cboxGenero.Text) ||
                nudTelefono.Value <= 0 ||
                nudPuntuacion.Value < 0)
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvHuespedes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvHuespedes.EndEdit();
            if (e.RowIndex < 0)
                return;

            if (dgvHuespedes.Columns[e.ColumnIndex].Name != "Seleccionar")
                return;

            if (!chkSeleccionar.Checked)
            {
                dgvHuespedes.Rows[e.RowIndex].Cells["Seleccionar"].Value = false;
                return;
            }

            bool seleccionActual = Convert.ToBoolean(
                dgvHuespedes.Rows[e.RowIndex].Cells["Seleccionar"].Value ?? false);

            if (seleccionActual)
                MtdDesactivaFilaSeleccionada();
            else
                MtdActivarFilaSeleccionada(e.RowIndex);
            if (e.RowIndex >= 0 && dgvHuespedes.Rows.Count > 0)
            {
                MtdActivarFilaSeleccionada(e.RowIndex);
                MtdEstadoFilaSelecionada(true);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            MtdLimpiarControlesForm();
            MtdEstadoFilaSelecionada(false);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtBuscarNombre.Text.Trim();

                dgvHuespedes.DataSource = huespedesNegocio.MtdBuscarHuesped(nombre);

                dgvHuespedes.ClearSelection();
                dgvHuespedes.CurrentCell = null;

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
            txtBuscarNombre.Clear();
            MtdConsultarHuespedes();

            MtdLimpiarControlesForm();
            MtdEstadoFilaSelecionada(false);
            MtdContarTotalRegistros();
        }

        private void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            dgvHuespedes.Columns["Seleccionar"].ReadOnly = !chkSeleccionar.Checked;
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (MtdValidarDatos() == false)
                return;

            try
            {
                char generoAsignado = '\0';

                string seleccion = cboxGenero.Text.Trim();

                if (seleccion.Equals("Masculino", StringComparison.OrdinalIgnoreCase))
                    generoAsignado = 'M';
                else if (seleccion.Equals("Femenino", StringComparison.OrdinalIgnoreCase))
                    generoAsignado = 'F';
                else if (seleccion.Equals("Otro", StringComparison.OrdinalIgnoreCase))
                    generoAsignado = 'O';
                else
                    throw new Exception("Por favor, seleccione un género válido de la lista.");

                HuespedesEntidad huesped = new HuespedesEntidad
                {
                    Nombre = txtNombre.Text.Trim(),
                    TipoIdentificacion = cboxTipoIdentificacion.Text,
                    NumeroIdentificacion = Convert.ToInt64(nudNumID.Value),
                    FechaNacimiento = dtpFechaNacimiento.Value,
                    Genero = generoAsignado,
                    Telefono = Convert.ToInt32(nudTelefono.Value),
                    Puntuacion = nudPuntuacion.Value,
                    Direccion = txtDirección.Text.Trim(),
                    Estado = rdbActivo.Checked
                };
                string mensaje = huespedesNegocio.MtdAgregarHuespedes(huesped);

                MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MtdLimpiarControlesForm();
                MtdConsultarHuespedes();
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
                MessageBox.Show("Seleccione un huésped para editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MtdValidarDatos() == false)
                return;

            try
            {
                char generoAsignado = '\0';

                string seleccion = cboxGenero.Text.Trim();

                if (seleccion.Equals("Masculino", StringComparison.OrdinalIgnoreCase))
                    generoAsignado = 'M';
                else if (seleccion.Equals("Femenino", StringComparison.OrdinalIgnoreCase))
                    generoAsignado = 'F';
                else if (seleccion.Equals("Otro", StringComparison.OrdinalIgnoreCase))
                    generoAsignado = 'O';
                else
                    throw new Exception("Por favor, seleccione un género válido de la lista.");

                HuespedesEntidad huesped = new HuespedesEntidad
                {
                    CodigoHuesped = Convert.ToInt32(txtCodigo.Text),
                    Nombre = txtNombre.Text.Trim(),
                    TipoIdentificacion = cboxTipoIdentificacion.SelectedItem.ToString(),
                    NumeroIdentificacion = Convert.ToInt64(nudNumID.Value),
                    FechaNacimiento = dtpFechaNacimiento.Value,
                    Genero = generoAsignado,
                    Telefono = Convert.ToInt32(nudTelefono.Value),
                    Puntuacion = nudPuntuacion.Value,
                    Direccion = txtDirección.Text.Trim(),
                    Estado = rdbActivo.Checked
                };

                string mensaje = huespedesNegocio.MtdEditarHuesped(huesped);
                MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MtdLimpiarControlesForm();
                MtdConsultarHuespedes();
                MtdEstadoFilaSelecionada(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCodigo.Text))
                    throw new Exception("Debe seleccionar un huésped");

                int codigoHuesped = Convert.ToInt32(txtCodigo.Text);

                DialogResult confirmacion = MessageBox.Show("¿Está seguro de eliminar este huésped?", "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacion == DialogResult.No)
                    return;

                string mensaje = huespedesNegocio.MtdEliminarHuesped(codigoHuesped);
                MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                filaActiva = null;

                MtdLimpiarControlesForm();
                MtdConsultarHuespedes();
                MtdEstadoFilaSelecionada(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
