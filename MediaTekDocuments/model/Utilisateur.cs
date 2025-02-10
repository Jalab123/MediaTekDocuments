
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Utilisateur ()
    /// </summary>
    public class Utilisateur
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Pwd { get; set; }
        public string IdService { get; set; }

        public Utilisateur(string Id, string Login, string Pwd, string IdService)
        {
            this.Id = Id;
            this.Login = Login;
            this.Pwd = Pwd;
            this.IdService = IdService;
        }
    }
}
