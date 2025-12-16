namespace AgendaClinica.Models
{
    // REGISTRO: cita agendada en un slot
    public record Cita(int Id, int PacienteId, SlotHorario Slot, string Motivo);
}
