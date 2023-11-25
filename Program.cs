namespace Project_Melody
{
    class Program
    {
        static void Main()
        {
            // Assuming standard MIDI timing, 120 Tempo using 120 ticks per quarter note
            MidiChordSequenceGenerator midiChordSequenceGenerator = new MidiChordSequenceGenerator(120, 120);

            // Example parameters: "Test.mid", root note C4 (MIDI note number 60), minor scale, generating 6 chords, isArpeggio
            midiChordSequenceGenerator.GenerateMidiFile("Test.mid", 60, ChordGenerator.ScaleType.Minor, 12, true, true);
            // MelodyGeneration.CreateMelody(true, "dropA");
            // DrumGeneration.CreateDrumPattern();
        }
    }
}