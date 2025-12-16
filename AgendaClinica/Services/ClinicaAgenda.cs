using AgendaClinica.Models;

namespace AgendaClinica.Services
{
    public class ClinicaAgenda
    {
        // MATRIZ: Agenda semanal [7 días x 8 bloques]
        private readonly Cita?[,] agenda = new Cita?[7, 8];

        // VECTORES: pacientes y citas
        private readonly Paciente[] pacientes = new Paciente[50];
        private readonly Cita[] citas = new Cita[200];

        private int pacientesCount = 0;
        private int citasCount = 0;

        private int nextPacienteId = 1;
        private int nextCitaId = 1;

        public readonly string[] Dias =
        {
            "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo"
        };

        public readonly string[] Horas =
        {
            "08:00","09:00","10:00","11:00","14:00","15:00","16:00","17:00"
        };

        // =========================
        // PACIENTES
        // =========================
        public Paciente RegistrarPaciente(string nombre, string cedula, string telefono)
        {
            if (pacientesCount >= pacientes.Length)
                throw new InvalidOperationException("Capacidad de pacientes llena.");

            // Validación simple: no duplicar cédula
            for (int i = 0; i < pacientesCount; i++)
            {
                if (pacientes[i].Cedula == cedula.Trim())
                    throw new InvalidOperationException("Ya existe un paciente con esa cédula.");
            }

            var p = new Paciente(nextPacienteId++, nombre.Trim(), cedula.Trim(), telefono.Trim());
            pacientes[pacientesCount++] = p;
            return p;
        }

        public void ListarPacientes()
        {
            Console.WriteLine("\n=== PACIENTES REGISTRADOS ===");
            if (pacientesCount == 0)
            {
                Console.WriteLine("No hay pacientes.");
                return;
            }

            for (int i = 0; i < pacientesCount; i++)
            {
                var p = pacientes[i];
                Console.WriteLine($"[{p.Id}] {p.Nombre} | CI: {p.Cedula} | Tel: {p.Telefono}");
            }
        }

        private bool ExistePaciente(int pacienteId)
        {
            for (int i = 0; i < pacientesCount; i++)
                if (pacientes[i].Id == pacienteId) return true;
            return false;
        }

        private Paciente? GetPaciente(int pacienteId)
        {
            for (int i = 0; i < pacientesCount; i++)
                if (pacientes[i].Id == pacienteId) return pacientes[i];
            return null;
        }

        // =========================
        // CITAS
        // =========================
        public Cita AgendarCita(int pacienteId, int diaIndex, int bloqueIndex, string motivo)
        {
            if (!ExistePaciente(pacienteId))
                throw new InvalidOperationException("El paciente no existe.");

            if (diaIndex < 0 || diaIndex > 6) throw new ArgumentOutOfRangeException(nameof(diaIndex));
            if (bloqueIndex < 0 || bloqueIndex > 7) throw new ArgumentOutOfRangeException(nameof(bloqueIndex));

            if (agenda[diaIndex, bloqueIndex] != null)
                throw new InvalidOperationException("Ese turno ya está ocupado.");

            if (citasCount >= citas.Length)
                throw new InvalidOperationException("Capacidad de citas llena.");

            var cita = new Cita(
                nextCitaId++,
                pacienteId,
                new SlotHorario(diaIndex, bloqueIndex),
                motivo.Trim()
            );

            agenda[diaIndex, bloqueIndex] = cita;
            citas[citasCount++] = cita;

            return cita;
        }

        public bool CancelarCita(int diaIndex, int bloqueIndex)
        {
            if (diaIndex < 0 || diaIndex > 6) return false;
            if (bloqueIndex < 0 || bloqueIndex > 7) return false;

            if (agenda[diaIndex, bloqueIndex] == null) return false;

            agenda[diaIndex, bloqueIndex] = null;
            return true;
        }

        // =========================
        // REPORTERÍA
        // =========================
        public void MostrarAgendaCompleta()
        {
            Console.WriteLine("\n=== AGENDA SEMANAL (MATRIZ) ===");
            Console.Write("Hora\\Día".PadRight(12));
            for (int d = 0; d < 7; d++)
                Console.Write(Dias[d].PadRight(14));
            Console.WriteLine();

            for (int b = 0; b < 8; b++)
            {
                Console.Write(Horas[b].PadRight(12));
                for (int d = 0; d < 7; d++)
                {
                    var cita = agenda[d, b];
                    string cell = cita == null ? "Libre" : $"Cita#{cita.Id}";
                    Console.Write(cell.PadRight(14));
                }
                Console.WriteLine();
            }
        }

        public void MostrarCitasPorDia(int diaIndex)
        {
            Console.WriteLine($"\n=== CITAS DEL {Dias[diaIndex].ToUpper()} ===");
            bool hay = false;

            for (int b = 0; b < 8; b++)
            {
                var cita = agenda[diaIndex, b];
                if (cita != null)
                {
                    hay = true;
                    var p = GetPaciente(cita.PacienteId);
                    Console.WriteLine($"{Horas[b]} -> Cita#{cita.Id} | Paciente: {p?.Nombre} | Motivo: {cita.Motivo}");
                }
            }

            if (!hay) Console.WriteLine("No hay citas ese día.");
        }

        public void ReporteGeneral()
        {
            int ocupados = 0;
            int totalSlots = 7 * 8;

            for (int d = 0; d < 7; d++)
                for (int b = 0; b < 8; b++)
                    if (agenda[d, b] != null) ocupados++;

            Console.WriteLine("\n=== REPORTE GENERAL ===");
            Console.WriteLine($"Pacientes registrados: {pacientesCount}");
            Console.WriteLine($"Citas creadas: {citasCount}");
            Console.WriteLine($"Slots totales: {totalSlots}");
            Console.WriteLine($"Slots ocupados: {ocupados}");
            Console.WriteLine($"Slots libres: {totalSlots - ocupados}");
        }
    }
}
