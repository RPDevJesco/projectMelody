namespace Project_Melody.Instruments
{
    public abstract class MusicalInstrument
    {
        public string Name { get; private set; }
        public int MinNote { get; private set; }
        public int MaxNote { get; private set; }

        protected MusicalInstrument(string name, int minNote, int maxNote)
        {
            Name = name;
            MinNote = minNote;
            MaxNote = maxNote;
        }
    }
}