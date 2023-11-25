using NAudio.Midi;

namespace Project_Melody
{
    public class MelodyGeneration
    {
        private static Dictionary<string, List<int>> pitchClassToMidi = new Dictionary<string, List<int>>
        {
            {"A", new List<int> { 21,33,45,57,69,81,93,105 }},
            {"A#", new List<int> { 22,34,46,58,70,82,94,106 }},
            {"Bb", new List<int> { 22,34,46,58,70,82,94,106 }},
            {"B", new List<int> { 23,35,47,59,71,83,95,107 }},
            {"C", new List<int> { 24,36,48,60,72,84,96,108 }},
            {"C#", new List<int> { 25,37,49,61,73,85,97,109 }},
            {"Db", new List<int> { 25,37,49,61,73,85,97,109 }},
            {"D", new List<int> { 26,38,50,62,74,86,98,110 }},
            {"D#", new List<int> { 27,39,51,63,75,87,99,111 }},
            {"Eb", new List<int> { 27,39,51,63,75,87,99,111 }},
            {"E", new List<int> { 28,40,52,64,76,88,100,112 }},
            {"F", new List<int> { 29,41,53,65,77,89,101,113 }},
            {"F#", new List<int> { 30,42,54,66,78,90,102,114 }},
            {"Gb", new List<int> { 30,42,54,66,78,90,102,114 }},
            {"G", new List<int> { 31,43,55,67,79,91,103,115 }},
            {"G#", new List<int> { 32,44,56,68,80,92,104,116 }},
            {"Ab", new List<int> { 32,44,56,68,80,92,104,116 }},
        };
        private static Dictionary<string, List<string>> scales = new Dictionary<string, List<string>> { 
            // This is the basic C Major scale, serving as a foundation for other modes and scales.
            { "C_Major", new List<string> { "C", "D", "E", "F", "G", "A", "B", "C" } },
            // This scale is derived from the major scale but starts on the sixth degree. It's often used in minor key music.
            { "C_NaturalMinor", new List<string> { "C", "D", "E", "F", "G", "A", "B", "C" } },
            // Similar to the natural minor but with a raised 7th degree, which gives it a distinctive Eastern or classical sound.
            { "C_HarmonicMinor", new List<string> { "C", "D", "E", "F", "G", "A", "B", "C" } },
            // This scale raises both the 6th and 7th degrees when ascending and reverts to the natural minor form when descending.
            { "C_MelodicMinor", new List<string> { "C", "D", "E", "F", "G", "A", "B", "C" } },
            // This scale removes the 4th and 7th degrees from the major scale, commonly used in folk, blues, and rock.
            { "C_PentatonicMajor", new List<string> { "C", "D", "E", "F", "G", "A", "B", "C" } },
            // This is a five-note scale derived from the natural minor scale, often used in blues and rock.
            { "C_PentatonicMinor", new List<string> { "C", "D", "E", "F", "G", "A", "B", "C" } },
            // Similar to the pentatonic minor but with an added ♭5 (flat fifth), known as the "blue note."
            { "C_Blues", new List<string> { "C", "D", "E", "F", "G", "A", "B", "C" } },
            // This is the second mode of the B♭ Major scale, characterized by its minor third and major sixth.
            { "C_Dorian", new List<string> { "C", "D", "E", "F", "G", "A", "B", "C" } },
            // This is the third mode of the A♭ Major scale, known for its flamenco-like sound.
            { "C_Phrygian", new List<string> { "C", "D", "E", "F", "G", "A", "B", "C" } },
            // This is the fourth mode of the G Major scale, characterized by its raised 4th degree.
            { "C_Lydian", new List<string> { "C", "D", "E", "F", "G", "A", "B", "C" } },
            // This is the fifth mode of the F Major scale, often used in rock and jazz.
            { "C_Mixolydian", new List<string> { "C", "D", "E", "F", "G", "A", "B", "C" } },
            // This is the seventh mode of the D♭ Major scale, characterized by its diminished fifth.
            { "C_Locrian", new List<string> { "C", "D", "E", "F", "G", "A", "B", "C" } }
        };
        private static Dictionary<string, List<List<int>>> chordProgressions = new Dictionary<string, List<List<int>>>
        {
            { "C_Major_I_IV_V_I", new List<List<int>> {
                new List<int> { 60, 64, 67 }, // C chord
                new List<int> { 65, 69, 72 }, // F chord
                new List<int> { 67, 71, 74 }, // G chord
                new List<int> { 60, 64, 67 }  // C chord
            }}
            // ... other progressions
        };
        private static List<List<int>> rhythmPatterns = new List<List<int>>
        {
            // All quarter notes
            new List<int> { 480, 480, 480, 480 },
            // All eighth notes
            new List<int> { 240, 240, 240, 240, 240, 240, 240, 240 },
            // Mixed quarter and eighth notes
            new List<int> { 480, 240, 240, 480, 240, 240 },
            // Mix of sixteenth (120 ticks) and eighth notes (240 ticks)
            //new List<int> { 120, 240, 120, 240, 120, 240, 120, 240 },
            // Mix of quarter (480 ticks), eighth (240 ticks), and sixteenth notes (120 ticks)
            //new List<int> { 480, 240, 120, 240, 120, 480, 240, 120 },
            // All sixteenth notes (120 ticks each)
            //new List<int> { 120, 120, 120, 120, 120, 120, 120, 120 }
        };

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
            var midiScales = ConvertScalesToMidi(scales, availableNotes);

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
                    .SelectMany(note => pitchClassToMidi[note])
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

            var progression = chordProgressions[progressionKey];

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
                var rhythmPattern = rhythmPatterns[random.Next(rhythmPatterns.Count)];

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