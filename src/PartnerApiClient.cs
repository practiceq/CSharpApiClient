using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntakeQ.ApiClient.Models;


namespace IntakeQ.ApiClient
{
    public class PartnerApiClient : ClientBase
    {
        private static readonly string _baseUrl = "https://intakeq.com/api/partner/";

        public PartnerApiClient(string apiKey) : base(apiKey, _baseUrl)
        {
        }

        public async Task<IEnumerable<Practice>> GetPractices(int? pageNumber = null) => await GetOrThrow<IEnumerable<Practice>>(
            "practice",
            new[]
            {
                ("page", pageNumber.HasValue ? pageNumber.Value.ToString() : null)
            });

        public async Task<Practice> GetPracticeById(string id) =>
            (await GetOrThrow<List<Practice>>(
                "practice",
                new[]
                {
                    ("id", id)
                })
            )
            .FirstOrDefault();

        public async Task<Practice> GetPracticeByExternalId(string externalPracticeId) =>
            (await GetOrThrow<List<Practice>>(
                "practice",
                new[]
                {
                    ("externalPracticeId", externalPracticeId)
                })
            )
            .FirstOrDefault();

        public async Task<Practice> CreatePractice(Practice practice) => await PostOrThrow<Practice>("practice", practice);

        public async Task<Practice> UpdatePractice(Practice practice) => await PutOrThrow<Practice>("practice", practice);

        public async Task<string> GetPracticeApiKey(string idOrExternalPracticeId) => (await GetOrThrow<dynamic>($"practice/{idOrExternalPracticeId}/key"))?.ApiKey;

        public async Task<EphemeralToken> GetEphemeralToken(string idOrExternalPracticeId, string userId = null) => await GetOrThrow<EphemeralToken>(
            $"practice/{idOrExternalPracticeId}/ephemeral",
            new[]
            {
                ("userId", userId)
            });

        public async Task<List<FormTemplate>> ListMasterForms() => await GetOrThrow<List<FormTemplate>>("masterForms");

        public async Task<List<FormTemplate>> ListPracticeForms(string practiceId) => await GetOrThrow<List<FormTemplate>>($"practice/{practiceId}/forms");

        public async Task<FormTemplate> CopyForm(CopyFormOptions options) => await PostOrThrow<FormTemplate>("practice/copyForm", options);
    }
}
