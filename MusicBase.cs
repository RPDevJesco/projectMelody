namespace ProjectMelodyLibrary
{
    public static class MusicBase
    {
        /// <summary>
        /// Available instruments
        /// </summary>
        public enum InstrumentType
        {
            FourStringBass,
            FiveStringBass,
            SixStringGuitar,
            SevenStringGuitar,
            Piano,
            Violin
        }

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
        /// Chords should have their own separate rhythm patterns. They don't need eighth notes and below.
        /// </summary>
        public static List<List<int>> chordRhythmPatterns = new List<List<int>>
        {
            // All whole notes
            new List<int> { 1920 },
            // dotted half note
            new List<int> { 1440, 480},
            new List<int> { 480, 1440},
            // All Half notes
            new List<int> { 960, 960 },
            // quarter and Half notes
            new List<int> { 480, 480, 960 },
            new List<int> { 960, 480, 480 },
            new List<int> { 480, 960, 480 },
            // All quarter notes
            new List<int> { 480, 480, 480, 480 },
            // dotted eighth notes with quarter
            new List<int> { 360, 360, 360, 360, 360, 480 },
            new List<int> { 360, 360, 360, 360, 480, 360 },
            new List<int> { 360, 360, 480, 360, 360, 360 },
            new List<int> { 360, 480, 360, 360, 360, 360 },
            new List<int> { 480, 360, 360, 360, 360, 360 }
        };

        /// <summary>
        /// Patterns of note lengths to generate rhythms.
        /// </summary>
        public static List<List<int>> rhythmPatterns = new List<List<int>>
        {
            // All whole notes
            new List<int> { 1920 },
            // dotted half note
            new List<int> { 1440, 480},
            new List<int> { 480, 1440},
            // All Half notes
            new List<int> { 960, 960 },
            // quarter and Half notes
            new List<int> { 480, 480, 960 },
            new List<int> { 960, 480, 480 },
            new List<int> { 480, 960, 480 },
            // All quarter notes
            new List<int> { 480, 480, 480, 480 },
            // dotted eighth notes with a sixteenth
            new List<int> { 360, 360, 360, 360, 360, 360, 120 },
            new List<int> { 360, 360, 360, 360, 360, 120, 360 },
            new List<int> { 360, 360, 360, 360, 120, 360, 360 },
            new List<int> { 360, 360, 360, 120, 360, 360, 360 },
            new List<int> { 360, 360, 120, 360, 360, 360, 360 },
            new List<int> { 360, 120, 360, 360, 360, 360, 360 },
            new List<int> { 120, 360, 360, 360, 360, 360, 360 },
            // dotted eighth notes with quarter
            new List<int> { 360, 360, 360, 360, 360, 480 },
            new List<int> { 360, 360, 360, 360, 480, 360 },
            new List<int> { 360, 360, 480, 360, 360, 360 },
            new List<int> { 360, 480, 360, 360, 360, 360 },
            new List<int> { 480, 360, 360, 360, 360, 360 },
            // All eighth notes
            new List<int> { 240, 240, 240, 240, 240, 240, 240, 240 },
            // Mixed quarter and eighth notes
            new List<int> { 480, 240, 240, 480, 240, 240 },
            // Mixed quarter and eighth notes
            new List<int> { 480, 480, 240, 240, 240, 240 },
            // Mixed quarter and eighth notes
            new List<int> { 240, 240, 240, 240, 480, 480 },
            new List<int> { 240, 240, 240, 240, 240, 240, 480 },
            new List<int> { 720, 240, 720, 240 },
            new List<int> { 480, 720, 240 },
            new List<int> { 240, 720, 480, 240 },
            new List<int> { 1440, 240, 240 },
            new List<int> { 120, 720, 120, 480, 120, 240 },
            new List<int> { 720, 120, 120, 720 },
            new List<int> { 480, 120, 120, 120, 1440 },
            new List<int> { 720, 720, 480 },
            new List<int> { 120, 1440, 120, 120, 120 },
            new List<int> { 480, 240, 240, 240, 240, 240, 240 },
            new List<int> { 240, 240, 240, 480, 240, 240, 240 },
            new List<int> { 480,240,240,120,120,120,120,240,240,120,120,120,120 },
            new List<int> { 480,120,120,480,120,120,480,120,120,480 },
            new List<int> { 480,480,240,120,120,240,120,120,240 },
            new List<int> { 480,240,120,120,240,480,120,120,240 },
            new List<int> { 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120 },
            new List<int> { 120, 240, 60 },
            new List<int> { 120, 1440, 120, 240 },
            new List<int> { 480, 240, 60, 120 },
            new List<int> { 960, 240, 120, 120 },
            new List<int> { 60, 1440, 60, 300, 60 },
            new List<int> { 240, 240, 240, 480, 480, 480 },
            // All thirty-second notes
            new List<int> { 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60 }
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

        public static List<List<int>> CommonProgressions = new List<List<int>>
        {
            new List<int> { 1, 4, 5, 1 }, // I-IV-V-I: This is one of the most fundamental and widely used sequences in Western music
            new List<int> { 1, 5, 6, 4 }, // I-V-vi-IV: This is a very popular progression in modern music
            new List<int> { 2, 5, 1 }, // ii-V-I: A fundamental progression in jazz
            new List<int> { 6, 4, 1, 5 }, // vi-IV-I-V: Often used in pop music
            new List<int> { 1, 6, 4, 5 }, // I-vi-IV-V: A classic doo-wop progression
            new List<int> { 1, 4, 5 }, // I-IV-V: A basic progression often used in blues and rock
            new List<int> { 1, 2, 4, 5 }, // I-ii-IV-V: A variation of the I-IV-V with an added ii degree
            new List<int> { 1, 7, 6, 5 }, // i-bVII-bVI-V: A common minor progression
            new List<int> { 1, 5, 6, 3, 4, 1, 4, 5 }, // I-V-vi-iii-IV-I-IV-V: A longer progression often found in pop ballads
            new List<int> { 1, 4, 7, 3, 6, 2, 5, 1 }, // i-iv-VII-III-VI-ii°-V-i: A full diatonic cycle in a minor key
            new List<int> { 1, 2, 3, 5 },
            new List<int> { 6, 7, 1 }, // Flat VI - Flat VII - I Progression
            new List<int> { 1, 4, 6, 5 },
            new List<int> { 3, 6, 2, 5 },
            new List<int> { 1, 7, 6, 5 }, // chromatic Descent Progression
            new List<int> { 1, 6, 3, 7}, // Modal Interchange Progression
            new List<int> { 1, 4, 7, 3 }, // Ascending Fourth Progression
            new List<int> { 1, 1, 1, 1 }, // Line Cliché Progression
            new List<int> { 1, 2, 3, 4 }, // Whole Tone Progression
            new List<int> { 5, 2, 5, 1 }, // Secondary Dominant Series
            new List<int> { 1, 4, 6, 3 } // Minor Key Shift Progression
        };
    }
}
