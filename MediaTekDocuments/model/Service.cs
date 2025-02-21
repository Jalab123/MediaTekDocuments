
using System;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Service ()
    /// </summary>
    public class Service
    {
        public string Id { get; set; }
        public string Nom { get; set; }
        public Service(string Id, string Nom)
        {
            this.Id = Id;
            this.Nom = Nom;
        }
    }
}
