using NAudio.Midi;
using ProjectMelodyLibrary.Instruments;
using static ProjectMelodyLibrary.MusicBase;
using static ProjectMelodyLibrary.MelodyGenerator;
using static ProjectMelodyLibrary.ChordGenerator;

namespace ProjectMelodyLibrary
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
        public static void GenerateMidiFile(string filePath, string fileName, BasicNote rootNote, ScaleType scaleType, MusicalInstrument instrument, int tempo, string type, int numberOfNotes)
        {
            switch (type)
            {
                case "chord":
                    var chords = GenerateChordProgression(rootNote, scaleType, instrument.MinNote, instrument.MaxNote);
                    AddChordsToTrack(chords, filePath, fileName, tempo, chordRhythmPatterns);
                    break;
                case "arpeggio":
                    var arpeggios = GenerateChordProgression(rootNote, scaleType, instrument.MinNote, instrument.MaxNote);
                    AddArpeggiosToTrack(arpeggios, filePath, fileName, tempo, rhythmPatterns);
                    break;
                case "melody":
                    var melody = GenerateMusicalPhrase((int)rootNote, numberOfNotes / 4, scaleType, numberOfNotes, instrument.MinNote, instrument.MaxNote);
                    AddMelodyToTrack(melody, filePath, fileName, tempo, rhythmPatterns);
                    break;
                case "mixer":
                    var mixer = GenerateChordProgression(rootNote, scaleType, instrument.MinNote, instrument.MaxNote);
                    AddMixerToTrack(mixer, mixer, filePath, fileName, tempo, chordRhythmPatterns, rhythmPatterns);
                    break;
                case "all":
                    var all = GenerateChordProgression(rootNote, scaleType, instrument.MinNote, instrument.MaxNote);
                    var melodies = GenerateMusicalPhrase((int)rootNote, numberOfNotes / 4, scaleType, numberOfNotes, instrument.MinNote, instrument.MaxNote);
                    AddAllToTrack(all, all, melodies, filePath, fileName, tempo, rhythmPatterns);
                    break;
            }
        }

        private static void AddChordsToTrack(List<List<int>> chords, string filePath, string fileName, int tempo, List<List<int>> rhythmPatterns)
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

            string fullPath = Path.Combine(filePath, fileName);
            // Write to file
            MidiFile.Export(fullPath, midiEvents);
        }

        private static void AddArpeggiosToTrack(List<List<int>> arpeggios, string filePath ,string fileName, int tempo, List<List<int>> rhythmPatterns)
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
                if (rhythmIndex >= rhythmPattern.Count) // When the rhythm pattern ends, choose a new one
                {
                    rhythmPattern = rhythmPatterns[random.Next(rhythmPatterns.Count)];
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

                string fullPath = Path.Combine(filePath, fileName);
                // Write to file
                MidiFile.Export(fullPath, midiEvents);
            }
        }

        private static void AddMelodyToTrack(List<int> melody, string filePath, string fileName, int tempo, List<List<int>> rhythmPatterns)
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

            if (rhythmIndex < rhythmPattern.Count)
            {
                int duration = rhythmPattern[rhythmIndex];
                while (noteIndex < melody.Count)
                {
                    if (rhythmIndex >= rhythmPattern.Count) // When the rhythm pattern ends, choose a new one
                    {
                        rhythmPattern = rhythmPatterns[random.Next(rhythmPatterns.Count)];
                        rhythmIndex = 0;
                    }

                    int note = melody[noteIndex++]; // Get the current note and move to the next

                    // Note On event for the melody note
                    var noteOn = new NoteOnEvent(absoluteTime, 1, note, velocity, duration);
                    track.Add(noteOn);

                    // Note Off event for the melody note
                    var noteOff = new NoteEvent(absoluteTime + duration, 1, MidiCommandCode.NoteOff, note, 0);
                    track.Add(noteOff);

                    // Move to the next note time
                    absoluteTime += duration;
                }
                midiEvents.AddTrack(track);
                string fullPath = Path.Combine(filePath, fileName);
                MidiFile.Export(fullPath, midiEvents);
            }
        }

        private static void AddMixerToTrack(List<List<int>> chords, List<List<int>> arpeggios, string filePath, string fileName, int tempo, List<List<int>> chordRhythmPatterns, List<List<int>> arpeggioRhythmPatterns)
        {
            var midiEvents = new MidiEventCollection(1, 480); // 480 ticks per quarter note
            var track = new List<MidiEvent>();
            int ticksPerQuarterNote = midiEvents.DeltaTicksPerQuarterNote;
            int microsecondsPerQuarterNote = 60000000 / tempo;
            track.Add(new TempoEvent(microsecondsPerQuarterNote, 0));

            int velocity = 100; // A standard velocity for the notes

            // Chords
            int chordAbsoluteTime = 0;
            foreach (var chord in chords)
            {
                var chordRhythmPattern = chordRhythmPatterns[random.Next(chordRhythmPatterns.Count)];
                int chordDuration = chordRhythmPattern[random.Next(chordRhythmPattern.Count)];

                foreach (var note in chord)
                {
                    var noteOn = new NoteOnEvent(chordAbsoluteTime, 1, note, velocity, 0);
                    track.Add(noteOn);

                    var noteOff = new NoteEvent(chordAbsoluteTime + chordDuration, 1, MidiCommandCode.NoteOff, note, 0);
                    track.Add(noteOff);
                }
                chordAbsoluteTime += chordDuration;
            }

            // Arpeggios
            int arpeggioAbsoluteTime = 0;
            foreach (var arpeggio in arpeggios)
            {
                var arpeggioRhythmPattern = arpeggioRhythmPatterns[random.Next(arpeggioRhythmPatterns.Count)];
                int arpeggioRhythmIndex = 0;

                foreach (var note in arpeggio)
                {
                    int arpeggioDuration = arpeggioRhythmPattern[arpeggioRhythmIndex++ % arpeggioRhythmPattern.Count];

                    var noteOn = new NoteOnEvent(arpeggioAbsoluteTime, 1, note, velocity, 0);
                    track.Add(noteOn);

                    var noteOff = new NoteEvent(arpeggioAbsoluteTime + arpeggioDuration, 1, MidiCommandCode.NoteOff, note, 0);
                    track.Add(noteOff);

                    arpeggioAbsoluteTime += arpeggioDuration;
                }
            }

            // Add End Track event
            int finalAbsoluteTime = Math.Max(chordAbsoluteTime, arpeggioAbsoluteTime);
            track.Add(new MetaEvent(MetaEventType.EndTrack, 0, finalAbsoluteTime));
            midiEvents.AddTrack(track);

            string fullPath = Path.Combine(filePath, fileName);
            MidiFile.Export(fullPath, midiEvents);
        }

        private static void AddAllToTrack(List<List<int>> chords, List<List<int>> arpeggios, List<int> melody, string filePath, string fileName, int tempo, List<List<int>> rhythmPatterns)
        {
            var midiEvents = new MidiEventCollection(1, 480); // 480 ticks per quarter note
            var track = new List<MidiEvent>();
            int ticksPerQuarterNote = midiEvents.DeltaTicksPerQuarterNote;
            int microsecondsPerQuarterNote = 60000000 / tempo;
            track.Add(new TempoEvent(microsecondsPerQuarterNote, 0));

            int velocity = 100; // A standard velocity for the notes

            // Chords
            int chordAbsoluteTime = 0;
            foreach (var chord in chords)
            {
                var chordRhythmPattern = chordRhythmPatterns[random.Next(chordRhythmPatterns.Count)];
                int chordDuration = chordRhythmPattern[random.Next(chordRhythmPattern.Count)];

                foreach (var note in chord)
                {
                    var noteOn = new NoteOnEvent(chordAbsoluteTime, 1, note, velocity, 0);
                    track.Add(noteOn);

                    var noteOff = new NoteEvent(chordAbsoluteTime + chordDuration, 1, MidiCommandCode.NoteOff, note, 0);
                    track.Add(noteOff);
                }
                chordAbsoluteTime += chordDuration;
            }

            // Arpeggios
            int arpeggioAbsoluteTime = 0;
            foreach (var arpeggio in arpeggios)
            {
                var arpeggioRhythmPattern = rhythmPatterns[random.Next(rhythmPatterns.Count)];
                int arpeggioRhythmIndex = 0;

                foreach (var note in arpeggio)
                {
                    int arpeggioDuration = arpeggioRhythmPattern[arpeggioRhythmIndex++ % arpeggioRhythmPattern.Count];

                    var noteOn = new NoteOnEvent(arpeggioAbsoluteTime, 1, note, velocity, 0);
                    track.Add(noteOn);

                    var noteOff = new NoteEvent(arpeggioAbsoluteTime + arpeggioDuration, 1, MidiCommandCode.NoteOff, note, 0);
                    track.Add(noteOff);

                    arpeggioAbsoluteTime += arpeggioDuration;
                }
            }

            // Melody
            int melodyAbsoluteTime = 0;
            var melodyRhythmPattern = rhythmPatterns[random.Next(rhythmPatterns.Count)];
            int melodyRhythmIndex = 0;
            int noteIndex = 0;

            while (noteIndex < melody.Count)
            {
                if (melodyRhythmIndex >= melodyRhythmPattern.Count)
                {
                    melodyRhythmPattern = rhythmPatterns[random.Next(rhythmPatterns.Count)];
                    melodyRhythmIndex = 0;
                }

                int note = melody[noteIndex++];
                int melodyDuration = melodyRhythmPattern[melodyRhythmIndex++];

                var noteOn = new NoteOnEvent(melodyAbsoluteTime, 1, note, velocity, melodyDuration);
                track.Add(noteOn);

                var noteOff = new NoteEvent(melodyAbsoluteTime + melodyDuration, 1, MidiCommandCode.NoteOff, note, 0);
                track.Add(noteOff);

                melodyAbsoluteTime += melodyDuration;
            }

            // Add End Track event
            int finalAbsoluteTime = Math.Max(Math.Max(chordAbsoluteTime, arpeggioAbsoluteTime), melodyAbsoluteTime);
            track.Add(new MetaEvent(MetaEventType.EndTrack, 0, finalAbsoluteTime));
            midiEvents.AddTrack(track);

            string fullPath = Path.Combine(filePath, fileName);
            MidiFile.Export(fullPath, midiEvents);
        }
    }
}