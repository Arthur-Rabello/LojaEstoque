namespace LojaEstoque.Dominio.Exceptions
{
    public class RegraDeNegocioException : Exception
    {
        #region Construtor
        public RegraDeNegocioException(string message) : base(message)
        {
        }
        #endregion
    }
}
