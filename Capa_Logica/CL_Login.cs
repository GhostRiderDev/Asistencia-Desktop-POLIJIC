using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Conexion;

namespace Capa_Logica
{
    public class CL_Login
    {
        public String Usuario, Contraseña, Cargo, msn;

        public void SP_FrmLogin_Ingresar()
        {
            CL_Conexion objconect = new CL_Conexion();
            SqlCommand con; SqlDataReader Lectura;
            con = new SqlCommand("SP_FrmLogin_Ingresar", objconect.connection);
            con.CommandType = CommandType.StoredProcedure;
            con.Parameters.AddWithValue("@Usuario", Usuario);
            con.Parameters.AddWithValue("@Contraseña", Contraseña);
            objconect.connection.Open();
            con.ExecuteNonQuery();
            Lectura = con.ExecuteReader();
            if (Lectura.Read() == true)
            {
                Cargo = Convert.ToString(Lectura[0]);
                msn = "";
                objconect.connection.Close();
                return;
            }
            if (Lectura.Read() == false)
            {
                msn = "No hay Registro";
                objconect.connection.Close();
                return;
            }
        }
    }
}
