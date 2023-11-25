using NAudio.Midi;

namespace Project_Melody
{
    public class MelodyGeneration
    {
        public static void CreateMelody(bool forGuitar = false, string tuning = "standard")
        {
            var fullRange = Enumerable.Range(21, 96).ToList(); // Full range from A0 to G#8/Ab8

            List<int> guitarRange;
            switch (tuning.ToLower())
            {
                case "7string":
                    guitarRange = Enumerable.Range(35, 30).ToList(); // B1 (35) to E4 (64) for 7-string standard
                    break;
                case "dropA":
                    guitarRange = Enumerable.Range(33, 32).ToList(); // A1 (33) to E4 (64) for 7-string drop A
                    break;
                default:
                    guitarRange = Enumerable.Range(40, 25).ToList(); // E2 (40) to E4 (64) for standard 6-string
                    break;
            }

            var availableNotes = forGuitar ? guitarRange : fullRange;

            // Convert scale from note names to MIDI values
            var midiScales = ConvertScalesToMidi(PitchClass.scales, availableNotes);

            // Select a scale randomly.
            Random random = new Random();
            var selectedScaleKey = midiScales.Keys.ElementAt(random.Next(midiScales.Count));
            var selectedScale = midiScales[selectedScaleKey];

            string fileName = $"{selectedScaleKey}Melody.mid";

            GenerateMelodyWithVariedRhythmPatterns(selectedScale, fileName, 240);
        }

        private static Dictionary<string, List<int>> ConvertScalesToMidi(Dictionary<string, List<string>> scales, List<int> availableNotes)
        {
            var midiScales = new Dictionary<string, List<int>>();
            foreach (var scale in scales)
            {
                midiScales[scale.Key] = scale.Value
                    .SelectMany(note => PitchClass.pitchClassToMidi[note])
                    .Where(midiNote => availableNotes.Contains(midiNote))
                    .ToList();
            }
            return midiScales;
        }

        static void GenerateMelody(List<int> scale, string fileName)
        {
            Random random = new Random();
            MidiEventCollection midiEvents = new MidiEventCollection(0, 960);
            long absoluteTime = 0;

            for (int i = 0; i < 64; i++)
            {
                int noteIndex = random.Next(scale.Count);
                int midiNoteNumber = scale[noteIndex];
                var pitchClass = GetPitchClass(midiNoteNumber);

                Console.WriteLine($"Adding Note: {pitchClass}, MIDI Number: {midiNoteNumber}");

                // Note On event
                var noteOnEvent = new NoteOnEvent((int)absoluteTime, 1, midiNoteNumber, 127, 0);
                midiEvents.AddEvent(noteOnEvent, 0);

                absoluteTime += 480;

                // Note Off event
                var noteOffEvent = new NoteEvent((int)absoluteTime, 1, MidiCommandCode.NoteOff, midiNoteNumber, 0);
                midiEvents.AddEvent(noteOffEvent, 0);
            }

            // End Track event
            midiEvents.AddEvent(new MetaEvent(MetaEventType.EndTrack, 0, (int)absoluteTime), 0);

            // Write to file
            MidiFile.Export(fileName, midiEvents);

            Console.WriteLine($"MIDI file '{fileName}' has been created!");
        }

        static void GenerateMelodyDynamics(List<int> scale, string fileName)
        {
            Random random = new Random();
            MidiEventCollection midiEvents = new MidiEventCollection(0, 960);
            long absoluteTime = 0;

            for (int i = 0; i < 64; i++)
            {
                int midiNoteNumber = scale[i];
                int velocity = random.Next(60, 127); // Dynamic variation
                int duration = new int[] { 120, 240, 480 }[random.Next(3)]; // Articulation variation (e.g., different note lengths)

                // Note On event
                var noteOnEvent = new NoteOnEvent((int)absoluteTime, 1, midiNoteNumber, velocity, 0);
                midiEvents.AddEvent(noteOnEvent, 0);

                absoluteTime += duration;

                // Note Off event
                var noteOffEvent = new NoteEvent((int)absoluteTime, 1, MidiCommandCode.NoteOff, midiNoteNumber, 0);
                midiEvents.AddEvent(noteOffEvent, 0);
            }

            // End Track event
            midiEvents.AddEvent(new MetaEvent(MetaEventType.EndTrack, 0, (int)absoluteTime), 0);

            // Write to file
            MidiFile.Export(fileName, midiEvents);

            Console.WriteLine($"MIDI file '{fileName}' has been created!");
        }

        static void GenerateMelodyWithChords(string progressionKey, string fileName)
        {
            Random random = new Random();
            MidiEventCollection midiEvents = new MidiEventCollection(0, 960);
            long absoluteTime = 0;

            var progression = PitchClass.chordProgressions[progressionKey];

            foreach (var chord in progression)
            {
                for (int i = 0; i < 64; i++) // For each chord, generate 4 notes
                {
                    int midiNoteNumber = chord[random.Next(chord.Count)];
                    int duration = new int[] { 120, 240, 480 }[random.Next(3)];

                    var noteOnEvent = new NoteOnEvent((int)absoluteTime, 1, midiNoteNumber, 100, 0);
                    midiEvents.AddEvent(noteOnEvent, 0);

                    absoluteTime += duration;

                    var noteOffEvent = new NoteEvent((int)absoluteTime, 1, MidiCommandCode.NoteOff, midiNoteNumber, 0);
                    midiEvents.AddEvent(noteOffEvent, 0);
                }
            }

            // End Track event
            midiEvents.AddEvent(new MetaEvent(MetaEventType.EndTrack, 0, (int)absoluteTime), 0);

            // Write to file
            MidiFile.Export(fileName, midiEvents);

            Console.WriteLine($"MIDI file '{fileName}' has been created!");
        }

        static void GenerateMelodyWithVariedRhythmPatterns(List<int> scale, string fileName, int totalNotes)
        {
            Random random = new Random();
            MidiEventCollection midiEvents = new MidiEventCollection(0, 960);
            long absoluteTime = 0;
            int notesAdded = 0;

            while (notesAdded < totalNotes)
            {
                // Select a random rhythm pattern
                var rhythmPattern = PitchClass.rhythmPatterns[random.Next(PitchClass.rhythmPatterns.Count)];

                foreach (var duration in rhythmPattern)
                {
                    if (scale.Count == 0 || notesAdded >= totalNotes) break; // In case of an empty scale or note limit reached

                    int midiNoteNumber = scale[random.Next(scale.Count)];
                    int velocity = 100; // Fixed velocity for simplicity

                    // Note On event
                    var noteOnEvent = new NoteOnEvent((int)absoluteTime, 1, midiNoteNumber, velocity, 0);
                    midiEvents.AddEvent(noteOnEvent, 0);

                    absoluteTime += duration;

                    // Note Off event
                    var noteOffEvent = new NoteEvent((int)absoluteTime, 1, MidiCommandCode.NoteOff, midiNoteNumber, 0);
                    midiEvents.AddEvent(noteOffEvent, 0);

                    notesAdded++;
                }
            }

            // End Track event
            midiEvents.AddEvent(new MetaEvent(MetaEventType.EndTrack, 0, (int)absoluteTime), 0);

            // Write to file
            MidiFile.Export(fileName, midiEvents);

            Console.WriteLine($"MIDI file '{fileName}' has been created!");
        }

        private static string GetPitchClass(int midiNumber)
        {
            string[] noteNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
            int octave = (midiNumber / 12) - 1;
            int noteIndex = midiNumber % 12;
            return $"{noteNames[noteIndex]}{octave}";
        }
    }
}