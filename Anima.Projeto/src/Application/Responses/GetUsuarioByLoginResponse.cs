namespace Anima.Projeto.Application.Requests
{
    public class GetUsuarioByLoginResponse : PessoaResponse
    {
        public string Login { get; set; }
        public string Token { get; set; }

    }
}
