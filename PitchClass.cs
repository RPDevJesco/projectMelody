using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Melody
{
    internal static class PitchClass
    {
        public static Dictionary<string, List<int>> pitchClassToMidi = new Dictionary<string, List<int>>
        {
            {"A", new List<int> { 21,33,45,57,69,81,93,105 }},
            {"A#", new List<int> { 22,34,46,58,70,82,94,106 }},
            {"Bb", new List<int> { 22,34,46,58,70,82,94,106 }},
            {"B", new List<int> { 23,35,47,59,71,83,95,107 }},
            {"C", new List<int> { 24,36,48,60,72,84,96,108 }},
            {"C#", new List<int> { 25,37,49,61,73,85,97,109 }},
            {"Db", new List<int> { 25,37,49,61,73,85,97,109 }},
            {"D", new List<int> { 26,38,50,62,74,86,98,110 }},
            {"D#", new List<int> { 27,39,51,63,75,87,99,111 }},
            {"Eb", new List<int> { 27,39,51,63,75,87,99,111 }},
            {"E", new List<int> { 28,40,52,64,76,88,100,112 }},
            {"F", new List<int> { 29,41,53,65,77,89,101,113 }},
            {"F#", new List<int> { 30,42,54,66,78,90,102,114 }},
            {"Gb", new List<int> { 30,42,54,66,78,90,102,114 }},
            {"G", new List<int> { 31,43,55,67,79,91,103,115 }},
            {"G#", new List<int> { 32,44,56,68,80,92,104,116 }},
            {"Ab", new List<int> { 32,44,56,68,80,92,104,116 }},
        };
        public static Dictionary<string, List<string>> scales = new Dictionary<string, List<string>> { 
            // This is the basic C Major scale, serving as a foundation for other modes and scales.
            { "C_Major", new List<string> { "C", "D", "E", "F", "G", "A", "B", "C" } },
            // This scale is derived from the major scale but starts on the sixth degree. It's often used in minor key music.
            { "C_NaturalMinor", new List<string> { "C", "D", "E", "F", "G", "A", "B", "C" } },
            // Similar to the natural minor but with a raised 7th degree, which gives it a distinctive Eastern or classical sound.
            { "C_HarmonicMinor", new List<string> { "C", "D", "E", "F", "G", "A", "B", "C" } },
            // This scale raises both the 6th and 7th degrees when ascending and reverts to the natural minor form when descending.
            { "C_MelodicMinor", new List<string> { "C", "D", "E", "F", "G", "A", "B", "C" } },
            // This scale removes the 4th and 7th degrees from the major scale, commonly used in folk, blues, and rock.
            { "C_PentatonicMajor", new List<string> { "C", "D", "E", "F", "G", "A", "B", "C" } },
            // This is a five-note scale derived from the natural minor scale, often used in blues and rock.
            { "C_PentatonicMinor", new List<string> { "C", "D", "E", "F", "G", "A", "B", "C" } },
            // Similar to the pentatonic minor but with an added ♭5 (flat fifth), known as the "blue note."
            { "C_Blues", new List<string> { "C", "D", "E", "F", "G", "A", "B", "C" } },
            // This is the second mode of the B♭ Major scale, characterized by its minor third and major sixth.
            { "C_Dorian", new List<string> { "C", "D", "E", "F", "G", "A", "B", "C" } },
            // This is the third mode of the A♭ Major scale, known for its flamenco-like sound.
            { "C_Phrygian", new List<string> { "C", "D", "E", "F", "G", "A", "B", "C" } },
            // This is the fourth mode of the G Major scale, characterized by its raised 4th degree.
            { "C_Lydian", new List<string> { "C", "D", "E", "F", "G", "A", "B", "C" } },
            // This is the fifth mode of the F Major scale, often used in rock and jazz.
            { "C_Mixolydian", new List<string> { "C", "D", "E", "F", "G", "A", "B", "C" } },
            // This is the seventh mode of the D♭ Major scale, characterized by its diminished fifth.
            { "C_Locrian", new List<string> { "C", "D", "E", "F", "G", "A", "B", "C" } }
        };
        public static Dictionary<string, List<List<int>>> chordProgressions = new Dictionary<string, List<List<int>>>
        {
            { "C_Major_I_IV_V_I", new List<List<int>> {
                new List<int> { 60, 64, 67 }, // C chord
                new List<int> { 65, 69, 72 }, // F chord
                new List<int> { 67, 71, 74 }, // G chord
                new List<int> { 60, 64, 67 }  // C chord
            }}
            // ... other progressions
        };
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
    }
}