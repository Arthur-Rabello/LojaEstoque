namespace LojaEstoque.Dominio.Interfaces
{
    public interface IServSenha
    {
        public string GerarHash(string senha);
        public bool VerificarSenha(string senha, string senhaHash);
    }
}
