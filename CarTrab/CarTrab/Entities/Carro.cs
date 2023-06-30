namespace CarTrab.Entities
{
    public class Carro
    {
        public string id { get; set; }
        public string id_modelo { get; set; }
        public string name { get; set; }
        public string renavam { get; set; }
        public string placa { get; set; }
        public double valor { get; set; }
        public DateOnly Ano { get; set; }
    }   
}
