public static class ChordGenerator
{
    private static Random random = new Random();
    public enum ScaleType { Major, Minor }
    public enum ChordType { Triad, Seventh, Ninth, Eleventh, Thirteenth }

    private static readonly Dictionary<ScaleType, List<int>> scaleIntervals = new Dictionary<ScaleType, List<int>>
    {
        { ScaleType.Major, new List<int> { 2, 2, 1, 2, 2, 2, 1 } }, // Whole-Whole-Half-Whole-Whole-Whole-Half
        { ScaleType.Minor, new List<int> { 2, 1, 2, 2, 1, 2, 2 } }  // Whole-Half-Whole-Whole-Half-Whole-Whole
    };

    public static List<int> GenerateScale(int rootNote, ScaleType scaleType)
    {
        List<int> scale = new List<int> { rootNote };
        int currentNote = rootNote;

        foreach (int interval in scaleIntervals[scaleType])
        {
            currentNote += interval;
            scale.Add(currentNote);
        }

        return scale;
    }

    public static List<List<int>> GenerateChords(int rootNote, ScaleType scaleType)
    {
        var scale = GenerateScale(rootNote, scaleType);
        var chords = new List<List<int>>();

        for (int i = 0; i < scale.Count - 2; i++) // Looping through scale notes for triads
        {
            var chord = new List<int>
            {
                scale[i],                            // Root
                scale[(i + 2) % scale.Count],        // Third (major or minor)
                scale[(i + 4) % scale.Count]         // Fifth (perfect)
            };

            chords.Add(chord);
        }

        return chords;
    }

    public static List<List<int>> GenerateRandomlySelectedChordsInKey(int rootNote, ScaleType scaleType)
    {
        var scale = GenerateScale(rootNote, scaleType);
        var chords = new List<List<int>>();

        for (int i = 0; i < scale.Count; i++) // Looping through scale notes
        {
            var chordType = (ChordType)random.Next(5); // Randomly select chord type
            var chord = new List<int> { scale[i] }; // Root

            // Adding additional notes based on randomly selected chord type
            if (chordType >= ChordType.Triad)
            {
                AddNoteToChord(chord, scale, i, 2);  // Third
                AddNoteToChord(chord, scale, i, 4);  // Fifth
            }
            if (chordType >= ChordType.Seventh)
            {
                AddNoteToChord(chord, scale, i, 6);  // Seventh
            }
            if (chordType >= ChordType.Ninth)
            {
                AddNoteToChord(chord, scale, i, 8);  // Ninth
            }
            if (chordType >= ChordType.Eleventh)
            {
                AddNoteToChord(chord, scale, i, 10); // Eleventh
            }
            if (chordType >= ChordType.Thirteenth)
            {
                AddNoteToChord(chord, scale, i, 12); // Thirteenth
            }

            chords.Add(chord);
        }

        return chords;
    }


    public static List<List<int>> GenerateChords(int rootNote, ScaleType scaleType, ChordType chordType)
    {
        var scale = GenerateScale(rootNote, scaleType);
        var chords = new List<List<int>>();

        for (int i = 0; i < scale.Count; i++) // Looping through scale notes
        {
            var chord = new List<int> { scale[i] }; // Root

            // Adding additional notes based on chord type
            if (chordType >= ChordType.Triad)
            {
                AddNoteToChord(chord, scale, i, 2);  // Third
                AddNoteToChord(chord, scale, i, 4);  // Fifth
            }
            if (chordType >= ChordType.Seventh)
            {
                AddNoteToChord(chord, scale, i, 6);  // Seventh
            }
            if (chordType >= ChordType.Ninth)
            {
                AddNoteToChord(chord, scale, i, 8);  // Ninth
            }
            if (chordType >= ChordType.Eleventh)
            {
                AddNoteToChord(chord, scale, i, 10); // Eleventh
            }
            if (chordType >= ChordType.Thirteenth)
            {
                AddNoteToChord(chord, scale, i, 12); // Thirteenth
            }

            chords.Add(chord);
        }

        return chords;
    }

    private static void AddNoteToChord(List<int> chord, List<int> scale, int rootIndex, int scaleStep)
    {
        int noteIndex = (rootIndex + scaleStep) % scale.Count;
        chord.Add(scale[noteIndex]);
    }
}