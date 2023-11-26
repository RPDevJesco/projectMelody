namespace Project_Melody.Instruments
{
    public static class InstrumentFactory
    {
        public static MusicalInstrument GetInstrument(string instrumentName)
        {
            switch (instrumentName)
            {
                case "4 String Bass":
                    return new Bass4String();
                case "5 String Bass":
                    return new Bass5String();
                case "6 String Guitar":
                    return new GuitarStandard6String();
                case "7 String Guitar":
                    return new GuitarStandard7String();
                case "Piano":
                    return new Piano();
                case "Violin":
                    return new Violin();
                default:
                    throw new ArgumentException("Instrument not recognized");
            }
        }
    }
}
