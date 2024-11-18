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
    public class CL_Ingresos
    {
        public String Documento, Nombre_Completo, Correo, Codigo, msn,msn1, Estado, Usuario;
        public DataTable dtDgvMaterias = new DataTable();
        public DataTable dtDgvMateriasVinculadas = new DataTable();
        public DataTable dtDgvEstados = new DataTable();
        public void SP_FrmIngresos_GuardarDocente()
        {
            CL_Conexion Obj = new CL_Conexion();
            SqlDataReader Lectura;
            SqlCommand con = new SqlCommand("SP_FrmIngresos_BuscarDocente", Obj.connection);
            con.CommandType = CommandType.StoredProcedure;
            con.Parameters.AddWithValue("@Documento", Documento);
            //con.Parameters.AddWithValue("@Codigo", Codigo);
            Obj.connection.Open();
            con.ExecuteNonQuery();
            Lectura = con.ExecuteReader();
            if (Lectura.Read() == false)
            {
                Obj.connection.Close();
                con = new SqlCommand("SP_FrmIngresos_GuardarDocente", Obj.connection);
                con.CommandType = CommandType.StoredProcedure;
                con.Parameters.AddWithValue("@Documento", Documento);
                con.Parameters.AddWithValue("@Nombre", Nombre_Completo);
                con.Parameters.AddWithValue("@Correo", Correo);
                con.Parameters.AddWithValue("@Codigo", Codigo);
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

        public void SP_FrmIngresos_ActualizarDocente()
        {
            CL_Conexion objconect = new CL_Conexion();
            SqlCommand con; SqlDataReader Lectura;
            con = new SqlCommand("SP_FrmIngresos_ActualizarDocente", objconect.connection);
            con.CommandType = CommandType.StoredProcedure;
            con.Parameters.AddWithValue("@Documento", Documento);
            con.Parameters.AddWithValue("@Nombre", Nombre_Completo);
            con.Parameters.AddWithValue("@Correo", Correo);
            con.Parameters.AddWithValue("@Codigo", Codigo);
            con.Connection.Open();
            con.ExecuteNonQuery();
            Lectura = con.ExecuteReader();
            con.Connection.Close();
        }

        public void SP_FrmIngresos_BuscarDocente()
        {
            CL_Conexion objconect = new CL_Conexion();
            SqlCommand con; SqlDataReader Lectura;
            con = new SqlCommand("SP_FrmIngresos_BuscarDocente", objconect.connection);
            con.CommandType = CommandType.StoredProcedure;
            con.Parameters.AddWithValue("@Documento", Documento);
            objconect.connection.Open();
            con.ExecuteNonQuery();
            Lectura = con.ExecuteReader();
            if (Lectura.Read() == true)
            {
                Nombre_Completo = Convert.ToString(Lectura[0]);
                Correo = Convert.ToString(Lectura[1]);
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

        public void SP_FrmIngresos_BuscarDocenteCodigo()
        {
            CL_Conexion objconect = new CL_Conexion();
            SqlCommand con; SqlDataReader Lectura;
            con = new SqlCommand("SP_FrmIngresos_BuscarDocenteCodigo", objconect.connection);
            con.CommandType = CommandType.StoredProcedure;
            con.Parameters.AddWithValue("@Codigo", Codigo);
            objconect.connection.Open();
            con.ExecuteNonQuery();
            Lectura = con.ExecuteReader();
            if (Lectura.Read() == true)
            {
                Documento = Convert.ToString(Lectura[0]);
                Nombre_Completo = Convert.ToString(Lectura[1]);
                Correo = Convert.ToString(Lectura[2]);
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

        public void SP_FrmIngresos_CargarMaterias()
        {
            CL_Conexion obj = new CL_Conexion();
            SqlCommand con = new SqlCommand("SP_FrmIngresos_CargarMaterias", obj.connection);
            con.CommandType = CommandType.StoredProcedure;
            obj.connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(con);
            da.Fill(dtDgvMaterias);
            obj.connection.Close();
        }
        public void SP_FrmIngresos_CargarEstadoIngresos()
        {
            CL_Conexion obj = new CL_Conexion();
            SqlCommand con = new SqlCommand("SP_FrmIngresos_CargarEstadoIngresos", obj.connection);
            con.Parameters.AddWithValue("@Documento", Documento);
            con.CommandType = CommandType.StoredProcedure;
            obj.connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(con);
            da.Fill(dtDgvEstados);
            obj.connection.Close();
        }

        public void SP_FrmIngresos_CargarMateriasVinculadas()
        {
            CL_Conexion obj = new CL_Conexion();
            SqlCommand con = new SqlCommand("SP_FrmIngresos_CargarMateriasVinculadas", obj.connection);
            con.Parameters.AddWithValue("@Documento", Documento);
            con.CommandType = CommandType.StoredProcedure;
            obj.connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(con);
            da.Fill(dtDgvMateriasVinculadas);
            obj.connection.Close();
        }

        public void SP_FrmIngresos_VincularMaterias()
        {
            CL_Conexion Obj = new CL_Conexion();
            SqlCommand con = new SqlCommand("SP_FrmIngresos_VincularMateria", Obj.connection);
            con.CommandType = CommandType.StoredProcedure;
            con.Parameters.AddWithValue("@Documento", Documento);
            con.Parameters.AddWithValue("@Codigo", Codigo);
            con.Connection.Open();
            con.ExecuteNonQuery();
            con.Connection.Close();
            msn = "Vinculación exitosa";
        }

        public void SP_FrmIngresos_DesvincularMaterias()
        {
            CL_Conexion Obj = new CL_Conexion();
            SqlCommand con = new SqlCommand("SP_FrmIngresos_DesvincularMateria", Obj.connection);
            con.CommandType = CommandType.StoredProcedure;
            con.Parameters.AddWithValue("@Documento", Documento);
            con.Parameters.AddWithValue("@Codigo", Codigo);
            con.Connection.Open();
            con.ExecuteNonQuery();
            con.Connection.Close();
            msn = "Desvinculación exitosa";
        }

        public void SP_FrmIngresos_VinculacionExisMateria()
        {
            CL_Conexion Obj = new CL_Conexion();
            SqlCommand con = new SqlCommand("SP_FrmIngresos_VinculacionExisMateria", Obj.connection);
            con.CommandType = CommandType.StoredProcedure;
            con.Parameters.AddWithValue("@Documento", Documento);
            con.Parameters.AddWithValue("@Codigo", Codigo);
            con.Connection.Open();
            con.ExecuteNonQuery();
            con.Connection.Close();
            msn = "Vinculación exitosa";
        }

        public void SP_FrmIngresos_BuscarMaterias()
        {
            CL_Conexion objconect = new CL_Conexion();
            SqlCommand con; SqlDataReader Lectura;
            con = new SqlCommand("SP_FrmIngresos_BuscarMateria", objconect.connection);
            con.CommandType = CommandType.StoredProcedure;
            con.Parameters.AddWithValue("@Documento", Documento);
            con.Parameters.AddWithValue("@Codigo", Codigo);          
            objconect.connection.Open();
            con.ExecuteNonQuery();
            Lectura = con.ExecuteReader();
            if (Lectura.Read() == true)
            {
                Estado = Convert.ToString(Lectura[0]);
                msn1 = "";
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

        public void SP_FrmIngresos_BuscarIngreso()
        {
            CL_Conexion objconect = new CL_Conexion();
            SqlCommand con; SqlDataReader Lectura;
            con = new SqlCommand("SP_FrmIngresos_BuscarIngreso", objconect.connection);
            con.CommandType = CommandType.StoredProcedure;
            con.Parameters.AddWithValue("@Documento", Documento);
            con.Parameters.AddWithValue("@Codigo", Codigo);
            objconect.connection.Open();
            con.ExecuteNonQuery();
            Lectura = con.ExecuteReader();
            if (Lectura.Read() == true)
            {
                Estado = Convert.ToString(Lectura[0]);
                msn1 = "";
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
        public void SP_FrmIngresos_GuardarIngresos()
            {
                CL_Conexion Obj = new CL_Conexion();
                SqlCommand con = new SqlCommand("SP_FrmIngresos_GuardarIngresos", Obj.connection);
                con.CommandType = CommandType.StoredProcedure;
                
                con.Parameters.AddWithValue("@Codigo", Codigo);
                con.Parameters.AddWithValue("@Usuario", Usuario);
                con.Parameters.AddWithValue("@Documento", Documento);
                con.Connection.Open();
                con.ExecuteNonQuery();
                con.Connection.Close();
                msn = "Ingreso exitoso";
                return;
        }

        public void SP_FrmIngresos_GuardarSalidas()
        {
            CL_Conexion objconect = new CL_Conexion();
            SqlCommand con; SqlDataReader Lectura;
            con = new SqlCommand("SP_FrmIngresos_GuardarSalidas", objconect.connection);
            con.CommandType = CommandType.StoredProcedure;
            con.Parameters.AddWithValue("@Codigo", Codigo);
            con.Parameters.AddWithValue("@Documento", Documento);
            con.Connection.Open();
            con.ExecuteNonQuery();
            Lectura = con.ExecuteReader();
            con.Connection.Close();
        }
    }
}
