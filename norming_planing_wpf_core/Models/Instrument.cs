using System.Collections.Generic;

namespace norming_planing_wpf_core
{
    public class Instrument
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<TOType> TOTypes { get; set; }
    }
}
