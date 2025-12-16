using System;
using AgendaClinica.Services;

var sistema = new ClinicaAgenda();

while (true)
{
    Console.WriteLine("\n===============================");
    Console.WriteLine("  AGENDA DE TURNOS - CLÍNICA");
    Console.WriteLine("===============================");
    Console.WriteLine("1) Registrar paciente");
    Console.WriteLine("2) Listar pacientes");
    Console.WriteLine("3) Agendar cita");
    Console.WriteLine("4) Cancelar cita");
    Console.WriteLine("5) Mostrar agenda completa");
    Console.WriteLine("6) Mostrar citas por día");
    Console.WriteLine("7) Reporte general");
    Console.WriteLine("0) Salir");
    Console.Write("Seleccione: ");

    var opcion = Console.ReadLine();

    try
    {
        switch (opcion)
        {
            case "1":
                Console.Write("Nombre: ");
                var nombre = Console.ReadLine() ?? "";

                Console.Write("Cédula: ");
                var cedula = Console.ReadLine() ?? "";

                Console.Write("Teléfono: ");
                var telefono = Console.ReadLine() ?? "";

                var p = sistema.RegistrarPaciente(nombre, cedula, telefono);
                Console.WriteLine($"Paciente registrado con ID {p.Id}");
                break;

            case "2":
                sistema.ListarPacientes();
                break;

            case "3":
                Console.Write("ID del paciente: ");
                int pacienteId = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Día (0=Lun … 6=Dom): ");
                int dia = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Bloque (0–7): ");
                int bloque = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Motivo: ");
                var motivo = Console.ReadLine() ?? "";

                sistema.AgendarCita(pacienteId, dia, bloque, motivo);
                Console.WriteLine("Cita registrada.");
                break;

            case "4":
                Console.Write("Día (0–6): ");
                int d = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Bloque (0–7): ");
                int b = int.Parse(Console.ReadLine() ?? "0");

                bool resultado = sistema.CancelarCita(d, b);
                Console.WriteLine(resultado ? "Cita cancelada." : "No había cita.");
                break;

        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}
