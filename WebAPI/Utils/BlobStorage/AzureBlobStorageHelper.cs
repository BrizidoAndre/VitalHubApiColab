using Azure.Storage.Blobs;

namespace WebAPI.Utils.BlobStorage
{
    public class AzureBlobStorageHelper
    {
        public static async Task<string> UploadImageBlobASync(IFormFile arquivo, string stringConexao, string nomeContainer)
        {
			try
			{

                if (arquivo != null)
				{
					//retorna a uri com imagem salva
					var blobName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(arquivo.FileName);

					//cria uma instância do BlobServiceClient passando a string de conexão com o blob da Azure
					var blobServiceClient = new BlobServiceClient(stringConexao);

					//obtem dados do container client
					var blobContainerClient = blobServiceClient.GetBlobContainerClient(nomeContainer);

					//obtem um blobClient usando o blob name
					var blobClient = blobContainerClient.GetBlobClient(blobName);

					//abre o fluxo de entrada do arquivo (foto)
					using (var stream = arquivo.OpenReadStream())
					{
						await blobClient.UploadAsync(stream, true);
					}
					return blobClient.Uri.ToString();
				}
				else
				{
					//retorna uri padrão de uma imagem caso nenhumma imagem seja enviada na requisição
					return "https://blobvitalhubg01m.blob.core.windows.net/blobvitalhubg01mcontainer/user-profile-icon-free-vector.jpg";

                }
			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}
