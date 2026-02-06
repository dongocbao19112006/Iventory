using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Builder;
using System;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", async () =>
{
    // URL Key Vault của bạn
    string keyVaultUrl = "https://asmnbaokey7.vault.azure.net/";

    try 
    {
        // Tạo client kết nối Key Vault bằng Managed Identity (DefaultAzureCredential)
        var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());

        // LẤY SECRET TÊN "admin01"
        KeyVaultSecret secret = await client.GetSecretAsync("admin01");

        return $"Điểm của asm 2: {secret.Value}";
    }
    catch (Exception ex)
    {
        return $"Lỗi khi kết nối Key Vault: {ex.Message}";
    }
});

app.Run();