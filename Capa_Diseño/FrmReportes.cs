using System;
using System.Drawing;
using Capa_Logica;
using System.Windows.Forms;
using DGVPrinterHelper;
using OfficeOpenXml;
using System.Data;
using System.IO;
using System.Collections;
using System.Linq;

namespace Capa_Diseño
{
    public partial class FrmReportes : Form
    {
        private OpenFileDialog openFileDialog;
        public FrmReportes()
        {
            InitializeComponent();
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        protected void Func_Imprimir()
        {
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "Reporte de ingreso de docentes";
            printer.SubTitle = "";
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "Politécnico Jaime Isaza Cadavid";
            printer.FooterSpacing = 15;
            //printer.PrintDataGridView(DgvIngresos);
            printer.ExportDiataToExcel(DgvIngresos);
        }
        protected void Func_FrmReportes_CargarIngresosDocumento()
        {
            CL_Reportes ObjMaterias = new CL_Reportes();
            ObjMaterias.Documento = TxtDocumento.Text;
            ObjMaterias.SP_FrmReportes_CargarIngresosDocumento();
            DgvIngresos.DataSource = ObjMaterias.dtIngresosDocumento;
        }
        protected void Func_FrmReportes_CargarIngresosFechas()
        {
            CL_Reportes ObjMaterias = new CL_Reportes();
            ObjMaterias.fecha_ini = Convert.ToDateTime(Dtinicial.Text);
            ObjMaterias.fecha_fin = Convert.ToDateTime(DtFinal.Text);
            ObjMaterias.SP_FrmReportes_CargarIngresosFechas();
            DgvIngresos.DataSource = ObjMaterias.dtIngresosFechas;
        }
        protected void Func_FrmReportes_CargarIngresos()
        {
            CL_Reportes ObjMaterias = new CL_Reportes();
            ObjMaterias.fecha_ini = Convert.ToDateTime(Dtinicial.Text);
            ObjMaterias.fecha_fin = Convert.ToDateTime(DtFinal.Text);
            ObjMaterias.SP_FrmReportes_CargarIngresos();
            DgvIngresos.DataSource = ObjMaterias.dtIngresos;
        }
        protected void Func_FrmReportes_CargarSalidasFechas()
        {
            CL_Reportes ObjMaterias = new CL_Reportes();
            ObjMaterias.fecha_ini = Convert.ToDateTime(Dtinicial.Text);
            ObjMaterias.fecha_fin = Convert.ToDateTime(DtFinal.Text);
            ObjMaterias.SP_FrmReportes_CargarSalidasFechas();
            DgvIngresos.DataSource = ObjMaterias.dtSalidas;
        }
        protected void Func_FrmReportes_Actualizar()
        {
            CL_Reportes ObjReportes = new CL_Reportes();
            ObjReportes.Codigo = Convert.ToInt32(LblRegistro.Text);
            ObjReportes.Observacion = TxtObservaciones.Text;
            ObjReportes.SP_FrmReportes_Actualizar();
            String message = "Observación actualizada";
            String caption = "Reportes";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        protected void Func_FrmReportes_BuscarObservacion()
        {
            CL_Reportes ObjReportes = new CL_Reportes();
            ObjReportes.Codigo = Convert.ToInt32(LblRegistro.Text);
            ObjReportes.SP_FrmReportes_BuscarObservacion();
            if (ObjReportes.msn == "")
            {
                TxtObservaciones.Text = ObjReportes.Observacion;
            }
            else
            {
                TxtObservaciones.Text = "Ninguna";
            }

        }
        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            Func_Imprimir();
        }

        private void FrmReportes_Load(object sender, EventArgs e)
        {

        }

        private void CmbBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbBusqueda.SelectedIndex == 0)
            {
                panel1.Visible = true;
                panel2.Visible = false;
            }
            if (CmbBusqueda.SelectedIndex == 1)
            {
                panel1.Visible = false;
                panel2.Visible = true;
            }
            if (CmbBusqueda.SelectedIndex == 2)
            {
                panel1.Visible = false;
                panel2.Visible = true;
            }
            if (CmbBusqueda.SelectedIndex == 3)
            {
                panel1.Visible = false;
                panel2.Visible = true;
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

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            if (CmbBusqueda.SelectedIndex == 1)
            {
                Func_FrmReportes_CargarIngresosFechas();
            }
            if (CmbBusqueda.SelectedIndex == 2)
            {
                Func_FrmReportes_CargarIngresos();
            }
            if (CmbBusqueda.SelectedIndex == 3)
            {
                Func_FrmReportes_CargarSalidasFechas();
            }
        }

        private void BtnBuscarPorPersona_Click(object sender, EventArgs e)
        {


        }

        private void BtnBuscarPorPersona_Click_1(object sender, EventArgs e)
        {
            Func_FrmReportes_CargarIngresosDocumento();
        }

        private void DgvIngresos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int fila = DgvIngresos.CurrentRow.Index;
            LblRegistro.Text = Convert.ToString(DgvIngresos.Rows[fila].Cells[0].Value);
            TxtNombre.Text = Convert.ToString(DgvIngresos.Rows[fila].Cells[5].Value);
            Func_FrmReportes_BuscarObservacion();
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LblRegistro.Text = "0";
            TxtNombre.Clear();
            TxtObservaciones.Clear();
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            Func_FrmReportes_Actualizar();
        }



        private void BtnCargarHorarioExcel(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                CargarDatosDesdeExcel(filePath);
            }
        }

        private void CargarDatosDesdeExcel(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                DataTable dt = new DataTable();
                var columns = new string[] { "CODIGO", "NOMBRE ASIGNATURA", "GRUPO", "CÉDULA", "DOCENTES", "DIA", "HORA", "AULA" };

                foreach (var col in columns)
                {
                    dt.Columns.Add(col);
                }

                // Cargar las filas del Excel en el DataTable
                for (int rowNum = 5; rowNum <= worksheet.Dimension.End.Row; rowNum++)
                {
                    var wsRow = worksheet.Cells[rowNum, 1, rowNum, worksheet.Dimension.End.Column];
                    // CODIGO | NOMBRE ASIGNATURA | GRUPO | CÉDULA | DOCENTES | DIA | HORA | AULA
                    if (!string.IsNullOrEmpty(wsRow[rowNum, 1].Text))
                    {
                        dt.Rows.Add(wsRow[rowNum, 1].Text, wsRow[rowNum, 2].Text, wsRow[rowNum, 3].Text, wsRow[rowNum, 5].Text, wsRow[rowNum, 6].Text, wsRow[rowNum, 7].Text, wsRow[rowNum, 8].Text, wsRow[rowNum, 9].Text);
                    }
                }
                DgvHorario.DataSource = dt;

                var query = dt.AsEnumerable()
    .GroupBy(row => new { Codigo = row.Field<string>("CODIGO"), Grupo = row.Field<string>("GRUPO"), Cedula = row.Field<string>("CÉDULA") }).Select(g =>
    {
        var row = dt.NewRow();
        row["CODIGO"] = g.Key.Codigo;
        row["GRUPO"] = g.Key.Grupo;
        row["NOMBRE ASIGNATURA"] = string.Join(", ", g.Select(r => r.Field<string>("NOMBRE ASIGNATURA")));
        row["DOCENTES"] = string.Join(", ", g.Select(r => r.Field<string>("DOCENTES")));
        row["DIA"] = string.Join(", ", g.Select(r => r.Field<string>("DIA")));
        row["HORA"] = string.Join(", ", g.Select(r => r.Field<string>("HORA")));
        row["AULA"] = string.Join(", ", g.Select(r => r.Field<string>("AULA")));
        return row;
    }).CopyToDataTable();

                //Agrupo por codigo y por grupo
                foreach (DataRow item in query.Rows)
                {
                    var codigo = item["CODIGO"];
                    var grupo = item["GRUPO"];
                    var nombreAsignatura = item["NOMBRE ASIGNATURA"].ToString().Split(',')[0];
                    var docentes = item["DOCENTES"].ToString().Split(',')[0];
                    var dias = item["DIA"].ToString().Split(',');
                    var horas = item["HORA"].ToString().Split(',');
                    var aulas = item["AULA"].ToString().Split(',');
                    Console.WriteLine(codigo);
                    Console.WriteLine(grupo);
                    Console.WriteLine(nombreAsignatura);
                    Console.WriteLine(docentes);
                    dias = dias.Select(x => x.Split('-')[1].Trim()
                        .Replace("Á", "A")
                        .Replace("É", "E")
                        .Replace("Í", "I")
                        .Replace("Ó", "O")
                        .Replace("Ú", "U")
                    ).ToArray();
                    horas = horas.Select(x => x.Trim()).ToArray();
                    aulas = aulas.Select(x => x.Trim()).ToArray();
                }

            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
