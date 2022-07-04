using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VehicalRegistrationSystem.Data;
using VehicalRegistrationSystem.Model;
using VehicleRegistrationSystem.Dtos;

namespace VehicleRegistrationSystem.Services.Owner
{
    public class OwnerService : IOwnerService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public OwnerService(DataContext context , IMapper mapper , IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<string>> LogIn(String Email , String Password)
        {
            var responce = new ServiceResponse<string>();

            var Owner = await _context.Owners.FirstOrDefaultAsync(x => x.Email == Email);

            if (Owner == null)
            {
                responce.Success = false;
                responce.message = "Owner not found !!!";
            }

            else if (!VerifyPasswordHash(Password, Owner.PasswordHash, Owner.passwordSalt))
            {
                responce.Success = false;
                responce.message = "password is wrong !!!";
            }

            else
            {
                responce.Data = CreateToken(Owner);
                responce.message = "Login done !!!"; 
            }
            return responce;
        }

        public async Task<ServiceResponse<int>> Register(OwnerRegisterDto newOwner)
        {
            var serviceResponce = new ServiceResponse<int>();
            var owner1 = _mapper.Map<VehicleRegistrationSystem.Model.Owner>(newOwner);
            if (await UserExist(newOwner.Email))
            {
                serviceResponce.Success = false;
                serviceResponce.message = "Owner already exist";
            }


            else
            {
                CreateHashPass(newOwner.password, out byte[] PasswordHash, out byte[] PasswordSalt);

                owner1.PasswordHash = PasswordHash;
                owner1.passwordSalt = PasswordSalt;

                _context.Owners.Add(owner1);
                await _context.SaveChangesAsync();

                serviceResponce.Data = owner1.Id;

            }

            return serviceResponce;
        }


       

      

            

        public async Task<bool> UserExist(string Email)
        {
            if (await _context.Owners.AnyAsync(x => x.Email.ToLower().Equals(Email.ToLower())))
            {
                return true;
            }

            return false;
        }

        private void CreateHashPass(String Password, out byte[] passwordHash, out byte[] PasswordSalt)

        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
                PasswordSalt = hmac.Key;
            }
        }


        private bool VerifyPasswordHash(String Password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(PasswordSalt))
            {
                var ComputeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));

                for (int i = 0; i < ComputeHash.Length; i++)
                {
                    if (ComputeHash[i] != PasswordHash[i])
                    {
                        return false;
                    }


                }

                return true;
            }
        }


        private String CreateToken(VehicleRegistrationSystem.Model.Owner owner)
        {
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, owner.Id.ToString()),
                new Claim(ClaimTypes.Name, owner.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(Claims),
                Expires = System.DateTime.Now.AddDays(1),
                SigningCredentials = creds


            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);


        }

    }
}
