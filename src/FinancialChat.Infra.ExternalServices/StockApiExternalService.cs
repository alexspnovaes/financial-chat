﻿using FinancialChat.Domain.Interfaces.ExternalServices;
using FinancialChat.Domain.Models.Inputs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FinancialChat.Infra.ExternalServices
{
    public class StockApiExternalService : IStockApiExternalService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;

        public StockApiExternalService(IConfiguration configuration)
        {
            _configuration = configuration;

            _client = new();
            _client.BaseAddress = new Uri(_configuration.GetSection("StockApi").GetSection("URL").Value);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task PostAsync(StockInput model)
        {
            var response = await _client.PostAsJsonAsync("stock",model);
            response.EnsureSuccessStatusCode();
        }
    }
}
