public static class ArpeggioGenerator
{
    public static List<int> GenerateArpeggio(List<int> chord, int numberOfNotes)
    {
        var arpeggio = new List<int>();
        int chordSize = chord.Count;

        // Repeating the chord notes to reach the desired number of notes
        for (int i = 0; i < numberOfNotes; i++)
        {
            // Cycling through the chord notes
            int noteIndex = i % chordSize;
            arpeggio.Add(chord[noteIndex]);
        }

        return arpeggio;
    }

    public static List<List<int>> GenerateArpeggiosFromChords(List<List<int>> chords, int numberOfNotesPerArpeggio)
    {
        var arpeggios = new List<List<int>>();
        foreach (var chord in chords)
        {
            var arpeggio = GenerateArpeggio(chord, numberOfNotesPerArpeggio);
            arpeggios.Add(arpeggio);
        }

        return arpeggios;
    }
}