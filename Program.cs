using Project_Melody.Instruments;
using static ChordGenerator;

namespace Project_Melody
{
    class Program
    {
        static void Main()
        {
            // Assuming standard MIDI timing, 120 Tempo using 120 ticks per quarter note
            MidiChordSequenceGenerator midiChordSequenceGenerator = new MidiChordSequenceGenerator(120, 120);

            // Example parameters: "Test.mid", root note C3, Shang scale, generating 12 chords, not arpeggio, not includeBothChordAndArpeggio
            midiChordSequenceGenerator.GenerateMidiFile("Test1.mid", BasicNote.C, ScaleType.Zhi, false, false, InstrumentFactory.GetInstrument("6 String Guitar"), 12);
            midiChordSequenceGenerator.GenerateMidiFile("Test2.mid", BasicNote.C, ScaleType.Zhi, false, false, InstrumentFactory.GetInstrument("6 String Guitar"), 12);
            midiChordSequenceGenerator.GenerateMidiFile("Test3.mid", BasicNote.C, ScaleType.Zhi, true, false, InstrumentFactory.GetInstrument("6 String Guitar"), 4);
            midiChordSequenceGenerator.GenerateMidiFile("Test4.mid", BasicNote.C, ScaleType.Zhi, true, true, InstrumentFactory.GetInstrument("6 String Guitar"), 4);
        }
    }
}