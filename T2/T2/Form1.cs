using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T2
{
    public partial class Form1 : Form
    {
        private List<Medicamento> medicamentos = new List<Medicamento>();

        public Form1()
        {
            InitializeComponent();
            //Tamaño fijo del Form1
            this.Size = new Size(815, 570);
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            //Labels transparentes
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;

            //Funciones de customizar los GroupBox
            SetRoundedGroupBox(groupBox1, 10);
            SetRoundedGroupBox2(groupBox2, 10);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            int codigo = int.Parse(txtCodigo.Text);
            string nombre = txtNombre.Text;
            int cantidad = int.Parse(txtCantidad.Text);
            decimal precioUnitario = decimal.Parse(txtPrecioUnitario.Text);

            Medicamento medicamento = new Medicamento
            {
                Codigo = codigo,
                Nombre = nombre,
                Cantidad = cantidad,
                PrecioUnitario = precioUnitario
            };

            medicamentos.Add(medicamento);

            // Limpiar los cuadros de texto después de agregar el medicamento
            txtCodigo.Clear();
            txtNombre.Clear();
            txtCantidad.Clear();
            txtPrecioUnitario.Clear();

            ActualizarListaMedicamentos();

        }

        private void ActualizarListaMedicamentos()
        {
            var medicamentosConMontoInvertido = medicamentos.Select(m => new
            {
                m.Codigo,
                m.Nombre,
                m.Cantidad,
                PrecioUnitario = m.PrecioUnitario,
                MontoInvertido = m.Cantidad * m.PrecioUnitario
            }).ToList();

            dgvMedicamentos.DataSource = new BindingSource { DataSource = medicamentosConMontoInvertido };

        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string nombreBusqueda = txtBuscar.Text;
            bool encontrado = BuscarMedicamentoPorNombre(nombreBusqueda);

            if (encontrado)
            {
                MessageBox.Show("Medicamento encontrado.");
            }
            else
            {
                MessageBox.Show("Medicamento no encontrado.");
            }

        }
        private bool BuscarMedicamentoPorNombre(string nombre)
        {
            foreach (Medicamento medicamento in medicamentos)
            {
                if (medicamento.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int codigoEliminar = int.Parse(txtEliminar.Text);
            bool eliminado = EliminarMedicamentoPorCodigo(codigoEliminar);

            if (eliminado)
            {
                MessageBox.Show("Medicamento eliminado correctamente.");
            }
            else
            {
                MessageBox.Show("Medicamento no encontrado.");
            }

            ActualizarListaMedicamentos();
        }
        private bool EliminarMedicamentoPorCodigo(int codigo)
        {
            Medicamento medicamentoEliminar = medicamentos.Find(m => m.Codigo == codigo);

            if (medicamentoEliminar != null)
            {
                medicamentos.Remove(medicamentoEliminar);
                return true;
            }

            return false;
        }

        //Bordes redondos del gropuBox1
        private void SetRoundedGroupBox(GroupBox groupBox, int cornerRadius)
        {
            // Crear un rectángulo que coincide con el tamaño del GroupBox
            Rectangle bounds = new Rectangle(0, 0, groupBox.Width, groupBox.Height);

            // Crear un camino redondeado con los bordes redondos deseados
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(bounds.X, bounds.Y, cornerRadius, cornerRadius, 180, 90); // Esquina superior izquierda
                path.AddArc(bounds.Right - cornerRadius, bounds.Y, cornerRadius, cornerRadius, 270, 90); // Esquina superior derecha
                path.AddArc(bounds.Right - cornerRadius, bounds.Bottom - cornerRadius, cornerRadius, cornerRadius, 0, 90); // Esquina inferior derecha
                path.AddArc(bounds.X, bounds.Bottom - cornerRadius, cornerRadius, cornerRadius, 90, 90); // Esquina inferior izquierda
                path.CloseFigure();

                // Establecer la región del GroupBox como el camino redondeado
                groupBox.Region = new Region(path);
            }
        }


        //Bordes redondos del gropuBox2
        private void SetRoundedGroupBox2(GroupBox groupBox, int cornerRadius)
        {
            // Crear un rectángulo que coincide con el tamaño del GroupBox
            Rectangle bounds = new Rectangle(0, 0, groupBox.Width, groupBox.Height);

            // Crear un camino redondeado con los bordes redondos deseados
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(bounds.X, bounds.Y, cornerRadius, cornerRadius, 180, 90); // Esquina superior izquierda
                path.AddArc(bounds.Right - cornerRadius, bounds.Y, cornerRadius, cornerRadius, 270, 90); // Esquina superior derecha
                path.AddArc(bounds.Right - cornerRadius, bounds.Bottom - cornerRadius, cornerRadius, cornerRadius, 0, 90); // Esquina inferior derecha
                path.AddArc(bounds.X, bounds.Bottom - cornerRadius, cornerRadius, cornerRadius, 90, 90); // Esquina inferior izquierda
                path.CloseFigure();

                // Establecer la región del GroupBox como el camino redondeado
                groupBox.Region = new Region(path);
            }
        }

    }
}
