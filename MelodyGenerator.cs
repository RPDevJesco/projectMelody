using static ProjectMelodyLibrary.MusicBase;

namespace ProjectMelodyLibrary
{
    public static class MelodyGenerator
    {
        private static Random random = new Random();

        public static List<int> GenerateMelody(BasicNote rootNote, ScaleType scaleType, int length, int minNote, int maxNote)
        {
            var originalScale = GenerateScale((int)rootNote, scaleType);
            var adjustedScale = TransposeToInstrument(originalScale, minNote, maxNote);
            var melody = new List<int>();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(adjustedScale.Count);
                int note = adjustedScale[index];
                melody.Add(note);
            }
            return melody;
        }

        public static List<int> GenerateMusicalPhrase(int startNote, int phraseLength, ScaleType scaleType, int length, int minNote, int maxNote)
        {
            var originalScale = GenerateScale(startNote, scaleType);
            var phrase = new List<int>();
            int currentNote = startNote;

            // Adjusting the scale to fit the minNote and maxNote constraints
            var adjustedScale = TransposeToInstrument(originalScale, minNote, maxNote);

            int notesAdded = 0; // Count of notes added to the phrase

            for (int i = 0; i < length; i++)
            {
                if (notesAdded < phraseLength)
                {
                    int nextNoteIndex = GetNextNoteIndex(adjustedScale, currentNote);
                    int nextNote = adjustedScale[nextNoteIndex];

                    // Add the next note to the phrase
                    phrase.Add(nextNote);
                    currentNote = nextNote;

                    notesAdded++;
                }
                else
                {
                    // If the phrase length has been reached, do not add more notes
                    break;
                }
            }

            return phrase;
        }

        private static int GetNextNoteIndex(List<int> scale, int currentNote)
        {
            // Implement logic to select the next note index
            // This could involve checking the current note, and based on it, deciding the next note
            // You could use random selection but with constraints like limiting interval size, etc.

            // Example: (Simple version)
            int currentIndex = scale.IndexOf(currentNote);
            int nextIndex = random.Next(Math.Max(0, currentIndex - 2), Math.Min(scale.Count, currentIndex + 3));
            // This ensures that we mostly move stepwise, with occasional leaps

            return nextIndex;
        }

        private static List<int> TransposeToInstrument(List<int> scale, int minNote, int maxNote)
        {
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