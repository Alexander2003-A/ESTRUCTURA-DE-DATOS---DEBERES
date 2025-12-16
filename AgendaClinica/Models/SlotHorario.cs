namespace AgendaClinica.Models
{
    // ESTRUCTURA: representa un bloque de la agenda (día + bloque horario)
    public struct SlotHorario
    {
        public int DiaIndex { get; }
        public int BloqueIndex { get; }

        public SlotHorario(int diaIndex, int bloqueIndex)
        {
            DiaIndex = diaIndex;
            BloqueIndex = bloqueIndex;
        }
    }
}
