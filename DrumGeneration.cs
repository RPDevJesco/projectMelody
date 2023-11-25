using NAudio.Midi;

using System.Runtime.Intrinsics.Arm;

namespace Project_Melody
{
    public class DrumGeneration
    {
        private static bool isLeftNote = true;
        private static int bpm = 120; // Set a default non-zero value for bpm
        private static int ticksPerBeat = 480; // Ticks per quarter note
        private static int beatsPerMeasure; // For time signature
        private static int beatNote = 8; // Assuming quarter note gets the beat
        private static int Bass_Drum = 24;
        private static int rim_Snare = 25;
        private static int Snare_Drum = 26;
        private static int Flam_Snare = 27;
        private static int Roll_Snare = 28;
        private static int Snare_Drum_2 = 29;

        static void ProcessKickHerta(int i, ref long absoluteTime, MidiEventCollection midiEvents, int kickDrumNote)
        {
            if (i % 8 == 0 || i % 8 == 1 || i % 8 == 4 || i % 8 == 5)
            {
                var kickNoteOnEvent = new NoteOnEvent((int)absoluteTime, 1, kickDrumNote, 100, 0);
                midiEvents.AddEvent(kickNoteOnEvent, 0);

                // Adjust the timing for the herta rhythm
                absoluteTime += (i % 8 == 1 || i % 8 == 5) ? 240 : 720;

                var kickNoteOffEvent = new NoteEvent((int)absoluteTime, 1, MidiCommandCode.NoteOff, kickDrumNote, 0);
                midiEvents.AddEvent(kickNoteOffEvent, 0);
            }
        }

        static void ProcessKickTriplet(int i, ref long absoluteTime, MidiEventCollection midiEvents, int kickDrumNote)
        {
            if (i % 6 == 0 || i % 6 == 2 || i % 6 == 4)
            {
                var kickNoteOnEvent = new NoteOnEvent((int)absoluteTime, 1, kickDrumNote, 100, 0);
                midiEvents.AddEvent(kickNoteOnEvent, 0);

                // Adjust the timing for the herta rhythm
                absoluteTime += (i % 8 == 1 || i % 8 == 5) ? 240 : 720;

                var kickNoteOffEvent = new NoteEvent((int)absoluteTime, 1, MidiCommandCode.NoteOff, kickDrumNote, 0);
                midiEvents.AddEvent(kickNoteOffEvent, 0);
            }
        }

        static void ProcessSnareDrum(int i, ref long absoluteTime, MidiEventCollection midiEvents, int leftNote, int rightNote)
        {
            if (i % 8 == 1 || i % 8 == 0)
            {
                int note = isLeftNote ? leftNote : rightNote; // Alternate between right and left
                isLeftNote = !isLeftNote; // Toggle for next snare hit

                var noteOnEvent = new NoteOnEvent((int)absoluteTime, 1, note, 100, 0);
                midiEvents.AddEvent(noteOnEvent, 0);

                absoluteTime += 240; // Assuming 120 BPM, an eighth note lasts 240 ticks

                var noteOffEvent = new NoteEvent((int)absoluteTime, 1, MidiCommandCode.NoteOff, note, 0);
                midiEvents.AddEvent(noteOffEvent, 0);
            }
        }

        static void ProcessProgressiveKick(int i, ref long absoluteTime, MidiEventCollection midiEvents, int kickDrumNote)
        {
            // Example of a complex pattern with rapid double kicks and syncopation
            if (i % 16 == 0 || i % 16 == 4 || i % 16 == 6 || i % 16 == 10 || i % 16 == 12 || i % 16 == 14)
            {
                var kickNoteOnEvent = new NoteOnEvent((int)absoluteTime, 1, kickDrumNote, 100, 0);
                midiEvents.AddEvent(kickNoteOnEvent, 0);

                // Adjust the timing for the progressive rhythm
                // Rapid double kicks on certain beats, with irregular timings
                int duration = (i % 16 == 6 || i % 16 == 14) ? 120 : 480; // Shorter duration for double kicks

                absoluteTime += duration;

                var kickNoteOffEvent = new NoteEvent((int)absoluteTime, 1, MidiCommandCode.NoteOff, kickDrumNote, 0);
                midiEvents.AddEvent(kickNoteOffEvent, 0);
            }
        }

        public static void CreateDrumPattern()
        {
            List<(int signature, int tempo)> sections = new List<(int signature, int tempo)>
            {
                (4, 120), // 4/4 time signature at 120 BPM
                (3, 140), // 3/4 time signature at 140 BPM
                (7, 100)  // 7/4 time signature at 100 BPM
            };

            MidiEventCollection midiEvents = new MidiEventCollection(0, ticksPerBeat);
            long absoluteTime = 0;

            foreach (var section in sections)
            {
                beatsPerMeasure = section.signature;
                bpm = section.tempo;
                long beatDuration = (60 * ticksPerBeat) / bpm; // Calculate beatDuration here

                int totalBeats = beatsPerMeasure * 4; // Example: 4 measures per section

                for (int i = 0; i < totalBeats; i++)
                {
                    ProcessProgressiveKick(i, ref absoluteTime, midiEvents, Bass_Drum);
                    ProcessSnareDrum(i, ref absoluteTime, midiEvents, Snare_Drum, Snare_Drum_2);

                    // Increment absolute time by beat duration at the end of each beat
                    absoluteTime += beatDuration;
                }
            }

            string fileName = "DrumPattern.mid";
            MidiFile.Export(fileName, midiEvents);

            Console.WriteLine($"MIDI file '{fileName}' has been created with dynamically changing time signatures and BPM!");
        }
    }
}