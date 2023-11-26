using static Project_Melody.MusicBase;

namespace Project_Melody
{
    public static class MelodyGenerator
    {
        private static Random random = new Random();

        public static List<int> GenerateMelody(BasicNote rootNote, ScaleType scaleType, int length, int minNote, int maxNote)
        {
            Console.WriteLine("GenerateMelody called.");
            var originalScale = GenerateScale((int)rootNote, scaleType);
            var adjustedScale = GetRandomNoteFromScale(originalScale, minNote, maxNote);
            var melody = new List<int>();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(adjustedScale.Count);
                int note = adjustedScale[index];
                melody.Add(note);
            }
            return melody;
        }

        private static List<int> GetRandomNoteFromScale(List<int> scale, int minNote, int maxNote)
        {
            Console.WriteLine("GetRandomNoteFromScale called.");

            // Adjust the scale notes to fit within the instrument's range
            var adjustedScale = new List<int>(scale);

            // Check if transposition is needed
            if (adjustedScale.Any(note => note < minNote || note > maxNote))
            {
                int minChordNote = adjustedScale.Min();
                int maxChordNote = adjustedScale.Max();

                // Transpose up if the lowest note of the chord is below the minNote
                while (minChordNote < minNote)
                {
                    adjustedScale = adjustedScale.Select(note => note + 12).ToList();
                    minChordNote += 12;
                }

                // Transpose down if the highest note of the chord is above the maxNote
                while (maxChordNote > maxNote)
                {
                    adjustedScale = adjustedScale.Select(note => note - 12).ToList();
                    maxChordNote -= 12;
                }
            }

            return adjustedScale;
        }
    }
}