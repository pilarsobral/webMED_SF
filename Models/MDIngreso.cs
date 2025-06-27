using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webMED.Models
{
    public class MDIngreso
    {
        public Medico Medico { get; set; }
        public Paciente Paciente { get; set; }

        public List<PacienteCondicion> Condiciones { get; set; }
        public List<PacienteMedicamento> Medicamentos { get; set; }
        public string IDPaciente { get; set; }

        public void CargarPaciente(Paciente pac)
        {
            if (Paciente == null)
                Paciente = new Paciente();
            Paciente.Cargar(pac); 
        }
        public void CargarCondiciones(List<PacienteCondicion> cond)
        {
            if (Condiciones == null)
                Condiciones = new List<PacienteCondicion>();
            else
                Condiciones.Clear();

            Condiciones.AddRange(cond);
        }

        public void CargarMedicamentos(List<PacienteMedicamento> med)
        {
            if (Medicamentos == null)
                Medicamentos = new List<PacienteMedicamento>();
            else
                Medicamentos.Clear();

            Medicamentos.AddRange(med);
        }

        public void CargarDrogas(List<PacienteDroga> med)
        {
            foreach (var m in med)
            {
                PacienteMedicamento p = new PacienteMedicamento();
                p.ID_Droga = m.ID_Droga;
                p.DescripcionDroga = m.DescripcionDroga;
                p.FechaDesde = m.FechaDesde;
                p.FechaHasta = m.FechaHasta;
                p.Estado = "Pediente";
                Medicamentos.Add(p);
            }
        }
    }
}
