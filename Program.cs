using Project_Melody.Instruments;
using Project_Melody;
using static Project_Melody.MusicBase;

class Program
{
    static void Main()
    {
        try
        {
            MidiGenerator.GenerateMidiFile("chord.mid", BasicNote.CSharp_DFlat, ScaleType.Shang, InstrumentFactory.GetInstrument("6 String Guitar"), 120, "chord", 4); // Increased number of notes to 10
            MidiGenerator.GenerateMidiFile("arpeggio.mid", BasicNote.CSharp_DFlat, ScaleType.Shang, InstrumentFactory.GetInstrument("6 String Guitar"), 120, "arpeggio", 4); // Increased number of notes to 10
            MidiGenerator.GenerateMidiFile("melody.mid", BasicNote.CSharp_DFlat, ScaleType.Shang, InstrumentFactory.GetInstrument("6 String Guitar"), 120, "melody", 35); // Increased number of notes to 10
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
