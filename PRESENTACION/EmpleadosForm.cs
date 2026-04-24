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
    public partial class EmpleadosForm : Form
    {
        EmpleadosNegocio empleadosNegocio = new EmpleadosNegocio();

        public EmpleadosForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void EmpleadosForm_Load(object sender, EventArgs e)
        {
            MtdConsultarEmpleados(); // Carga los datos de la DB
            MtdRenombrarNombreColumna(); // Pone nombres bonitos a las columnas
            MtdEstadoBotonNuevo();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        /*  -----   CONSULTAR   -----   */

        // Cambia el titulo a las columnas 
        private void MtdRenombrarNombreColumna()
        {
            CambiarTitulo("CodigoEmpleado", "Código");
            CambiarTitulo("NumeroDPI", "Numero DPI");
            CambiarTitulo("FechaNacimiento", "Fecha Nacimiento");
            CambiarTitulo("FechaContratacion", "Fecha Contratacion");
        }
        private void CambiarTitulo(string nombreColumna, string titulo)
        {
            if (dgvEmpleados.Columns.Contains(nombreColumna))
            {
                dgvEmpleados.Columns[nombreColumna].HeaderText = titulo;
            }
        }
        // Contar cantidad de lineas del DataGridView 
        private void MtdContarTotalRegistros()
        {
            int totalRegistros = dgvEmpleados.Rows.Count;
            lblTotalRegistros.Text = "Total de registros: " + totalRegistros.ToString();
        }
        // Consultar datos de tabla Huespedes y mostrar en DataGridView 
        private void MtdConsultarEmpleados()
        {
            try
            {
                dgvEmpleados.DataSource = empleadosNegocio.MtdConsultar();
                dgvEmpleados.ClearSelection();
                dgvEmpleados.CurrentCell = null;

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
            if (dgvEmpleados.Columns[e.ColumnIndex].Name == "Genero" && e.Value != null)
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
            nudDPI.Enabled = true;
            cboxGenero.Enabled = true;
            dtpFechaNacimiento.Enabled = true;
            dtpFechaContratacion.Enabled = true;
            cboxCargo.Enabled = true;
            nudSalario.Enabled = true;
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
            nudDPI.Enabled = editable;
            cboxGenero.Enabled = editable;
            dtpFechaNacimiento.Enabled = editable;
            dtpFechaContratacion.Enabled = editable;
            nudSalario.Enabled = editable;
            cboxCargo.Enabled = editable;
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
            nudDPI.Value = 0;
            cboxGenero.SelectedIndex = -1;
            dtpFechaNacimiento.Value = DateTime.Now;
            dtpFechaContratacion.Value = DateTime.Now;
            nudSalario.Value = 0;
            cboxCargo.SelectedIndex = -1;

            /*--Estado: Activo o Inactivo--*/
            rdbActivo.Checked = false;
            rdbInactivo.Checked = false;

            dgvEmpleados.ClearSelection();
            dgvEmpleados.CurrentCell = null;

            foreach (DataGridViewRow row in dgvEmpleados.Rows)
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
            nudDPI.Enabled = true;
            cboxGenero.Enabled = true;
            dtpFechaNacimiento.Enabled = true;
            dtpFechaContratacion.Enabled = true;
            cboxCargo.Enabled = true;
            nudSalario.Enabled = true;
            /*--Estado: Activo o Inactivo--*/
            rdbActivo.Enabled = true;
            rdbInactivo.Enabled = true;
        }
        //Cargar datos de la fila selecciohnada en los controles del Form
        private void MtdCargarDatosFilaEnControlesForm(int filaSeleccionada)
        {
            if (filaSeleccionada >= 0 && filaSeleccionada < dgvEmpleados.Rows.Count)
            {
                DataGridViewRow fila = dgvEmpleados.Rows[filaSeleccionada];
                txtCodigo.Text = fila.Cells["CodigoEmpleado"].Value?.ToString();
                txtNombre.Text = fila.Cells["Nombre"].Value?.ToString();
                nudDPI.Value = Convert.ToDecimal(fila.Cells["NumeroDPI"].Value);
                cboxGenero.Text = fila.Cells["Genero"].Value?.ToString();
                if (DateTime.TryParse(fila.Cells["FechaNacimiento"].Value?.ToString(), out DateTime FechaNacimiento))
                {
                    dtpFechaNacimiento.Value = FechaNacimiento;
                }
                else
                {
                    dtpFechaNacimiento.Value = DateTime.Now;
                }
                if (DateTime.TryParse(fila.Cells["FechaContratacion"].Value?.ToString(), out DateTime FechaContratacion))
                {
                    dtpFechaContratacion.Value = FechaContratacion;
                }
                else
                {
                    dtpFechaContratacion.Value = DateTime.Now;
                }
                cboxCargo.SelectedItem = fila.Cells["Cargo"].Value?.ToString();
                nudSalario.Value = Convert.ToDecimal(fila.Cells["Salario"].Value);
                
                bool estadoEmpleado = Convert.ToBoolean(fila.Cells["Estado"].Value);
                rdbActivo.Checked = estadoEmpleado;
                rdbInactivo.Checked = !estadoEmpleado;
                MtdEstadoFilaSelecionada(true);
            }
        }
        //Activar fila seleccionada
        private int? filaActiva = null;
        private void MtdActivarFilaSeleccionada(int filaSeleccionada)
        {
            try
            {
                if (filaActiva.HasValue && filaActiva.Value < dgvEmpleados.Rows.Count)
                {
                    dgvEmpleados.Rows[filaActiva.Value].Cells["Seleccionar"].Value = false;
                    dgvEmpleados.Rows[filaActiva.Value].DefaultCellStyle.BackColor = Color.White;
                }
                if (filaSeleccionada >= 0 && filaSeleccionada < dgvEmpleados.Rows.Count)
                {
                    filaActiva = filaSeleccionada;
                    dgvEmpleados.Rows[filaSeleccionada].Cells["Seleccionar"].Value = true;
                    dgvEmpleados.Rows[filaSeleccionada].DefaultCellStyle.BackColor = Color.FromArgb(220, 235, 255);

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
                dgvEmpleados.Rows[filaActiva.Value].Cells["Seleccionar"].Value = false;
                dgvEmpleados.Rows[filaActiva.Value].DefaultCellStyle.BackColor = Color.White;
            }

            filaActiva = null;

            MtdLimpiarControlesForm();
            MtdEstadoFilaSelecionada(false);
        }
        //Metodo validar datos
        private bool MtdValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                nudDPI.Value <= 0 ||
                cboxGenero.SelectedIndex == -1 || // Es mejor verificar si hay selección
                dtpFechaNacimiento.Value.Date >= DateTime.Today || // .Date para comparar solo días
                string.IsNullOrWhiteSpace(cboxCargo.Text) ||
                nudSalario.Value <= 0) // Un salario de 0 normalmente no es válido
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios correctamente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void dgvEmpleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvEmpleados.EndEdit();
            if (e.RowIndex < 0)
                return;

            if (dgvEmpleados.Columns[e.ColumnIndex].Name != "Seleccionar")
                return;

            if (!chkSeleccionar.Checked)
            {
                dgvEmpleados.Rows[e.RowIndex].Cells["Seleccionar"].Value = false;
                return;
            }

            bool seleccionActual = Convert.ToBoolean(
                dgvEmpleados.Rows[e.RowIndex].Cells["Seleccionar"].Value ?? false);

            if (seleccionActual)
                MtdDesactivaFilaSeleccionada();
            else
                MtdActivarFilaSeleccionada(e.RowIndex);
            if (e.RowIndex >= 0 && dgvEmpleados.Rows.Count > 0)
            {
                MtdActivarFilaSeleccionada(e.RowIndex);
                MtdEstadoFilaSelecionada(true);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtBuscarNombre.Text.Trim();

                dgvEmpleados.DataSource = empleadosNegocio.MtdBuscarEmpleado(nombre);

                dgvEmpleados.ClearSelection();
                dgvEmpleados.CurrentCell = null;

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
            MtdConsultarEmpleados();

            MtdLimpiarControlesForm();
            MtdEstadoFilaSelecionada(false);
            MtdContarTotalRegistros();
        }

        private void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            dgvEmpleados.Columns["Seleccionar"].ReadOnly = !chkSeleccionar.Checked;
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (MtdValidarDatos() == false) return;

            try
            {


                EmpleadosEntidad empleados = new EmpleadosEntidad
                {
                    Nombre = txtNombre.Text,
                    NumeroDPI = Convert.ToInt64(nudDPI.Value),
                    Genero = cboxGenero.Text.ToString(),
                    FechaNacimiento = dtpFechaNacimiento.Value,
                    FechaContratacion = dtpFechaContratacion.Value,
                    Cargo = cboxCargo.Text,
                    Salario = nudSalario.Value,
                    Estado = rdbActivo.Checked
                };
                string mensaje = empleadosNegocio.MtdAgregarEmpleado(empleados);
                MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MtdConsultarEmpleados();
                MtdLimpiarControlesForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("Por favor, seleccione un empleado para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MtdValidarDatos() == false)
                return;

            try
            {
                string generoAsignado = "";
                string seleccion = cboxGenero.Text.Trim();

                if (seleccion.Equals("Masculino", StringComparison.OrdinalIgnoreCase))
                    generoAsignado = "M";
                else if (seleccion.Equals("Femenino", StringComparison.OrdinalIgnoreCase))
                    generoAsignado = "F";
                else if (seleccion.Equals("Otro", StringComparison.OrdinalIgnoreCase))
                    generoAsignado = "O";
                else
                    throw new Exception("Por favor, seleccione un género válido.");

                EmpleadosEntidad empleado = new EmpleadosEntidad
                {
                    CodigoEmpleado = Convert.ToInt32(txtCodigo.Text),
                    Nombre = txtNombre.Text.Trim(),
                    NumeroDPI = Convert.ToInt64(nudDPI.Value),
                    Genero = generoAsignado,
                    FechaNacimiento = dtpFechaNacimiento.Value,
                    FechaContratacion = dtpFechaContratacion.Value,
                    Cargo = cboxCargo.Text.Trim(),
                    Salario = nudSalario.Value,
                    Estado = rdbActivo.Checked
                };

                string mensaje = empleadosNegocio.MtdEditarEmpleado(empleado);
                MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MtdConsultarEmpleados();
                MtdLimpiarControlesForm();
                MtdDesactivaFilaSeleccionada();

                // Estado de botones
                btnGuardar.Enabled = true;
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
                btnNuevo.Enabled = false;
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
                    throw new Exception("Por favor, seleccione un empleado para eliminar.");

                int codigoEmpleado = Convert.ToInt32(txtCodigo.Text);

                DialogResult confirmacion = MessageBox.Show("¿Está seguro de que desea eliminar éste empleado?", "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacion == DialogResult.No)
                    return;

                string mensaje = empleadosNegocio.MtdEliminarEmpleado(codigoEmpleado);

                MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MtdLimpiarControlesForm();
                MtdConsultarEmpleados();
                MtdDesactivaFilaSeleccionada();

                btnGuardar.Enabled = true;
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
