using Capa_Logica;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Net.NetworkInformation;
namespace Capa_Diseño
{
    public partial class FrmIngresos : Form
    {
        public FrmIngresos()
        {
            InitializeComponent();
            TxtDocumento.Focus();
            DgvMaterias.CurrentCellDirtyStateChanged += DgvMaterias_CurrentCellDirtyStateChanged;
            DgvMaterias.CellValueChanged += DgvMaterias_CellValueChanged;
        }

        public String Estado, Estado1;
        //--------Validaciones-------------

       

        public void ValidacionUsuario()
        {
            //Le damos tamaño al formulario dependiendo el cargo del usuario y le damos sus respectivos permisos de controles

        
            if (LblPerfil.Text == "Administrador")
            {
                this.Size = new Size (1246, 628);
                BtnRegistrar.Visible = true;
                BtnActualizar.Visible = true;
                TxtBusqueda.Visible = true;
                BtnAsignaturas.Visible = true;
                BtnReportes.Visible = true;
                TxtNombre.Enabled = true;
                TxtCorreo.Enabled = true;
                Func_FrmIngresos_CargarMaterias();
            }
            if(LblPerfil.Text == "Auxiliar")
            {
                this.Size = new Size(776, 628);
                BtnRegistrar.Visible = false;
                BtnActualizar.Visible = false;
                TxtBusqueda.Visible = false;
                BtnAsignaturas.Visible = false;
                BtnReportes.Visible = false;
                TxtNombre.Enabled = false;
                TxtCorreo.Enabled = false;
            }
        }

        protected void ResetControl()
        {
            TxtNombre.Text = "";
            TxtDocumento.Text = "";
            TxtCorreo.Text = "";
            TxtCodigo.Text = "";
            TxtBusqueda.Clear();
            TxtDocumento.Focus();
            NoCargaImagen();
                DataTable dt = (DataTable)DgvMaterias.DataSource;
                dt.Clear();
            
        }
        //----------Cargar imagen--------------
        protected void NoCargaImagen()
        {
            //Dirección de carpeta en pc a instalar @"C:\Users\POLITÉCNICO\Pictures\Docentes"
            //Dirección de carpeta donde se desarrolla @"C:\Users\Poli\Pictures\Docentes"
            string rutaCarpetaNN = @"C:\Users\Poli\Pictures\Docentes";
            string nombreArchivoNN = "img.png"; // Reemplaza con el nombre de tu imagen

            // Combina la ruta de la carpeta con el nombre del archivo
            string rutaCompletaNN = System.IO.Path.Combine(rutaCarpetaNN, nombreArchivoNN);
            // MessageBox.Show("No se encontró la imagen en la ruta especificada.");
            pictureBox1.Image = new Bitmap(rutaCompletaNN);
        }
        protected void CargarImagen()
        {
            // Especifica la ruta de la carpeta y el nombre del archivo
            string rutaCarpeta = @"C:\Users\Poli\Pictures\Docentes";
            string nombreArchivo = TxtDocumento.Text + ".png"; // Reemplaza con el nombre de la imagen

            // Combina la ruta de la carpeta con el nombre del archivo
            string rutaCompleta = System.IO.Path.Combine(rutaCarpeta, nombreArchivo);

            // Verifica si el archivo existe
            if (System.IO.File.Exists(rutaCompleta))
            {
                // Cargar la imagen en el PictureBox
                pictureBox1.Image = new Bitmap(rutaCompleta);
            }
            else
            {
                NoCargaImagen();
            }
        }
        //--------------Docente----------------

        protected void Func_FrmIngresos_GuardarDocente()
        {
            CL_Ingresos ObjDocente = new CL_Ingresos();
            ObjDocente.Documento = TxtDocumento.Text;
            ObjDocente.Nombre_Completo = TxtNombre.Text.ToUpper();
            ObjDocente.Correo = TxtCorreo.Text;
            ObjDocente.Codigo = TxtCodigo.Text;
            ObjDocente.SP_FrmIngresos_GuardarDocente();
            String message = "Docente Registrado correctamente";
            String caption = "Registro de docentes";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            ResetControl();
        }
        protected void Func_FrmIngresos_ActualizarDocente()
        {
            CL_Ingresos ObjDocente = new CL_Ingresos();
            ObjDocente.Documento = TxtDocumento.Text;
            ObjDocente.Nombre_Completo = TxtNombre.Text.ToUpper();
            ObjDocente.Correo = TxtCorreo.Text;
            ObjDocente.Codigo = TxtCodigo.Text;
            ObjDocente.SP_FrmIngresos_ActualizarDocente();
            String message = "Docente Actualizado correctamente";
            String caption = "Registro de docentes";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            ResetControl();
        }

        protected void Func_FrmIngresos_BuscarDocente()
        {
            CL_Ingresos ObjDocente = new CL_Ingresos();
            ObjDocente.Documento = TxtDocumento.Text;
            ObjDocente.SP_FrmIngresos_BuscarDocente();
            TxtNombre.Text = ObjDocente.Nombre_Completo;
            TxtCorreo.Text = ObjDocente.Correo;
            if (ObjDocente.msn == "")
            {       
                
            }
            else
            {
                String message = "No esta registrado";
                String caption = "Registro de docentes";
                var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        protected void Func_FrmIngresos_BuscarDocenteCodigo()
        {
            CL_Ingresos ObjDocente = new CL_Ingresos();
            ObjDocente.Codigo = TxtCodigo.Text;
            ObjDocente.SP_FrmIngresos_BuscarDocenteCodigo();
            TxtDocumento.Text = ObjDocente.Documento;
            TxtNombre.Text = ObjDocente.Nombre_Completo;
            TxtCorreo.Text = ObjDocente.Correo;
            if (ObjDocente.msn == "")
            {
            }
            else
            {
                String message = "No esta registrado";
                String caption = "Registro de docentes";
                var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        protected void Func_FrmIngresos_VincularMaterias()
        {
            CL_Ingresos ObjMaterias = new CL_Ingresos();
            int fila = DgvAsignaturas.CurrentRow.Index;
            ObjMaterias.Documento = TxtDocumento.Text;
            ObjMaterias.Codigo = Convert.ToString(DgvAsignaturas.Rows[fila].Cells[0].Value);
            ObjMaterias.SP_FrmIngresos_VincularMaterias();
            if (ObjMaterias.msn == "")
            {
            }
            else
            {
                String message = "Materia vinculada exitosamente";
                String caption = "Registro de materias";
                var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        protected void Func_FrmIngresos_DesvincularMaterias()
        {
            CL_Ingresos ObjMaterias = new CL_Ingresos();
            int fila = DgvAsignaturas.CurrentRow.Index;
            ObjMaterias.Documento = TxtDocumento.Text;
            ObjMaterias.Codigo = Convert.ToString(DgvAsignaturas.Rows[fila].Cells[0].Value);
            ObjMaterias.SP_FrmIngresos_DesvincularMaterias();
            if (ObjMaterias.msn == "")
            {
            }
            else
            {
                String message = "Materia desvinculada exitosamente";
                String caption = "Registro de materias";
                var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //Vinculamos la materias ya registradas, solo actualizamos el estado de la vinculación
        protected void Func_FrmIngresos_VinculacionExisMateria()
        {
            CL_Ingresos ObjMaterias = new CL_Ingresos();
            int fila = DgvAsignaturas.CurrentRow.Index;
            ObjMaterias.Documento = TxtDocumento.Text;
            ObjMaterias.Codigo = Convert.ToString(DgvAsignaturas.Rows[fila].Cells[0].Value);
            ObjMaterias.SP_FrmIngresos_VinculacionExisMateria();
            if (ObjMaterias.msn == "")
            {
            }
            else
            {
                String message = "Materia Vinculada exitosamente";
                String caption = "Registro de materias";
                var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        protected void Func_FrmIngresos_BuscarMaterias()
        {
            if (TxtDocumento.Text != "")
            {
                CL_Ingresos ObjMaterias = new CL_Ingresos();
                int fila = DgvAsignaturas.CurrentRow.Index;
                ObjMaterias.Documento = TxtDocumento.Text;
                ObjMaterias.Codigo = Convert.ToString(DgvAsignaturas.Rows[fila].Cells[0].Value);
                ObjMaterias.SP_FrmIngresos_BuscarMaterias();
                Estado = ObjMaterias.Estado;
                if (ObjMaterias.msn1 == "")
                {
                    if (Estado == "Vinculado")
                    {
                        Func_FrmIngresos_DesvincularMaterias();
                    }
                    if (Estado == "Desvinculado")
                    {
                        Func_FrmIngresos_VinculacionExisMateria();
                    }
                }
                else
                {
                    Func_FrmIngresos_VincularMaterias();
                }
            }else
            {
                String message = "Recuerde poner el docente a vincular con la materia";
                String caption = "Registro de ingresos";
                var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        protected void Func_FrmIngresos_CargarMaterias()
        {
            try
            {
                CL_Ingresos ObjRegistro = new CL_Ingresos();               
                ObjRegistro.SP_FrmIngresos_CargarMaterias();
                DgvAsignaturas.DataSource = ObjRegistro.dtDgvMaterias;
            }
            catch { }
        }

        protected void Func_FrmIngresos_CargarMateriasVinculadas()
        {           
                CL_Ingresos ObjMaterias = new CL_Ingresos();
                ObjMaterias.Documento = TxtDocumento.Text;
                ObjMaterias.SP_FrmIngresos_CargarMateriasVinculadas();
                DgvMaterias.DataSource = ObjMaterias.dtDgvMateriasVinculadas;        
        }
        protected void Func_FrmIngresos_CargarEstadoIngresos()
        {
            CL_Ingresos ObjMaterias = new CL_Ingresos();
            ObjMaterias.Documento = TxtDocumento.Text;
            ObjMaterias.SP_FrmIngresos_CargarEstadoIngresos();
            DgvEstados.DataSource = ObjMaterias.dtDgvEstados;
        }
        protected void Func_FrmIngresos_GuardarIngresos()
        {
            CL_Ingresos ObjMaterias = new CL_Ingresos();
            int fila = DgvMaterias.CurrentRow.Index;
            ObjMaterias.Codigo = Convert.ToString(DgvMaterias.Rows[fila].Cells[0].Value);
            ObjMaterias.Usuario = LblUsuario.Text;
            ObjMaterias.Documento = TxtDocumento.Text;
            ObjMaterias.SP_FrmIngresos_GuardarIngresos();
            String message = "Ingresaste a clases";
            String caption = "Registro de materias";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        protected void Func_FrmIngresos_GuardarSalida()
        {
            CL_Ingresos ObjMaterias = new CL_Ingresos();
            int fila = DgvMaterias.CurrentRow.Index;
            ObjMaterias.Codigo = Convert.ToString(DgvMaterias.Rows[fila].Cells[0].Value);
            ObjMaterias.Documento = TxtDocumento.Text;
            ObjMaterias.SP_FrmIngresos_GuardarSalidas();
            String message = "Saliste de clases";
            String caption = "Registro de materias";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        protected void Func_FrmIngresos_BuscarIngreso()
        {
            CL_Ingresos ObjMaterias = new CL_Ingresos();
            int fila = DgvMaterias.CurrentRow.Index;
            ObjMaterias.Codigo = Convert.ToString(DgvMaterias.Rows[fila].Cells[0].Value);
            ObjMaterias.Documento = TxtDocumento.Text;
            ObjMaterias.SP_FrmIngresos_BuscarIngreso();
            if (ObjMaterias.msn1 == "")
            {
                if (ObjMaterias.Estado == "En clase")
                {
                    Func_FrmIngresos_GuardarSalida();
                    //CompararEstados();
                }
                if (ObjMaterias.Estado == "Fuera de clase")
                {
                    Func_FrmIngresos_GuardarIngresos();
                    //CompararEstados();
                }
            }
            if(ObjMaterias.msn1 != "")
            {
                Func_FrmIngresos_GuardarIngresos();
                
            }
            
        }

        protected void CompararEstados()
        {
            for (int i = 0; i < DgvMaterias.Rows.Count; i++)
            {
               // Func_FrmIngresos_CargarEstadoIngresos();
                    int valor1 = Convert.ToInt32(DgvEstados.Rows[i].Cells[1].Value);
                    object valor2 = DgvEstados.Rows[i].Cells[0].Value;
                    object valor3 = DgvMaterias.Rows[i].Cells[0].Value;

                // Comparar los valores de las celdas
                if (valor1 == 5 && object.Equals(valor2, valor3))
                    {
                    MessageBox.Show("Si entro"+ valor1);
                        DgvMaterias.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                    else
                    {
                    MessageBox.Show("Si entro2");
                        DgvMaterias.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }
                
            }
            
        }


        private void FrmIngresos_Load(object sender, EventArgs e)
        {
            
            ValidacionUsuario();
            TxtNombre.Focus();
            Func_FrmIngresos_CargarMateriasVinculadas();
            //Func_FrmIngresos_CargarEstadoIngresos();


        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            if(TxtDocumento.Text != "" && TxtNombre.Text != "")
            {
                Func_FrmIngresos_GuardarDocente();
            }
            else
            {
                String message = "Recuerde llenar los campos";
                String caption = "Registro de docente";
                var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            Func_FrmIngresos_ActualizarDocente();
        }

        private void TxtDocumento_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void TxtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            { 
                Func_FrmIngresos_BuscarDocenteCodigo();
                Func_FrmIngresos_CargarMateriasVinculadas();
                Func_FrmIngresos_CargarMaterias();
                CargarImagen();
            }
        }

        private void BtnAsignaturas_Click(object sender, EventArgs e)
        {
            FrmAsignaturas Objinicio = new FrmAsignaturas();
            Objinicio.LblUsuario.Text = LblUsuario.Text;
            Objinicio.LblPerfil.Text = LblPerfil.Text;
            Objinicio.Show();
            Hide();
        }

        private void BtnReportes_Click(object sender, EventArgs e)
        {
            FrmReportes Objinicio = new FrmReportes();
            Objinicio.LblUsuario.Text = LblUsuario.Text;
            Objinicio.LblPerfil.Text = LblPerfil.Text;
            Objinicio.Show();
            Hide();
        }

        private void BtnDevolver_Click(object sender, EventArgs e)
        {
            FrmLogin Objinicio = new FrmLogin();
            Objinicio.Show();
            Hide();
        }


        private void BtnVinculados_Click(object sender, EventArgs e)
        {

        }
        //Buscamos desde la caja de texto y vinculamos al docente
        //Falta hacer que seleciones la fila
        private void TxtBusqueda_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                int fila = DgvAsignaturas.CurrentRow.Index;
                int sw = 0;
                for (int i = 0; i <= DgvAsignaturas.RowCount - 1; i++)
                {
                    
                    if (Convert.ToString(DgvAsignaturas.Rows[i].Cells[0].Value) == TxtBusqueda.Text)
                    {
                        sw = 1;
                        fila = i;
                    }
                    if (sw == 1)
                    {
                        CL_Ingresos ObjMaterias = new CL_Ingresos();
                        ObjMaterias.Documento = TxtDocumento.Text;
                        ObjMaterias.Codigo = Convert.ToString(DgvAsignaturas.Rows[fila].Cells[0].Value);
                        ObjMaterias.SP_FrmIngresos_BuscarMaterias();
                        Estado = ObjMaterias.Estado;
                        if (ObjMaterias.msn1 == "")
                        {
                            if (Estado == "Vinculado")
                            {
                                ObjMaterias.Documento = TxtDocumento.Text;
                                ObjMaterias.Codigo = Convert.ToString(DgvAsignaturas.Rows[fila].Cells[0].Value);
                                ObjMaterias.SP_FrmIngresos_DesvincularMaterias();
                                if (ObjMaterias.msn == "")
                                {
                                }
                                else
                                {
                                    String message = "Materia desvinculada exitosamente";
                                    String caption = "Registro de materias";
                                    var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            if (Estado == "Desvinculado")
                            {
                                ObjMaterias.Documento = TxtDocumento.Text;
                                ObjMaterias.Codigo = Convert.ToString(DgvAsignaturas.Rows[fila].Cells[0].Value);
                                ObjMaterias.SP_FrmIngresos_VinculacionExisMateria();
                                if (ObjMaterias.msn == "")
                                {
                                }
                                else
                                {
                                    String message = "Materia Vinculada exitosamente";
                                    String caption = "Registro de materias";
                                    var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        else
                        {
                            ObjMaterias.Documento = TxtDocumento.Text;
                            ObjMaterias.Codigo = Convert.ToString(DgvAsignaturas.Rows[fila].Cells[0].Value);
                            ObjMaterias.SP_FrmIngresos_VincularMaterias();
                            if (ObjMaterias.msn == "")
                            {
                               
                            }
                            else
                            {
                                String message = "Materia vinculada exitosamente";
                                String caption = "Registro de materias";
                                var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        sw = 0;

                    }
                    
                }
                if (sw == 0)
                {
                    String message = "Materia no se encuentra vinculada";
                    String caption = "Registro de materias";
                    var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Func_FrmIngresos_CargarMateriasVinculadas();
            }
        }

        private void TxtDocumento_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Func_FrmIngresos_BuscarDocente();
                Func_FrmIngresos_CargarMateriasVinculadas();
                Func_FrmIngresos_CargarMaterias();
                CargarImagen();
                //Func_FrmIngresos_CargarEstadoIngresos();
                //CompararEstados();

            }
        }

        private void DgvAsignaturas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (LblPerfil.Text == "Administrador")
            {
                Func_FrmIngresos_BuscarMaterias();
                Func_FrmIngresos_CargarMateriasVinculadas();
            }
           
        }

        private void FrmIngresos_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                if(LblPerfil.Text == "Auxiliar")
                {
                    ResetControl();
                }
                else
                {
                    ResetControl();
                    DataTable dt = (DataTable)DgvAsignaturas.DataSource;
                    dt.Clear();
                }
                
            }
        }

        private void DgvMaterias_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void DgvMaterias_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
        }

        private void TxtDocumento_TextChanged(object sender, EventArgs e)
        {

        }

        private void DgvMaterias_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (LblPerfil.Text == "Auxiliar")
            {
                Func_FrmIngresos_BuscarIngreso();
            }

        }
    }
}
