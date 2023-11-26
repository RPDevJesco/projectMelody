using NAudio.Midi;

using Project_Melody.Instruments;

using System;
using System.Collections.Generic;

using static ChordGenerator;

public class MidiChordSequenceGenerator
{
    private int tempo; // Beats per minute
    private int ticksPerQuarterNote; // For MIDI timing

    public MidiChordSequenceGenerator(int tempo, int ticksPerQuarterNote)
    {
        this.tempo = tempo;
        this.ticksPerQuarterNote = ticksPerQuarterNote;
    }

    public void GenerateMidiFile(string fileName, int rootNote, ScaleType scaleType, int numChords, bool generateArpeggios)
    {
        var midiEvents = new MidiEventCollection(1, ticksPerQuarterNote);
        var track = new List<MidiEvent>();

        // Set the tempo
        int microsecondsPerQuarterNote = 60000000 / tempo;
        track.Add(new TempoEvent(microsecondsPerQuarterNote, 0));

        var chords = ChordGenerator.GenerateRandomlySelectedChordsInKey(rootNote, scaleType);
        int absoluteTime = 0; // Start time for the first chord or arpeggio note
        int noteDuration = ticksPerQuarterNote; // Duration for each chord or arpeggio note

        for (int i = 0; i < numChords; i++)
        {
            var chordOrArpeggio = generateArpeggios ? ArpeggioGenerator.GenerateArpeggio(chords[i % chords.Count], chords[i % chords.Count].Count) : chords[i % chords.Count];

            foreach (var note in chordOrArpeggio)
            {
                // Note On event
                track.Add(new NoteOnEvent(absoluteTime, 1, note, 100, noteDuration));
                // Note Off event
                track.Add(new NoteEvent(absoluteTime + noteDuration, 1, MidiCommandCode.NoteOff, note, 0));

                if (generateArpeggios)
                {
                    absoluteTime += noteDuration; // Move to next note in arpeggio
                }
            }
            if (!generateArpeggios)
            {
                absoluteTime += noteDuration * chordOrArpeggio.Count; // Move to next chord
            }
        }

        // Add End Track event
        track.Add(new MetaEvent(MetaEventType.EndTrack, 0, absoluteTime));

        midiEvents.AddTrack(track);

        // Write to file
        MidiFile.Export(fileName, midiEvents);

        Console.WriteLine($"MIDI file '{fileName}' has been created!");
    }

    public void GenerateMidiFile(string fileName, int rootNote, ScaleType scaleType, int numChords, bool generateArpeggios, bool includeBothChordAndArpeggio)
    {
        var midiEvents = new MidiEventCollection(1, ticksPerQuarterNote);
        var track = new List<MidiEvent>();

        // Set the tempo
        int microsecondsPerQuarterNote = 60000000 / tempo;
        track.Add(new TempoEvent(microsecondsPerQuarterNote, 0));

        var chords = ChordGenerator.GenerateRandomlySelectedChordsInKey(rootNote, scaleType);
        int absoluteTime = 0; // Start time for the first chord or arpeggio note
        int chordDuration = ticksPerQuarterNote * 4; // Duration for each chord
        int arpeggioNoteDuration = ticksPerQuarterNote; // Duration for each arpeggio note

        for (int i = 0; i < numChords; i++)
        {
            var chord = chords[i % chords.Count];
            var arpeggio = ArpeggioGenerator.GenerateArpeggio(chord, chord.Count);

            // Play the chord
            if (!generateArpeggios || includeBothChordAndArpeggio)
            {
                foreach (var note in chord)
                {
                    // Note On event
                    track.Add(new NoteOnEvent(absoluteTime, 1, note, 100, chordDuration));
                    // Note Off event
                    track.Add(new NoteEvent(absoluteTime + chordDuration, 1, MidiCommandCode.NoteOff, note, 0));
                }
                absoluteTime += chordDuration;
            }

            // Play the arpeggio
            if (generateArpeggios)
            {
                foreach (var note in arpeggio)
                {
                    // Note On event
                    track.Add(new NoteOnEvent(absoluteTime, 1, note, 100, arpeggioNoteDuration));
                    // Note Off event
                    track.Add(new NoteEvent(absoluteTime + arpeggioNoteDuration, 1, MidiCommandCode.NoteOff, note, 0));
                    absoluteTime += arpeggioNoteDuration; // Move to next note in arpeggio
                }
            }
        }

        // Add End Track event
        track.Add(new MetaEvent(MetaEventType.EndTrack, 0, absoluteTime));

        midiEvents.AddTrack(track);

        // Write to file
        MidiFile.Export(fileName, midiEvents);

        Console.WriteLine($"MIDI file '{fileName}' has been created!");
    }

    public void GenerateMidiFile(string fileName, BasicNote rootNote, ScaleType scaleType, bool generateArpeggios, bool includeBothChordAndArpeggio, MusicalInstrument instrument, int numberOfChords)
    {
        var midiEvents = new MidiEventCollection(1, ticksPerQuarterNote);
        var track = new List<MidiEvent>();

        // Set the tempo
        int microsecondsPerQuarterNote = 60000000 / tempo;
        track.Add(new TempoEvent(microsecondsPerQuarterNote, 0));

        var chords = GenerateRandomlySelectedChordsInKey(rootNote, scaleType, instrument.MinNote, instrument.MaxNote, numberOfChords); // Adjusted to use the new method
        int absoluteTime = 0; // Start time for the first chord or arpeggio note
        int chordDuration = ticksPerQuarterNote * 4; // Duration for each chord
        int arpeggioNoteDuration = ticksPerQuarterNote; // Duration for each arpeggio note

        foreach (var chord in chords)
        {
            var arpeggio = ArpeggioGenerator.GenerateArpeggio(chord, chord.Count * 4); // Assuming 4 notes per arpeggio pattern

            // Play the chord
            if (!generateArpeggios || includeBothChordAndArpeggio)
            {
                foreach (var note in chord)
                {
                    // Note On event
                    track.Add(new NoteOnEvent(absoluteTime, 1, note, 100, chordDuration));
                    // Note Off event
                    track.Add(new NoteEvent(absoluteTime + chordDuration, 1, MidiCommandCode.NoteOff, note, 0));
                }
                absoluteTime += chordDuration;
            }

            // Play the arpeggio
            if (generateArpeggios)
            {
                foreach (var note in arpeggio)
                {
                    // Note On event
                    track.Add(new NoteOnEvent(absoluteTime, 1, note, 100, arpeggioNoteDuration));
                    // Note Off event
                    track.Add(new NoteEvent(absoluteTime + arpeggioNoteDuration, 1, MidiCommandCode.NoteOff, note, 0));
                    absoluteTime += arpeggioNoteDuration; // Move to next note in arpeggio
                }
            }
        }

        // Add End Track event
        track.Add(new MetaEvent(MetaEventType.EndTrack, 0, absoluteTime));

        midiEvents.AddTrack(track);

        // Write to file
        MidiFile.Export(fileName, midiEvents);

        Console.WriteLine($"MIDI file '{fileName}' has been created!");
    }

}