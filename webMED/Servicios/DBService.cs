using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace webMED.Models
{
    public class DBService
    {
        private readonly string _connectionString;

        public DBService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public Usuario ObtenerUsuario(String id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Usuario WHERE ID_Usuario = '" + id + "'", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Usuario usr = new Usuario();
                    usr.ID_Usuario = (string)reader["ID_Usuario"];
                    usr.Password = (string) reader["Password"];
                    usr.Tipo = (string) reader["Tipo_Usuario"];
                    return usr;
                }

            }
            return null; 
        }


        public Paciente ObtenerPaciente(decimal grupo, decimal id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Paciente WHERE ID_GrupoSocio = " + grupo.ToString() + " AND ID_Socio = " + id.ToString(), connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Paciente pac = new Paciente();

                    pac.ID_GrupoSocio = (decimal)reader["ID_GrupoSocio"]; 
                    pac.ID_Socio = (decimal)reader["ID_Socio"]; 
                    pac.Nombre = (string)reader["Nombre"];
                    pac.Apellido = (string)reader["Apellido"];
                    pac.FechaNacimiento = (DateTime)reader["FechaNacimiento"];
                    pac.FechaIngreso = (DateTime)reader["FechaIngreso"];
                    pac.Telefono = (string)reader["Telefono"];
                    pac.Mail = (string)reader["Mail"];

                    return pac;
                }

            }
            return null;
        }


        public List<Droga> ObtenerDrogas()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Droga", connection);
                SqlDataReader reader = command.ExecuteReader();

                List<Droga> dg = new List<Droga>();
                while (reader.Read())
                {
                    dg.Add(new Droga((string)reader["ID_Droga"], (string)reader["Descripcion"]));
                }
                return dg;
            }
            return null;
        }
        public List<PacienteCondicion> ObtenerCondiciones(decimal grupo, decimal id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT pc.id_Condicion, pc.Estado, pc.FechaDesde, pc.FechaHasta, coalesce( pc.Observaciones, ' ') Observaciones, c.Descripcion FROM Paciente_Condicion pc, Condicion c WHERE pc.ID_Condicion = c.ID_Condicion AND  pc.ID_GrupoSocio = " + grupo.ToString() + " AND pc.ID_Socio = " + id.ToString() + " ORDER BY Estado, FechaDesde, FechaHasta", connection);
                SqlDataReader reader = command.ExecuteReader();
                List<PacienteCondicion> lst = new List<PacienteCondicion>(); 

                while (reader.Read())
                {
                    PacienteCondicion pac = new PacienteCondicion();
                    pac.ID_Condicion = (string)reader["ID_Condicion"];
    
                    if (((string)reader["Estado"]).Trim().Equals("AC"))
                        pac.Estado = "Vigente";
                    else
                        pac.Estado = "Finalizado";

                    pac.FechaDesde = (DateTime)reader["FechaDesde"];
                    pac.FechaHasta = (DateTime)reader["FechaHasta"];
                    pac.Observaciones = (string)reader["Observaciones"];
                    pac.Descripcion = (string)reader["Descripcion"];

                    lst.Add(pac);
                }
                return lst;

            }
            return null;
        }

        public List<PacienteMedicamento> ObtenerMedicamentos(decimal grupo, decimal id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();


                SqlCommand command = new SqlCommand("SELECT pm.id_droga, d.descripcion DescripcionDroga, pm.id_medicamento,(m.descripcion + ' - lab. ' + m.Laboratorio + '(' + m.Presentacion + ')') DescripcionMedicamento, pm.Estado,  pm.fechadesde, pm.fechahasta" +
                " FROM paciente_medicamento pm, Medicamento m, Droga d WHERE pm.id_medicamento = m.id_medicamento and pm.id_droga = d.id_droga  AND  pm.ID_GrupoSocio = " + grupo.ToString() + " AND pm.ID_Socio = " + id.ToString() + " ORDER BY FechaDesde, FechaHasta", connection);
                SqlDataReader reader = command.ExecuteReader();
                List<PacienteMedicamento> lst = new List<PacienteMedicamento>();

                while (reader.Read())
                {
                    PacienteMedicamento pac = new PacienteMedicamento();

                    pac.ID_Medicamento = (string)reader["ID_Medicamento"];
                    pac.ID_Droga = (string)reader["ID_Droga"];
                    pac.DescripcionMedicamento = (string)reader["DescripcionMedicamento"];
                    pac.DescripcionDroga = (string)reader["DescripcionDroga"];

                    if (((string)reader["Estado"]).Trim().Equals("AC"))
                        pac.Estado = "Vigente";
                    else
                        pac.Estado = "Finalizado";

                    pac.FechaDesde = (DateTime)reader["FechaDesde"];
                    pac.FechaHasta = (DateTime)reader["FechaHasta"];

                    lst.Add(pac);
                }
                return lst;

            }
            return null;
        }

        public List<PacienteDroga> ObtenerDrogas(decimal grupo, decimal id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT pm.id_droga, d.descripcion DescripcionDroga, pm.Estado,  pm.fechadesde, pm.fechahasta" +
                " FROM paciente_droga pm, Droga d WHERE pm.id_droga = d.id_droga  AND  pm.ID_GrupoSocio = " + grupo.ToString() + " AND pm.ID_Socio = " + id.ToString() + " ORDER BY FechaDesde, FechaHasta", connection);
                SqlDataReader reader = command.ExecuteReader();
                List<PacienteDroga> lst = new List<PacienteDroga>();

                while (reader.Read())
                {
                    PacienteDroga pac = new PacienteDroga();

                    pac.ID_Droga = (string)reader["ID_Droga"];
                    pac.DescripcionDroga = (string)reader["DescripcionDroga"];

                    pac.FechaDesde = (DateTime)reader["FechaDesde"];
                    pac.FechaHasta = (DateTime)reader["FechaHasta"];

                    lst.Add(pac);
                }
                return lst;

            }
            return null;
        }


        public List<DrogaCondicion> ObtenerCondicionesDroga(string droga)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("select  dc.id_droga, d.descripcion DescripcionDroga, dc.id_condicion, c.descripcion DescripcionCondicion from droga_condicion dc, condicion c , droga d where dc.id_droga = '" + droga.Trim() + "'  and dc.estado = 'AC'  and dc.id_condicion = c.id_condicion and dc.id_droga = d.id_droga", connection);
                SqlDataReader reader = command.ExecuteReader();
                List<DrogaCondicion> lst = new List<DrogaCondicion>();

                while (reader.Read())
                {
                    DrogaCondicion pac = new DrogaCondicion();

                    pac.ID_Droga = (string)reader["ID_Droga"];
                    pac.DescripcionDroga = (string)reader["DescripcionDroga"];
                    pac.ID_Condicion = (string)reader["ID_Condicion"];
                    pac.DescripcionCondicion = (string)reader["DescripcionCondicion"];

                    lst.Add(pac);
                }
                return lst;

            }
            return null;
        }

        public void IngresarDroga(PacienteDroga pd)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string error = ""; 
                using (SqlCommand command = new SqlCommand(@"INSERT INTO Paciente_Droga 
                                (ID_GrupoSocio, ID_Socio, ID_Droga, Estado, FechaDesde, FechaHasta, Observaciones) 
                        VALUES (@ID_GrupoSocio, @ID_Socio, @ID_Droga, 'PD', @FechaDesde, @FechaHasta, NULL)", connection))
                {
                    command.Parameters.AddWithValue("@ID_GrupoSocio", pd.ID_GrupoSocio);
                    command.Parameters.AddWithValue("@ID_Socio", pd.ID_Socio);
                    command.Parameters.AddWithValue("@ID_Droga", pd.ID_Droga);

                    // FechaDesde como DATE
                    SqlParameter paramDesde = new SqlParameter("@FechaDesde", SqlDbType.Date);
                    paramDesde.Value = pd.FechaDesde.Date;
                    command.Parameters.Add(paramDesde);

                    // FechaHasta como DATE, si es nullable manejalo así:
                    SqlParameter paramHasta = new SqlParameter("@FechaHasta", SqlDbType.Date);
                    paramHasta.Value = pd.FechaHasta.Date ;
                    command.Parameters.Add(paramHasta);

                    try { int i = command.ExecuteNonQuery(); }
                    catch (Exception e) { error = "ERROR " + e.Message;  }
                    
                }
            }
        }
        public void CargarMedico(Medico md)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Medico WHERE ID_Usuario = '" + md.ID_Usuario + "'", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    md.Matricula = (string)reader["Matricula"];
                    md.Nombre = (string)reader["Nombre"];
                    md.Apellido = (string)reader["Apellido"];
                    md.Especialidad = (string)reader["Especialidad"];
                }
            }
        }
    }
}
