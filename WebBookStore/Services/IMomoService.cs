using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using WebBookStore.Models;

namespace WebBookStore.Services
{
    public interface IMomoService
    {
        Task<MomoCreatePaymentResponse> CreatePaymentAsync(OrderInfo model); 
        MomoExecuteResponse PaymentExecuteAsync(IQueryCollection collection);
    }
}
