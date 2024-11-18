using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;
namespace Capa_Conexion
{
    public class CL_Conexion
    {
        public SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=DBAsistencia; persist security info=False;user id=sa;pwd=12345678");
    }
}

//Observaciones a tener en cuenta DESKTOP-Q4Q4MR9
//Relaizar manuales de usuario L44973
//Recuperación de contraseñas
//Hacer modulo web
//Consultar topologia de conexión del lector rfid y verificar el trafico del sistema con el lector