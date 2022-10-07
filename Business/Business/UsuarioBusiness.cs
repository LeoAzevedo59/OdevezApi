using Odevez.Business.Business.Interfaces;
using Odevez.DTO;
using Odevez.Repository.Repositorys.Interfaces;
using System;
using System.Threading.Tasks;

namespace Odevez.Business.Business
{
    public class UsuarioBusiness : IUsuarioBusiness
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAutenticarBusiness _autenticarBusiness;

        public UsuarioBusiness(IUsuarioRepository usuarioRepository, IAutenticarBusiness autenticarBusiness)
        {
            _usuarioRepository = usuarioRepository;
            _autenticarBusiness = autenticarBusiness;
        }

        public async Task<bool> InserirUsuario(UsuarioDTO usuario)
        {
            try
            {
                VerificarAtributos(usuario);

                Random random = new Random();
                bool ExisteUsuario = false;

                bool usuarioJaCadastrado = await VerificarCPF(usuario.Cpf);
                if (usuarioJaCadastrado)
                    throw new("CPF já cadastrado.");

                do
                {
                    usuario.Id = random.Next();
                    ExisteUsuario = await _usuarioRepository.ObterUsuarioPorId(usuario.Id);

                } while (ExisteUsuario);

                usuario.DatUltAlt = DateTime.Now.Date;
                usuario.Apelido = usuario.Nome; //provisorio

                string senhaPassword = _autenticarBusiness.CriptografarSenha(usuario.Senha);
                usuario.SenhaHash = senhaPassword;

                return await _usuarioRepository.InserirUsuario(usuario);
            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }
        }

        private void VerificarAtributos(UsuarioDTO usuario)
        {
            if (string.IsNullOrEmpty(usuario.Nome))
                throw new("O campo Nome não pode ser vazio.");

            if (string.IsNullOrEmpty(usuario.Sobrenome))
                throw new("O campo Sobrenome não pode ser vazio.");

            if (string.IsNullOrEmpty(usuario.Email))
                throw new("O campo E-mail não pode ser vazio.");

            if (string.IsNullOrEmpty(usuario.Cpf))
                throw new("O campo CPF não pode ser vazio.");

            if (string.IsNullOrEmpty(usuario.Celular))
                throw new("O campo Celular não pode ser vazio.");

            if (string.IsNullOrEmpty(usuario.Senha))
                throw new("O campo Senha não pode ser vazio.");

            if (string.IsNullOrEmpty(usuario.ConfirmarSenha))
                throw new("O campo Confirmar Senha não pode ser vazio.");

            if (usuario.Senha != usuario.ConfirmarSenha)
                throw new("A senha está diferente da confirmação da senha.");

            //obterPorCPF
            //validarCPF
            //validarCelular
            //validarEmail
            //validarNome (sem numero)
            //validarSobrenome (sem numero)
        }

        public async Task<bool> VerificarCPF(string cpf)
        {
            return await _usuarioRepository.VerificarCPF(cpf);
        }

        public async Task<UsuarioDTO> ObterUsuarioPorCelular(string celular)
        {
            return await _usuarioRepository.ObterUsuarioPorCelular(celular);
        }
    }
}
