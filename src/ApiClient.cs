using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using IntakeQ.ApiClient.Helpers;
using IntakeQ.ApiClient.Models;
using Newtonsoft.Json;
using System.IO;
using Client = IntakeQ.ApiClient.Models.Client;
using ClientProfile = IntakeQ.ApiClient.Models.ClientProfile;


namespace IntakeQ.ApiClient
{
    public class ApiClient
    {
        private readonly string _apiKey;
//#if DEBUG
//        private readonly string _baseUrl = "https://localhost/api/v1/";
//#else
        private readonly string _baseUrl = "https://intakeq.com/api/v1/";
//#endif
        public ApiClient(string apiKey)
        {
            _apiKey = apiKey;
#if DEBUG
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
#endif
        }

        private HttpRequestMessage GetHttpMessage(string methodName, HttpMethod methodType)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_baseUrl + methodName),
                Method = methodType,
            };
            request.Headers.Add("X-Auth-Key", _apiKey);
            return request;
        }
        
        public async Task<IEnumerable<IntakeSummary>> GetIntakesSummary(string clientSearch, DateTime? startDate = null, DateTime? endDate = null, int? pageNumber = null)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var parameters = new NameValueCollection();
                if (!string.IsNullOrEmpty(clientSearch))
                    parameters.Add("client", clientSearch);
                if (startDate.HasValue)
                    parameters.Add("startDate", startDate.Value.ToString("yyyy-MM-dd"));
                if (endDate.HasValue)
                    parameters.Add("endDate", endDate.Value.ToString("yyyy-MM-dd"));
                if (pageNumber.HasValue)
                    parameters.Add("page", pageNumber.Value.ToString());

                var request = GetHttpMessage("intakes/summary" + parameters.ToQueryString(), HttpMethod.Get);
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var intakes = JsonConvert.DeserializeObject<IEnumerable<IntakeSummary>>(json);
                    return intakes;
                }
                else
                {
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
                }
            }
        }

        public async Task<Intake> GetFullIntake(string intakeId)
        {
            using (HttpClient client = new HttpClient())
            {
                var request = GetHttpMessage($"intakes/{intakeId}", HttpMethod.Get);

                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Console.Write(json);
                    var intake = JsonConvert.DeserializeObject<Intake>(json);
                    return intake;
                }
                else
                {
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
                }
            }
        }
        
        public async Task<Intake> SendForm(SendForm sendForm)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

               var request = GetHttpMessage($"intakes/send", HttpMethod.Post);
                request.Content = new StringContent(JsonConvert.SerializeObject(sendForm), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Intake>(json);
                }
                else
                {
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
                }
            }
        }

        public async Task<byte[]> DownloadPdf(string intakeId)
        {
            using (HttpClient client = new HttpClient())
            {
                var request = GetHttpMessage($"intakes/{intakeId}/pdf", HttpMethod.Get);

                 HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {   
                    var bytes = await response.Content.ReadAsByteArrayAsync();
                    return bytes;
                }
                else
                {
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
                }
            }
        }

        public async Task DownloadPdfAndSave(string intakeId, string destinationPath)
        {
            using (HttpClient client = new HttpClient())
            {

                var request = GetHttpMessage($"intakes/{intakeId}/pdf", HttpMethod.Get);

                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    using (var contentStream = await response.Content.ReadAsStreamAsync())
                    {
                        using (var stream = new FileStream(destinationPath, FileMode.CreateNew))
                        {
                            await contentStream.CopyToAsync(stream);
                        }
                    }
                }
                else
                {
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
                }
            }
        }

        public async Task<IEnumerable<Client>> GetClients(string search = null, int? pageNumber = null, DateTime? dateCreatedStart = null, DateTime? dateCreatedEnd = null, Dictionary<string, string> customFields = null)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var parameters = new NameValueCollection();
                if (!string.IsNullOrEmpty(search))
                    parameters.Add("search", search);
                if (pageNumber.HasValue)
                    parameters.Add("page", pageNumber.Value.ToString());
                if (dateCreatedStart.HasValue)
                    parameters.Add("dateCreatedStart", dateCreatedStart.Value.ToString("yyyy-MM-dd"));
                if (dateCreatedEnd.HasValue)
                    parameters.Add("dateCreatedEnd", dateCreatedEnd.Value.ToString("yyyy-MM-dd"));

                if (customFields?.Count > 0)
                {
                    foreach (var field in customFields)
                    {
                        parameters.Add($"custom[{field.Key}]", field.Value);
                    }                    
                }
                
                var request = GetHttpMessage("clients" + parameters.ToQueryString(), HttpMethod.Get);
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var clients = JsonConvert.DeserializeObject<IEnumerable<Client>>(json);
                    return clients;
                }
                else
                {
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
                }
            }
        }

        public async Task<IEnumerable<ClientProfile>> GetClientsWithProfile(string search = null, int? pageNumber = null, DateTime? dateCreatedStart = null, DateTime? dateCreatedEnd = null, Dictionary<string, string> customFields = null)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var parameters = new NameValueCollection();
                if (!string.IsNullOrEmpty(search))
                    parameters.Add("search", search);
                if (pageNumber.HasValue)
                    parameters.Add("page", pageNumber.Value.ToString());
                parameters.Add("includeProfile", "true");
                if (dateCreatedStart.HasValue)
                    parameters.Add("dateCreatedStart", dateCreatedStart.Value.ToString("yyyy-MM-dd"));
                if (dateCreatedEnd.HasValue)
                    parameters.Add("dateCreatedEnd", dateCreatedEnd.Value.ToString("yyyy-MM-dd"));

                if (customFields?.Count > 0)
                {
                    foreach (var field in customFields)
                    {
                        parameters.Add($"custom[{field.Key}]", field.Value);
                    }                    
                }
                
                var request = GetHttpMessage("clients" + parameters.ToQueryString(), HttpMethod.Get);
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(json);
                    var clients = JsonConvert.DeserializeObject<IEnumerable<ClientProfile>>(json);
                    return clients;
                }
                else
                {
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
                }
            }
        }
        
        public async Task<ClientProfile> SaveClient(ClientProfile clientProfile)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var request = GetHttpMessage($"clients", HttpMethod.Post);
                request.Content = new StringContent(JsonConvert.SerializeObject(clientProfile), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ClientProfile>(json);
                }
                else
                {
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
                }
            }
        }

        public async Task<byte[]> DownloadAttachment(string attachmentId)
        {
            using (HttpClient client = new HttpClient())
            {

                var request = GetHttpMessage($"attachments/{attachmentId}", HttpMethod.Get);

                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var bytes = await response.Content.ReadAsByteArrayAsync();
                    return bytes;
                }
                else
                {
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
                }
            }
        }

        public async Task DownloadAttachmentAndSave(string attachmentId, string destinationPath)
        {
            using (HttpClient client = new HttpClient())
            {

                var request = GetHttpMessage($"attachments/{attachmentId}", HttpMethod.Get);

                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    using (var contentStream = await response.Content.ReadAsStreamAsync())
                    {
                        using (var stream = new FileStream(destinationPath, FileMode.CreateNew))
                        {
                            await contentStream.CopyToAsync(stream);
                        }
                    }
                }
                else
                {
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
                }
            }
        }

        public async Task<Intake> UpdateOfficeUseAnswers(Intake intake)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Let's clean the intake object to keep only the data we need to send
                var cleanIntake = new Intake()
                {
                    Id = intake.Id,
                    Questions = intake.Questions.Where(x => x.OfficeUse).ToList()
                };

                var request = GetHttpMessage($"intakes", HttpMethod.Post);
                request.Content = new StringContent(JsonConvert.SerializeObject(cleanIntake), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Intake>(json);
                }
                else
                {
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
                }
            }
        }

        public async Task<IEnumerable<Appointment>> GetAppointments(DateTime? startDate = null, DateTime? endDate = null, string status = null, string clientSearch = null, string practitionerEmail = null, int? pageNumber = null)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var parameters = new NameValueCollection();
                if (!string.IsNullOrEmpty(clientSearch))
                    parameters.Add("client", clientSearch);
                if (!string.IsNullOrEmpty(status))
                    parameters.Add("status", status);
                if (!string.IsNullOrEmpty(practitionerEmail))
                    parameters.Add("practitionerEmail", practitionerEmail);
                if (startDate.HasValue)
                    parameters.Add("startDate", startDate.Value.ToString("yyyy-MM-dd"));
                if (endDate.HasValue)
                    parameters.Add("endDate", endDate.Value.ToString("yyyy-MM-dd"));
                if (pageNumber.HasValue)
                    parameters.Add("page", pageNumber.Value.ToString());

                var request = GetHttpMessage("appointments" + parameters.ToQueryString(), HttpMethod.Get);
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var appointments = JsonConvert.DeserializeObject<IEnumerable<Appointment>>(json);
                    return appointments;
                }
                else
                {
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
                }
            }
        }

        public async Task<Appointment> GetAppointment(string appointmentId)
        {
            using (HttpClient client = new HttpClient())
            {
                var request = GetHttpMessage($"appointments/{appointmentId}", HttpMethod.Get);

                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Console.Write(json);
                    var appointment = JsonConvert.DeserializeObject<Appointment>(json);
                    return appointment;
                }
                else
                {
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
                }
            }
        }

        public async Task<AppointmentSettings> GetAppointmentSettings()
        {
            using (HttpClient client = new HttpClient())
            {
                var request = GetHttpMessage($"appointments/settings", HttpMethod.Get);

                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Console.Write(json);
                    var settings = JsonConvert.DeserializeObject<AppointmentSettings>(json);
                    return settings;
                }
                else
                {
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
                }
            }
        }
        
        public async Task<IEnumerable<Questionnaire>> GetQuestionnaires(DateTime? startDate = null, DateTime? endDate = null, string status = null, string clientSearch = null, string practitionerEmail = null, int? pageNumber = null)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

              
                var request = GetHttpMessage("questionnaires", HttpMethod.Get);
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var questionnaires = JsonConvert.DeserializeObject<IEnumerable<Questionnaire>>(json);
                    return questionnaires;
                }
                else
                {
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
                }
            }
        }
    }
}
