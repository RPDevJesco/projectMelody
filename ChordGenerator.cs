using System;

using static ProjectMelodyLibrary.MusicBase;

namespace ProjectMelodyLibrary
{
    public static class ChordGenerator
    {
        private static Random random = new Random();

        public static List<List<int>> GenerateChordProgression(BasicNote basicNote, ScaleType scaleType, int minNote, int maxNote)
        {
            var scale = GenerateScale((int)basicNote, scaleType);

            // Randomly select a progression
            int progressionIndex = random.Next(CommonProgressions.Count);
            var progressionDegrees = CommonProgressions[progressionIndex];

            var chords = new List<List<int>>();

            foreach (var degree in progressionDegrees)
            {
                var chordType = GetChordTypeForScaleDegree(degree, scaleType);
                var chord = GenerateChord(scale, chordType, degree, minNote, maxNote); // Pass 'degree' to GenerateChord
                chords.Add(chord);
            }

            return chords;
        }

        private static ChordType GetChordTypeForScaleDegree(int scaleDegree, ScaleType scaleType)
        {
            switch (scaleType)
            {
                case ScaleType.Major:
                    // Standard major scale harmonization
                    switch (scaleDegree)
                    {
                        case 1: return ChordType.Major6; // I
                        case 2: return ChordType.Minor6; // ii
                        case 3: return ChordType.Minor6; // iii
                        case 4: return ChordType.Major6; // IV
                        case 5: return ChordType.Seventh; // V
                        case 6: return ChordType.Minor6; // vi
                        case 7: return ChordType.Diminished; // vii°
                    }
                    break;
                case ScaleType.Minor:
                    // Harmonization for natural minor scale
                    switch (scaleDegree)
                    {
                        case 1: return ChordType.Minor6; // i
                        case 2: return ChordType.Diminished; // ii°
                        case 3: return ChordType.Major6; // III
                        case 4: return ChordType.Minor6; // iv
                        case 5: return ChordType.Minor6; // v
                        case 6: return ChordType.Major6; // VI
                        case 7: return ChordType.Major6; // VII
                    }
                    break;
                case ScaleType.Blues:
                    return ChordType.Seventh; // Dominant 7th chords are common in blues
                case ScaleType.Pentatonic:
                    return (scaleDegree == 1 || scaleDegree == 5) ? ChordType.Triad : ChordType.Add9;
                case ScaleType.Dorian:
                    switch (scaleDegree)
                    {
                        case 1: return ChordType.Minor6; // i
                        case 2: return ChordType.Minor6; // ii
                        case 3: return ChordType.Major6; // III
                        case 4: return ChordType.Major6; // IV
                        case 5: return ChordType.Seventh; // v
                        case 6: return ChordType.Diminished; // vi°
                        case 7: return ChordType.Major6; // VII
                    }
                    break;
                case ScaleType.Phyrgian:
                    switch (scaleDegree)
                    {
                        case 1: return ChordType.Minor6; // i
                        case 2: return ChordType.Major6; // II
                        case 3: return ChordType.Diminished; // iii°
                        case 4: return ChordType.Minor6; // iv
                        case 5: return ChordType.Diminished; // v°
                        case 6: return ChordType.Major6; // VI
                        case 7: return ChordType.Minor6; // vii
                    }
                    break;
                // Exotic scales like WholeTone, Arabic, etc., have less conventional harmonic structures
                case ScaleType.WholeTone:
                    return ChordType.Augmented; // Whole tone scales often use augmented chords
                case ScaleType.Byzantine:
                case ScaleType.Arabic:
                    // Arabic scale harmonization can vary widely; this is an example
                    switch (scaleDegree)
                    {
                        case 1: return ChordType.Major6; // Use creative liberties for harmonization
                        case 2: return ChordType.Minor6;
                            // ... (Other degrees)
                    }
                    break;
                case ScaleType.InSen:
                    // In-Sen scale has a melancholic and introspective mood
                    return ChordType.Minor6; // Simplified approach, as traditional In-Sen doesn't focus on chords
                case ScaleType.Jirajoshi:
                    // Jirajoshi scale is tranquil and mysterious
                    return ChordType.Diminished; // Simplified approach, as traditional Jirajoshi doesn't focus on chords
                case ScaleType.Yo:
                    // Yo scale is used in folk music, bright and open-sounding
                    return ChordType.Major6; // Simplified, as Yo scale traditionally doesn't use chords
                case ScaleType.ChineseMajorPentatonic:
                case ScaleType.Gong:
                    // Chinese major pentatonic and Gong mode
                    return (scaleDegree == 1 || scaleDegree == 5) ? ChordType.Major6 : ChordType.Add9;
                case ScaleType.ChineseMinorPentatonic:
                case ScaleType.Yu:
                case ScaleType.Shang:
                    // Chinese minor pentatonic, Yu, and Shang modes
                    return ChordType.Minor6;
                case ScaleType.Jiao:
                case ScaleType.Zhi:
                    // Jiao and Zhi modes
                    return ChordType.Sus4; // Simplified, these modes traditionally don't focus on chords
                default:
                    return ChordType.Triad; // Default or fallback
            }
            return ChordType.Triad; // Fallback for scales not specifically handled
        }

        private static List<int> GenerateChord(List<int> scale, ChordType chordType, int scaleDegree, int minNote, int maxNote)
        {
            var chord = new List<int> { scale[scaleDegree - 1] };
            // Add chord notes based on the chord type
            switch (chordType)
            {
                case ChordType.Triad:
                    AddNoteToChord(chord, scale,scaleDegree, 2);  // Third
                    AddNoteToChord(chord, scale,scaleDegree, 4);  // Fifth
                    break;
                case ChordType.Seventh:
                    AddNoteToChord(chord, scale,scaleDegree, 2);  // Third
                    AddNoteToChord(chord, scale,scaleDegree, 4);  // Fifth
                    AddNoteToChord(chord, scale,scaleDegree, 6);  // Seventh
                    break;
                case ChordType.Ninth:
                    AddNoteToChord(chord, scale,scaleDegree, 2);  // Third
                    AddNoteToChord(chord, scale,scaleDegree, 4);  // Fifth
                    AddNoteToChord(chord, scale,scaleDegree, 6);  // Seventh
                    AddNoteToChord(chord, scale,scaleDegree, 8);  // Ninth
                    break;
                case ChordType.Eleventh:
                    AddNoteToChord(chord, scale,scaleDegree, 2);  // Third
                    AddNoteToChord(chord, scale,scaleDegree, 4);  // Fifth
                    AddNoteToChord(chord, scale,scaleDegree, 6);  // Seventh
                    AddNoteToChord(chord, scale,scaleDegree, 8);  // Ninth
                    AddNoteToChord(chord, scale,scaleDegree, 10); // Eleventh
                    break;
                case ChordType.Thirteenth:
                    AddNoteToChord(chord, scale,scaleDegree, 2);  // Third
                    AddNoteToChord(chord, scale,scaleDegree, 4);  // Fifth
                    AddNoteToChord(chord, scale,scaleDegree, 6);  // Seventh
                    AddNoteToChord(chord, scale,scaleDegree, 8);  // Ninth
                                                         // The eleventh is often omitted in many voicings, but you can include it:
                                                         //AddNoteToChord(chord, scale,scaleDegree, 10); // Eleventh (optional)
                    AddNoteToChord(chord, scale,scaleDegree, 12); // Thirteenth
                    break;
                case ChordType.Sus2:
                    AddNoteToChord(chord, scale,scaleDegree, 1);  // Second
                    AddNoteToChord(chord, scale,scaleDegree, 4);  // Fifth
                    break;
                case ChordType.Sus4:
                    AddNoteToChord(chord, scale,scaleDegree, 3);  // Fourth
                    AddNoteToChord(chord, scale,scaleDegree, 4);  // Fifth
                    break;
                case ChordType.Add9:
                    AddNoteToChord(chord, scale,scaleDegree, 2); // Third
                    AddNoteToChord(chord, scale,scaleDegree, 4); // Fifth
                    AddNoteToChord(chord, scale,scaleDegree, 8); // Ninth
                    break;
                case ChordType.Add11:
                    AddNoteToChord(chord, scale,scaleDegree, 2); // Third
                    AddNoteToChord(chord, scale,scaleDegree, 4); // Fifth
                    AddNoteToChord(chord, scale,scaleDegree, 10); // Eleventh
                    break;
                case ChordType.Add13:
                    AddNoteToChord(chord, scale,scaleDegree, 2); // Third
                    AddNoteToChord(chord, scale,scaleDegree, 4); // Fifth
                    AddNoteToChord(chord, scale,scaleDegree, 12); // Thirteenth
                    break;
                case ChordType.Major6:
                    AddNoteToChord(chord, scale,scaleDegree, 2); // Third
                    AddNoteToChord(chord, scale,scaleDegree, 4); // Fifth
                    AddNoteToChord(chord, scale,scaleDegree, 7); // Sixth
                    break;
                case ChordType.Minor6:
                    AddNoteToChord(chord, scale,scaleDegree, 1); // Minor Third
                    AddNoteToChord(chord, scale,scaleDegree, 4); // Fifth
                    AddNoteToChord(chord, scale,scaleDegree, 7); // Sixth
                    break;
                case ChordType.Diminished:
                    AddNoteToChord(chord, scale,scaleDegree, 1); // Minor Third
                    AddNoteToChord(chord, scale,scaleDegree, 3); // Diminished Fifth
                    break;
                case ChordType.Diminished7th:
                    AddNoteToChord(chord, scale,scaleDegree, 1); // Minor Third
                    AddNoteToChord(chord, scale,scaleDegree, 3); // Diminished Fifth
                    AddNoteToChord(chord, scale,scaleDegree, 6); // Diminished Seventh
                    break;
                case ChordType.HalfDiminished:
                    AddNoteToChord(chord, scale,scaleDegree, 1); // Minor Third
                    AddNoteToChord(chord, scale,scaleDegree, 3); // Diminished Fifth
                    AddNoteToChord(chord, scale,scaleDegree, 6); // Minor Seventh
                    break;
                case ChordType.Augmented:
                    AddNoteToChord(chord, scale,scaleDegree, 2); // Major Third
                    AddNoteToChord(chord, scale,scaleDegree, 5); // Augmented Fifth
                    break;
                case ChordType.Augmented7th:
                    AddNoteToChord(chord, scale,scaleDegree, 2); // Major Third
                    AddNoteToChord(chord, scale,scaleDegree, 5); // Augmented Fifth
                    AddNoteToChord(chord, scale,scaleDegree, 6); // Minor/Major Seventh (choice depends on the musical context)
                    break;
                case ChordType.PowerChord:
                    AddNoteToChord(chord, scale,scaleDegree, 4);  // Fifth
                    break;
                case ChordType.Polychord:
                    // Example: Combine a C Major Triad and an F# Major Triad
                    AddNoteToChord(chord, scale,scaleDegree, 2);  // C Major Triad
                    AddNoteToChord(chord, scale,scaleDegree, 4);
                    AddNoteToChord(chord, scale, scaleDegree + 6, 2); // F# Major Triad (6 semitones above C)
                    AddNoteToChord(chord, scale, scaleDegree + 6, 4);
                    break;
                case ChordType.Quartal:
                    AddNoteToChord(chord, scale,scaleDegree, 3);  // Fourth
                    AddNoteToChord(chord, scale,scaleDegree, 6);  // Another Fourth
                    break;
                case ChordType.Quintal:
                    AddNoteToChord(chord, scale,scaleDegree, 4);  // Fifth
                    AddNoteToChord(chord, scale,scaleDegree, 8);  // Another Fifth
                    break;
                case ChordType.AlteredSeventh:
                    AddNoteToChord(chord, scale,scaleDegree, 2); // Third
                    AddNoteToChord(chord, scale,scaleDegree, 5); // Sharp Fifth (#5)
                                                        //AddNoteToChord(chord, scale,scaleDegree, 3); // Flat Fifth (b5)
                    AddNoteToChord(chord, scale,scaleDegree, 6); // Seventh (usually minor seventh)
                                                        // Add other alterations as needed (e.g., #5, b9)
                    break;
                case ChordType.ExtendedAltered:
                    AddNoteToChord(chord, scale,scaleDegree, 2); // Third
                    AddNoteToChord(chord, scale,scaleDegree, 4); // Fifth (can be altered)
                    AddNoteToChord(chord, scale,scaleDegree, 6); // Seventh (usually minor)
                    AddNoteToChord(chord, scale,scaleDegree, 8); // Ninth (can be altered to #9/b9)
                                                        // Add other extensions and alterations as needed
                    break;
            }

            return TransposeChordToRange(chord, minNote, maxNote);
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
}