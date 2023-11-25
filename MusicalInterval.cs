internal static class MusicalInterval
{
    public static int GetIntervalMidiNote(int rootMidiNote, Interval interval)
    {
        int semitoneShift = interval switch
        {
            Interval.Root => 0,
            Interval.Third => 4,  // Major third is 4 semitones above the root
            Interval.Fifth => 7,  // Perfect fifth is 7 semitones above the root
            Interval.Seventh => 11, // Major seventh is 11 semitones above the root
            Interval.Ninth => 14,  // Major ninth is 14 semitones (an octave plus a major second)
            Interval.Eleventh => 17, // Perfect eleventh is 17 semitones (an octave plus a perfect fourth)
            Interval.Thirteenth => 21, // Major thirteenth is 21 semitones (an octave plus a major sixth)
            _ => 0,
        };

        return rootMidiNote + semitoneShift;
    }
}

enum Interval
{
    Root,
    Third,
    Fifth,
    Seventh,
    Ninth,
    Eleventh,
    Thirteenth
}
