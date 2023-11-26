public static class ChordGenerator
{
    private static Random random = new Random();
    
    /// <summary>
    /// Names of all the scale types this application can utilizie
    /// </summary>
    public enum ScaleType { 
        Major, Minor, HarmonicMinor, MelodicMinor, Blues, Pentatonic, PentatonicMinor,
        Dorian, Phyrgian, Lydian, Mixolydian, Locrian, WholeTone, Chromatic, 
        Octatonic, Diminished, Altered, LydianDominant, LydianAugmented, GypsyMinor, NeapolitanMinor, 
        NeapolitanMajor, Arabic, Byzantine, InSen, Jirajoshi, Yo, 
        ChineseMajorPentatonic, ChineseMinorPentatonic, Jiao, Zhi, Yu, Gong, Shang 
    }

    /// <summary>
    /// Names of the chord types that this application can build.
    /// </summary>
    public enum ChordType
    {
        Triad, Seventh, Ninth, Eleventh, Thirteenth,
        Sus2, Sus4, Add9, Add11, Add13,
        Major6, Minor6, Diminished, Diminished7th, HalfDiminished,
        Augmented, Augmented7th, PowerChord, Polychord, Quartal, Quintal,
        AlteredSeventh, ExtendedAltered
    }

    public enum BasicNote
    {
        C, CSharp_DFlat, D, DSharp_EFlat, E, F, FSharp_GFlat, G, GSharp_AFlat, A, ASharp_BFlat, B
    }

    /// <summary>
    /// The various scales represented by their intervals. 
    /// 1 = Half Step, 2 = Whole Step, 3 = Minor Third / Augmented Second, 4 = Major Third
    /// </summary>
    private static readonly Dictionary<ScaleType, List<int>> scaleIntervals = new Dictionary<ScaleType, List<int>>
{
    // Traditional Western major scale, bright and happy sound.
    { ScaleType.Major, new List<int> { 2, 2, 1, 2, 2, 2, 1 } },
    // Natural minor scale, often associated with a melancholic sound.
    { ScaleType.Minor, new List<int> { 2, 1, 2, 2, 1, 2, 2 } },
    // Harmonic minor scale, characterized by an exotic feel with a raised seventh.
    { ScaleType.HarmonicMinor, new List<int> { 2, 1, 2, 2, 1, 3, 1 } },
    // Melodic minor scale, ascending pattern differs from descending, used in jazz.
    { ScaleType.MelodicMinor, new List<int> { 2, 1, 2, 2, 2, 2, 1 } },
    // Blues scale, known for its soulful and melancholic sound.
    { ScaleType.Blues, new List<int> { 3, 2, 1, 1, 3, 2 } },
    // Major pentatonic scale, used in folk music and is cheerful.
    { ScaleType.Pentatonic, new List<int> { 2, 2, 3, 2, 3 } },
    // Minor pentatonic scale, commonly used in blues and rock.
    { ScaleType.PentatonicMinor, new List<int> { 3, 2, 2, 3, 2 } },
    // Dorian mode, similar to the natural minor but with a raised sixth.
    { ScaleType.Dorian, new List<int> { 2, 1, 2, 2, 2, 1, 2 } },
    // Phrygian mode, known for its Spanish flamenco and Middle Eastern vibes.
    { ScaleType.Phyrgian, new List<int> { 1, 2, 2, 2, 1, 2, 2} },
    // Lydian mode, characterized by its raised fourth degree, dreamy sound.
    { ScaleType.Lydian, new List<int> { 2, 2, 2, 1, 2, 2, 1 } },
    // Mixolydian mode, similar to the major scale but with a flattened seventh.
    { ScaleType.Mixolydian, new List<int> {  2, 2, 1, 2, 2, 1, 2 } },
    // Locrian mode, diminished feel, with a flat second and fifth.
    { ScaleType.Locrian, new List<int> {  1, 2, 2, 1, 2, 2, 2 } },
    // Whole tone scale, creates a dreamlike, ambiguous atmosphere.
    { ScaleType.WholeTone, new List<int> { 2, 2, 2, 2, 2, 2 } },
    // Chromatic scale, uses all twelve tones, creating tension or movement.
    { ScaleType.Chromatic, new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } },
    // Octatonic scale, alternating half and whole steps, used in jazz and modern classical.
    { ScaleType.Octatonic, new List<int> { 1, 2, 1, 2, 1, 2, 1, 2 } },
    // Diminished scale, similar to octatonic, but starts with a whole step.
    { ScaleType.Diminished, new List<int> { 2, 1, 2, 1, 2, 1, 2, 1 } },
    // Altered scale, derived from the melodic minor, used over altered dominants in jazz.
    { ScaleType.Altered, new List<int> { 1, 2, 1, 2, 2, 2, 2 } },
    // Lydian dominant scale, a major scale with raised fourth and flattened seventh.
    { ScaleType.LydianDominant, new List<int> { 2, 2, 2, 1, 2, 1, 2 } },
    // Lydian augmented scale, a lydian scale with an augmented fifth.
    { ScaleType.LydianAugmented, new List<int> { 2, 2, 2, 2, 1, 2, 1 } },
    // Hungarian minor scale, exotic and mysterious, similar to harmonic minor with a raised fourth.
    { ScaleType.GypsyMinor, new List<int> { 2, 1, 3, 1, 1, 3, 1 } },
    // Neapolitan minor scale, characterized by its lowered second and raised seventh.
    { ScaleType.NeapolitanMinor, new List<int> { 1, 2, 2, 2, 1, 3, 1 } },
    // Neapolitan major scale, a major scale with a lowered second.
    { ScaleType.NeapolitanMajor, new List<int> { 1, 2, 2, 2, 2, 2, 1 } },
    // Arabian scale, exotic and mysterious, used in Middle Eastern music.
    { ScaleType.Arabic, new List<int> { 1, 3, 1, 2, 1, 3, 1 } },
    // Byzantine scale, similar to the Arabian scale, used in Eastern music.
    { ScaleType.Byzantine, new List<int> { 1, 3, 1, 2, 1, 3, 1 } },
    // In-Sen scale, a Japanese scale creating a melancholic and introspective mood.
    { ScaleType.InSen, new List<int> {  1, 4, 2, 3, 2 } },
    // Hirajoshi scale, a Japanese scale known for its tranquil and mysterious sound.
    { ScaleType.Jirajoshi, new List<int> {  2, 1, 4, 1, 4 } },
    // Yo scale, a Japanese scale used in folk music, bright and open-sounding.
    { ScaleType.Yo, new List<int> { 2, 3, 2, 2, 3 } },
    // Chinese major pentatonic scale, commonly used in traditional folk music.
    { ScaleType.ChineseMajorPentatonic, new List<int> { 2, 2, 3, 2, 3 } },
    // Chinese minor pentatonic scale, used in traditional music, soulful and melancholic.
    { ScaleType.ChineseMinorPentatonic, new List<int> { 3, 2, 2, 3, 2 } },
    // Jiao mode, a Chinese pentatonic scale with a bright and joyful character.
    { ScaleType.Jiao, new List<int> { 2, 3, 2, 3, 2 } },
    // Zhi mode, a Chinese pentatonic scale with an introspective and gentle quality.
    { ScaleType.Zhi, new List<int> { 3, 2, 2, 3, 2 } },
    // Yu mode, a Chinese pentatonic scale, peaceful and serene.
    { ScaleType.Yu, new List<int> { 3, 2, 3, 2, 2 } },
    // Gong mode, the Chinese major pentatonic scale, often the basis for folk melodies.
    { ScaleType.Gong, new List<int> { 2, 2, 3, 2, 3 } },
    // Shang mode, a Chinese pentatonic scale, with a somewhat melancholic feel.
    { ScaleType.Shang, new List<int> { 2, 3, 2, 2, 3 } }
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

    public static List<List<int>> GenerateRandomlySelectedChordsInKey(int rootNote, ScaleType scaleType)
    {
        var scale = GenerateScale(rootNote, scaleType);
        var chords = new List<List<int>>();

        for (int i = 0; i < scale.Count; i++) // Looping through scale notes
        {
            var chordType = (ChordType)random.Next(22); // Randomly select chord type
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

    public static List<List<int>> GenerateChords(int rootNote, ScaleType scaleType, ChordType chordType)
    {
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
        while (transposedChord.Any(note => note < minNote))
        {
            // Transpose whole chord up an octave
            transposedChord = transposedChord.Select(note => note + 12).ToList();
        }
        while (transposedChord.Any(note => note > maxNote))
        {
            // Transpose whole chord down an octave
            transposedChord = transposedChord.Select(note => note - 12).ToList();
        }
        return transposedChord;
    }
}