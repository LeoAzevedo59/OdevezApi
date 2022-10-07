using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Odevez.Business.Business.Interfaces;
using Odevez.DTO;
using Odevez.Repository.Repositorys.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Odevez.Business.Business
{
    public class AutenticarBusiness : IAutenticarBusiness
    {
        private readonly IAutenticarRepository _autenticarRepository;
        private readonly AuthSettings _authSettings;
        public AutenticarBusiness(IAutenticarRepository autenticarRepository, IOptions<AuthSettings> authSettings)
        {
            _autenticarRepository = autenticarRepository;
            _authSettings = authSettings.Value;
        }

        #region Métodos privados

        private bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

        private async Task<UsuarioDTO> ObterPasswordHash(string celular)
        {
            var retorno = await _autenticarRepository.ObterPasswordHash(celular);
            return retorno;
        }

        #endregion

        #region Métodos públicos

        public string CriptografarSenha(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }

        public async Task<TokenDTO> LoginUsuario(string celular, string senha)
        {
            try
            {
                var usuarioDTO = await ObterPasswordHash(celular);

                if (usuarioDTO != null && VerifyPassword(senha.Trim(), usuarioDTO.SenhaHash))
                    return GenerateTokenAsync(usuarioDTO);

                return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private TokenDTO GenerateTokenAsync(UsuarioDTO usuario)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var date = DateTime.UtcNow;
                var expire = date.AddHours(_authSettings.ExpireInHours);
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.Secret));

                var securityToken = tokenHandler.CreateToken(new SecurityTokenDescriptor()
                {
                    Issuer = "a3xZF9D8SM7Pxy8DTqY2a84M9aLKU3UG",
                    IssuedAt = date,
                    NotBefore = date,
                    Expires = expire,
                    SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                    Subject = new ClaimsIdentity(new GenericIdentity(usuario.Id.ToString(), JwtBearerDefaults.AuthenticationScheme),
                    new[]
                    {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
                    })
                });

                var response = new TokenDTO()
                {
                    Token = tokenHandler.WriteToken(securityToken),
                    ExpireIn = _authSettings.ExpireInHours,
                    Type = JwtBearerDefaults.AuthenticationScheme
                };

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

    }
}