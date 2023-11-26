using NAudio.Midi;
using Project_Melody.Instruments;
using static Project_Melody.MusicBase;
using static Project_Melody.MelodyGenerator;
using static ChordGenerator;

namespace Project_Melody
{
    public static class MidiGenerator
    {
        private static Random random = new Random();

        /// <summary>
        /// Generates a midi file for arpeggios, chords or melodies.
        /// </summary>
        /// <param name="fileName">name of the output file</param>
        /// <param name="rootNote">BasicNote's starting note for melodies, chord or arpeggio</param>
        /// <param name="scaleType">ScaleType to use as the basis for the melodies, chord or arpeggio</param>
        /// <param name="instrument">Instrument type to have the notes in the range of</param>
        /// <param name="tempo">bpm of the instrumentation</param>
        /// <param name="type">chord, melody or arpeggio selection</param>
        /// <param name="numberOfNotes">for melody, it is the total number of notes to generate, for arpeggio and chord, it is the number of chords to use.</param>
        public static void GenerateMidiFile(string fileName, BasicNote rootNote, ScaleType scaleType, MusicalInstrument instrument, int tempo, string type, int numberOfNotes)
        {
            Console.WriteLine("GenerateMidiFile called.");
            switch (type)
            {
                case "chord":
                    Console.WriteLine("chord selected.");
                    var chords = GenerateRandomlySelectedChordsInKey(rootNote, scaleType, instrument.MinNote, instrument.MaxNote, numberOfNotes);
                    AddChordsToTrack(chords, fileName, tempo, MusicBase.rhythmPatterns);
                    break;
                case "arpeggio":
                    Console.WriteLine("arpeggio selected.");
                    var arpeggios = GenerateRandomlySelectedChordsInKey(rootNote, scaleType, instrument.MinNote, instrument.MaxNote, numberOfNotes);
                    AddArpeggiosToTrack(arpeggios, fileName, tempo, MusicBase.rhythmPatterns);
                    break;
                case "melody":
                    Console.WriteLine("melody selected.");
                    var melody = GenerateMelody(rootNote, scaleType, numberOfNotes, instrument.MinNote, instrument.MaxNote);
                    AddMelodyToTrack(melody, fileName, tempo, MusicBase.rhythmPatterns);
                    break;
            }
        }

        private static void AddChordsToTrack(List<List<int>> chords, string fileName, int tempo, List<List<int>> rhythmPatterns)
        {
            var midiEvents = new MidiEventCollection(1, 480); // 480 ticks per quarter note
            var track = new List<MidiEvent>();
            int ticksPerQuarterNote = midiEvents.DeltaTicksPerQuarterNote;
            int microsecondsPerQuarterNote = 60000000 / tempo;
            track.Add(new TempoEvent(microsecondsPerQuarterNote, 0));

            int absoluteTime = 0;
            int velocity = 100; // A standard velocity for the notes
            var rhythmPattern = rhythmPatterns[random.Next(rhythmPatterns.Count)]; // Choose a random rhythm pattern

            foreach (var chord in chords)
            {
                int duration = rhythmPattern[random.Next(rhythmPattern.Count)]; // Random duration from rhythm pattern
                foreach (var note in chord)
                {
                    // Note On event for each note in the chord
                    var noteOn = new NoteOnEvent(absoluteTime, 1, note, velocity, 0);
                    track.Add(noteOn);

                    // Note Off event for each note in the chord
                    var noteOff = new NoteEvent(absoluteTime + duration, 1, MidiCommandCode.NoteOff, note, 0);
                    track.Add(noteOff);
                }

                // Move to the next chord time
                absoluteTime += duration;
            }

            // Add End Track event
            track.Add(new MetaEvent(MetaEventType.EndTrack, 0, absoluteTime));

            midiEvents.AddTrack(track);

            // Write to file
            MidiFile.Export(fileName, midiEvents);

            Console.WriteLine($"MIDI file '{fileName}' has been created!");
        }

        private static void AddArpeggiosToTrack(List<List<int>> arpeggios, string fileName, int tempo, List<List<int>> rhythmPatterns)
        {
            var midiEvents = new MidiEventCollection(1, 480); // 480 ticks per quarter note
            var track = new List<MidiEvent>();
            int ticksPerQuarterNote = midiEvents.DeltaTicksPerQuarterNote;
            int microsecondsPerQuarterNote = 60000000 / tempo;
            track.Add(new TempoEvent(microsecondsPerQuarterNote, 0));

            int absoluteTime = 0;
            int velocity = 100; // A standard velocity for the notes

            foreach (var arpeggio in arpeggios)
            {
                var rhythmPattern = rhythmPatterns[random.Next(rhythmPatterns.Count)]; // Choose a random rhythm pattern
                int rhythmIndex = 0;

                foreach (var note in arpeggio)
                {
                    int duration = rhythmPattern[rhythmIndex++ % rhythmPattern.Count]; // Loop the rhythm pattern

                    // Note On event for each note in the arpeggio
                    var noteOn = new NoteOnEvent(absoluteTime, 1, note, velocity, 0);
                    track.Add(noteOn);

                    // Note Off event for each note in the arpeggio
                    var noteOff = new NoteEvent(absoluteTime + duration, 1, MidiCommandCode.NoteOff, note, 0);
                    track.Add(noteOff);

                    // Move to the next note time
                    absoluteTime += duration;
                }
            }

            // Add End Track event
            track.Add(new MetaEvent(MetaEventType.EndTrack, 0, absoluteTime));

            midiEvents.AddTrack(track);

            // Write to file
            MidiFile.Export(fileName, midiEvents);

            Console.WriteLine($"MIDI file '{fileName}' has been created!");
        }

        private static void AddMelodyToTrack(List<int> melody, string fileName, int tempo, List<List<int>> rhythmPatterns)
        {
            Console.WriteLine("AddMelodyToTrack called.");
            try
            {
                var midiEvents = new MidiEventCollection(1, 480); // 480 ticks per quarter note
                var track = new List<MidiEvent>();
                int ticksPerQuarterNote = midiEvents.DeltaTicksPerQuarterNote;
                int microsecondsPerQuarterNote = 60000000 / tempo;
                track.Add(new TempoEvent(microsecondsPerQuarterNote, 0));

                int absoluteTime = 0;
                int velocity = 100; // A standard velocity for the notes
                var rhythmPattern = rhythmPatterns[random.Next(rhythmPatterns.Count)]; // Choose a random rhythm pattern

                int noteIndex = 0, rhythmIndex = 0;
                Console.WriteLine("Entering While loop.");
                while (noteIndex < melody.Count)
                {
                    int note = melody[noteIndex++]; // Get the current note and move to the next
                    int duration = rhythmPattern[rhythmIndex++ % rhythmPattern.Count]; // Loop the rhythm pattern

                    // Note On event for the melody note
                    var noteOn = new NoteOnEvent(absoluteTime, 1, note, velocity, duration);
                    track.Add(noteOn);

                    // Note Off event for the melody note
                    var noteOff = new NoteEvent(absoluteTime + duration, 1, MidiCommandCode.NoteOff, note, 0);
                    track.Add(noteOff);

                    // Move to the next note time
                    absoluteTime += duration;
                }
                Console.WriteLine("Exiting While loop.");
                Console.WriteLine("Adding track to MIDI events...");
                midiEvents.AddTrack(track);
                Console.WriteLine("Exporting MIDI file...");
                MidiFile.Export(fileName, midiEvents);
                Console.WriteLine($"MIDI file '{fileName}' has been created!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}