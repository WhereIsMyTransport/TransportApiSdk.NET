using System.Threading.Tasks;

namespace TransportApi.Sdk.Interfaces
{
    public interface ITokenComponent
    {
        string DefaultErrorMessage { get; }
        Task<string> GetAccessToken();
    }
}
