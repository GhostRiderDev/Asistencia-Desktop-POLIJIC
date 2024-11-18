using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Logica;
namespace Capa_Diseño
{
    public partial class FrmAsignaturas : Form
    {
        public FrmAsignaturas()
        {
            InitializeComponent();
        }
        //----------Validaciones------------

        //------------Asignatura------------

        protected void Func_FrmAsignaturas_Guardar()
        {
            CL_Asignaturas ObjAsignatura = new CL_Asignaturas();
            ObjAsignatura.Codigo = TxtCodigo.Text;
            ObjAsignatura.Nombre = TxtNombre.Text;
            ObjAsignatura.SP_FrmAsignaturas_Guardar();
            String message = "Asignatura registrada correctamente";
            String caption = "Registro de asignaturas";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected void Func_FrmAsignaturas_Buscar()
        {
            CL_Asignaturas ObjAsignatura = new CL_Asignaturas();
            ObjAsignatura.Codigo = TxtCodigo.Text;
            ObjAsignatura.SP_FrmAsignaturas_Buscar();
            TxtNombre.Text = ObjAsignatura.Nombre;
            if (ObjAsignatura.msn == "")
            {
            }
            else
            {
                String message = "No esta registrada";
                String caption = "Registro de Asignaturas";
                var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            Func_FrmAsignaturas_Guardar();
            TxtNombre.Text = "";
            TxtCodigo.Text = "";
        }

        private void TxtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                Func_FrmAsignaturas_Buscar();
            }
        }

        private void BtnDevolver_Click(object sender, EventArgs e)
        {
            FrmIngresos Objinicio = new FrmIngresos();
            Objinicio.LblUsuario.Text = LblUsuario.Text;
            Objinicio.LblPerfil.Text = LblPerfil.Text;
            Objinicio.Show();
            Hide();
        }
    }
}
