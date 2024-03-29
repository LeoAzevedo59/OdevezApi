﻿using Odevez.Business.Business.Interfaces;
using Odevez.DTO;
using Odevez.Repository.Repositorys.Interfaces;
using Odevez.Services;
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

        public async Task<long> InserirUsuario(UsuarioDTO usuario)
        {
            try
            {
                VerificarAtributos(usuario);

                Random random = new Random();
                bool ExisteUsuario = false;

                bool usuarioJaCadastrado = await VerificarCPF(usuario.Email);
                if (usuarioJaCadastrado)
                    throw new("CPF já cadastrado.");

                do
                {
                    usuario.Id = random.Next();
                    ExisteUsuario = await _usuarioRepository.ObterUsuarioPorId(usuario.Id);

                } while (ExisteUsuario);

                usuario.DatUltAlt = DateTime.Now.Date;
                usuario.Apelido = string.Empty;

                string senhaPassword = _autenticarBusiness.CriptografarSenha(usuario.Senha);
                usuario.SenhaHash = senhaPassword;

                if (await _usuarioRepository.InserirUsuario(usuario))
                    return usuario.Id;

                return 0;
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

            if (string.IsNullOrEmpty(usuario.Celular))
                throw new("O campo Celular não pode ser vazio.");

            if (string.IsNullOrEmpty(usuario.Senha))
                throw new("O campo Senha não pode ser vazio.");

            if (string.IsNullOrEmpty(usuario.ConfirmarSenha))
                throw new("O campo Confirmar Senha não pode ser vazio.");

            if (usuario.Senha != usuario.ConfirmarSenha)
                throw new("A senha está diferente da confirmação da senha.");
        }

        public async Task<bool> VerificarCPF(string cpf)
        {
            return await _usuarioRepository.VerificarCPF(cpf);
        }

        public async Task<UsuarioDTO> ObterUsuarioPorCelular(string celular)
        {
            return await _usuarioRepository.ObterUsuarioPorCelular(celular);
        }

        public async Task<string> InserirApelido(UsuarioDTO usuario)
        {
            try
            {
                var nome = await ObterNomePorCodigo(Convert.ToInt32(usuario.Codigo));
                usuario.Apelido = usuario.Apelido is null ? nome : usuario.Apelido;

                return await _usuarioRepository.InserirApelido(usuario);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> ObterNomePorCodigo(long usuario)
        {
            return await _usuarioRepository.ObterNomePorCodigo(usuario);
        }

        public async Task<string> IncluirImagemPerfilAzure(UploadImageDTO uploadImage)
        {
            var service = new ImageUpdate();
            var retorno = service.UploadBase64Image(uploadImage.ImageBase64, "container");
            await IncluirImagemPerfil(retorno, uploadImage.Usuario);

            return retorno;
        }

        private async Task IncluirImagemPerfil(string nomeImagem, int usuario)
        {
            await _usuarioRepository.IncluirImagemPerfil(nomeImagem, usuario);
        }

        public async Task<bool> Excluir(int user)
        {
            return await _usuarioRepository.Excluir(user);
        }
    }
}
