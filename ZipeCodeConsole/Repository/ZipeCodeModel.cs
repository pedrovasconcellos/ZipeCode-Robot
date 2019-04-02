using System;
using System.ComponentModel.DataAnnotations;

namespace ZipeCodeConsole.Repository
{
    public class ZipeCode
    {
        [Key]
        public string cep { get; set; }
        public string bairro { get; set; }
        public string logradouro { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public short? ddd { get; set; }
        public int? ibge { get; set; }
        public decimal? altitude { get; set; }
        public decimal? latitude { get; set; }
        public decimal? longitude { get; set; }
        public DateTime datetime { get; set; }
    }
}
