using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IntakeQ.ApiClient.Models;
using System.IO;
using Client = IntakeQ.ApiClient.Models.Client;
using ClientProfile = IntakeQ.ApiClient.Models.ClientProfile;

namespace IntakeQ.ApiClient
{
    public class ApiClient : ClientBase
    {
        private static readonly string _baseUrl = "https://intakeq.com/api/v1/";

        public ApiClient(string apiKey) : base(apiKey, _baseUrl)
        {
        }

        public async Task<IEnumerable<IntakeSummary>> GetIntakesSummary(string clientSearch, DateTime? startDate = null, DateTime? endDate = null, int? clientId = null, string externalClientId = null, int? pageNumber = null, bool getAll = false) => await GetOrThrow<IEnumerable<IntakeSummary>>(
            "intakes/summary",
            new[]
            {
                ("client", clientSearch),
                ("startDate", startDate.Value.ToString("yyyy-MM-dd")),
                ("endDate", endDate.Value.ToString("yyyy-MM-dd")),
                ("clientId", clientId.HasValue ? clientId.Value.ToString() : null),
                ("externalClientId", externalClientId),
                ("page", pageNumber.HasValue ? pageNumber.Value.ToString(): null),
                ("all", "true")
            });

        public async Task<Intake> GetFullIntake(string intakeId) => await GetOrThrow<Intake>($"intakes/{intakeId}");

        public async Task<Intake> SendForm(SendForm sendForm) => await PostOrThrow<Intake>("intakes/send", sendForm);

        /// <summary>
        /// Creates a form, but doesn't send it to the client. 
        /// </summary>
        public async Task<Intake> CreateForm(SendForm sendForm) => await PostOrThrow<Intake>("intakes/create", sendForm);

        public async Task<IntakeAuthToken> CreateFormAuthToken(string intakeId) => await PostOrThrow<IntakeAuthToken>($"intakes/{intakeId}/token");

        public async Task<byte[]> DownloadPdf(string id, bool isNote = false) => await GetBytesOrThrow($"{(isNote ? "notes" : "intakes")}/{id}/pdf");

        public async Task DownloadPdfAndSave(string id, string destinationPath, bool isNote = false)
        {
            var requestType = isNote ? "notes" : "intakes";
            HttpResponseMessage response = await _httpClient.GetAsync($"{requestType}/{id}/pdf");
            response.EnsureSuccessStatusCode();
            using (var contentStream = await response.Content.ReadAsStreamAsync())
            using (var stream = new FileStream(destinationPath, FileMode.CreateNew))
            {
                await contentStream.CopyToAsync(stream);
            }
        }

        public async Task<IEnumerable<Client>> GetClients(string search = null, int? pageNumber = null, DateTime? dateCreatedStart = null, DateTime? dateCreatedEnd = null, Dictionary<string, string> customFields = null, string externalClientId = null)
        {
            {
                var fields = new List<(string, string)>{
                    ("search", search),
                    ("page", pageNumber.HasValue ? pageNumber.Value.ToString() : null),
                    ("dateCreatedStart", dateCreatedStart.HasValue ? dateCreatedStart.Value.ToString("yyyy-MM-dd"): null),
                    ("dateCreatedEnd", dateCreatedEnd.HasValue ? dateCreatedEnd.Value.ToString("yyyy-MM-dd"): null),
                    ("externalClientId", externalClientId)
                };
                if (customFields != null)
                {
                    fields.AddRange(customFields?.Select(cf => ($"custom[{cf.Key}]", cf.Value)));
                }

                return await GetOrThrow<IEnumerable<Client>>("clients", fields.ToArray());
            }
        }

        public async Task<ClientProfile> GetClientProfile(int clientId) => await GetOrThrow<ClientProfile>($"clients/profile/{clientId}");

        public async Task<IEnumerable<ClientProfile>> GetClientsWithProfile(string search = null, int? pageNumber = null, DateTime? dateCreatedStart = null, DateTime? dateCreatedEnd = null, Dictionary<string, string> customFields = null, string externalClientId = null)
        {
            var fields = new List<(string, string)>{
                ("search", search),
                ("page", pageNumber.HasValue ? pageNumber.Value.ToString() : null),
                ("includeProfile", "true"),
                ("dateCreatedStart", dateCreatedStart.HasValue ? dateCreatedStart.Value.ToString("yyyy-MM-dd") : null),
                ("dateCreatedEnd", dateCreatedEnd.HasValue ? dateCreatedEnd.Value.ToString("yyyy-MM-dd") : null),
                ("externalClientId", externalClientId)
            };
            if (customFields != null)
            {
                fields.AddRange(customFields?.Select(cf => ($"custom[{cf.Key}]", cf.Value)));
            }

            return await GetOrThrow<IEnumerable<ClientProfile>>("clients", fields.ToArray());
        }

        public async Task<ClientProfile> SaveClient(ClientProfile clientProfile) => await PostOrThrow<ClientProfile>("clients", clientProfile);

        public async Task<byte[]> DownloadAttachment(string attachmentId) => await GetBytesOrThrow($"attachments/{attachmentId}");

        public async Task DownloadAttachmentAndSave(string attachmentId, string destinationPath)
        {
            var response = await _httpClient.GetAsync($"attachments/{attachmentId}");
            response.EnsureSuccessStatusCode();
            using (var contentStream = await response.Content.ReadAsStreamAsync())
            using (var stream = new FileStream(destinationPath, FileMode.CreateNew))
            {
                await contentStream.CopyToAsync(stream);
            }
        }

        public async Task<Intake> UpdateOfficeUseAnswers(Intake intake) => await PostOrThrow<Intake>("intakes",
            //Let's clean the intake object to keep only the data we need to send
            new Intake()
            {
                Id = intake.Id,
                Questions = intake.Questions.Where(x => x.OfficeUse).ToList()
            });

        public async Task<IEnumerable<Appointment>> GetAppointments(DateTime? startDate = null, DateTime? endDate = null, string status = null, string clientSearch = null, string practitionerEmail = null, int? pageNumber = null) => await GetOrThrow<IEnumerable<Appointment>>(
                "appointments",
                new[]
                {
                    ("client", clientSearch),
                    ("status", status),
                    ("practitionerEmail", practitionerEmail),
                    ("startDate", startDate.HasValue ? startDate.Value.ToString("yyyy-MM-dd") : null),
                    ("endDate", endDate.HasValue ? endDate.Value.ToString("yyyy-MM-dd") : null),
                    ("page", pageNumber.HasValue ? pageNumber.Value.ToString(): null)
                });

        public async Task<Appointment> GetAppointment(string appointmentId) => await GetOrThrow<Appointment>($"appointments/{appointmentId}");

        public async Task<AppointmentSettings> GetAppointmentSettings() => await GetOrThrow<AppointmentSettings>("appointments/settings");

        public async Task<IEnumerable<Questionnaire>> GetQuestionnaires() => await GetOrThrow<IEnumerable<Questionnaire>>("questionnaires");

        public async Task<IEnumerable<Invoice>> GetInvoicesByClient(int clientId) => await GetOrThrow<IEnumerable<Invoice>>(
            "invoices",
            new[]
            {
                ("clientId", clientId.ToString())
            });

        public async Task<Appointment> CreateAppointment(CreateAppointmentDto dto) => await PostOrThrow<Appointment>("appointments", dto);

        public async Task<Appointment> UpdateAppointment(UpdateAppointmentDto dto) => await PutOrThrow<Appointment>("appointments", dto);

        public async Task<IEnumerable<Models.File>> GetFilesByClient(string clientId) => await GetOrThrow<IEnumerable<Models.File>>(
            "files",
            new[]
            {
                ("clientId", clientId)
            });

        public async Task<byte[]> GetFile(string id) => await GetBytesOrThrow($"files/{id}");

        public async Task<IEnumerable<Folder>> GetFolders() => await GetOrThrow<IEnumerable<Folder>>("folders");

        public async Task UploadFile(string clientId, byte[] fileData, string fileName, string contentType)
        {
            using (var content = new MultipartFormDataContent())
            using (var streamContent = new StreamContent(new MemoryStream(fileData)))
            {
                streamContent.Headers.Add("Content-Type", "application/octet-stream");
                streamContent.Headers.Add("Content-Disposition", "form-data; name=\"file\"; filename=\"" + fileName + "\"");
                streamContent.Headers.ContentType.MediaType = contentType;

                content.Add(streamContent, "file", fileName);

                var response = await _httpClient.PostAsync(new Uri($"files/{clientId}"), content);
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task DeleteFile(string id) => await DeleteOrThrow($"files/{id}");

        public async Task<IEnumerable<Practitioner>> ListPractitioners(bool includeInactive = false) => await GetOrThrow<IEnumerable<Practitioner>>($"practitioners?includeInactive={includeInactive}");

        public async Task<Practitioner> CreatePractitioner(Practitioner practitioner) => await PostOrThrow<Practitioner>("practitioners", practitioner);

        public async Task<Practitioner> UpdatePractitioner(Practitioner practitioner) => await PutOrThrow<Practitioner>("practitioners", practitioner);

        public async Task DisablePractitioner(string id) => await PostOrThrow($"practitioners/{id}/disable");

        public async Task EnablePractitioner(string id) => await PostOrThrow($"practitioners/{id}/enable");

        public async Task DeletePractitioner(string id) => await DeleteOrThrow($"practitioners/{id}");

        public async Task TransferPractitionerData(string sourcePractitionerId, string destinationPractitionerId) => await PostOrThrow($"practitioners/{sourcePractitionerId}/transferData/{destinationPractitionerId}");

        public async Task TransferClientOwnership(string sourcePractitionerId, string destinationPractitionerId) => await PostOrThrow($"practitioners/{sourcePractitionerId}/transferClientOwnership/{destinationPractitionerId}");

        public async Task<IEnumerable<Assistant>> ListAssistants() => await GetOrThrow<IEnumerable<Assistant>>("assistants");

        public async Task<Assistant> CreateAssistant(Assistant assistant) => await PostOrThrow<Assistant>("assistants", assistant);

        public async Task<Assistant> UpdateAssistant(Assistant assistant) => await PutOrThrow<Assistant>("assistants", assistant);

        public async Task DeleteAssistant(string id) => await DeleteOrThrow("assistants/{id}");

        public async Task<IEnumerable<TreatmentNoteSummary>> GetNotesSummary(string clientSearch = "", DateTime? startDate = null, DateTime? endDate = null, int? clientId = null, int? pageNumber = null, int? status = null) => await GetOrThrow<IEnumerable<TreatmentNoteSummary>>(
            "notes/summary",
            new[]
            {
                ("client", clientSearch),
                ("startDate", startDate.HasValue ? startDate.Value.ToString("yyyy-MM-dd") : null),
                ("endDate", endDate.HasValue ? endDate.Value.ToString("yyyy-MM-dd") : null),
                ("clientId", clientId.HasValue ? clientId.Value.ToString() : null),
                ("page", pageNumber.HasValue ? pageNumber.Value.ToString(): null),
                ("status", status.HasValue ? status.Value.ToString(): null)
            });

        public async Task<TreatmentNote> GetFullNote(string noteId) => await GetOrThrow<TreatmentNote>($"notes/{noteId}");
    }
}
