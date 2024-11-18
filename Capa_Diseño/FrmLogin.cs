using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using Capa_Logica;
namespace Capa_Diseño
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }
        public bool VerificarConexionInternet()
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply respuesta = ping.Send("8.8.8.8", 1000); // Dirección IP de Google y timeout de 1000 ms (1 segundo)
                    if (respuesta.Status == IPStatus.Success)
                    {
                        return true; // Tiene conexión a Internet
                    }
                    else
                    {
                        return false; // No hay conexión
                    }
                }
            }
            catch (Exception)
            {
                return false; // En caso de error o excepción, se asume que no hay conexión
            }
        }



        protected void Func_FrmLogin_Ingresar()
        {
             CL_Login ObjLogin = new CL_Login();
             ObjLogin.Usuario = TxtUsuario.Text;
             ObjLogin.Contraseña = TxtContraseña.Text;
             ObjLogin.SP_FrmLogin_Ingresar();      
             LblPerfil.Text = ObjLogin.Cargo;
             LblUsuario.Text = TxtUsuario.Text;
            if (ObjLogin.msn == "")
             {

                 if (LblPerfil.Text == "Auxiliar")
                 {
                     FrmIngresos Objinicio = new FrmIngresos();
                     Objinicio.LblUsuario.Text = LblUsuario.Text;
                     Objinicio.LblPerfil.Text = LblPerfil.Text;
                     Objinicio.Show();
                     Hide();
                 }
                 if (LblPerfil.Text == "Administrador")
                 {
                     FrmIngresos Objinicio = new FrmIngresos();
                     Objinicio.LblUsuario.Text = LblUsuario.Text;
                     Objinicio.LblPerfil.Text = LblPerfil.Text;
                     Objinicio.Show();
                     Hide();
                 }

             }

             else
             {
                 String message = "Datos incorrectos ";
                 String caption = "Inicio Sesion";
                 var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
             }
        }
    

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {
            if (!VerificarConexionInternet())
            {
                // Si no hay conexión, mostrar mensaje
                MessageBox.Show("No hay conexión a Internet. Por favor, verifique su conexión y vuelva a intentarlo.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Salimos de la función sin intentar el login
            }

            // Si hay conexión, continuar con la consulta de login
            try
            {
                // Aquí va tu código para el login (procedimiento almacenado, conexión a la base de datos, etc.)
                Func_FrmLogin_Ingresar(); // Suponiendo que esta es tu función para el login
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error durante el inicio de sesión: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtContraseña_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!VerificarConexionInternet())
                {
                    // Si no hay conexión, mostrar mensaje
                    MessageBox.Show("No hay conexión a Internet. Por favor, verifique su conexión y vuelva a intentarlo.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Salimos de la función sin intentar el login
                }

                // Si hay conexión, continuar con la consulta de login
                try
                {
                    // Aquí va tu código para el login (procedimiento almacenado, conexión a la base de datos, etc.)
                    Func_FrmLogin_Ingresar(); // Suponiendo que esta es tu función para el login
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error durante el inicio de sesión: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        
        }
    }
}
