using static Project_Melody.MusicBase;
public static class ChordGenerator
{
    private static Random random = new Random();

    public static List<List<int>> GenerateChords(BasicNote basicNote, ScaleType scaleType, ChordType chordType, int minNote, int maxNote)
    {
        // Find the closest octave of the basicNote that fits within the instrument's range
        int basicNoteValue = (int)basicNote; // MIDI value of the basic note in the lowest octave
        int octave = 0;
        while ((basicNoteValue + octave * 12) < minNote) octave++;
        int rootNote = basicNoteValue + octave * 12;

        // Ensure root note does not exceed maxNote
        if (rootNote > maxNote) return new List<List<int>>(); // Return empty list or handle as needed

        var scale = GenerateScale(rootNote, scaleType);
        var chords = new List<List<int>>();

        for (int i = 0; i < scale.Count; i++) // Looping through scale notes
        {
            var chord = new List<int> { scale[i] }; // Root

            switch (chordType)
            {
                case ChordType.Triad:
                    AddNoteToChord(chord, scale, i, 2);  // Third
                    AddNoteToChord(chord, scale, i, 4);  // Fifth
                    break;
                case ChordType.Seventh:
                    AddNoteToChord(chord, scale, i, 2);  // Third
                    AddNoteToChord(chord, scale, i, 4);  // Fifth
                    AddNoteToChord(chord, scale, i, 6);  // Seventh
                    break;
                case ChordType.Ninth:
                    AddNoteToChord(chord, scale, i, 2);  // Third
                    AddNoteToChord(chord, scale, i, 4);  // Fifth
                    AddNoteToChord(chord, scale, i, 6);  // Seventh
                    AddNoteToChord(chord, scale, i, 8);  // Ninth
                    break;
                case ChordType.Eleventh:
                    AddNoteToChord(chord, scale, i, 2);  // Third
                    AddNoteToChord(chord, scale, i, 4);  // Fifth
                    AddNoteToChord(chord, scale, i, 6);  // Seventh
                    AddNoteToChord(chord, scale, i, 8);  // Ninth
                    AddNoteToChord(chord, scale, i, 10); // Eleventh
                    break;
                case ChordType.Thirteenth:
                    AddNoteToChord(chord, scale, i, 2);  // Third
                    AddNoteToChord(chord, scale, i, 4);  // Fifth
                    AddNoteToChord(chord, scale, i, 6);  // Seventh
                    AddNoteToChord(chord, scale, i, 8);  // Ninth
                    // The eleventh is often omitted in many voicings, but you can include it:
                    //AddNoteToChord(chord, scale, i, 10); // Eleventh (optional)
                    AddNoteToChord(chord, scale, i, 12); // Thirteenth
                    break;
                case ChordType.Sus2:
                    AddNoteToChord(chord, scale, i, 1);  // Second
                    AddNoteToChord(chord, scale, i, 4);  // Fifth
                    break;
                case ChordType.Sus4:
                    AddNoteToChord(chord, scale, i, 3);  // Fourth
                    AddNoteToChord(chord, scale, i, 4);  // Fifth
                    break;
                case ChordType.Add9:
                    AddNoteToChord(chord, scale, i, 2); // Third
                    AddNoteToChord(chord, scale, i, 4); // Fifth
                    AddNoteToChord(chord, scale, i, 8); // Ninth
                    break;
                case ChordType.Add11:
                    AddNoteToChord(chord, scale, i, 2); // Third
                    AddNoteToChord(chord, scale, i, 4); // Fifth
                    AddNoteToChord(chord, scale, i, 10); // Eleventh
                    break;
                case ChordType.Add13:
                    AddNoteToChord(chord, scale, i, 2); // Third
                    AddNoteToChord(chord, scale, i, 4); // Fifth
                    AddNoteToChord(chord, scale, i, 12); // Thirteenth
                    break;
                case ChordType.Major6:
                    AddNoteToChord(chord, scale, i, 2); // Third
                    AddNoteToChord(chord, scale, i, 4); // Fifth
                    AddNoteToChord(chord, scale, i, 7); // Sixth
                    break;
                case ChordType.Minor6:
                    AddNoteToChord(chord, scale, i, 1); // Minor Third
                    AddNoteToChord(chord, scale, i, 4); // Fifth
                    AddNoteToChord(chord, scale, i, 7); // Sixth
                    break;
                case ChordType.Diminished:
                    AddNoteToChord(chord, scale, i, 1); // Minor Third
                    AddNoteToChord(chord, scale, i, 3); // Diminished Fifth
                    break;
                case ChordType.Diminished7th:
                    AddNoteToChord(chord, scale, i, 1); // Minor Third
                    AddNoteToChord(chord, scale, i, 3); // Diminished Fifth
                    AddNoteToChord(chord, scale, i, 6); // Diminished Seventh
                    break;
                case ChordType.HalfDiminished:
                    AddNoteToChord(chord, scale, i, 1); // Minor Third
                    AddNoteToChord(chord, scale, i, 3); // Diminished Fifth
                    AddNoteToChord(chord, scale, i, 6); // Minor Seventh
                    break;
                case ChordType.Augmented:
                    AddNoteToChord(chord, scale, i, 2); // Major Third
                    AddNoteToChord(chord, scale, i, 5); // Augmented Fifth
                    break;
                case ChordType.Augmented7th:
                    AddNoteToChord(chord, scale, i, 2); // Major Third
                    AddNoteToChord(chord, scale, i, 5); // Augmented Fifth
                    AddNoteToChord(chord, scale, i, 6); // Minor/Major Seventh (choice depends on the musical context)
                    break;
                case ChordType.PowerChord:
                    AddNoteToChord(chord, scale, i, 4);  // Fifth
                    break;
                case ChordType.Polychord:
                    // Example: Combine a C Major Triad and an F# Major Triad
                    AddNoteToChord(chord, scale, i, 2);  // C Major Triad
                    AddNoteToChord(chord, scale, i, 4);
                    AddNoteToChord(chord, scale, i + 6, 2); // F# Major Triad (6 semitones above C)
                    AddNoteToChord(chord, scale, i + 6, 4);
                    break;
                case ChordType.Quartal:
                    AddNoteToChord(chord, scale, i, 3);  // Fourth
                    AddNoteToChord(chord, scale, i, 6);  // Another Fourth
                    break;
                case ChordType.Quintal:
                    AddNoteToChord(chord, scale, i, 4);  // Fifth
                    AddNoteToChord(chord, scale, i, 8);  // Another Fifth
                    break;
                case ChordType.AlteredSeventh:
                    AddNoteToChord(chord, scale, i, 2); // Third
                    AddNoteToChord(chord, scale, i, 5); // Sharp Fifth (#5)
                    //AddNoteToChord(chord, scale, i, 3); // Flat Fifth (b5)
                    AddNoteToChord(chord, scale, i, 6); // Seventh (usually minor seventh)
                                                        // Add other alterations as needed (e.g., #5, b9)
                    break;
                case ChordType.ExtendedAltered:
                    AddNoteToChord(chord, scale, i, 2); // Third
                    AddNoteToChord(chord, scale, i, 4); // Fifth (can be altered)
                    AddNoteToChord(chord, scale, i, 6); // Seventh (usually minor)
                    AddNoteToChord(chord, scale, i, 8); // Ninth (can be altered to #9/b9)
                                                        // Add other extensions and alterations as needed
                    break;
            }

            chords.Add(chord);
        }

        return chords;
    }

    public static List<List<int>> GenerateRandomlySelectedChordsInKey(BasicNote basicNote, ScaleType scaleType, int minNote, int maxNote, int numberOfChords)
    {
        var scale = GenerateScale((int)basicNote, scaleType);
        var chords = new List<List<int>>();

        for (int i = 0; i < numberOfChords; i++)
        {
            int chordTypeCount = Enum.GetValues(typeof(ChordType)).Length;
            var chordType = (ChordType)random.Next(chordTypeCount);
            int scaleDegree = random.Next(scale.Count);
            var chord = new List<int> { scale[scaleDegree] };

            // Add chord notes based on the chord type
            switch (chordType)
                {
                    case ChordType.Triad:
                        AddNoteToChord(chord, scale, i, 2);  // Third
                        AddNoteToChord(chord, scale, i, 4);  // Fifth
                        break;
                    case ChordType.Seventh:
                        AddNoteToChord(chord, scale, i, 2);  // Third
                        AddNoteToChord(chord, scale, i, 4);  // Fifth
                        AddNoteToChord(chord, scale, i, 6);  // Seventh
                        break;
                    case ChordType.Ninth:
                        AddNoteToChord(chord, scale, i, 2);  // Third
                        AddNoteToChord(chord, scale, i, 4);  // Fifth
                        AddNoteToChord(chord, scale, i, 6);  // Seventh
                        AddNoteToChord(chord, scale, i, 8);  // Ninth
                        break;
                    case ChordType.Eleventh:
                        AddNoteToChord(chord, scale, i, 2);  // Third
                        AddNoteToChord(chord, scale, i, 4);  // Fifth
                        AddNoteToChord(chord, scale, i, 6);  // Seventh
                        AddNoteToChord(chord, scale, i, 8);  // Ninth
                        AddNoteToChord(chord, scale, i, 10); // Eleventh
                        break;
                    case ChordType.Thirteenth:
                        AddNoteToChord(chord, scale, i, 2);  // Third
                        AddNoteToChord(chord, scale, i, 4);  // Fifth
                        AddNoteToChord(chord, scale, i, 6);  // Seventh
                        AddNoteToChord(chord, scale, i, 8);  // Ninth
                                                             // The eleventh is often omitted in many voicings, but you can include it:
                                                             //AddNoteToChord(chord, scale, i, 10); // Eleventh (optional)
                        AddNoteToChord(chord, scale, i, 12); // Thirteenth
                        break;
                    case ChordType.Sus2:
                        AddNoteToChord(chord, scale, i, 1);  // Second
                        AddNoteToChord(chord, scale, i, 4);  // Fifth
                        break;
                    case ChordType.Sus4:
                        AddNoteToChord(chord, scale, i, 3);  // Fourth
                        AddNoteToChord(chord, scale, i, 4);  // Fifth
                        break;
                    case ChordType.Add9:
                        AddNoteToChord(chord, scale, i, 2); // Third
                        AddNoteToChord(chord, scale, i, 4); // Fifth
                        AddNoteToChord(chord, scale, i, 8); // Ninth
                        break;
                    case ChordType.Add11:
                        AddNoteToChord(chord, scale, i, 2); // Third
                        AddNoteToChord(chord, scale, i, 4); // Fifth
                        AddNoteToChord(chord, scale, i, 10); // Eleventh
                        break;
                    case ChordType.Add13:
                        AddNoteToChord(chord, scale, i, 2); // Third
                        AddNoteToChord(chord, scale, i, 4); // Fifth
                        AddNoteToChord(chord, scale, i, 12); // Thirteenth
                        break;
                    case ChordType.Major6:
                        AddNoteToChord(chord, scale, i, 2); // Third
                        AddNoteToChord(chord, scale, i, 4); // Fifth
                        AddNoteToChord(chord, scale, i, 7); // Sixth
                        break;
                    case ChordType.Minor6:
                        AddNoteToChord(chord, scale, i, 1); // Minor Third
                        AddNoteToChord(chord, scale, i, 4); // Fifth
                        AddNoteToChord(chord, scale, i, 7); // Sixth
                        break;
                    case ChordType.Diminished:
                        AddNoteToChord(chord, scale, i, 1); // Minor Third
                        AddNoteToChord(chord, scale, i, 3); // Diminished Fifth
                        break;
                    case ChordType.Diminished7th:
                        AddNoteToChord(chord, scale, i, 1); // Minor Third
                        AddNoteToChord(chord, scale, i, 3); // Diminished Fifth
                        AddNoteToChord(chord, scale, i, 6); // Diminished Seventh
                        break;
                    case ChordType.HalfDiminished:
                        AddNoteToChord(chord, scale, i, 1); // Minor Third
                        AddNoteToChord(chord, scale, i, 3); // Diminished Fifth
                        AddNoteToChord(chord, scale, i, 6); // Minor Seventh
                        break;
                    case ChordType.Augmented:
                        AddNoteToChord(chord, scale, i, 2); // Major Third
                        AddNoteToChord(chord, scale, i, 5); // Augmented Fifth
                        break;
                    case ChordType.Augmented7th:
                        AddNoteToChord(chord, scale, i, 2); // Major Third
                        AddNoteToChord(chord, scale, i, 5); // Augmented Fifth
                        AddNoteToChord(chord, scale, i, 6); // Minor/Major Seventh (choice depends on the musical context)
                        break;
                    case ChordType.PowerChord:
                        AddNoteToChord(chord, scale, i, 4);  // Fifth
                        break;
                    case ChordType.Polychord:
                        // Example: Combine a C Major Triad and an F# Major Triad
                        AddNoteToChord(chord, scale, i, 2);  // C Major Triad
                        AddNoteToChord(chord, scale, i, 4);
                        AddNoteToChord(chord, scale, i + 6, 2); // F# Major Triad (6 semitones above C)
                        AddNoteToChord(chord, scale, i + 6, 4);
                        break;
                    case ChordType.Quartal:
                        AddNoteToChord(chord, scale, i, 3);  // Fourth
                        AddNoteToChord(chord, scale, i, 6);  // Another Fourth
                        break;
                    case ChordType.Quintal:
                        AddNoteToChord(chord, scale, i, 4);  // Fifth
                        AddNoteToChord(chord, scale, i, 8);  // Another Fifth
                        break;
                    case ChordType.AlteredSeventh:
                        AddNoteToChord(chord, scale, i, 2); // Third
                        AddNoteToChord(chord, scale, i, 5); // Sharp Fifth (#5)
                                                            //AddNoteToChord(chord, scale, i, 3); // Flat Fifth (b5)
                        AddNoteToChord(chord, scale, i, 6); // Seventh (usually minor seventh)
                                                            // Add other alterations as needed (e.g., #5, b9)
                        break;
                    case ChordType.ExtendedAltered:
                        AddNoteToChord(chord, scale, i, 2); // Third
                        AddNoteToChord(chord, scale, i, 4); // Fifth (can be altered)
                        AddNoteToChord(chord, scale, i, 6); // Seventh (usually minor)
                        AddNoteToChord(chord, scale, i, 8); // Ninth (can be altered to #9/b9)
                                                            // Add other extensions and alterations as needed
                        break;
            }
            // Transpose the chord to fit within the instrument's range
            chord = TransposeChordToRange(chord, minNote, maxNote);
            chords.Add(chord);
        }

        return chords;
    }
    
    private static void AddNoteToChord(List<int> chord, List<int> scale, int rootIndex, int scaleStep)
    {
        int noteIndex = (rootIndex + scaleStep) % scale.Count;
        chord.Add(scale[noteIndex]);
    }

    private static List<int> TransposeChordToRange(List<int> chord, int minNote, int maxNote)
    {
        List<int> transposedChord = new List<int>(chord);

        // Check if transposition is needed
        if (transposedChord.Any(note => note < minNote || note > maxNote))
        {
            int minChordNote = transposedChord.Min();
            int maxChordNote = transposedChord.Max();

            // Transpose up if the lowest note of the chord is below the minNote
            while (minChordNote < minNote)
            {
                transposedChord = transposedChord.Select(note => note + 12).ToList();
                minChordNote += 12;
            }

            // Transpose down if the highest note of the chord is above the maxNote
            while (maxChordNote > maxNote)
            {
                transposedChord = transposedChord.Select(note => note - 12).ToList();
                maxChordNote -= 12;
            }
        }

        return transposedChord;
    }
}