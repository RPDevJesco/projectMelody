using static ProjectMelodyLibrary.MusicBase;

namespace ProjectMelodyLibrary.Instruments
{
    public static class InstrumentFactory
    {

        public static IEnumerable<InstrumentType> GetAvailableInstruments()
        {
            return Enum.GetValues(typeof(InstrumentType)).Cast<InstrumentType>();
        }
        public static MusicalInstrument GetInstrument(InstrumentType instrumentType)
        {
            switch (instrumentType)
            {
                case InstrumentType.FourStringBass:
                    return new Bass4String();
                case InstrumentType.FiveStringBass:
                    return new Bass5String();
                case InstrumentType.SixStringGuitar:
                    return new GuitarStandard6String();
                case InstrumentType.SevenStringGuitar:
                    return new GuitarStandard7String();
                case InstrumentType.Piano:
                    return new Piano();
                case InstrumentType.Violin:
                    return new Violin();
                default:
                    throw new ArgumentException("Instrument not recognized");
            }
        }
    }
}
