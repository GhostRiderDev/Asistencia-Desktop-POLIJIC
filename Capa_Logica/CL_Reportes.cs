using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Capa_Conexion;

// Comentario para subir al repositorio 
// funciono
namespace Capa_Logica
{
    public class CL_Reportes
    {
        public DataTable dtIngresosDocumento = new DataTable();
        public DataTable dtIngresosFechas = new DataTable();
        public DataTable dtIngresos = new DataTable();
        public DataTable dtHorario = new DataTable();
        public DataTable dtSalidas = new DataTable();
        public DateTime fecha_ini = new DateTime();
        public DateTime fecha_fin = new DateTime();
        public String Documento, Observacion, msn;
        public int Codigo;

        public void SP_FrmReportes_CargarIngresosDocumento()
        {
            CL_Conexion obj = new CL_Conexion();
            SqlCommand con = new SqlCommand("SP_FrmReportes_BuscarIngresos", obj.connection);
            con.CommandType = CommandType.StoredProcedure;
            con.Parameters.AddWithValue("@Documento", Documento);
            obj.connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(con);
            da.Fill(dtIngresosDocumento);
            obj.connection.Close();
            //SP_FrmReportes_BuscarHorarioProfesor();
        }

        public void SP_FrmReportes_BuscarHorarioProfesor()
        {
            CL_Conexion conn = new CL_Conexion();

            SqlCommand cmd = new SqlCommand("SP_FrmReportes_BuscarHorarioProfesor", conn.connection);

            cmd.CommandType = CommandType.StoredProcedure;

            // Quiero traer el horario de un profesor en especifico e imprimirlo por Console.WriteLine no quiero hacer fill de un datatable

            conn.connection.Open();

            // Hay que pasarle el documento del profesor

            cmd.Parameters.AddWithValue("@Documento", Documento);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(reader["DiaSemana"].ToString() + "*******\n");
            }

            conn.connection.Close();
        }
        public void SP_FrmReportes_CargarIngresosFechas()
        {
            CL_Conexion obj = new CL_Conexion();
            SqlCommand con = new SqlCommand("SP_FrmReportes_BuscarIngresosFechas", obj.connection);
            con.CommandType = CommandType.StoredProcedure;
            con.Parameters.AddWithValue("@fecha_ini", fecha_ini);
            con.Parameters.AddWithValue("@fecha_fin", fecha_fin);
            obj.connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(con);
            da.Fill(dtIngresosFechas);
            obj.connection.Close();
        }


        public void SP_FrmReportes_CargarIngresos()
        {
            CL_Conexion obj = new CL_Conexion();
            SqlCommand con = new SqlCommand("SP_FrmReportes_IngresosFechas", obj.connection);
            con.CommandType = CommandType.StoredProcedure;
            con.Parameters.AddWithValue("@fecha_ini", fecha_ini);
            con.Parameters.AddWithValue("@fecha_fin", fecha_fin);
            obj.connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(con);
            da.Fill(dtIngresos);
            obj.connection.Close();
        }

        public DataTable SP_FrmReportes_CargarIngresoByDate(string date)
        {
            CL_Conexion obj = new CL_Conexion();
            SqlCommand con = new SqlCommand("SP_FrmReportes_IngresosPorDia", obj.connection);
            con.CommandType = CommandType.StoredProcedure;
            con.Parameters.AddWithValue("@fecha", date);
            obj.connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            obj.connection.Close();
            return dt;
        }
        public void SP_FrmReportes_CargarSalidasFechas()
        {
            CL_Conexion obj = new CL_Conexion();
            SqlCommand con = new SqlCommand("SP_FrmReportes_SalidasFechas", obj.connection);
            con.CommandType = CommandType.StoredProcedure;
            con.Parameters.AddWithValue("@fecha_ini", fecha_ini);
            con.Parameters.AddWithValue("@fecha_fin", fecha_fin);
            obj.connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(con);
            da.Fill(dtSalidas);
            obj.connection.Close();
        }



        public void SP_FrmReportes_Actualizar()
        {
            CL_Conexion objconect = new CL_Conexion();
            SqlCommand con; SqlDataReader Lectura;
            con = new SqlCommand("SP_FrmReportes_Actualizar", objconect.connection);
            con.CommandType = CommandType.StoredProcedure;
            con.Parameters.AddWithValue("@Codigo", Codigo);
            con.Parameters.AddWithValue("@Observacion", Observacion);
            con.Connection.Open();
            con.ExecuteNonQuery();
            Lectura = con.ExecuteReader();
            con.Connection.Close();
        }
        public void SP_FrmReportes_BuscarObservacion()
        {
            CL_Conexion objconect = new CL_Conexion();
            SqlCommand con; SqlDataReader Lectura;
            con = new SqlCommand("SP_FrmReportes_BuscarObservacion", objconect.connection);
            con.CommandType = CommandType.StoredProcedure;
            con.Parameters.AddWithValue("@Codigo", Codigo);
            objconect.connection.Open();
            con.ExecuteNonQuery();
            Lectura = con.ExecuteReader();
            if (Lectura.Read() == true)
            {
                Observacion = Convert.ToString(Lectura[0]);
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
