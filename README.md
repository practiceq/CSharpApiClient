# IntakeQ C# API Client
C# client library used to access IntakeQ's API (https://intakeq.com).

For API documentation refer to http://support.intakeq.com/intakeq-api

## Using this library
All methods are available in the APIClient class. Instantiate the class by passing your API key through the constructor. 
```csharp
   var api = new ApiClient("Your API Key");
```
### Query Intake Forms

```csharp
   var api = new ApiClient("Your API Key");
   var intakes = await api.GetIntakesSummary("search term");
```
The GetIntakesSummary accepts other optional parameters, like start and end dates, page number, etc.

### Get Full Intake

```csharp
   var api = new ApiClient("Your API Key");
   var intake = await api.GetFullIntake("intake id");
```

### Download Intake PDF

```csharp
   var api = new ApiClient("Your API Key");
   var bytes = await api.DownloadPdf("intake id"); //returns the PDF in byte[]
```

### Download and Save PDF to Disk
```csharp
   var api = new ApiClient("You API Key");
   await api.DownloadPdfAndSave("intake id", "c:\\test.pdf");
```
### Query Clients

```csharp
   var api = new ApiClient("Your API Key");
   var clients = await api.GetClients("search term");
```
