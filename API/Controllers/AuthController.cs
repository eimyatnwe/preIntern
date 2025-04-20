using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models.DTO;
using API.Models.Domain;
using API.Data;
using API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthController : ControllerBase
    {   
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;
        private readonly ApplicationDbContext dbContext;
        
        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository, ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        
        

        //POST:api/auth/login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request){
            var identityUser = await userManager.FindByEmailAsync(request.Email);
            
            //check email
            if(identityUser is not null){
                //check password
                var checkPasswordResult = await userManager.CheckPasswordAsync(identityUser,request.Password);
                
                if(checkPasswordResult){

                    var roles = await userManager.GetRolesAsync(identityUser);
                    

                        var jwtToken = tokenRepository.CreateJwtToken(identityUser, roles.ToList());
                        var response = new LoginResponseDto()
                        {
                            Email = request.Email,
                            Roles = roles.ToList(),
                            Token = jwtToken,
                        };
                        return Ok(response);
                }
            }
            ModelState.AddModelError("","Email or Password is incorrect");
            return ValidationProblem(ModelState);
        }   

        //POST:api/auth/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request){
            //Create IdentityUser Object
            var user = new IdentityUser{
                UserName = request.Email?.Trim(),
                Email = request.Email?.Trim()
            };

            //Create user
            var identity = await userManager.CreateAsync(user,request.Password);

            if(identity.Succeeded){
                identity = await userManager.AddToRoleAsync(user,"Reader");
                if(identity.Succeeded){
                    var member = new Member{
                        Id = Guid.Parse(user.Id),
                        Email = request.Email,
                        Name = request.Email.Split('@')[0]
                    };
                    await dbContext.Members.AddAsync(member);
                    await dbContext.SaveChangesAsync();

                    return Ok(new {Message = "User Created Successfully"});
                }else{
                    if(identity.Errors.Any()){
                        foreach (var error in identity.Errors)
                        {
                            ModelState.AddModelError("",error.Description);
                        }
                    
                    }
                } 
            }
            else{
                if(identity.Errors.Any()){
                    foreach (var error in identity.Errors)
                    {
                        ModelState.AddModelError("",error.Description);
                    }
                }
            }

            return ValidationProblem(ModelState);
        }

        
    
        
    }
}