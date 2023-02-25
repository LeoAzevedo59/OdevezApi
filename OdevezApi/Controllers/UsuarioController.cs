﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Odevez.Business.Business.Interfaces;
using Odevez.DTO;
using System;
using System.Threading.Tasks;

namespace Odevez.API.Controllers
{
    [Route("api/v1/[Controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioBusiness _usuarioBusiness;
        private readonly IAutenticarBusiness _autenticarBusiness;

        public UsuarioController(IUsuarioBusiness usuarioBusiness, IAutenticarBusiness autenticarBusiness)
        {
            _usuarioBusiness = usuarioBusiness;
            _autenticarBusiness = autenticarBusiness;
        }

        [HttpGet]
        [Route("entrar")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginClient(string celular, string senha)
        {
            var tokenUsuario = new TokenUsuarioDTO();
            var usuario = new UsuarioDTO();

            try
            {
                if (string.IsNullOrEmpty(senha))
                    return BadRequest("Senha é obrigatório.");

                var retorno = await _autenticarBusiness.LoginUsuario(celular, senha);

                if (retorno != null)
                    usuario = await _usuarioBusiness.ObterUsuarioPorCelular(celular);
                else
                    return BadRequest("Senha incorreta ou usário não cadastrado.");
                tokenUsuario.Token = retorno.Token;
                tokenUsuario.Type = retorno.Type;
                tokenUsuario.Apelido = usuario.Apelido;
                tokenUsuario.Codigo = usuario.Codigo;
                tokenUsuario.Imagem = usuario.Imagem;

                return Ok(tokenUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("cadastrar")]
        [AllowAnonymous]
        public async Task<IActionResult> InserirUsuario([FromBody] UsuarioDTO usuario)
        {
            try
            {
                var retorno = await _usuarioBusiness.InserirUsuario(usuario);

                if (retorno)
                    return Ok("Usuário cadastrado com sucesso.");

                return BadRequest("Erro");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("inserir-apelido")]
        public async Task<IActionResult> InserirApelido([FromBody] UsuarioDTO usuario)
        {
            try
            {
                var retorno = await _usuarioBusiness.InserirApelido(usuario);

                if (!string.IsNullOrEmpty(retorno))
                    return Ok(retorno);

                return BadRequest("Erro");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("obter-nome")]
        public async Task<IActionResult> ObterNomePorCodigo(int usuario)
        {
            try
            {
                var retorno = await _usuarioBusiness.ObterNomePorCodigo(usuario);
                return Ok(retorno);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("incluir-imagem-perfil")]
        public async Task<IActionResult> IncluirImagemPerfil([FromBody] UploadImageDTO uploadImage)
        {
            return Ok(await _usuarioBusiness.IncluirImagemPerfilAzure(uploadImage));
        }

        [HttpDelete]
        [Route("excluir")]
        public async Task<IActionResult> Excluir(int user)
        {
            return Ok(await _usuarioBusiness.Excluir(user));
        }
    }
}
