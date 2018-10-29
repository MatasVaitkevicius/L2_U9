namespace L1_U1_9
{
    /// <summary>
    /// Klasė sauganti direktorių duomenis
    /// </summary>
    class Director
    {
        public string Name { get; set; }   // Direktoriaus Vardas
        public int Amount { get; set; }    // Direktoriaus sukurtų filmų rodiklis

        /// <summary>
        /// Konstruktrorius su klasės savybėmis
        /// </summary>
        public Director (string name, int amount)
        {
            Name = name;
            Amount = amount;
        }
    }

}
