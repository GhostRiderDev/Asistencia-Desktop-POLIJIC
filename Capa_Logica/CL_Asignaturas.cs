using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Conexion;
using System.Data.SqlClient;
using System.Data;
namespace Capa_Logica
{
    public class CL_Asignaturas
    {
        public String Codigo, Nombre, msn;
        public void SP_FrmAsignaturas_Guardar()
        {
            CL_Conexion Obj = new CL_Conexion();
            SqlDataReader Lectura;
            SqlCommand con = new SqlCommand("SP_FrmAsignaturas_Buscar", Obj.connection);
            con.CommandType = CommandType.StoredProcedure;
            con.Parameters.AddWithValue("@Codigo", Codigo);
            Obj.connection.Open();
            con.ExecuteNonQuery();
            Lectura = con.ExecuteReader();
            if (Lectura.Read() == false)
            {
                Obj.connection.Close();
                con = new SqlCommand("SP_FrmAsignaturas_Guardar", Obj.connection);
                con.CommandType = CommandType.StoredProcedure;
                con.Parameters.AddWithValue("@Codigo", Codigo);
                con.Parameters.AddWithValue("@Nombre", Nombre);
                con.Connection.Open();
                con.ExecuteNonQuery();
                con.Connection.Close();
                msn = "";
                return;
            }
            if (Lectura.Read() == true)
            {
                Obj.connection.Close();
            }
        }

        public void SP_FrmAsignaturas_Buscar()
        {
            CL_Conexion objconect = new CL_Conexion();
            SqlCommand con; SqlDataReader Lectura;
            con = new SqlCommand("SP_FrmAsignaturas_Buscar", objconect.connection);
            con.CommandType = CommandType.StoredProcedure;
            con.Parameters.AddWithValue("@Codigo", Codigo);
            objconect.connection.Open();
            con.ExecuteNonQuery();
            Lectura = con.ExecuteReader();
            if (Lectura.Read() == true)
            {
                Nombre = Convert.ToString(Lectura[0]);
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
