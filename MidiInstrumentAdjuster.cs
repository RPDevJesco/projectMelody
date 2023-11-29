using NAudio.Midi;

namespace ProjectMelodyLibrary
{
    public class MidiInstrumentAdjuster
    {
        private int minNote; // Lowest note playable on the target instrument
        private int maxNote; // Highest note playable on the target instrument

        public MidiInstrumentAdjuster(int minNote, int maxNote)
        {
            this.minNote = minNote;
            this.maxNote = maxNote;
        }

        public void AdjustMidiFile(string inputFilePath, string outputFilePath)
        {
            var midiFile = new MidiFile(inputFilePath, false);
            var events = new MidiEventCollection(midiFile.FileFormat, midiFile.DeltaTicksPerQuarterNote);

            foreach (var track in midiFile.Events)
            {
                var adjustedTrack = new List<MidiEvent>();
                foreach (var midiEvent in track)
                {
                    if (midiEvent.CommandCode == MidiCommandCode.NoteOn || midiEvent.CommandCode == MidiCommandCode.NoteOff)
                    {
                        var noteEvent = (NoteEvent)midiEvent;
                        int adjustedNoteNumber = AdjustNoteInRange(noteEvent.NoteNumber);
                        noteEvent.NoteNumber = adjustedNoteNumber;
                    }
                    adjustedTrack.Add(midiEvent);
                }
                events.AddTrack(adjustedTrack);
            }

            MidiFile.Export(outputFilePath, events);
        }

        private int AdjustNoteInRange(int noteNumber)
        {
            while (noteNumber < minNote)
            {
                noteNumber += 12; // Move up an octave
            }
            while (noteNumber > maxNote)
            {
                noteNumber -= 12; // Move down an octave
            }
            return noteNumber;
        }
    }
}
