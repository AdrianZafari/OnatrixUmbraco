# Onatrix Umbraco CMS

[![Umbraco CMS](https://img.shields.io/badge/Umbraco-CMS-blue)](https://umbraco.com/)

Onatrix Umbraco is a custom CMS built with **Umbraco 13**, **.NET 8**, and **Azure Communication Services** for email. It supports form submissions, automated confirmation emails, and integrates with Azure SQL Database.

---

## Features

* **Custom form submissions** stored in Umbraco content
* **Email notifications** via Azure Communication Services (ACS)
* **SurfaceController**-based form handling
* Async-safe email sending to avoid server errors
* Deployable to **Azure App Service**
* Fully integrated with **Umbraco backoffice**

---

## Prerequisites

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* [Visual Studio 2022](https://visualstudio.microsoft.com/) or equivalent IDE
* [Azure Subscription](https://portal.azure.com)
* [Umbraco CMS NuGet packages](https://www.nuget.org/packages/Umbraco.Cms)
* Azure Communication Services (Email enabled)
* Azure SQL Database

---

## Getting Started (Local Development)

1. Clone the repository:

```bash
git clone https://github.com/AdrianZafari/OnatrixUmbraco.git
cd OnatrixUmbraco/UmbracoCMS
```

2. Update your local `appsettings.json` with connection strings:

```json
{
  "ConnectionStrings": {
    "umbracoDbDSN": "Server=<your-sql-server>;Database=<db>;User ID=<user>;Password=<password>",
    "umbracoDbDSN_ProviderName": "Microsoft.Data.SqlClient"
  },
  "ACS": {
    "ConnectionString": "<your-acs-connection-string>",
    "SenderAddress": "DoNotReply@yourdomain.com"
  }
}
```
## Configuration

### Umbraco

* CMS configuration is in `appsettings.json` and can be overridden via environment variables.
* The root content container for form submissions must exist (`formSubmissions`) in the backoffice.

### Azure Communication Services (ACS)

* Ensure you have an ACS **Email Communication Service** created in your Azure portal.
* Add the **ConnectionString** and **SenderAddress** to `appsettings.json` or Azure App Service environment variables.

Example:

```json
"ACS": {
  "ConnectionString": "endpoint=https://<resource>.communication.azure.com/;accesskey=<your-key>",
  "SenderAddress": "DoNotReply@yourdomain.com"
}
```

---

## Project Structure

```
UmbracoCMS/
├── Controllers/          # SurfaceControllers for form submissions
├── Services/             # FormSubmissionsService and EmailService
├── ViewModels/           # Form view models
├── wwwroot/              # Static files
├── appsettings.json      # Local configuration
├── Program.cs            # Application startup
└── UmbracoCMS.csproj
```

### Key Components

* `FormSubmissionsService` — Saves form data to Umbraco content
* `FormController` — Handles form POSTs and triggers email notifications
* `EmailService` — Sends confirmation emails via Azure Communication Services
* Razor views — Forms integrated via `Html.BeginUmbracoForm(...)`

---

## Deployment to Azure

1. **Push the code** to a GitHub repository or Azure DevOps.

2. **Create resources** in Azure:

   * App Service Plan
   * App Service
   * Azure SQL Database
   * Azure Communication Service (Email)

3. **Set App Service environment variables**:

| Name                             | Value                                                                  |
| -------------------------------- | ---------------------------------------------------------------------- |
| `ConnectionStrings:umbracoDbDSN` | `Server=<sql-server>;Database=<db>;User ID=<user>;Password=<password>` |
| `ACS:ConnectionString`           | `<your ACS connection string>`                                         |
| `ACS:SenderAddress`              | `DoNotReply@yourdomain.com`                                            |

## Usage

* Fill in a callback form on the website.
* The data is stored in Umbraco under `formSubmissions`.
* The user receives a confirmation email via ACS.

---
This was written with the help of ChatGPT because life is short.
