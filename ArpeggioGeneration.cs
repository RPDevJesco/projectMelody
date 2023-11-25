using NAudio.Midi;

namespace Project_Melody
{
    public class ArpeggioGeneration
    {
        public static void CreateArpeggios()
        {
            var chords = new Dictionary<string, List<int>>
            {
                { "CMajor", new List<int> { 60, 64, 67 } }, // C, E, G
                { "AMinor", new List<int> { 57, 60, 64 } }, // A, C, E
                { "FMajor", new List<int> { 53, 57, 60 } }, // F, A, C
                { "GMajor", new List<int> { 55, 59, 62 } }, // G, B, D
                { "DMinor", new List<int> { 50, 53, 57 } }, // D, F, A
                { "EMajor", new List<int> { 52, 56, 59 } }  // E, G#, B
            };


            Random random = new Random();
            var selectedChordKey = chords.Keys.ElementAt(random.Next(chords.Count));
            var selectedChord = chords[selectedChordKey];
            string fileName = $"{selectedChordKey}Arpeggio.mid";
            GenerateArpeggio(selectedChord, fileName);
        }

        static void GenerateArpeggio(List<int> chord, string fileName)
        {
            MidiEventCollection midiEvents = new MidiEventCollection(0, 960);
            long absoluteTime = 0;

            foreach (int midiNoteNumber in chord)
            {
                var noteName = GetNoteName(midiNoteNumber);

                Console.WriteLine($"Adding Note: {noteName}, MIDI Number: {midiNoteNumber}");

                // Note On event
                var noteOnEvent = new NoteOnEvent((int)absoluteTime, 1, midiNoteNumber, 127, 0);
                midiEvents.AddEvent(noteOnEvent, 0);

                absoluteTime += 480; // Set the note duration

                // Note Off event
                var noteOffEvent = new NoteEvent((int)absoluteTime, 1, MidiCommandCode.NoteOff, midiNoteNumber, 0);
                midiEvents.AddEvent(noteOffEvent, 0);

                absoluteTime += 240; // Set the time between notes
            }

            // End Track event
            midiEvents.AddEvent(new MetaEvent(MetaEventType.EndTrack, 0, (int)absoluteTime), 0);

            // Write to file
            MidiFile.Export(fileName, midiEvents);

            Console.WriteLine($"MIDI file '{fileName}' has been created!");
        }

        private static string GetNoteName(int midiNumber)
        {
            string[] noteNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
            int octave = (midiNumber / 12) - 1;
            int noteIndex = midiNumber % 12;
            return $"{noteNames[noteIndex]}{octave}";
        }
    }
}
