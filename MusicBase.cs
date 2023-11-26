namespace Project_Melody
{
    public static class MusicBase
    {
        /// <summary>
        /// Names of all the scale types this application can utilizie
        /// </summary>
        public enum ScaleType
        {
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

        /// <summary>
        /// Basic Notes that exist with any Scale and Chord in any Key.
        /// </summary>
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

        /// <summary>
        /// Patterns of note lengths to generate rhythms.
        /// </summary>
        public static List<List<int>> rhythmPatterns = new List<List<int>>
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
    }
}
