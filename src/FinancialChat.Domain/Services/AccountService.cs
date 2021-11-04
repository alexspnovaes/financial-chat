using AutoMapper;
using FinancialChat.Domain.Entities;
using FinancialChat.Domain.Interfaces.Services;
using FinancialChat.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialChat.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        public AccountService(UserManager<User> userManager,
            SignInManager<User> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        public async Task<IdentityResult> RegisterAsync(UserModel model)
        {
            var user = _mapper.Map<User>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
                await _signInManager.SignInAsync(user, isPersistent: false);
            return result;
        }
    }
}
